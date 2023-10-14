using DAMDisco.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DAMDisco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SemaphoreSlim sem;
        private ObservableCollection<Data.Person> queueList;
        private ObservableCollection<Data.Person> discoList;
        private ObservableCollection<String> historyList;

        public MainWindow()
        {
            InitializeComponent();
            sem = new SemaphoreSlim(0);

            queueList = new ObservableCollection<Data.Person>();
            discoList = new ObservableCollection<Data.Person>();
            historyList = new ObservableCollection<string>();

            this.lbQueue.ItemsSource = queueList;
            this.lbDisco.ItemsSource = discoList;
            this.lbHistory.ItemsSource = historyList;


            this.historyList.Insert(0, "Starting application.");
        }

        private void btCapacity_Click(object sender, RoutedEventArgs e)
        {
            SelectCapacityWindow win;
            bool? result;
            win = new SelectCapacityWindow();
            result = win.ShowDialog();
            if (result.HasValue && result.Value)
            {
                this.lblSelectedCapacity.Content = "You selected " + win.Capacity.ToString() + " of capacity.";
                this.btAddPerson.IsEnabled= true;
                sem = new SemaphoreSlim(win.Capacity);
                this.historyList.Insert(0, "Initialized capcacity: " + win.Capacity);
                this.btCapacity.IsEnabled = false;
            }
        }

        private void btAddPerson_Click(object sender, RoutedEventArgs e)
        {
            PersonDataWindow win;
            bool? result;
            Data.Person person;

            win = new PersonDataWindow();
            result = win.ShowDialog();
            if (result.HasValue && result.Value)
            {
                person = win.Person;
                
                Task t = Task.Run(() =>
                {
                    ProcessItem(person);     
                });
                
            }
        }

        private void ProcessItem(Person person)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.queueList.Add(person);
                this.historyList.Insert(0, person.Name + " enter to queue");
            });
            if (sem.Wait(person.QueueMaxTime * 1000))
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    this.queueList.Remove(person);
                    this.discoList.Add(person);
                    this.historyList.Insert(0, person.Name + " enters disco and wait " + person.DiscoTime + " seconds");
                });
                Task.Delay(person.DiscoTime * 1000).Wait();
                sem.Release();
                App.Current.Dispatcher.Invoke(() =>
                {
                    this.historyList.Insert(0, person.Name + " exits disco");
                    this.discoList.Remove(person);
                });
            } else
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    this.historyList.Insert(0, person.Name + " exit qeue without going to the disco");
                    this.queueList.Remove(person);
                });
                
            }
            

        }
    }
}

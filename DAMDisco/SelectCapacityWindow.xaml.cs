using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAMDisco
{
    /// <summary>
    /// Lógica de interacción para SelectCapacityWindow.xaml
    /// </summary>
    public partial class SelectCapacityWindow : Window
    {
        public SelectCapacityWindow()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(txtCapacity, CapacityPasteHandler);
            this.txtCapacity.Focus();
        }

        public int Capacity
        {
            get
            {
                return Convert.ToInt32(this.txtCapacity.Text);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult=false;
            this.Close();
        }

        private void CapacityPasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            bool ok = false;
            Regex regex = new Regex("[0-9]+");
            if (e.DataObject.GetDataPresent(typeof(string)))
            {  
                var txt = e.DataObject.GetData(typeof(string)) as string;
                if (txt != null)
                {
                    ok = regex.IsMatch(txt);
                }
            }
            if (!ok)
                e.CancelCommand();
            
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            bool correct = true;
            string msg = "";
            try
            {
                var num = Convert.ToInt32(this.txtCapacity.Text);
                if ((num<0)|| (num>100))
                {
                    correct = false;
                    msg += "The number must be between 1 and 100" + Environment.NewLine;
                }
            } catch (Exception)
            {
                correct = false;
                msg += "Incorrect number" + Environment.NewLine;
            }

            if (correct)
            {
                this.DialogResult = true;
                this.Close();
            } else
            {
                MessageBox.Show(msg,"Enter capacity", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtCapacity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}

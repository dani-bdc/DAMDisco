using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para PersonDataWindow.xaml
    /// </summary>
    public partial class PersonDataWindow : Window
    {
        public PersonDataWindow()
        {
            InitializeComponent();

            this.txtName.Focus();
        }

        public Data.Person Person
        {
            get
            {
                Data.Person p = new Data.Person();

                p.Name = this.txtName.Text;
                p.DiscoTime = Convert.ToInt32(this.txtDiscoTime.Text);
                p.QueueMaxTime = Convert.ToInt32(this.txtQueueMaxTime.Text);

                return p;
            }
        }

        private bool ValidateInput()
        {
            bool correct = true;
            string msg = "";

            if (String.IsNullOrEmpty(this.txtName.Text))
            {
                correct = false;
                msg += "You must enter a valid name." + Environment.NewLine;
            }

            try
            {
                var dt = Convert.ToInt32(this.txtDiscoTime.Text);
                if ((dt<1)||(dt>100))
                {
                    correct = false;
                    msg += "Disco time must be between 1 and 100 seconds." + Environment.NewLine;
                }
            } catch (Exception)
            {
                correct = false;
                msg += "Invalid disco time." + Environment.NewLine;
            }

            try
            {
                var dt = Convert.ToInt32(this.txtQueueMaxTime.Text);
                if ((dt < 1) || (dt > 100))
                {
                    correct = false;
                    msg += "Queue Max time must be between 1 and 100 seconds." + Environment.NewLine;
                }
            }
            catch (Exception)
            {
                correct = false;
                msg += "Invalid queue max time." + Environment.NewLine;
            }

            if (!correct)
            {
                MessageBox.Show(msg, "Validate input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return correct;
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

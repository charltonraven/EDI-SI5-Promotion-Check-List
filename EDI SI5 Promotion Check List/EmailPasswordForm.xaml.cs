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

namespace EDI_SI5_Promotion_Check_List
{
    /// <summary>
    /// Interaction logic for EmailPasswordForm.xaml
    /// </summary>
    public partial class EmailPasswordForm : Window
    {
        public  String Email="";
        public  String Password="";
        public EmailPasswordForm()
        {
            InitializeComponent();
        }
        public EmailPasswordForm(String Email)
        {
            txtEmail.Text = Email;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EmailPasswordWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

                Email = txtEmail.Text;
                Password = txtPassword.Password;
            if (!Email.Equals("") && !Password.Equals(""))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Email and Password.", "INVALID", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EmailPasswordWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSubmit_Click(this, new RoutedEventArgs());
            }
        }
    }
}

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
    /// Interaction logic for ProjectManager_Name.xaml
    /// </summary>
    /// 
    public partial class ProjectManager_Name : Window
    {

        private string Name = "";
        private String Email = "";

        public string Name1 { get => Name; set => Name = value; }
        public string Email1 { get => Email; set => Email = value; }

        public ProjectManager_Name()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Name1 = txtName.Text;
            Email1 = txtEmail.Text;
            DialogResult = true;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }
    }
}

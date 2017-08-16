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
    /// Interaction logic for Error_SendButton.xaml
    /// </summary>
    /// 

    
    public partial class Error_SendButton : Window
    {
        private String Error;

        public string Error1 { get => Error; set => Error = value; }

        public Error_SendButton(String error)
        {
            InitializeComponent();
            this.Error = error;
        }

        public Error_SendButton()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
            lblErrors.Content = Error1;
        }
    }
}

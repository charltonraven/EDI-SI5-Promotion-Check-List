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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EDI_SI5_Promotion_Check_List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        String User;
        String Partner;
        String Date;
        String CMRN;
        bool UAOP;
        bool PAOIP;
        bool tableParm;
        String tableParmName;
        bool developementCompleted;
        bool testingCompleted;
        bool codeReview;
        String codeReviewBY;
        String codeReviewDate;
        bool keyUserSignOff;
        bool partnerSignOff;
        String impFinalStatus;
        String PostImpReview;

        bool Envelopes;
        bool BP;
        bool ServiceAdapters;
        bool perlScripts;
        bool EmailCodeList;
        bool docMaps;
        bool docExtractionMap;
        bool XSLTEmail;
        bool mapCodeTables;
        bool RAILStable;
        bool RAILSrecord;
        bool RAILSfilter;
        bool fileStructureProd;
        bool FTPconnect;
        bool TRANSPORTfile;

        String ProjectManager;
        String CompletionDate;
        String to;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            
        

        }
    }
}

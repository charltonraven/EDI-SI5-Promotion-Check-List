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
using System.Net.Mail;

namespace EDI_SI5_Promotion_Check_List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        String User;
        String Partner;
        String currentDate = DateTime.Today.ToShortDateString();
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
        String sendTo;


      
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(String filename)
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            User = txtUser.Text.ToString();
            Partner = txtPartner.Text.ToString();
            CMRN = txtCMRN.Text;
            tableParmName = txtTableParmName.Text;
            codeReviewBY = txtCodeReviewBY.Text;
            codeReviewDate = txtCheckSignOffDate.Text;

            String[] lineTitles = { "User", "Partner", "Date", "ChangeManagementRequestNumber", "UserApprovalofProject", "PartnerApprovalofInitialProject", "Table/ParmUpdate", "Table/ParmName", "DevelopmentCompleted", "TestingCompleted", "CodeReview/CheckSignOff", "KeyUserSignoff", "PartnerSignoff", "ImplementationFinalStatus", "PostImplementationReview", "Envelopes", "BusinessProcess", "ServiceAdapters", "PerlScirpts", "EmailCodeList", "DocumentMaps", "DocumentExtractionMap", "XSLTEmailErrorHeader", "MapCodeTables", "RAILScsvTable", "RAILScsvRecord", "RAILScsvFilter", "FileStructureinProduction", "FTPConnect", "TRANSPORTParmFile" };
            String[] lineAnswers = { User, Partner, currentDate, CMRN, UAOP.ToString(), PAOIP.ToString(), tableParm.ToString(), tableParmName,developementCompleted.ToString(),testingCompleted.ToString(),codeReview.ToString(),keyUserSignOff.ToString(),partnerSignOff.ToString(),impFinalStatus,PostImpReview,Envelopes.ToString(),BP.ToString(),ServiceAdapters.ToString(),perlScripts.ToString(),EmailCodeList.ToString(),docMaps.ToString(),docExtractionMap.ToString(),XSLTEmail.ToString(),mapCodeTables.ToString(),RAILStable.ToString(),RAILSrecord.ToString(),RAILSfilter.ToString(),fileStructureProd.ToString(),FTPconnect.ToString(),TRANSPORTfile.ToString() };
            String filename = User + "_" + Partner + "_"+DateTime.Now.ToString("yyyyMMddHHmm");
            String filePath= @"c:\users\63530\desktop\" + filename+".arc";
            System.IO.File.WriteAllLines(filePath, lineAnswers);

           // SendEmailForApproval(filePath);
            SendEmailForApproval(lineTitles,lineAnswers);


        }

        private void rbPJNovak_Checked(object sender, RoutedEventArgs e)
        {
            sendTo = "Patrick.Novak@sonoco.com";
        }

        private void rbBrianFerger_Checked(object sender, RoutedEventArgs e)
        {
            sendTo = "Brian.Ferger@sonoco.com";
        }

        private void rbCharltonWilliams_Checked(object sender, RoutedEventArgs e)
        {
            sendTo = "Charlton.Williams@sonoco.com";
        }

        private void formMain_loaded(object sender, RoutedEventArgs e)
        {

            txtDate.Text = currentDate;
            txtCodeReviewBY.IsEnabled = false;
            txtCheckSignOffDate.IsEnabled = false;
            lblCodeReviewBY.IsEnabled = false;
            lblCheckSignOffDate.IsEnabled = false;
            txtTableParmName.IsEnabled = false;
            lblTableParmUpdate.IsEnabled = false;

        }

        private void rbInstalledIMP_Checked(object sender, RoutedEventArgs e)
        {
            impFinalStatus="Installed";
        }

        private void rbBackedOutIMP_Checked(object sender, RoutedEventArgs e)
        {
            impFinalStatus="Backed Out";
        }

        private void rbAbandonedIMP_Checked(object sender, RoutedEventArgs e)
        {
            impFinalStatus = "Abandoned";
        }

        private void rbInstalledPOST_Checked(object sender, RoutedEventArgs e)
        {
            PostImpReview = "Installed";
        }

        private void rbBackedOutPOST_Checked(object sender, RoutedEventArgs e)
        {
            PostImpReview = "Backed Out";
        }

        private void rbAbandonedPOST_Checked(object sender, RoutedEventArgs e)
        {
            PostImpReview = "Abandoned";
        }

        private void cbUAOP_checked(object sender, RoutedEventArgs e)
        {
            UAOP = true;
        }

        private void cbUAOP_Unchecked(object sender, RoutedEventArgs e)
        {
            UAOP = false;
        }

        private void cbPAOIP_Checked(object sender, RoutedEventArgs e)
        {
            PAOIP = true;
        }

        private void cbPAOIP_Unchecked(object sender, RoutedEventArgs e)
        {
            PAOIP = false;
        }
        private void cbTPU_Checked(object sender, RoutedEventArgs e)
        {
            tableParm = true;
            txtTableParmName.IsEnabled = true;
            lblTableParmUpdate.IsEnabled = true;
        }
        private void cbTPU_Unchecked(object sender, RoutedEventArgs e)
        {
            tableParm = false;
            txtTableParmName.IsEnabled = false;
            lblTableParmUpdate.IsEnabled = false;
        }

        private void cbDevelopmentComplete_Checked(object sender, RoutedEventArgs e)
        {
            developementCompleted = true;
        }

        private void cbDevelopmentComplete_Unchecked(object sender, RoutedEventArgs e)
        {
            developementCompleted = false;
        }

        private void cbTestingComplete_Checked(object sender, RoutedEventArgs e)
        {
            testingCompleted = true;
        }

        private void cbTestingComplete_Unchecked(object sender, RoutedEventArgs e)
        {
            testingCompleted = false;
        }

        private void cbCodeReview_Checked(object sender, RoutedEventArgs e)
        {
            codeReview = true;
            txtCodeReviewBY.IsEnabled = true;
            txtCheckSignOffDate.IsEnabled = true;
            lblCodeReviewBY.IsEnabled = true;
            lblCheckSignOffDate.IsEnabled = true;
        }

        private void cbCodeReview_Unchecked(object sender, RoutedEventArgs e)
        {
            codeReview = false;
            txtCodeReviewBY.IsEnabled = false;
            txtCheckSignOffDate.IsEnabled = false;
            lblCodeReviewBY.IsEnabled = false;
            lblCheckSignOffDate.IsEnabled = false;
        }
        private void cbKeyUserSignOff_Checked(object sender, RoutedEventArgs e)
        {
            keyUserSignOff = true;
        }
        private void cbKeyUserSignOff_Unchecked(object sender, RoutedEventArgs e)
        {
            keyUserSignOff = false;
        }


        private void cbPartnerSignOff_Checked(object sender, RoutedEventArgs e)
        {
            partnerSignOff = true;
        }

        private void cbPartnerSignOff_Unchecked(object sender, RoutedEventArgs e)
        {
            partnerSignOff = false;
        }

        private void cbEnvelopes_Checked(object sender, RoutedEventArgs e)
        {
            Envelopes = true;
        }

        private void cbEnvelopes_Unchecked(object sender, RoutedEventArgs e)
        {
            Envelopes = false;
        }
        private void cbBusinessProcesses_Checked(object sender, RoutedEventArgs e)
        {
            BP = true;

        }

        private void cbBusinessProcesses_Unchecked(object sender, RoutedEventArgs e)
        {
            BP = false;
        }

        private void cbServiceAdapters_Checked(object sender, RoutedEventArgs e)
        {
            ServiceAdapters = true;
        }

        private void cbServiceAdapters_Unchecked(object sender, RoutedEventArgs e)
        {
            ServiceAdapters = false;
        }

        private void cbPerlScripts_Checked(object sender, RoutedEventArgs e)
        {
            perlScripts = true;
        }

        private void cbPerlScripts_Unchecked(object sender, RoutedEventArgs e)
        {
            perlScripts = false;
        }

        private void cbEmailCodeList_Checked(object sender, RoutedEventArgs e)
        {
            EmailCodeList = true;
        }

        private void cbEmailCodeList_Unchecked(object sender, RoutedEventArgs e)
        {
            EmailCodeList = false;
        }
        private void cbDocumentMaps_Checked(object sender, RoutedEventArgs e)
        {
            docMaps = true;
        }
        private void cbDocumentMaps_Unchecked(object sender, RoutedEventArgs e)
        {
            docMaps = false;
        }

        private void cbDocumentExtractionMap_Checked(object sender, RoutedEventArgs e)
        {
            docExtractionMap = true;
        }

        private void cbDocumentExtractionMap_Unchecked(object sender, RoutedEventArgs e)
        {
            docExtractionMap = false;
        }

        private void cbXSLTEmailErrorHeader_Checked(object sender, RoutedEventArgs e)
        {
            XSLTEmail = true;
            
        }

        private void cbXSLTEmailErrorHeader_Unchecked(object sender, RoutedEventArgs e)
        {
            XSLTEmail = false;
        }

        private void cbMapCodeTables_Checked(object sender, RoutedEventArgs e)
        {
            mapCodeTables = true;
        }

        private void cbMapCodeTables_Unchecked(object sender, RoutedEventArgs e)
        {
            mapCodeTables = false;
        }

        private void cbRAILS_csv_Table_Checked(object sender, RoutedEventArgs e)
        {
            RAILStable = true;
        }

        private void cbRAILS_csv_Table_Unchecked(object sender, RoutedEventArgs e)
        {
            RAILStable = false;
        }

        private void cbRAILS_csv_Record_Checked(object sender, RoutedEventArgs e)
        {
            RAILSrecord = true;
        }

        private void cbRAILS_csv_Record_Unchecked(object sender, RoutedEventArgs e)
        {
            RAILSrecord = false;
        }

        private void cbRAILS_csv_Filter_Checked(object sender, RoutedEventArgs e)
        {
            RAILSfilter = true;

        }

        private void cbRAILS_csv_Filter_Unchecked(object sender, RoutedEventArgs e)
        {
            RAILSfilter = true;
        }

        private void cbFile_Structure_in_prodcution_Checked(object sender, RoutedEventArgs e)
        {
            fileStructureProd = true;
        }

        private void cbFile_Structure_in_prodcution_Unchecked(object sender, RoutedEventArgs e)
        {
            fileStructureProd = false;
        }

        private void cbFTPConnect_Checked(object sender, RoutedEventArgs e)
        {
            FTPconnect = true;
        }

        private void cbFTPConnect_Unchecked(object sender, RoutedEventArgs e)
        {
            FTPconnect = false;
        }

        private void cbTRANSPORTparmFile_Checked(object sender, RoutedEventArgs e)
        {
            TRANSPORTfile = true;
        }

        private void cbTRANSPORTparmFile_Unchecked(object sender, RoutedEventArgs e)
        {
            TRANSPORTfile = false;
        }
        public void SendEmailForApproval(String FilePath)
        {
            MailMessage mail = new MailMessage("williamscharlton@hotmail.com", "Charlton.williams@sonoco.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = "10.77.48.132";
            mail.Subject = "test";
            mail.Body = "body test";
            mail.Attachments.Add(new Attachment(FilePath));
            client.Send(mail);
        }
        public void SendEmailForApproval(String [] lineTitles, String  [] lineAnswers)
        {
            StringBuilder stringbuilder = new StringBuilder();

            for(int i = 0; i < lineTitles.Length; i++)
            {
                stringbuilder.AppendLine(lineTitles[i] + "\t" + lineAnswers[i]);
            }

            String body = stringbuilder.ToString();

            MailMessage mail = new MailMessage("williamscharlton@hotmail.com", "Charlton.williams@sonoco.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = "10.77.48.132";
            mail.Subject = "test";
            mail.Body = body;
            client.Send(mail);

        }
    }
}

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
using System.IO;
using EDI_SI5_Promotion_Check_List.Properties;

namespace EDI_SI5_Promotion_Check_List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>STRINGS<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        String User, Partner, CMRN, tableParmName, codeReviewBY, codeReviewDate, impFinalStatus, PostImpReview, ProjectManager, CompletionDate, sendTo, Description, Title;
        String currentDate = DateTime.Today.ToShortDateString();
        String SendFrom;
        String path;
        String folderName = "";
        String name;
        String Subject;
        String EmailAddress = "";
        String perlScriptsName = "Test Perl Name";
        StringBuilder sbExtraAttachement;
        string[] args;

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>BOOLEAN<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        bool UAOP, PAOIP, tableParm, developementCompleted, testingCompleted, codeReview, keyUserSignOff, partnerSignOff, Envelopes, BP, ServiceAdapters, perlScripts, EmailCodeList, docMaps, docExtractionMap, XSLTEmail;
        bool mapCodeTables, RAILStable, RAILSrecord, RAILSfilter, fileStructureProd, FTPconnect, TRANSPORTfile;
        bool BusinessProcessSchedule, ServiceAdapterSchedule, SetPartnerGISStatsTable;
        bool ReadOnly = false;
        bool BusinessProcessSch = false;
        bool ServiceAdapterSch = false;
        bool SetPartnerInGISStatsTable = false;

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>INTEGERS<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        int EnvelopeCount, ExtraCount, BusinessProccessCount, ServiceAdapterCount, PerlScriptCount, EmailCodeListCount, DocumentMapsCount, DocumentExtractionCount, XSLTCount, MapCodeCount, csvTableCount, RecordCount, csvFilterCount, FileStructureCount, FTPConnectCount, transportCount = 0;

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>SPECIAL<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private List<String> attachments = new List<string>();
        SortedDictionary<String, String> attachString = new SortedDictionary<String, String>();
        Point startPosition;
        //><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>><><><><><><><>
        public MainWindow()
        {
            InitializeComponent();

            args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                if (args.Length > 1)
                {
                    if (args[1] != null)
                    {
                        if (args[1].EndsWith(".epcl"))
                        {
                            ReadOnly = true;
                            processFile();
                        }
                    }
                }
            }

            else
            {
                name = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
                txtProjectManager.Text = name;
            }


            //ReadOnly = true;
            // processFile();

            txtDescription.TextWrapping = TextWrapping.Wrap;
            txtDescription.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            txtDescription.AcceptsReturn = true;
        }

        public void processFile()
        {
            path = @"C:\Users\63530\Work\test.epcl";
            path = args[1];
            StreamReader sr = new StreamReader(path);

            String line;
            while ((line = sr.ReadLine()) != null)
            {
                int whitespace = line.IndexOf("\t");
                if (whitespace > 0)
                {
                    String item = line.Substring(0, whitespace).Trim();
                    String value = line.Substring(whitespace).Trim();

                    getFormItems(item, value);
                }

            }

        }
        public void ProcessFile_DragAndDrop(String File)
        {
            path = File;
            StreamReader sr = new StreamReader(path);

            String line;
            while ((line = sr.ReadLine()) != null)
            {
                int whitespace = line.IndexOf("\t");
                if (whitespace > 0)
                {
                    String item = line.Substring(0, whitespace).Trim();
                    String value = line.Substring(whitespace).Trim();

                    getFormItems(item, value);
                }

            }

        }
        public void SubmitNewProject()
        {

            String Email = "";
            String Password = "";

            EmailPasswordForm Popup_Email_Password = new EmailPasswordForm();

            Popup_Email_Password.ShowDialog();
            if (Popup_Email_Password.DialogResult.HasValue && Popup_Email_Password.DialogResult.Value)
            {
                //Get Email And Password
                Email = Popup_Email_Password.Email;
                Password = Popup_Email_Password.Password;


                String DestDirectory = @"C:\SharePoint Upload";
                if (sbExtraAttachement != null)
                {
                    txtDescription.AppendText(sbExtraAttachement.ToString());
                    txtDescription.Text.Replace(Environment.NewLine, ",");
                }

                User = txtUser.Text.ToString();
                Partner = txtPartner.Text.ToString();
                CMRN = txtCMRN.Text;
                tableParmName = txtTableParmName.Text;
                codeReviewBY = txtCodeReviewBY.Text;
                codeReviewDate = txtCheckSignOffDate.Text;
                ProjectManager = txtProjectManager.Text;
                Description = txtDescription.Text;
                Title = txtTitle.Text;
                SendFrom = EmailAddress;
                Subject = ProjectManager + ": User: " + User + " and Partner " + Partner + "-> " + Title;


                String date = DateTime.Now.ToString("yyyyMMdd");
                String ModUser = User.Replace(" ", "");
                String ModTitle = Title.Replace(" ", "");
                String ModPartner = Partner.Replace(" ", "");
                String Initials = "CW";
                String folderName = date + "_" + ModUser + "_" + ModPartner + "_" + ModTitle + "_" + Initials;
                String Error = Errors();
                if (Error == null)
                {
                    String[] lineTitles = { "User", "Partner", "Date", "Title", "ChangeManagementRequestNumber", "UserApprovalofProject", "PartnerApprovalofInitialProject", "Table/ParmUpdate", "Table/ParmName", "DevelopmentCompleted", "TestingCompleted", "CodeReview/CheckSignOff", "CodeReviewBy", "CodeReviewDate", "KeyUserSignoff", "PartnerSignoff", "ImplementationFinalStatus", "PostImplementationReview", "Envelopes", "BusinessProcess", "ServiceAdapters", "PerlScripts", "EmailCodeList", "DocumentMaps", "DocumentExtractionMap", "XSLTEmailErrorHeader", "MapCodeTables", "RAILScsvTable", "RAILScsvRecord", "RAILScsvFilter", "FileStructureinProduction", "FTPConnect", "TRANSPORTParmFile", "BusinessProcessSchedule", "ServiceAdapterSchedule", "SetPartnerInGISStatsTable", "Description", "Subject", "SharepointFolderName", "ProjectManager" };
                    String[] lineAnswers = { User, Partner, currentDate, Title, CMRN, UAOP.ToString(), PAOIP.ToString(), tableParm.ToString(), tableParmName, developementCompleted.ToString(), testingCompleted.ToString(), codeReview.ToString(), codeReviewBY, codeReviewDate, keyUserSignOff.ToString(), partnerSignOff.ToString(), impFinalStatus, PostImpReview, Envelopes.ToString(), BP.ToString(), ServiceAdapters.ToString(), perlScripts.ToString(), EmailCodeList.ToString(), docMaps.ToString(), docExtractionMap.ToString(), XSLTEmail.ToString(), mapCodeTables.ToString(), RAILStable.ToString(), RAILSrecord.ToString(), RAILSfilter.ToString(), fileStructureProd.ToString(), FTPconnect.ToString(), TRANSPORTfile.ToString(), BusinessProcessSchedule.ToString(), ServiceAdapterSchedule.ToString(), SetPartnerGISStatsTable.ToString(), Description, Subject, folderName, ProjectManager };


                    var task = Task.Run(() =>
                    {

                        //Step 1: Create Word Document Place into Temp Folder

                        CreateWordDoc();



                        //Step 2: Place Attachements in Temp Folder
                        foreach (var value in attachString.Values)
                        {

                            FileInfo file = new FileInfo(value);
                            String filename = file.Name;
                            if (Directory.Exists(DestDirectory))
                            {
                                if (!file.Exists)
                                {
                                    File.Copy(value, DestDirectory + @"\" + filename);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(DestDirectory);
                                File.Copy(value, DestDirectory + @"\" + filename);
                            }

                        }

                        //Step 3: Upload Temp Folder Contents to Sharepoint
                        SharepointUpload upload = new SharepointUpload(Email, Password, folderName);
                        upload.UploadToSharepoint();


                        //Step 4: Send Email
                        SendEmailForApproval(lineTitles, lineAnswers);

                    });

                    task.Wait();


                    Application.Current.Shutdown();
                }
                else
                {
                    Error_SendButton ErrorPopup = new Error_SendButton(Error);
                    ErrorPopup.ShowDialog();
                }
            }

        }
        public void ReviewProject()
        {
            bool? check = cbCodeReview.IsChecked;
            String Email = "";
            String Password = "";

            EmailPasswordForm Popup_Email_Password = new EmailPasswordForm();
            Popup_Email_Password.ShowDialog();
            if (Popup_Email_Password.DialogResult.HasValue && Popup_Email_Password.DialogResult.Value)
            {
                //Get Email And Password
                Email = Popup_Email_Password.Email;
                Password = Popup_Email_Password.Password;

                SendFrom = Email;
                User = txtUser.Text.ToString();
                Partner = txtPartner.Text.ToString();
                CMRN = txtCMRN.Text;
                tableParmName = txtTableParmName.Text;
                codeReviewBY = txtCodeReviewBY.Text;
                codeReviewDate = txtCheckSignOffDate.Text;
                ProjectManager = txtProjectManager.Text;
                Description = txtDescription.Text;
                Title = txtTitle.Text;
                //  SendFrom = EmailAddress;


                var task = Task.Run(() =>
                {
                    CreateWordDoc();

                    SharepointUpload upload = new SharepointUpload(Email, Password, folderName);
                    upload.UploadWordDocToSharepoint();

                    String[] lineTitles = { "User", "Partner", "Date", "Title", "ChangeManagementRequestNumber", "UserApprovalofProject", "PartnerApprovalofInitialProject", "Table/ParmUpdate", "Table/ParmName", "DevelopmentCompleted", "TestingCompleted", "CodeReview/CheckSignOff", "CodeReviewBy", "CodeReviewDate", "KeyUserSignoff", "PartnerSignoff", "ImplementationFinalStatus", "PostImplementationReview", "Envelopes", "BusinessProcess", "ServiceAdapters", "PerlScripts", "EmailCodeList", "DocumentMaps", "DocumentExtractionMap", "XSLTEmailErrorHeader", "MapCodeTables", "RAILScsvTable", "RAILScsvRecord", "RAILScsvFilter", "FileStructureinProduction", "FTPConnect", "TRANSPORTParmFile", "BusinessProcessSchedule", "ServiceAdapterSchedule", "SetPartnerInGISStatsTable", "Description", "SharepointFolderName", "ProjectManager" };
                    String[] lineAnswers = { User, Partner, currentDate, Title, CMRN, UAOP.ToString(), PAOIP.ToString(), tableParm.ToString(), tableParmName, developementCompleted.ToString(), testingCompleted.ToString(), codeReview.ToString(), codeReviewBY, codeReviewDate, keyUserSignOff.ToString(), partnerSignOff.ToString(), impFinalStatus, PostImpReview, Envelopes.ToString(), BP.ToString(), ServiceAdapters.ToString(), perlScripts.ToString(), EmailCodeList.ToString(), docMaps.ToString(), docExtractionMap.ToString(), XSLTEmail.ToString(), mapCodeTables.ToString(), RAILStable.ToString(), RAILSrecord.ToString(), RAILSfilter.ToString(), fileStructureProd.ToString(), FTPconnect.ToString(), TRANSPORTfile.ToString(), BusinessProcessSchedule.ToString(), ServiceAdapterSchedule.ToString(), SetPartnerGISStatsTable.ToString(), Description, folderName, ProjectManager };

                    if (check == true)
                    {
                        SendEmailStatus(lineTitles, lineAnswers, "Approved");
                    }

                    if (check == false)
                    {
                        SendEmailStatus(lineTitles, lineAnswers, impFinalStatus);
                    }

                });
                task.Wait();

                Application.Current.Shutdown();

            }
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (ReadOnly == false)
            {
                SubmitNewProject();
            }
            else
            {
                ReviewProject();
            }
        }
        private void formMain_loaded(object sender, RoutedEventArgs e)
        {
            if (ReadOnly == false)
            {
                txtDate.Text = currentDate;
                txtCodeReviewBY.IsEnabled = false;
                txtCheckSignOffDate.IsEnabled = false;
                lblCodeReviewBY.IsEnabled = false;
                lblCheckSignOffDate.IsEnabled = false;
                txtTableParmName.IsEnabled = false;
                lblTableParmUpdate.IsEnabled = false;
                rbAbandonedPOST.IsEnabled = false;
                rbBackedOutPOST.IsEnabled = false;
                rbSuccessPOST.IsEnabled = false;
                cbCodeReview.IsEnabled = false;
                txtCodeReviewBY.IsEnabled = false;
                txtCheckSignOffDate.IsEnabled = false;
            }
            else
            {

                txtCodeReviewBY.IsEnabled = true;
                txtCheckSignOffDate.IsEnabled = true;
                lblCodeReviewBY.IsEnabled = true;
                lblCheckSignOffDate.IsEnabled = true;
                txtTableParmName.IsEnabled = false;
                lblTableParmUpdate.IsEnabled = false;
                rbAbandonedPOST.IsEnabled = true;
                rbBackedOutPOST.IsEnabled = true;
                rbSuccessPOST.IsEnabled = true;
                cbCodeReview.IsEnabled = true;
                txtCodeReviewBY.IsEnabled = true;
                txtCheckSignOffDate.IsEnabled = true;
            }



        }

        //Radio Buttons
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

        private void rbInstalledIMP_Checked(object sender, RoutedEventArgs e)
        {
            impFinalStatus = "Installed";
        }

        private void rbBackedOutIMP_Checked(object sender, RoutedEventArgs e)
        {
            impFinalStatus = "Backed Out";
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


        
        //CheckBoxes
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

       //Check Boxes
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


            txtCheckSignOffDate.Text = currentDate;
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

        private void Service_Adapter_Schedule_Checked(object sender, RoutedEventArgs e)
        {
            ServiceAdapterSchedule = true;
        }

        private void cbCodeReview_Unchecked(object sender, RoutedEventArgs e)
        {
            codeReview = false;
            txtCodeReviewBY.IsEnabled = false;
            txtCheckSignOffDate.IsEnabled = false;
            lblCodeReviewBY.IsEnabled = false;
            lblCheckSignOffDate.IsEnabled = false;
            txtCodeReviewBY.Text = "";
            txtCheckSignOffDate.Text = "";
        }
        private void cbSetPartnerInGISStatsTable_Checked(object sender, RoutedEventArgs e)
        {
            SetPartnerGISStatsTable = true;
        }



        private void cbBusinessProcessSchedule_Checked(object sender, RoutedEventArgs e)
        {
            BusinessProcessSchedule = true;

        }

        private void cbBusinessProcesses_Checked(object sender, RoutedEventArgs e)
        {
            BP = true;
            if (ReadOnly == false)
            {

                String attachment = OpenFileDialog();
                do
                {
                    BusinessProccessCount = addAttachment(cbBusinessProcesses.Content.ToString(), attachment, BusinessProccessCount);
                    attachment = OpenFileDialog();
                } while (attachment != null);
            }
        }

        private void cbBusinessProcesses_Unchecked(object sender, RoutedEventArgs e)
        {
            BP = false;
            removeAttachment(cbBusinessProcesses.Content.ToString());
            BusinessProccessCount = 0;
        }

        private void cbServiceAdapters_Checked(object sender, RoutedEventArgs e)
        {
            ServiceAdapters = true;
            if (ReadOnly == false)
            {
                String attachment = OpenFileDialog();
                do
                {
                    ServiceAdapterCount = addAttachment(cbServiceAdapters.Content.ToString(), attachment, ServiceAdapterCount);
                    attachment = OpenFileDialog();
                } while (attachment != null);
            }
        }

        private void cbServiceAdapters_Unchecked(object sender, RoutedEventArgs e)
        {
            ServiceAdapters = false;
            removeAttachment(cbServiceAdapters.Content.ToString());
            ServiceAdapterCount = 0;
        }

        private void cbPerlScripts_Checked(object sender, RoutedEventArgs e)
        {
            perlScripts = true;
            if (ReadOnly == false)
            {
                String attachment = OpenFileDialog();
                do
                {
                    PerlScriptCount = addAttachment(cbPerlScripts.Content.ToString(), attachment, PerlScriptCount);
                    attachment = OpenFileDialog();

                } while (attachment != null);
            }
        }

        private void cbPerlScripts_Unchecked(object sender, RoutedEventArgs e)
        {
            perlScripts = false;
            removeAttachment(cbPerlScripts.Content.ToString());
            PerlScriptCount = 0;
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
            if (ReadOnly == false)
            {
                String attachment = OpenFileDialog();
                attachments.Add(attachment);
            }
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
            if (ReadOnly == false)
            {
                String attachment = OpenFileDialog();
                do
                {
                    transportCount = addAttachment(cbTRANSPORTparmFile.Content.ToString(), attachment, transportCount);
                    attachment = OpenFileDialog();

                } while (attachment != null);
            }
        }

        private void cbTRANSPORTparmFile_Unchecked(object sender, RoutedEventArgs e)
        {
            TRANSPORTfile = false;
            removeAttachment(cbTRANSPORTparmFile.Content.ToString());
            PerlScriptCount = 0;
        }








        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FormUserApproval_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default["ProjectManager"].Equals("") && Settings.Default["EmailAddress"].Equals(""))
            {
                ProjectManager_Name popup = new ProjectManager_Name();
                var Result = popup.ShowDialog();

                if (Result == true)
                {
                    Settings.Default["ProjectManager"] = popup.Name1;
                    Settings.Default["EmailAddress"] = popup.Email1;
                    Settings.Default.Save();

                }

                //txtProjectManager.Text = Settings.Default["ProjectManager"].ToString();
                EmailAddress = Settings.Default["EmailAddress"].ToString();
                txtProjectManager.IsEnabled = false;


            }
            else
            {
                // txtProjectManager.Text = Settings.Default["ProjectManager"].ToString();
                EmailAddress = Settings.Default["EmailAddress"].ToString();
                txtProjectManager.IsEnabled = false;
            }
        }

        private void btnAddAttachment_Click(object sender, RoutedEventArgs e)
        {
            sbExtraAttachement = new StringBuilder();
            String attachment = OpenFileDialog();
            if (attachment != null)
            {
                do
                {
                    ExtraCount = addAttachment("Extra", attachment, ExtraCount);
                    FileInfo file = new FileInfo(attachment);
                    sbExtraAttachement.Append(file.Name + "\n");
                    attachment = OpenFileDialog();
                } while (attachment != null);
            }
        }



        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void formMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Store the Mouse Position
            startPosition = e.GetPosition(null);
        }

        private void formMain_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPosition - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

            }
        }

      


        private void formMain_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void formMain_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                ReadOnly = true;
                String[] file = (String[])e.Data.GetData(DataFormats.FileDrop);
                ProcessFile_DragAndDrop(file[0]);
            }
        }



        private void txtProjectManager_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        public void SendEmailForApproval(String[] lineTitles, String[] lineAnswers)
        {
            MailMessage mail = new MailMessage(SendFrom, sendTo);

            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = "10.77.48.132";


            StringBuilder stringbuilder = new StringBuilder();
            for (int i = 0; i < lineTitles.Length; i++)
            {
                stringbuilder.AppendLine(lineTitles[i] + "\t" + lineAnswers[i]);
            }

            String body = stringbuilder.ToString();
            byte[] BodyArray = Encoding.UTF8.GetBytes(body);
            MemoryStream ms = new MemoryStream(BodyArray);

            mail.Attachments.Add(new Attachment(ms, "test.epcl"));

            foreach (var value in attachString.Values)
            {
                mail.Attachments.Add(new Attachment(value));
            }

            mail.Subject = Subject;
            mail.Body = body;
            client.Send(mail);

        }
        public void SendEmailStatus(String[] lineTitles, String[] lineAnswers, String Approval)
        {
            //MailMessage mail = new MailMessage(SendFrom, sendTo);
            MailMessage mail = new MailMessage(sendTo, SendFrom);
            mail.From = new MailAddress(SendFrom);
            mail.ReplyToList.Add(sendTo);

            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = "10.77.48.132";



            StringBuilder stringbuilder = new StringBuilder();

            if (Approval.Equals("Approved"))
            {
                stringbuilder.AppendLine("Status: Approved\n");
            }
            else if (Approval.Equals("Backed Out"))
            {
                stringbuilder.AppendLine("Status: Backed Out\n");
            }
            else if (Approval.Equals("Abandoned"))
            {
                stringbuilder.AppendLine("Status: Abandoned\n");
            }
            else if (Approval.Equals("Rejected"))
            {
                stringbuilder.AppendLine("Status: Rejected\n");
            }

            for (int i = 0; i < lineTitles.Length; i++)
            {
                stringbuilder.AppendLine(lineTitles[i] + "\t" + lineAnswers[i]);
            }


            String body = stringbuilder.ToString();
            byte[] BodyArray = Encoding.UTF8.GetBytes(body);
            MemoryStream ms = new MemoryStream(BodyArray);

            mail.Attachments.Add(new Attachment(ms, "test.epcl"));

            foreach (var value in attachString.Values)
            {
                mail.Attachments.Add(new Attachment(value));
            }

            mail.Subject = Subject;
            mail.Body = body;
            client.Send(mail);

        }

        private void txtCodeReviewBY_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private String OpenFileDialog()
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                return dlg.FileName;
            }
            return null;
        }

        public int addAttachment(String name, String file, int Count)
        {
            if (attachString.Count > 0)
            {
                while (attachString.ContainsKey(name + Count))
                {
                    Count++;
                }
                attachString.Add(name + Count, file);

            }
            else
            {
                attachString.Add(name + Count, file);
                Count = Count + 1;
            }
            return Count;

        }

        public void removeAttachment(String name)
        {


            foreach (var key in attachString.Keys.Reverse())
            {
                if (key.Contains(name))
                {
                    attachString.Remove(key);
                }
            }



        }

        public String Errors()
        {

            StringBuilder sbErrors = new StringBuilder();
            bool Errors = false;
            if (User.Equals(""))
            {
                sbErrors.AppendLine("**Enter the User**");
                Errors = true;
            }
            if (Partner.Equals(""))
            {
                sbErrors.AppendLine("**Enter the Partner**");
                Errors = true;
            }
            if (Title.Equals(""))
            {
                sbErrors.AppendLine("**Enter a Title**");
                Errors = true;
            }
            if (sendTo == null)
            {
                sbErrors.AppendLine("**Enter the Recipient**");
                Errors = true;
            }

            if (Errors == true)
            {
                return sbErrors.ToString();
            }

            return null;
        }
        public void getFormItems(String item, String value)
        {
            if (item.Equals("User"))
            {
                txtUser.Text = value;

            }
            if (item.Equals("Partner"))
            {
                txtPartner.Text = value;
            }
            if (item.Equals("Date"))
            {
                txtDate.Text = value;
            }
            if (item.Equals("ChangeManagementRequestNumber"))
            {
                txtCMRN.Text = value;

            }
            if (item.Equals("UserApprovalofProject"))
            {
                cbUAOP.IsChecked = bool.Parse(value);
            }
            if (item.Equals("PartnerApprovalofInitialProject"))
            {
                cbPAOIP.IsChecked = bool.Parse(value);
            }
            if (item.Equals("Table/ParmUpdate"))
            {
                cbTPU.IsChecked = bool.Parse(value);
            }
            if (item.Equals("Table/ParmName"))
            {
                txtTableParmName.Text = value;
            }
            if (item.Equals("DevelopmentCompleted"))
            {
                cbDevelopmentComplete.IsChecked = bool.Parse(value);
            }
            if (item.Equals("TestingCompleted"))
            {
                cbTestingComplete.IsChecked = bool.Parse(value);
            }
            if (item.Equals("CodeReview/CheckSignOff"))
            {
                cbCodeReview.IsChecked = bool.Parse(value);
            }
            if (item.Equals("CodeReviewBy"))
            {
                txtCodeReviewBY.Text = value;
            }
            if (item.Equals("CodeReviewDate"))
            {
                txtCheckSignOffDate.Text = value;
            }
            if (item.Equals("KeyUserSignoff"))
            {
                cbKeyUserSignOff.IsChecked = bool.Parse(value);
            }
            if (item.Equals("PartnerSignoff"))
            {
                cbPartnerSignOff.IsChecked = bool.Parse(value);
            }
            if (item.Equals("ImplementationFinalStatus"))
            {

                impFinalStatus = value;

                if (value.Equals("Installed"))
                {
                    rbInstalledIMP.IsChecked = true;

                }
                if (value.Equals("Backed Out"))
                {
                    rbBackedOutIMP.IsChecked = true;
                }
                if (value.Equals("Abandoned"))
                {
                    rbAbandonedIMP.IsChecked = true;
                }

            }
            if (item.Equals("PostImplementationReview"))
            {

                PostImpReview = value;

                if (value.Equals("Success"))
                {
                    rbSuccessPOST.IsChecked = true;
                }
                if (value.Equals("Backed Out"))
                {
                    rbBackedOutPOST.IsChecked = true;
                }
                if (value.Equals("Abandoned"))
                {
                    rbAbandonedPOST.IsChecked = true;
                }

            }
            if (item.Equals("Envelopes"))
            {
                cbEnvelopes.IsChecked = bool.Parse(value);
            }
            if (item.Equals("BusinessProcess"))
            {
                cbBusinessProcesses.IsChecked = bool.Parse(value);

            }
            if (item.Equals("ServiceAdapters"))
            {

                cbServiceAdapters.IsChecked = bool.Parse(value);
            }
            if (item.Equals("PerlScripts"))
            {
                cbPerlScripts.IsChecked = bool.Parse(value);

            }
            if (item.Equals("EmailCodeList"))
            {
                cbEmailCodeList.IsChecked = bool.Parse(value);

            }
            if (item.Equals("DocumentMaps"))
            {

                cbDocumentMaps.IsChecked = bool.Parse(value);
            }
            if (item.Equals("DocumentExtractionMap"))
            {
                cbDocumentExtractionMap.IsChecked = bool.Parse(value);

            }
            if (item.Equals("XSLTEmailErrorHeader"))
            {
                cbXSLTEmailErrorHeader.IsChecked = bool.Parse(value);

            }
            if (item.Equals("MapCodeTables"))
            {

                cbMapCodeTables.IsChecked = bool.Parse(value);
            }
            if (item.Equals("RAILScsvTable"))
            {
                cbRAILS_csv_Table.IsChecked = bool.Parse(value);

            }
            if (item.Equals("RAILScsvRecord"))
            {
                cbRAILS_csv_Record.IsChecked = bool.Parse(value);


            }
            if (item.Equals("RAILScsvFilter"))
            {
                cbRAILS_csv_Filter.IsChecked = bool.Parse(value);

            }
            if (item.Equals("FileStructureinProduction"))
            {
                cbFile_Structure_in_prodcution.IsChecked = bool.Parse(value);

            }
            if (item.Equals("FTPConnect"))
            {
                cbFTPConnect.IsChecked = bool.Parse(value);

            }
            if (item.Equals("TRANSPORTParmFile"))
            {
                cbTRANSPORTparmFile.IsChecked = bool.Parse(value);

            }
            if (item.Equals("ProjectManager"))
            {

                txtProjectManager.Text = value;
                ProjectManager = value;
            }
            if (item.Equals("Title"))
            {
                txtTitle.Text = value;
            }
            if (item.Equals("Description"))
            {
                txtDescription.Text = value;
            }

            if (item.Equals("SharepointFolderName"))
            {
                folderName = value;
            }
            if (item.Equals("Subject"))
            {
                Subject = value;
            }
        }

        public void CreateWordDoc()
        {

            String[] Section_1 = { User, Partner, currentDate, Title, CMRN };

            String[] Section_2 = { "Users Approval of Project: ", (UAOP == true) ? ((char)0x221A).ToString() : "", "", "",
                                    "Partner Approval of Initial Project: ", (PAOIP == true) ? ((char)0x221A).ToString() : "", "", "",
                                    "Table/Parm Update: ", (tableParm == true) ? ((char)0x221A).ToString() : "", "name: "+tableParmName, "",
                                    "Development Completed: ", (developementCompleted == true) ? ((char)0x221A).ToString() : "", "", "",
                                    "Testing Completed: ", (testingCompleted == true) ? ((char)0x221A).ToString() : "", "", "",
                                    "Code Review/Check Sign Off: ", (codeReview == true) ? ((char)0x221A).ToString() : "", "by: "+codeReviewBY, "date: "+codeReviewDate,
                                    "Key User Signoff: ", (keyUserSignOff == true) ? ((char)0x221A).ToString() : "", "", "",
                                    "Partner Signoff: ", (partnerSignOff == true) ? ((char)0x221A).ToString() : "", "", "",
                                    "Implementation Final Status: ", impFinalStatus, "", "",
                                    "Post Implementation Review: ",(PostImpReview==null) ? "Initial" :PostImpReview, "", "", };

            String[] Section_3 = { "Envelopes: ", (Envelopes == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Business Processes: ", (BP == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Service Adapters: ", (ServiceAdapters == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Perl Scripts: ", (perlScripts == true) ? ((char)0x221A).ToString() : "", "name: "+perlScriptsName,"",
                                    "Email Code List: ", (EmailCodeList == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Document Maps: ", (docMaps == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Document Extraction Map: ", (docExtractionMap == true) ? ((char)0x221A).ToString() : "", "","",
                                    "XSLT Email Error Header: ", (XSLTEmail == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Map Code Tables: ",  (mapCodeTables == true) ? ((char)0x221A).ToString() : "", "","",
                                    "RAILS csv Table: ", (RAILStable == true) ? ((char)0x221A).ToString() : "", "","",
                                    "RAILS csv Record: ", (RAILSrecord == true) ? ((char)0x221A).ToString() : "", "","",
                                    "RAILS csv Filter: ", (RAILSfilter == true) ? ((char)0x221A).ToString() : "", "","",
                                    "File Structure in Production: ", (fileStructureProd == true) ? ((char)0x221A).ToString() : "", "","",
                                    "FTP Connect: ", (FTPconnect == true) ? ((char)0x221A).ToString() : "", "","",
                                    "TRANSPORT Parm File: ", (TRANSPORTfile == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Business Process Schedule: ", (BusinessProcessSch == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Service Adapter Schedule: ", (ServiceAdapterSch == true) ? ((char)0x221A).ToString() : "", "","",
                                    "Set Partner in GIS Stats table: ", (SetPartnerInGISStatsTable == true) ? ((char)0x221A).ToString() : "", "","",};

            String[] Section_4 ={ "Business Process Schedule: ", (BusinessProcessSch == true) ? ((char)0x221A).ToString() : "","","",
                                    "Service Adapter Schedule: ", (ServiceAdapterSch == true) ? ((char)0x221A).ToString() : "","","",
                                    "Set Partner in GIS Stats table: ", (SetPartnerInGISStatsTable == true) ? ((char)0x221A).ToString() : "","","",};


            String[] SignatureAndDate = { ProjectManager, currentDate };

            CreateDocument create = new CreateDocument(Section_1, Section_2, Section_3, Section_4, SignatureAndDate);

        }
        private static T FindAnchestor<T>(DependencyObject current)
    where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
    }
}


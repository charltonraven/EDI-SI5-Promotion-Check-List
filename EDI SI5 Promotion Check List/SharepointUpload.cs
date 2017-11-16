using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.Office.Interop.Outlook;
using System.Security;
using System.IO;


namespace EDI_SI5_Promotion_Check_List
{
    class SharepointUpload
    {

        public String SiteURL = "https://sonoco.sharepoint.com/sites/business-technology/team/edi/";
        private String documentListName = "EDI Projects";
        private String folderName;
        private String Username = "";
        private String password = "";
        private ClientContext clientContext;



        public SharepointUpload()
        {

        }

        public SharepointUpload(String Username, String Password, String folderName)
        {
            this.Username = Username;
            this.password = Password;
            this.folderName = folderName;



        }

        public void UploadToSharepoint()
        {


            //Initialize contact with Sharepoint
            SecureString Password = new SecureString();
            foreach (char c in password.ToCharArray()) Password.AppendChar(c);
            SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(Username, Password);
            String SiteURL = "https://sonoco.sharepoint.com/sites/business-technology/team/edi/";
            clientContext = new ClientContext(SiteURL);
            clientContext.Credentials = credentials;
            Web web = clientContext.Web;
            clientContext.Load(web);
            try
            {
                //Create new Folder                  
                List olist = clientContext.Web.Lists.GetByTitle("EDI Projects");
                Microsoft.SharePoint.Client.Folder newFolder = olist.RootFolder.Folders.Add("EDI Projects/Completed Projects/" + folderName);
                User user = web.EnsureUser(Username);

                clientContext.ExecuteQuery();

                //Set permissions on Folder !!!!!!
                newFolder.ListItemAllFields.BreakRoleInheritance(true, true);
                clientContext.Load(clientContext.Web.RoleDefinitions);
                clientContext.ExecuteQuery();
                RoleDefinitionBindingCollection roleDefinitionBindingCollection = new RoleDefinitionBindingCollection(clientContext);
                roleDefinitionBindingCollection.Add(clientContext.Web.RoleDefinitions.GetByName("Full Control"));
                var siteUser = clientContext.Web.EnsureUser(Username);
                olist.RoleAssignments.Add(siteUser, roleDefinitionBindingCollection);
                clientContext.ExecuteQuery();

                //*************************Upload File to new Folder ********************
                DirectoryInfo directory = new DirectoryInfo(@"C:\SharePoint Upload");

                if (!directory.Exists)
                {
                    directory.Create();
                }
                FileInfo[] files = directory.GetFiles();

                if (clientContext.HasPendingRequest)
                    clientContext.ExecuteQuery();


                foreach (FileInfo file in files)
                {

                    using (FileStream fs = file.OpenRead())
                    {
                        String documentName = file.FullName;
                        byte[] byteFile = System.IO.File.ReadAllBytes(documentName);

                        Microsoft.SharePoint.Client.Folder folder = web.GetFolderByServerRelativeUrl("EDI Projects/Completed Projects/" + folderName);


                        if (clientContext.HasPendingRequest)
                            clientContext.ExecuteQuery();

                        List documentsList = clientContext.Web.Lists.GetByTitle(documentListName);

                        var fileCreationInformation = new FileCreationInformation();

                        fileCreationInformation.Content = byteFile;

                        fileCreationInformation.Overwrite = true;

                        fileCreationInformation.Url = "/sites/business-technology/team/edi/EDI Projects/Completed Projects/" + folderName + "/" + file.Name;

                        Microsoft.SharePoint.Client.File uploadFile = documentsList.RootFolder.Files.Add(fileCreationInformation);

                        uploadFile.ListItemAllFields.Update();
                        clientContext.ExecuteQuery();
                    }
                    file.Delete();
                }
            }
            catch (IdcrlException e)
            {
                EmailPasswordForm Popup = new EmailPasswordForm(Username);
                Popup.ShowDialog();
                this.Username = Popup.Email;
                this.password = Popup.Password;
                UploadToSharepoint();




            }
        }
        public void UploadWordDocToSharepoint()
        {

            //Initialize contact with Sharepoint
            SecureString Password = new SecureString();
            foreach (char c in password.ToCharArray()) Password.AppendChar(c);
            SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(Username, Password);
            String SiteURL = "https://sonoco.sharepoint.com/sites/business-technology/team/edi/";
            clientContext = new ClientContext(SiteURL);
            clientContext.Credentials = credentials;
            Web web = clientContext.Web;
            clientContext.Load(web);
            try
            {
                if (clientContext.HasPendingRequest)
                    clientContext.ExecuteQuery();


                DirectoryInfo directory = new DirectoryInfo(@"C:\SharePoint Upload");
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {

                    using (FileStream fs = file.OpenRead())
                    {

                            String documentName = file.FullName;
                            byte[] byteFile = System.IO.File.ReadAllBytes(documentName);

                            Microsoft.SharePoint.Client.Folder folder = web.GetFolderByServerRelativeUrl("EDI Projects/Completed Projects/" + folderName);


                            if (clientContext.HasPendingRequest)
                                clientContext.ExecuteQuery();

                            List documentsList = clientContext.Web.Lists.GetByTitle(documentListName);

                            var fileCreationInformation = new FileCreationInformation();

                            fileCreationInformation.Content = byteFile;

                            fileCreationInformation.Overwrite = true;

                            fileCreationInformation.Url = "/sites/business-technology/team/edi/EDI Projects/Completed Projects/" + folderName + "/" + file.Name;

                            Microsoft.SharePoint.Client.File uploadFile = documentsList.RootFolder.Files.Add(fileCreationInformation);

                            uploadFile.ListItemAllFields.Update();
                            clientContext.ExecuteQuery();
                        }
                        file.Delete();
                    }
                }
            catch (IdcrlException e)
            {
                Console.Write(e.Message);
                EmailPasswordForm Popup = new EmailPasswordForm(Username);
                Popup.ShowDialog();
                this.Username = Popup.Email;
                this.password = Popup.Password;
                UploadWordDocToSharepoint();
            }

        }



    }
}

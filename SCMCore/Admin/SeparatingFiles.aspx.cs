using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using System.IO;
using SCMCore.Classes;

namespace SCMCore.Admin
{
    public partial class SeparatingFiles : System.Web.UI.Page
    {
        Bis.SeparateingFilesMethod BisSeparateingFiles = new Bis.SeparateingFilesMethod();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSeparate_Click(object sender, EventArgs e)
        {
            ViewModel.Search SearchFiles = new ViewModel.Search();
            DataSet dsFiles = BisSeparateingFiles.GetAllFiles(SearchFiles);
            dsFiles.Tables[0].Columns.Add("Exist", typeof(bool));
            foreach (DataRow dr in dsFiles.Tables[0].Rows)
            {
                string SourcePath = Server.MapPath(@"..\SCM\" + dr["Url"].ToString());
                string DestinationPath = Server.MapPath(@"..\Philately\" + dr["Url"].ToString());

                if (File.Exists(SourcePath))
                {
                    File.Move(SourcePath, DestinationPath);
                    dr["Exist"] = true;
                }
                else
                {
                    dr["Exist"] = false;
                }
            }

        }

        protected void btnCreateAllImageSizes_Click(object sender, EventArgs e)
        {
            FileTypes ft = new FileTypes();
            string[] Folders = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"\Picture");
            foreach (var folder in Folders)
            {
                string[] filePaths = Directory.GetFiles(folder);
                foreach (var item in filePaths)
                {
                    string[] arrPath = item.Split('\\');
                    string FileType = ft.FindImageTypeInString("." + arrPath[arrPath.Length - 1].Split('.')[1]);
                    if (ft.IsImage(FileType))
                    {
                        System.Drawing.Image imageContent = System.Drawing.Image.FromFile(item);
                        string FileName = arrPath[arrPath.Length - 1].Split('.')[0];
                        //if (!Directory.Exists(folder + "\\Middle"))
                        //{
                        //    Directory.CreateDirectory(folder + "\\Middle");
                        //}
                        if (!Directory.Exists(folder + "\\Small"))
                        {
                            Directory.CreateDirectory(folder + "\\Small");
                        }
                        //if (!File.Exists(folder + @"\Middle\" + FileName + FileType))
                        //{
                        //imageContent.resizeImage(800, null).Save(folder + @"\Middle\" + FileName + FileType);
                        //}
                        if (!File.Exists(folder + @"\Small\" + FileName + FileType))
                        {
                            imageContent.resizeImage(300, null).Save(folder + @"\Small\" + FileName + FileType);
                        }
                    }
                }

            }
        }
    }
}
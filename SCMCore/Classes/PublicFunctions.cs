using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http.ModelBinding;

namespace SCMCore.Classes
{
    public class PublicFunctions
    {

        public ArrayList GetErrorListFromModelState(ModelStateDictionary MS)
        {
            ArrayList ErrorList = new ArrayList();
            foreach (ModelState modelState in MS.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    ErrorList.Add(error.ErrorMessage);
                }
            }
            return ErrorList;
        }


        public string[] ListFiles(String FtpUri, String UserName, String Pass)
        {
            List<String> Folders = new List<String>();
            List<String> files = new List<String>();
            Queue<String> folders = new Queue<String>();
            folders.Enqueue(FtpUri);

            while (folders.Count > 0)
            {
                String fld = folders.Dequeue();


                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(UserName, Pass);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        Folders.Add(line.Trim());
                        line = resp.ReadLine();
                    }
                }

                ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(UserName, Pass);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        if (line.Trim().ToLower().StartsWith("d") || line.Contains(" <DIR> "))
                        {
                            String dir = Folders.First(x => line.EndsWith(x));
                            Folders.Remove(dir);
                            folders.Enqueue(fld + dir + "/");
                        }
                        line = resp.ReadLine();
                    }
                }
                files.AddRange(from f in Folders select fld + f);
            }
            return Folders.ToArray();
        }


        
    }
}


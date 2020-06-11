using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SCMCore.Classes
{
    public class FileTypes
    {

        public ArrayList imgType()
        {
            ArrayList arr = new ArrayList();
            arr.Add(".jpg");
            arr.Add(".jpeg");
            arr.Add(".png");
            arr.Add(".bmp");
            arr.Add(".gif");
            return arr;
        }
        public ArrayList docType()
        {
            ArrayList arr = new ArrayList();
            arr.Add(".txt");
            arr.Add(".docx");
            arr.Add(".doc");
            arr.Add(".pdf");
            arr.Add(".rtf");
            arr.Add(".vss");

            return arr;


        }
        public ArrayList videoType()
        {
            ArrayList arr = new ArrayList();
            arr.Add(".mp4");
            return arr;


        }
        public ArrayList compactType()
        {
            ArrayList arr = new ArrayList();
            arr.Add(".rar");
            arr.Add(".zip");
            return arr;
        }

        public ArrayList AllTypes()
        {
            ArrayList arr = new ArrayList();
            arr.AddRange(compactType());
            arr.AddRange(videoType());
            arr.AddRange(docType());
            arr.AddRange(imgType());
            return arr;
        }
        public ArrayList AllTypesWithoutImages()
        {
            ArrayList arr = new ArrayList();
            arr.AddRange(compactType());
            arr.AddRange(videoType());
            arr.AddRange(docType());
            return arr;
        }
        public ArrayList DocAndComp()
        {
            ArrayList arr = new ArrayList();
            arr.AddRange(compactType());
            arr.AddRange(docType());
            return arr;
        }

        public string FindImageTypeInString(string InputStr)
        {
            ArrayList arr = new ArrayList();
            arr.AddRange(imgType());
            foreach(string type in arr)
            {
                if (InputStr.ToLower().Contains(type.Replace(".","")))
                {
                    return type;
                }
            }
            return "";
        }
        public string FindFileTypeInString(string InputStr)
        {
            ArrayList arr = new ArrayList();
            arr.AddRange(imgType());
            arr.AddRange(docType());
            arr.AddRange(compactType());
            arr.AddRange(videoType());
            foreach (string type in arr)
            {
                if (InputStr.ToLower().Contains(type.Replace(".", "")))
                {
                    return type;
                }
            }
            return "";
        }
        public bool IsImage(string InputStr)
        {
            ArrayList arr = new ArrayList();
            arr.AddRange(imgType());
            foreach (string type in arr)
            {
                if (InputStr.ToLower() == type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
using System.Collections.Generic;

namespace SCMCore.Classes
{
    public class Projects
    {

        public Dictionary<string, string> dicProjects = new Dictionary<string, string>();


        public Projects()
        {
            dicProjects.Add("farbin", ".ir");
            dicProjects.Add("sumico", ".ir");
            dicProjects.Add("localhost", "");
        }

        public string ReturnClientName(string CN)
        {

            foreach (KeyValuePair<string, string> client in dicProjects)
            {
                if (client.Key == CN)
                {
                    return CN;
                }
            }
            return "";
        }
        public string ReturnClientUrl(string CN)
        {
            foreach (KeyValuePair<string, string> client in dicProjects)
            {
                if (client.Key == CN)
                {
                    return client.Key + client.Value;
                }
            }
            return "";
        }
    }
}
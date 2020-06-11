using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCMCore.ExtensionMethod;

namespace SCMCore.Admin.UserControl
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public DateTime MergeDateTime(DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);

        }
        public DateTime miladiDate
        {
            get
            {
                
                DateTime dt = new DateTime();
                TimeSpan ts = new TimeSpan();
                dt = txtDate.Text.StrShamsiToDateMiladi();
                ts = TimeSpan.Parse(txtTimes.Text);
                return (MergeDateTime(dt, ts));

            }
        }
        public DateTime shamsiDate
        {
            get
            {
                DateTime dt = new DateTime();
                dt = txtDate.Text.StringToDateTime();
                TimeSpan ts = new TimeSpan();
                ts = TimeSpan.Parse(txtTimes.Text);
                return (MergeDateTime(dt, ts));

            }
            set
            {
                txtDate.Text = shamsiDate.Date.ToString();
            }
        }

        public void GetDate(DateTime dt)
        {
            txtDate.Text = dt.ToString().StringToDateTime().ToShamsiDateYMD();
            txtTimes.Text = dt.ToString("HH:mm");
        }

        public void clean()
        {
            txtDate.Text="";
            txtTimes.Text = "";
        }
        public bool isEmpty()
        {
            if (txtDate.Text == "" || txtTimes.Text == "")
            {
                return true;
            }
            else
                return false;
        }
    }
}
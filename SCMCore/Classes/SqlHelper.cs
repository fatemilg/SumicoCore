using Newtonsoft.Json.Linq;
using SCMCore.ExtensionMethod;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Classes
{
    public class SqlHelper
    {
        protected SqlConnection connection;
        private object objProp;
        public SqlCommand getSqlCommand(string strCommandText, CommandType cmdtype)
        {
            string DomainName = "";
            if (HttpContext.Current.Items["DomainName"] != null)
            {
                DomainName = HttpContext.Current.Items["DomainName"].ToString();
            }
            else
            {
                DomainName = HttpContext.Current.Request.Cookies["DomainName"].Value;
            }
            string str = ConfigurationManager.AppSettings[DomainName + "ConnectionString"].ToString().DecryptString();
            connection = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = cmdtype;
                cmd.CommandText = strCommandText;
                cmd.Connection = connection;
                cmd.Connection.Open();
                //cmd.Disposed += new EventHandler(cmd_Disposed);
            }
            catch (Exception ex) { }

            return cmd;

        }
        //static void cmd_Disposed(object sender, EventArgs e)
        //{
        //    SqlCommand cmd = sender as SqlCommand;
        //    if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //    {
        //        cmd.Connection.Close();
        //        cmd.Connection.Dispose();
        //        GC.SuppressFinalize(sender);
        //        GC.WaitForPendingFinalizers();
        //    }
        //}
        public void AddParameter(ref SqlCommand cmd, object obj)
        {
            try
            {
                cmd.Parameters.Clear();
                PropertyInfo[] props = obj.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.PropertyType == typeof(System.Byte[]))
                    {
                        cmd.Parameters.Add(prop.Name, SqlDbType.Image).Value = DBNull.Value;
                    }
                    else
                    {
                        if (prop.PropertyType.Name == "String" && getValue(prop.GetValue(obj, null)) != null)
                        {
                            objProp = getValue(prop.GetValue(obj, null)).ToString().FixFarsi();
                        }
                        else
                        {
                            objProp = getValue(prop.GetValue(obj, null));
                        }
                    }
                    if (objProp == null) continue;
                    if (prop.PropertyType.Name == "object")
                    {
                        cmd.Parameters.Add(prop.Name, SqlDbType.DateTime).Value = objProp;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(prop.Name, objProp);

                    }
                }
            }
            catch
            {
            }

        }
        private static object getValue(object p)
        {
            return (p == null) ? null : handelDateTime(p);
        }
        private static object handelDateTime(object p)
        {
            System.Text.RegularExpressions.Regex regx = new System.Text.RegularExpressions.Regex(@"^\d{4}/\d{2}/\d{2}$");
            if (regx.IsMatch(p.ToString()))
            {
                string[] dateParts = p.ToString().Split('/');
                PersianCalendar pcal = new PersianCalendar();
                return pcal.ToDateTime(int.Parse(dateParts[0]),
                                       int.Parse(dateParts[1]),
                                       int.Parse(dateParts[2]), 0, 0, 0, 0);

            }
            return p;
        }
        public int RunProcedure(string storedProcName, object obj, bool transaction)
        {
            SqlCommand cmd = getSqlCommand(storedProcName, System.Data.CommandType.StoredProcedure);
            cmd.CommandTimeout = 600;
            int row = 0;
            try
            {
                AddParameter(ref cmd, obj);
                var ReturnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                var ReturnError = cmd.Parameters.Add("@ReturnError", SqlDbType.NVarChar, 1000);
                ReturnParameter.Direction = ParameterDirection.Output;
                ReturnError.Direction = ParameterDirection.Output;

                row = cmd.ExecuteNonQuery();
                if (ReturnError.Value.ToString() != "")
                {
                    string[] ErrorLines = { DateTime.Now.ToString(), storedProcName, ReturnError.Value.ToString(), "**********\n" };
                    WriteError(ErrorLines);
                    return 0;
                }
                else
                {
                    var result = ReturnParameter.Value.ToString().StringToBool();
                    if (result)
                    {
                        return row;
                    }
                    else      // catch etefagh oftade va rollback shode ast
                    {
                        return 0;

                    }
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                cmd.Connection.Dispose();
                cmd.Connection.Close();
            }
            return row;
        }
        public int RunProcedure(string storedProcName, object obj)
        {
            SqlCommand cmd = getSqlCommand(storedProcName, System.Data.CommandType.StoredProcedure);
            //cmd.CommandTimeout = 600;
            int row = 0;
            try
            {

                AddParameter(ref cmd, obj);
                row = cmd.ExecuteNonQuery();

            }
            catch (Exception exp)
            {
                string[] ErrorLines = { DateTime.Now.ToString(), storedProcName, exp.Message, "**********\n" };
                WriteError(ErrorLines);
            }
            finally
            {
                cmd.Connection.Dispose();
                cmd.Connection.Close();
            }
            return row;
        }
        public DataSet returnDataSet(string storedProcName, object obj)
        {
            SqlCommand cmd = getSqlCommand(storedProcName, System.Data.CommandType.StoredProcedure);
            //cmd.CommandTimeout = 600;
            DataSet dataSet = new DataSet();
            try
            {

                AddParameter(ref cmd, obj);
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;
                sqlDA.Fill(dataSet);
                return dataSet;
            }
            catch (Exception exp)
            {
                string[] ErrorLines = { DateTime.Now.ToString(), storedProcName, exp.Message, "**********\n" };
                WriteError(ErrorLines);
            }
            finally
            {
                cmd.Connection.Dispose();
                cmd.Connection.Close();
            }
            return dataSet;
        }



        public JArray ReturnJsonData(string storedProcName, object obj)
        {
            SqlCommand cmd = getSqlCommand(storedProcName, System.Data.CommandType.StoredProcedure);
            try
            {

                var jsonResult = new StringBuilder();
                AddParameter(ref cmd, obj);
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    jsonResult.Append("[]");
                }
                else
                {
                    while (reader.Read())
                    {
                        jsonResult.Append(reader.GetValue(0).ToString());
                    }
                }
                return JArray.Parse(jsonResult.ToString());
            }
            catch (Exception exp)
            {
                string[] ErrorLines = { DateTime.Now.ToString(), storedProcName, exp.Message, "**********\n" };
                WriteError(ErrorLines);
                return JArray.Parse("[]");
            }
            finally
            {
                cmd.Connection.Dispose();
                cmd.Connection.Close();
            }
        }
        public static bool CheckAccess(string EventName, Guid IDUser)
        {
            try
            {
                Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();
                ViewModel.tblEventUser getUserEvent = new ViewModel.tblEventUser();
                getUserEvent.IDUser = IDUser;
                getUserEvent.EventName = EventName;
                DataSet ds = BisEventUser.CheckForAccessInEventUser(getUserEvent);
                if (ds.Null_Ds())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        protected void WriteError(string[] ErrorLines)
        {
            string ErrorFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Error\ErrorLog.txt";
            using (StreamWriter TW = File.AppendText(ErrorFilePath))
            {
                foreach (string str in ErrorLines)
                {
                    TW.WriteLine(str);
                }
            }
        }
    }
}
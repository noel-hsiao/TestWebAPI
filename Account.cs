using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TestWebAPI.Model;

namespace TestWebAPI
{    
    public class Account
    {
        static readonly string dbconn = "Data Source=tcp:127.0.0.1,1433;Initial Catalog=DockersDB;User ID=sa;Password=1qaz@WSX;";

        public bool CreateAccount(string userName, string password)
        {
            try
            {
                
                using (var conn = new SqlConnection(dbconn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@username", userName);
                    param.Add("@password", EncrypPassword(password));


                    var checkDuplicate = conn.Query<UserAccount>("SELECT UserName FROM UserAccount WHERE UserName = @username", param);

                    if (checkDuplicate.Count() > 0)
                    {
                        return false;
                    }

                    string insertSql = "INSERT INTO UserAccount (UserName, Password) VALUES(@username, @password)";                  
                    conn.Execute(insertSql, param);

                }
            }
            catch(Exception ex)
            {
                //TO DO handle exception
                return false;
            }
            return true;
        }

        public bool Login(string userName, string password)
        {
            try
            {
                using (var conn = new SqlConnection(dbconn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@username", userName);
                    param.Add("@password", EncrypPassword(password));

                    var checkDuplicate = conn.Query<UserAccount>("SELECT UserName FROM UserAccount WHERE UserName = @username AND Password = @password", param);

                    if (checkDuplicate.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                //TO DO handle exception
                return false;
            }
        }

        public string EncrypPassword(string origin)
        {
            string returnString = string.Empty;

            byte[] stringToBByte = Encoding.UTF8.GetBytes(origin);

            using (MD5 crypMD5 = MD5.Create())
            {
                returnString = Convert.ToBase64String(crypMD5.ComputeHash(stringToBByte));
            }

            return returnString;
        }
    }
}

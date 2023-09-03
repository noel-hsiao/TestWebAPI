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
    public class ChatBoard
    {
        static readonly string dbconn = "Data Source=tcp:127.0.0.1,1433;Initial Catalog=DockersDB;User ID=sa;Password=1qaz@WSX;";

        public List<Chat> GetChats(int quantity)
        {
            List<Chat> chats = new List<Chat>();
            try
            {
                using (var conn = new SqlConnection(dbconn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@quantity", quantity);

                    chats = conn.Query<Chat>("SELECT TOP (@quantity) * FROM ChatBoard ORDER BY CreateTime DESC", param).ToList();

                }
            }
            catch (Exception ex)
            {
                //TO DO handle exception
            }

            return chats;
        }

        public bool CreateChat(string userName, string title, string contentText)
        {
            try
            {                
                using (var conn = new SqlConnection(dbconn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@title", title);
                    param.Add("@contenttext", contentText);
                    param.Add("@username", userName);
                    
                    string insertSql = "INSERT INTO ChatBoard (Title, ContentText, CreateUserName) VALUES(@title, @contenttext, @username)";                  
                    if(conn.Execute(insertSql, param) != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                //TO DO handle exception
                return false;
            }
        }

        public bool EditChat(int sno, string contentText)
        {
            try
            {
                using (var conn = new SqlConnection(dbconn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@sno", sno);
                    param.Add("@contenttext", contentText);

                    string updateSql = "UPDATE ChatBoard SET ContentText = @contenttext, UpdateTime = GETDATE() WHERE Sno = @sno";
                    if (conn.Execute(updateSql, param) != 0)
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

        public bool DeleteChat(int sno)
        {
            try
            {
                using (var conn = new SqlConnection(dbconn))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@sno", sno);

                    string updateSql = "UPDATE ChatBoard SET IsDelete = 1, UpdateTime = GETDATE() WHERE Sno = @sno";
                    if (conn.Execute(updateSql, param) != 0)
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

    }
}

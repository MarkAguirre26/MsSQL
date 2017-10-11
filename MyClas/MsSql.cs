using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyClass
{
    public class MsSql
    {
        public static string connectionString = "";

        public static DataTable Table(string query, string[] value = null)
        {

            int paramCounter = 0;



            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(query);


            string[] param = TrimString(query).Split(' ');
            for (int i = 0; i <= param.Length - 1; i++)
            {
                string str = new string(param[i].Take(1).ToArray());
                if (str == "@")
                {
                    if (value != null)
                    {
                        cmd.Parameters.AddWithValue(param[i], value[paramCounter].ToString());
                        paramCounter++;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(param[i], "");
                    }


                }
            }


            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public static Boolean ExecuteQuery(string query, string[] value)
        {
            int paramCounter = 0;
              
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query);


            string[] param = TrimString(query).Split(' ');
            for (int i = 0; i <= param.Length - 1; i++)
            {
                string str = new string(param[i].Take(1).ToArray());
                if (str == "@")
                {
                    if (value != null)
                    {
                        cmd.Parameters.AddWithValue(param[i], value[paramCounter].ToString());
                        paramCounter++;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(param[i], "");
                    }

                }
            }

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();

            }
        }


        private static string TrimString(string str)
        {
            return str.Replace(",", " ").Replace("(", " ").Replace(")", " ").Replace(";", " ");
        }

        public static string[] getColumnsName(string TableName)
        {
            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "select CONCAT(c.name,' ',c.is_identity) from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = 't_user' and t.type = 'U'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string n = reader.GetString(0);

                        if (getLastChar(n) == "0")
                        {
                            listacolumnas.Add(n.Replace(" 0", ""));
                        }

                    }

                }
                connection.Close();
                connection.Dispose();
            }
            return listacolumnas.ToArray();
        }

        private static string getLastChar(string str)
        {
            return str.Substring((str.Length - 1), 1);
        }
        
    }
}

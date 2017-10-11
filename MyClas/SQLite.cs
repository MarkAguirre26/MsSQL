using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClass
{
  public  class SQLite
    {
        public static string connectionString = "";

        public static DataTable Table(string query, string[] value = null)
        {

            int paramCounter = 0;



            DataTable dt = new DataTable();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteDataAdapter sda = new SQLiteDataAdapter();
            SQLiteCommand cmd = new SQLiteCommand(query);


            string[] param = TrimString(query).Split(' ');
            for (int i = 0; i <= param.Length - 1; i++)
            {
                string str = new string(param[i].Take(1).ToArray());
                if (str == "@")
                {
                    if(value != null)
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

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(query);

            string[] param = TrimString(query).Split(' ');
            for (int i = 0; i <= param.Length - 1; i++)
            {
                string str = new string(param[i].Take(1).ToArray());
                if (str == "@")
                {

                    cmd.Parameters.AddWithValue(param[i], value[paramCounter].ToString());

                    paramCounter++;
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
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * from " + TableName + "";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            listacolumnas.Add(reader.GetName(i));
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
          
            
            
            return listacolumnas.ToArray();
        }
    }
}

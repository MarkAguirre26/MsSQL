using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsSql.connectionString = @"Data Source=MARK-AGUIRRE\SQLEXPRESS;Initial Catalog=testdb;Integrated Security=SSPI;User ID = sa;Password=sasa;";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLite.connectionString = @"Data Source = C:\Users\Asus\Desktop\sample.db; Version = 3; Legacy Format = True; ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQLite.ExecuteQuery("INSERT INTO t_user (Name, Lastname) VALUES (@name, @Lastname)", new string[] { "name1", "lastname1" });
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MsSql.ExecuteQuery("INSERT INTO t_user (Name, Lastname) VALUES (@name, @Lastname)", new string[] { "name1", "lastname1" });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = MsSql.Table("SELECT u.cn, u.Name, u.Lastname FROM  dbo.t_user AS u WHERE cn = @cn",new string[] {"1" });
            foreach(DataRow dr in dt.Rows)
            {
                MessageBox.Show(dr["Name"].ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = SQLite.Table("SELECT Name FROM t_user");
            foreach (DataRow dr in dt.Rows)
            {
                MessageBox.Show(dr["Name"].ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (string s in MsSql.getColumnsName("t_user"))
            {
                MessageBox.Show(s);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
       
            foreach(string s in SQLite.getColumnsName("t_user"))
            {
                MessageBox.Show(s);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "TableNameHere 1";
            string last = str.Substring((str.Length - 1), 1);
            MessageBox.Show(last);
        }
    }
}

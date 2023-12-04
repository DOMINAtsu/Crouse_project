using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crouse_project_
{
    internal class MainClass
    {

        public static readonly string con_string = "Data Source=PROSTOVLAD-PC; Initial Catalog=RM;Persist Security Info=True; User ID=sa; Password=123;";

        public static SqlConnection con = new SqlConnection(con_string);



        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = @"Select * from user where username ='" + user + "' and upass = '" + pass + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
            }

            return isValid;
        }

    }
}

using SearchableDropdown.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace CRUD_ADO.NET.Services
{
    public class UserDAL
    {
        private readonly string _connectionString;
        public int AccountClassId { get; private set; }
        public string? AccountTitle { get; private set; }
        public string? AccountClassTitle { get; private set; }

        public UserDAL(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Drop> ShowList()
        {
            List<Drop> users = new List<Drop>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Sp_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    users.Add(new Drop
                    {
                        AccountClassId = Convert.ToInt32(dr["AccountClassId"]),
                        AccountClassTitle = dr["AccountClassTitle"].ToString(),
                    });
                }
                return users;
            }
        }
        public void AddName(string newName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AccountClass", con))
                {
                    cmd.Parameters.AddWithValue("@Name", newName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

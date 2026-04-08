using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Lab02
{
    internal class DbHelper
    {
        public static bool DepartmentExists(SqlConnection connection, int deptId)
        {
            string query = "SELECT COUNT(*) FROM Department WHERE Dept_Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", deptId);

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
    }
}

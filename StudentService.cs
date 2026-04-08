using Labs.Lab02.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Lab02
{
    internal class StudentService
    {
        public static void ShowAllStudents(SqlConnection connection)
        {
            string GetAllStudents = @"select S.St_Name, S.St_Age, S.St_address, D.Dept_Name
                                    from Student S inner join Department D
                                    on D.Dept_id = S.Dept_id";

            using (SqlCommand GetAllStudentsCommand = new SqlCommand(GetAllStudents, connection))
            {

                var reader = GetAllStudentsCommand.ExecuteReader();

                Console.WriteLine("\nName        | Age  | Address  | Department Name");
                Console.WriteLine("---------------------------------------------");

                while (reader.Read())
                {
                    string St_Name = reader.GetString(0);
                    int St_Age = reader.GetInt32(1);
                    string St_Address = reader.GetString(2);
                    string Dept_Name = reader.GetString(3);

                    Console.WriteLine($"{St_Name,-11} | {St_Age,-4} | {St_Address,-8} | {Dept_Name}");
                }
                reader.Close();
                Console.WriteLine("");
            }
        }

        public static List<Student> ManageStudents(SqlConnection connection)
        {
            string GetAllProducts = @"select * from Student";

            using (SqlCommand GetAllProductsCommand = new SqlCommand(GetAllProducts, connection))
            {

                var reader = GetAllProductsCommand.ExecuteReader();

                List<Student> students = new List<Student>();

                while (reader.Read())
                {
                    int St_Id = reader.GetInt32(0);
                    string St_Name = reader.GetString(1);
                    int St_Age = reader.GetInt32(2);
                    string St_Address = reader.GetString(3);
                    int Dept_Id = reader.GetInt32(4);

                    students.Add(new Student(St_Id, St_Name, St_Age, St_Address, Dept_Id));
                }
                reader.Close();
                return students;

            }

        }


        public static int InsertStudent(SqlConnection connection, StudentWithOutId s)
        {
            string query = @"insert into Student (St_Name, St_Age, St_Address, Dept_Id)
                                 Values(@St_Name, @St_Age, @St_Address, @Dept_Id)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@St_Name", s.St_Name);
                command.Parameters.AddWithValue("@St_Age", s.St_Age);
                command.Parameters.AddWithValue("@St_Address", s.St_Address);
                command.Parameters.AddWithValue("@Dept_Id", s.Dept_Id);


                var rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;

            }

        }

        public static int DeleteStudent(SqlConnection connection, int id)
        {
            string query = @"Delete from Student where St_Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@id", id);


                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected;
            }

        }

        public static int UpdateStudent(SqlConnection connection, Student s)
        {
            string query = @"UPDATE Student
                            SET St_Name = @name, St_Age = @age, St_address = @address, Dept_Id = @did
                            where St_Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", s.St_Id);
                command.Parameters.AddWithValue("@name", s.St_Name);
                command.Parameters.AddWithValue("@age", s.St_Age);
                command.Parameters.AddWithValue("@address", s.St_Address);
                command.Parameters.AddWithValue("@did", s.Dept_Id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;


            }


        }

    }
}

using Labs.Lab02.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace Labs.Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Database=company;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            Console.WriteLine("\n===== Student Management System (ADO.NET Project) =====\n");

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("1. Show Students");
                Console.WriteLine("2. Manage Students");
                Console.WriteLine("3. Insert Student");
                Console.WriteLine("4. Exit");


                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input, try again:");
                }

                switch (choice)
                {
                    case 1:
                        StudentService.ShowAllStudents(connection);
                        break;

                    case 2:
                        List<Student> students = StudentService.ManageStudents(connection);
                        Console.WriteLine("\nID | Name         | Age | Address    | DeptId");
                        Console.WriteLine("--------------------------------------------");

                        foreach (var student in students)
                        {
                            Console.WriteLine($"{student.St_Id,-2} | {student.St_Name,-12} | {student.St_Age,-3} | {student.St_Address,-10} | {student.Dept_Id}");
                        }

                        Console.Write("\nEnter Student ID: ");

                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Invalid input, Please try again");
                            Console.Write("Enter Student ID: ");

                        }

                        var selectedStudent = students.FirstOrDefault(s => s.St_Id == id);


                        if (selectedStudent == null)
                        {
                            Console.WriteLine("Student not found \n");
                        }else
                        {
                            Console.WriteLine("1. Update Student");
                            Console.WriteLine("2. Delete Student");
                            Console.WriteLine("3. Return to main menue");


                            int option;
                            while (!int.TryParse(Console.ReadLine(), out option))
                            {
                                Console.WriteLine("Invalid input, try again:");
                            }

                            switch (option)
                            {
                                case 1:
                                    // Name
                                    Console.WriteLine("Enter New Name or press 'Enter' to keep current value:");
                                    string newName = Console.ReadLine();

                                    if (string.IsNullOrEmpty(newName))
                                        newName = selectedStudent.St_Name;


                                    // Age
                                    Console.WriteLine("Enter New Age or press 'Enter' to keep current value:");
                                    string input = Console.ReadLine();

                                    int newAge;

                                    if (string.IsNullOrEmpty(input))
                                    {
                                        newAge = selectedStudent.St_Age;
                                    }
                                    else
                                    {
                                        while (!int.TryParse(input, out newAge) || newAge < 20 || newAge > 30)
                                        {
                                            Console.WriteLine("Invalid Age, should be between 20 and 30:");
                                            input = Console.ReadLine();
                                        }
                                    }


                                    // Address
                                    Console.WriteLine("Enter New Address or press 'Enter' to keep current value:");
                                    string newAddress = Console.ReadLine();

                                    if (string.IsNullOrEmpty(newAddress))
                                        newAddress = selectedStudent.St_Address;


                                    // Department Id
                                    Console.WriteLine("Enter New Department Id or press 'Enter' to keep current value:");
                                    string dept = Console.ReadLine();

                                    int newDeptID;

                                    if (string.IsNullOrEmpty(dept))
                                    {
                                        newDeptID = selectedStudent.Dept_Id;
                                    }
                                    else
                                    {
                                        while (!int.TryParse(dept, out newDeptID) || !DbHelper.DepartmentExists(connection, newDeptID))
                                        {
                                            Console.WriteLine("Invalid Department Id, please try again:");
                                            dept = Console.ReadLine();
                                        }
                                    }

                                    int updRowsAffected = StudentService.UpdateStudent(connection, new Student(id, newName, newAge, newAddress, newDeptID));
                                    if (updRowsAffected > 0)
                                        Console.WriteLine("Student updated successfully\n");
                                    else
                                        Console.WriteLine("Update failed");
                                    break;

                                case 2:
                                    Console.Write("Are you sure? (y/n): ");
                                    string confirm = Console.ReadLine();
                                    
                                    if (confirm.ToLower() == "y")
                                    {
                                        int delRowsAffected = StudentService.DeleteStudent(connection, id);
                                        if (delRowsAffected > 0)
                                            Console.WriteLine($"Student With ID:{id} Deleted successfully\n");
                                        else
                                            Console.WriteLine("Deletion failed");
                                    }
                                    
                                    break;
                                case 3:
                                    Console.WriteLine("");
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        } 

                        break;

                    case 3:
                        // Name
                        Console.Write("Enter Student Name: ");
                        string name = Console.ReadLine();

                        // Age
                        Console.Write("Enter Student Age: ");
                        int age;
                        string ageInput = Console.ReadLine();

                        while (!int.TryParse(ageInput, out age) || age < 20 || age > 30)
                        {
                            Console.WriteLine("Invalid Age, should be between 20 and 30:");
                            ageInput = Console.ReadLine();
                        }

                        // Address
                        Console.Write("Enter Student Address: ");
                        string address = Console.ReadLine();


                        // Department
                        Console.Write("Enter Department Id: ");
                        string deptInput = Console.ReadLine();
                        int deptID;

                        while (!int.TryParse(deptInput, out deptID) ||
                               !DbHelper.DepartmentExists(connection, deptID))
                        {
                            Console.WriteLine("Invalid Department Id, please try again:");
                            deptInput = Console.ReadLine();
                        }

                        int insRowsAffected = StudentService.InsertStudent(connection, new StudentWithOutId(name, age, address, deptID));
                        if (insRowsAffected > 0)
                        {
                            Console.WriteLine("Student inserted Succefully\n");

                        }
                        else
                            Console.WriteLine("Insertion Faild \n");
                        break;

                    case 4:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, Please try again\n");
                        break;
                }
            }

            connection.Close();
        }

        
        
    }
}

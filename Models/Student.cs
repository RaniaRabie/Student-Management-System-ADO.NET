using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Lab02.Models
{
    internal class Student
    {
        

        public int St_Id { get; set; }
        public string St_Name { get; set; }
        public int St_Age { get; set; }
        public string St_Address { get; set; }
        public int Dept_Id { get; set; }

        public Student(int st_Id, string st_Name, int st_Age, string st_Address, int dept_Id)
        {
            St_Id = st_Id;
            St_Name = st_Name;
            St_Age = st_Age;
            St_Address = st_Address;
            Dept_Id = dept_Id;
        }
    }
}

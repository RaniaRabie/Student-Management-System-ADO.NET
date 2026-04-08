using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Lab02
{
    internal class Department
    {

        public int Dept_Id { get; set; }
        public string Dept_Name { get; set; }

        public Department(int dept_Id, string dept_Name)
        {
            Dept_Id = dept_Id;
            Dept_Name = dept_Name;
        }
    }
}

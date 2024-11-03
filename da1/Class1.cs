using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da1
{
    // step 1 create an ORM
    public class Student
    {
        public int SID { get; set; }
        public string SNAME { get; set; }
        public int DEPT { get; set; }

    }

    public class cdal
    {
        private readonly string cnnStr;
        SqlConnection cnn;
        SqlCommand cmd;

        public cdal(string cnnStr)
        {
            this.cnnStr = cnnStr;
            this.cnn = new SqlConnection(cnnStr);
            this.cmd = new SqlCommand();
            cmd.Connection = this.cnn;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> lst = new List<Student>();
            cmd.CommandText = "Select * from Student";
            cnn.Open();
            SqlDataReader reader = this.cmd.ExecuteReader();
            while (reader.Read())
            {
                Student std = new Student
                {
                    SID = (int)reader[0],
                    SNAME = reader[1].ToString(),
                    DEPT = (int)reader[2]
                };
                lst.Add(std);
            }
            cnn.Close();
            return lst;
        }

        public Student GetStudentById(int id)
        {
            Student stu = null;
            cmd.CommandText = $"select * from Student where SID = {id}";
            cnn.Open();
            SqlDataReader reader = this.cmd.ExecuteReader();
            if (reader.Read())
            {
                stu = new Student
                {
                    SID = (int)reader[0],
                    SNAME = reader[1].ToString(),
                    DEPT = (int)reader[2]
                };
            }
            cnn.Close();
            return stu;
        }


        public bool ModifyStudent(Student stu)
        {
            cmd.CommandText = $"Update Student set SNAME='{stu.SNAME}' , DEPT={stu.DEPT} where SID = {stu.SID} ";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }

        public bool DeleteStudent(int id)
        {
            cmd.CommandText = $"delete Student where SID = {id}";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }


        public bool AddStudent(Student stu)
        {
            cmd.CommandText = $"insert into Student values({stu.SID},'{stu.SNAME}' , {stu.DEPT})";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }
    }
}

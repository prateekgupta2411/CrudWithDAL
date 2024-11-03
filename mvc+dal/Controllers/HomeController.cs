using da1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_dal.Controllers
{
    public class HomeController : Controller
    {
        cdal dal = new cdal(@"Data Source = PRATEEKGUPTA\SQLEXPRESS; Initial Catalog = Prateek; Integrated Security = true; TrustServerCertificate=True;");
        public ActionResult Index()
        {
            var Student = dal.GetAllStudents();
            return View(Student);
        }

        public ActionResult Details(int id)
        {
            var Student = dal.GetStudentById(id);
            return View(Student);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Student stu)
        {
            dal.AddStudent(stu);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Student = dal.GetStudentById(id);
            return View(Student);
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            dal.ModifyStudent(student);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            dal.DeleteStudent(id);
            return RedirectToAction("Index");
        }

    }

}

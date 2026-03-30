using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MIT_AT2_Portal.Models;

namespace MIT_AT2_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDataContext _db;

        public HomeController(StudentDataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        //List<Student> uniList = _db.student.ToList<Student>();

 //return View(uniList);
    
            
           
     
        //Uni List
       
        
       

       


        [HttpGet]
        public IActionResult create()
        {
            Student s = new Student();
            s.dateOfBirth = DateTime.Now;
            FillArray(s);
            FillQualificationArray(s);
            return View(s);
        }

        [HttpPost]
        public IActionResult create(Student s)
        {
            if (!ModelState.IsValid) { FillArray(s); FillQualificationArray(s); return View(s); }

            try
            {
                _db.student.Add(s);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ContentResult { Content = ex.Message };
            }
            return RedirectToAction("Display");
        }
        [HttpGet]
        public IActionResult edit(long id)
        {

            Student s = _db.student.Find(id);
            FillQualificationArray(s);
            FillArray(s);
            return View(s);
        }
        
        public IActionResult edit(Student s)
        {
            if (!ModelState.IsValid)
            {
                FillQualificationArray(s);
                FillArray(s);
                return View(s);
            }
            _db.student.Update(s);
            _db.SaveChanges();
            return RedirectToAction("Display");
        }
        
        public IActionResult delete(long id)
        {
            Student s = _db.student.Find(id);
            _db.student.Remove(s);
            _db.SaveChanges();
            return RedirectToAction("Display");
        }


        //public IActionResult display(long id,string search)
        //{
        //    string searchTerm = search ?? "";
        //    List<Student> studentList;

        //    if (string.IsNullOrEmpty(search))
        //    {
        //        studentList = _db.student.ToList();
        //    }
        //    else
        //    {
        //        studentList = _db.student.Where(e=> e.fullName != null && e.fullName.Contains(search)).ToList();
        //    }


        //        return View(studentList);

        //}
        public IActionResult display(long id, string search,string sortOrder)
        {
            List<Student> studentList;
            //list search

            if (string.IsNullOrEmpty(search))
            {
                studentList = _db.student.ToList();
            }
            else
            {
                studentList = _db.student.Where(e => e.fullName.Contains(search) || e.qualification.Contains(search) || e.university.Contains(search)).ToList();
            }
            
            switch (sortOrder)
            {
                case "Gpa_desc":
                    studentList = studentList.OrderByDescending(e => e.gpa).ToList();
                    break;
                case "Gpa_asc":
                    studentList = studentList.OrderBy(e => e.gpa).ToList();
                    break;
                default:
                    studentList = studentList.OrderByDescending(e => e.gpa).ToList();
                    break;


            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SearchString = search;
            return View(studentList);



            //List<Student> uniList = _db.student.ToList<Student>();
            //return View(uniList);

        }
        //public IActionResult display()
        //{
        //    List<Student> qualificationList = _db.student.ToList<Student>();
        //    return View(qualificationList);
        //}
        private void FillArray(Student pref)
        {
            SelectListItem obj = new SelectListItem();
            obj.Text = "Monash University";
            //obj.Value = "MO";
            pref.uniList.Add(obj);

            obj = new SelectListItem();
            obj.Text = "RMIT University";
            //obj.Value = "VU";
            pref.uniList.Add(obj);

            obj = new SelectListItem();
            obj.Text = "Melbourne University";
            pref.uniList.Add(obj);

            obj = new SelectListItem();
            obj.Text = "Swinburne University";
            pref.uniList.Add(obj);

            obj = new SelectListItem();
            obj.Text = "Deakin University";
            pref.uniList.Add(obj);



        }

        private void FillQualificationArray(Student choice)
        {
            SelectListItem obj = new SelectListItem();
            //obj.Value = "DS";
            obj.Text = "Masters of Data Science";

            choice.qualificationList.Add(obj);

            obj = new SelectListItem();
            //obj.Value = "AI";
            obj.Text = "Masters of Artificial Intelligence";

            choice.qualificationList.Add(obj);

            obj = new SelectListItem();
            //obj.Value = "IT";
            obj.Text = "Masters of Information Technology";

            choice.qualificationList.Add(obj);

        }


    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using pass.Models;
namespace pass.Controllers
{
    public class HomeController : Controller
    {
        private passContext _context;
        public HomeController(passContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.errors = ModelState.Values;
            ViewBag.success = TempData["success"];
            ViewBag.fail = TempData["fail"];
            return View("index");
        }
        [HttpPost]
        [Route("addStudent")]
        public IActionResult addStudent (Student student)
        {
            if(TryValidateModel(student) == false)
            {
                ViewBag.errors = ModelState.Values;
                return View("index");
            }
            else
            {
                _context.students.Add(student);
                _context.SaveChanges();
                TempData["success"] = "User has been created.";
                return RedirectToAction("index");

            }
        }
        [HttpPost]
        [Route("loginStudent")]
        public IActionResult loginStudent (string email, string password)
        {
            // int? sesh = HttpContext.Session.GetInt32("userid");
            Student aStudent = _context.students.SingleOrDefault(p => p.email == email);
            if (email == null || password == null)
            {
                TempData["fail"] = "Email or Password is invalid";
                return RedirectToAction("index");
                
            }
            else if (email != aStudent.email || password != aStudent.password)
            {
                TempData["fail"] = "Email or Password is invalid";
                return RedirectToAction("index");
            }
            else{
                // User aUser = _context.Users.SingleOrDefault(p => p.username == username);
                HttpContext.Session.SetString(key: "email", value: aStudent.email);
                HttpContext.Session.SetInt32(key: "studentid", value: aStudent.studentid);
                return RedirectToAction("dashboard");
            }
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? sesh = HttpContext.Session.GetInt32("studentid");
            int sesh2 = sesh ?? default(int);
            Student student = _context.students.SingleOrDefault(w => w.studentid == sesh2);
            ViewBag.student = student;
            List<Belt> allBelts = _context.belts.Where(b => b.beltid > 0).Include(w => w.student).ToList();
            ViewBag.allBelts = allBelts;

            List<Success> success = _context.success.Include(w => w.student).Where(r => r.studentid == sesh2).Include(e => e.belt).ToList();
            if(success == null)
            {
                ViewBag.success = "You have none.";
            }else{
                ViewBag.success = student.passedbelt;
            }
    
            return View("dashboard");
        }
        [HttpGet]
        [Route("create/{studentid}")]
        public IActionResult Create(int studentid)
        {
            return View("create");
        }
        [HttpPost]
        [Route("belt")]
        public IActionResult Belt (Belt belt)
        {
            int? sesh = HttpContext.Session.GetInt32("studentid");
            int sesh2 = sesh ?? default(int);
            Belt abelt = new Belt()
            {
                color = belt.color,
                description = belt.description,
                studentid = sesh2,
            };
            _context.belts.Add(abelt);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("add/{beltid}")]
        public IActionResult Add (int beltid)
        {
            Belt belt = _context.belts.SingleOrDefault(b => b.beltid == beltid);
            ViewBag.belt = belt;
            
            return View("add");
        }
        [HttpPost]
        [Route("adding/{beltid}")]
        public IActionResult Adding (int beltid, DateTime date, string description)
        {
            int? sesh = HttpContext.Session.GetInt32("studentid");
            int sesh2 = sesh ?? default(int);
            Success aSuccess = new Success()
            {
                studentid = sesh2,
                beltid = beltid,
                date = date,
                description = description,
            };
            _context.success.Add(aSuccess);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("show/{beltid}")]
        public IActionResult Show(int beltid)
        {
            Belt belt = _context.belts.SingleOrDefault(e => e.beltid == beltid);
            ViewBag.belt = belt;
            return View("show");
        }
        [HttpGet]
        [Route("showe/{successid}")]
        public IActionResult Showe(int successid)
        {
            List<Success> bad = _context.success.Include(b=> b.belt).ToList();
            Success success = _context.success.SingleOrDefault(w => w.successid == successid);
            ViewBag.nice = success;
            
            return View("showe");
        }
        [HttpGet]
        [Route("update/{successid}")]
        public IActionResult Update(int successid)
        {
            List<Success> aSuccess = _context.success.Include(w => w.belt).ToList();
            Success success = _context.success.SingleOrDefault(w => w.successid == successid);
            // int? sesh = HttpContext.Session.GetInt32("studentid");
            // int sesh2 = sesh ?? default(int);
            // Success =
            ViewBag.success = success;
            return View("update");
        }
        [HttpPost]
        [Route("updateinfo/{successid}")]
        public IActionResult UpdateInfo(int successid, string description)
        {
            // List<Success> aSuccess = _context.success.Include(w => w.belt).ToList();
            Success success = _context.success.SingleOrDefault(w => w.successid == successid);
            success.description = description;
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }
}

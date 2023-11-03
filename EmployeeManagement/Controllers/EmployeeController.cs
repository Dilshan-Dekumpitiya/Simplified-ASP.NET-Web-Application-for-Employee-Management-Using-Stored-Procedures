using EmployeeManagement.DAL_Data_Access_Layer_;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }
                bool result = _dal.Insert(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }

                TempData["successMessage"] = "Employee details saved";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        //Search method (get by id)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id : {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        //edit it (save it --> search id got employee)
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
                bool result = _dal.Update(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to update the data";
                    return View();
                }
                    TempData["successMessage"] = "Employee details updated";
                    return RedirectToAction("Index");
                }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}

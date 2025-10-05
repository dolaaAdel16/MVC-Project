using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;   

        // ASK CLR Create object form DepartmentRepository
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet] //GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreateDepartmentDTO model )
        {
           if(ModelState.IsValid) // Server Side Validation 
           {
                var department = new Department()
                {
                    Code = model.Code ,
                    Name = model.Name ,
                    CreateAt = model.CreateAt
                };
               　_unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();  
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
           }

            return View(model);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details"  )
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id "); // 400

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id ,UpdateDepartmentDTO model)
        {
            if (ModelState.IsValid)
            {
               
                {
                    var department = new Department()
                    {
                        Id = id,
                        Code = model.Code,
                        Name = model.Name,
                        CreateAt = model.CreateAt
                    };
                    _unitOfWork.DepartmentRepository.Update(department);
                    var count =_unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
               
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
        //    if (id is null) return BadRequest("Invalid Id "); // 400

        //    var department = _departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return Details(id , "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();

                
                    
                _unitOfWork.DepartmentRepository.Delete(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(department);

        }
        }
}

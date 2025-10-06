using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]
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
        public async Task <IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateDepartmentDTO model )
        {
           if(ModelState.IsValid) // Server Side Validation 
           {
                var department = new Department()
                {
                    Code = model.Code ,
                    Name = model.Name ,
                    CreateAt = model.CreateAt
                };
               　await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.Complete();  
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
           }

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id, string viewName = "Details"  )
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            var dto = new CreateDepartmentDTO()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt
            };

            return View(dto);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id ,UpdateDepartmentDTO model)
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
                    var count = await _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
               
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            var dto = new CreateDepartmentDTO()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();

                
                    
                _unitOfWork.DepartmentRepository.Delete(department);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(department);

        }
        }
}

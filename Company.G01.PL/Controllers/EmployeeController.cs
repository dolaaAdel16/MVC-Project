using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
using Company.G01.PL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeerepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        // ASK CLR Create object form DepartmentRepository
        public EmployeeController(
            //IEmployeeRepository employeeRepository ,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            // _employeerepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet] //GET: /Employee/Index
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            
            // Dictionary : Key , Value
            // 1.ViewData : Transfer Extra info from Controller (Action) to view
            //ViewData["Message"] = "Hello from ViewData";

            // 2.ViewBag  : Transfer Extra info from Controller (Action) to view
            //ViewBag.Message = "Hello from ViewBag";

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;

            return View(departments);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid) // Server Side Validation 
            {
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }

                    var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                    //_unitOfWork.EmployeeRepository.Update(employee);
                    //_unitOfWork.EmployeeRepository.Delete(employee);
                    var count = await _unitOfWork.Complete();
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }
               
            }

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult>  Edit(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with id :{id}  is not found" });

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;
            if (id is null) return BadRequest("Invalid Id "); // 400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            var dto = _mapper.Map<CreateEmployeeDTO>(employee);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit([FromRoute] int id, CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {

                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }



                //if(id != model.Id) return BadRequest();
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                    { 
                        return RedirectToAction(nameof(Index));
                    }
                

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //    if (id is null) return BadRequest("Invalid Id "); // 400

            //    var department = _departmentRepository.Get(id.Value);
            //    if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return await Edit(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
               

                var employee = _mapper.Map<Employee>(model);  
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    if (model.Image is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
    }
}

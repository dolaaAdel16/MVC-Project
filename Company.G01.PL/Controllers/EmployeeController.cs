using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
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
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                 employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }
            
            // Dictionary : Key , Value
            // 1.ViewData : Transfer Extra info from Controller (Action) to view
            //ViewData["Message"] = "Hello from ViewData";

            // 2.ViewBag  : Transfer Extra info from Controller (Action) to view
            //ViewBag.Message = "Hello from ViewBag";

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["Departments"] = departments;

            return View(departments);
        }

        [HttpPost]

        public IActionResult Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid) // Server Side Validation 
            {

                    var employee = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRepository.Add(employee);
                    //_unitOfWork.EmployeeRepository.Update(employee);
                    //_unitOfWork.EmployeeRepository.Delete(employee);
                    var count = _unitOfWork.Complete();
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                }

            return View(model);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with id :{id}  is not found" });

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["Departments"] = departments;
            if (id is null) return BadRequest("Invalid Id "); // 400

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            var dto = _mapper.Map<CreateEmployeeDTO>(employee);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {

                //if(id != model.Id) return BadRequest();
                var employee = new Employee()
                {
                    Id=id,
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt,
                    Age = model.Age,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted
                };

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
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

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {



                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);

        }
    }
}

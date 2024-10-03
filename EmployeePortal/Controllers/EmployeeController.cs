using EmployeePortal.Data;
using EmployeePortal.Models.DTO_s;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ResponseDTO _responseDTO;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost]
        public IActionResult AddNewEmployee(EmployeeDTO newEmployeeDetails)
        {
            var employee = new Employee()
            {
                Name = newEmployeeDetails.Name,
                Email = newEmployeeDetails.Email,
                Phone = newEmployeeDetails.Phone,
                Salary = newEmployeeDetails.Salary,
            };
            _dbContext.Add(employee);
            _dbContext.SaveChanges();

            _responseDTO.Message = $"Employee '{employee.Name}' has been created successfully";
            _responseDTO.Result = employee;

            return Ok(_responseDTO);
        }
        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            try
            {

                var allEmployees = _dbContext.Employees.ToList();
                if (allEmployees.Count == 0)
                {
                    _responseDTO.Message = "No Employees Found, Add New Employees";
                    _responseDTO.Result = allEmployees;
                    return Ok(_responseDTO);
                }

                return Ok(allEmployees);
            }
            catch (Exception ex)
            {

                _responseDTO.Message = ex.Message;

                return BadRequest(_responseDTO);
            }



        }
        [HttpGet]
        [Route("SingleEmployee/{id:guid}")]
        public ActionResult GetEmployeeById(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);

        }
        [HttpPut]
        [Route("UpdateEmployee/{id:guid}")]
        public IActionResult UpdateEmployeeDetails(Guid id, EmployeeDTO updatedEmployeeDetails)
        {

            var employee = _dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updatedEmployeeDetails.Name;
            employee.Email = updatedEmployeeDetails.Email;
            employee.Phone = updatedEmployeeDetails.Phone;
            employee.Salary = updatedEmployeeDetails.Salary;
            _dbContext.SaveChanges();

            _responseDTO.Message = $"'{employee.Id}' HAS BEEN UPDATED SUCCESFULLY";
            _responseDTO.Result = employee;
            return Ok(_responseDTO);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public ActionResult DeleteEmployee(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            _responseDTO.Message = $" The Entity '{employee.Id}' has been deleted";
            
            return Ok(_responseDTO);
        }

        


    }
    
}

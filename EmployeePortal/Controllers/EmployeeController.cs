using EmployeePortal.Data;
using EmployeePortal.Models.DTO_s;
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
        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            //var _responseDTO = new ResponseDTO();
            var allEmployees = _dbContext.Employees.ToList();
            if (allEmployees.Count == 0)
            {
                _responseDTO.Message = "No Employees Found";
                _responseDTO.Result = allEmployees;
                return NotFound(_responseDTO);
            }
            
            return Ok(allEmployees);

            
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using OData_Practice.Models;
using OData_Practice.Models.Repos;

namespace OData_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly 
            ICompanyRepo _companyRepo;

        public CompaniesController(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [EnableQuery(PageSize =3)]
        [HttpGet]
        public IQueryable<Company> Get() => _companyRepo.GetAll();

        [EnableQuery]
        [HttpGet]
        public SingleResult<Company> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_companyRepo.GetCompanyById(key));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _companyRepo.Create(company);
            return Created("companies", company);
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] int key, [FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != company.ID)
            {
                return BadRequest();
            }
            _companyRepo.Update(company);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            var company = _companyRepo.GetCompanyById(key);
            if (company is null)
            {
                return BadRequest();
            }
            _companyRepo.Delete(company.First());
            return NoContent();
        }
    }
}

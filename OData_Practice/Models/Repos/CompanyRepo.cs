
using Microsoft.EntityFrameworkCore;

namespace OData_Practice.Models.Repos
{
    public class CompanyRepo : ICompanyRepo
    {
        public readonly ApiContext _context;
        public CompanyRepo(ApiContext context)
        {
            this._context = context;
        }


        public void Create(Company company)
        {

            _context.Companies
                 .Add(company);
            _context.SaveChanges();
        }

        public void Delete(Company company)
        {
            _context.Companies
                .Remove(company);
            _context.SaveChanges();
        }

        public IQueryable<Company> GetAll()
        {
            return _context.Companies
                     .Include(a => a.Products)
                    .AsQueryable();
        }

        public IQueryable<Company> GetCompanyById(int id)
        {
            return _context.Companies.Include(s => s.Products).AsQueryable().Where(s => s.ID.Equals(id));
        }

        public void Update(Company company)
        {
            _context.Companies
                 .Update(company);
            _context.SaveChanges();
        }
    }
}

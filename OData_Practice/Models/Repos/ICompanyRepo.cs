namespace OData_Practice.Models.Repos
{
    public interface ICompanyRepo
    {
        public IQueryable<Company> GetAll();

        public IQueryable<Company> GetCompanyById(int id);
        public void Create(Company company);
        public void Update(Company company);
        public void Delete(Company company);
    }
}

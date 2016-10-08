using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class CityRepository : EFRepository<City>, ICityRepository
    {
        public CityRepository(DbContext context) : base(context) { }

        public City GetByName(string name)
        {
            return DbSet.Include(c => c.Country).Where(p => p.Name == name).FirstOrDefault();
        }

        public override City GetById(int id)
        {
            return DbSet.Include(c => c.Country).First(c => c.Id == id);
        }

        public override IQueryable<City> GetAll()
        {
            return DbSet.Include(c => c.Country);
        }
    }
}

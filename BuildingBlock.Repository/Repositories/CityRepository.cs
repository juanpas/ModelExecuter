using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class CityRepository : EFRepository<City>, ICityRepository
    {
        public CityRepository(DbContext context) : base(context) { }

        public City GetByName(string name)
        {
            return DbSet.Include(c => c.Country).Where(p => p.Name == name).FirstOrDefault();
        }

        public City GetByIdWithProductSellers(int id)
        {
            return DbSet.Include(c => c.Country).Include(c => c.ProductSellers).Where(c => c.Id == id).First();
        }

        public IQueryable<City> GetAllWithProductSellers()
        {
            return DbSet.Include(c => c.Country).Include(c => c.ProductSellers);
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

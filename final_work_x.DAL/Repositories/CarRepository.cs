using final_work_x.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace final_work_x.DAL.Repositories
{
    public class CarRepository : GenericRepository<CarEntity>
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context) : base(context) { _context = context; }

        public IQueryable<CarEntity> Cars => GetAll();
    }
}

using final_work_x.DAL.Entities;

namespace final_work_x.DAL.Repositories
{
    public class ManufactureRepository : GenericRepository<ManufactureEntity>
    {
        private readonly AppDbContext _context;

        public ManufactureRepository(AppDbContext context) : base(context) { _context = context; }

        public IQueryable<ManufactureEntity> Manufactures => GetAll();
    }
}

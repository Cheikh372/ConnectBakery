
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ConnectBakery.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ConnectBakeryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ConnectBakeryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        #region methods without async
        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(T entity,T oldEntity)
        {
            //_dbSet.Attach(entity);

            _context.Entry(oldEntity).State = EntityState.Detached;
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion methods without async


        #region methods with async
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await  _dbSet.FindAsync(id);
        }

        public async  Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

       

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        //public async Task UpdateAsync(T entity)
        //{
        //    _dbSet.Attach(entity);
        //    _context.Entry(entity).State = EntityState.Modified;
        //}

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion methods with async
    }
}

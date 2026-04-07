using HealthcareSystem.API.Data;
using HealthcareSystem.API.Repositories.Interfaces;
using HealthcareSystem.Models.DTOs;
using HealthcareSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.API.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _dbSet.FindAsync(id) != null;
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<bool> EmailExistsAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email.ToLower());

    public async Task<PagedResult<User>> GetPagedAsync(QueryParameters parameters)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(u => u.FullName.Contains(parameters.Search) ||
                                     u.Email.Contains(parameters.Search));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(u => u.CreatedAt)
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<User>
        {
            Items = items, TotalCount = total,
            Page = parameters.Page, PageSize = parameters.PageSize
        };
    }
}

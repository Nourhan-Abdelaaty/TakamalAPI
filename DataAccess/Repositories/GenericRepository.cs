using DataAccess.Context;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;

namespace DataAccess.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected DataContext _context;
    private readonly DbSet<T> Table;

    public GenericRepository(DataContext context)
    {
        _context = context;
        Table = _context.Set<T>();
    }
    /////////////////////////////
    public T FindById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    public bool Add(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        entity.IsActive = true;
        entity.CreatedDate = DateTime.Now;
        Table.Add(entity);
        return true;
    }
    public bool AddRange(IEnumerable<T> entity)
    {
        if (entity.Count() == 0)
        {
            throw new NullReferenceException();
        }
        foreach (var item in entity)
        {
            item.IsActive = true;
            item.CreatedDate = DateTime.Now;
        }
        Table.AddRange(entity);
        return true;
    }
    public async Task<ResponseResult> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        var model = await FindByIdAsync(entity.Id);
        if (model == null)
        {
            return new ResponseResult { IsSuccess = false, Message = "NotFound" };
        }
        Table.Update(entity);
        return new ResponseResult { IsSuccess = true, Message = "Update Success", Obj = model };
    }
    public bool Remove(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        Table.Remove(entity);
        return true;
    }
    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return Table.Where(e => e.IsActive == true).Where(expression);
    }
    public List<T> ToList()
    {
        return Table.Where(e => e.IsActive == true).ToList();
    }
    public T FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        if (expression != null)
            return Table.Where(x => x.IsActive == true).FirstOrDefault(expression);
        else
            return Table.Where(x => x.IsActive == true).FirstOrDefault();
    }
    public T LastOrDefault(Expression<Func<T, bool>> expression)
    {
        if (expression != null)
            return Table.Where(x => x.IsActive == true).OrderBy(x => x.Id).LastOrDefault(expression);
        else
            return Table.Where(x => x.IsActive == true).OrderBy(x => x.Id).LastOrDefault();
    }
    public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        IIncludableQueryable<T, object> query = null;
        foreach (var include in includes)
            query = Table.Include(include);
        return query.Where(x => x.IsActive == true);
    }
    public IQueryable<TType> Select<TType>(Expression<Func<T, TType>> select)
    {
        return Table.Where(e => e.IsActive == true).Select(select);
    }
    public bool Any(Expression<Func<T, bool>> expression = null)
    {
        if (expression != null)
            return Table.Where(x => x.IsActive == true).Any(expression);
        else
            return Table.Where(x => x.IsActive == true).Any();
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
    {
        if (expression != null)
            return await Table.Where(x => x.IsActive == true).AnyAsync(expression);
        else
            return await Table.Where(x => x.IsActive == true).AnyAsync();
    }
    public double Sum(Expression<Func<T, double>> expression)
    {
        return Table.Where(x => x.IsActive == true).Sum(expression);
    }
    public IQueryable<T> Take(int count)
    {
        return Table.Where(e => e.IsActive == true).Take(count);
    }
    public IQueryable<T> Skip(int count)
    {
        return Table.Where(e => e.IsActive == true).Skip(count);

    }
    public int Count(Expression<Func<T, bool>> expression = null)
    {
        return Table.Where(e => e.IsActive == true).Count(expression);
    }
    public int Max(Expression<Func<T, int>> expression)
    {
        try
        {
            return Table.Where(e => e.IsActive == true).Max(expression);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public IQueryable<T> OrderBy(Expression<Func<T, int>> expression)
    {
        return Table.Where(e => e.IsActive == true).OrderBy(expression);
    }
    public async Task<List<T>> ToListAsync()
    {
        var list = await Table.Where(e => e.IsActive == true).ToListAsync();
        return list;
    }
    public async Task<T> FindByIdAsync(int id)
    {
        var model = await Table.FindAsync(id);
        if (model != null)
        {
            if (!model.IsActive)
                return null;
        }

        return model;
    }
    public async Task<bool> AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }
        entity.IsActive = true;
        entity.CreatedDate = DateTime.Now;
        await Table.AddAsync(entity);
        return true;
    }
    public async Task<bool> AddRangeAsync(ICollection<T> entity)
    {
        if (entity.Count() == 0)
        {
            throw new NullReferenceException();
        }
        foreach (var item in entity)
        {
            item.IsActive = true;
            item.CreatedDate = DateTime.Now;
        }
        await Table.AddRangeAsync(entity);
        return true;
    }
    public async Task<ResponseResult> DeleteAsync(int id)
    {
        var model = await FindByIdAsync(id);
        if (model == null)
        {
            return new ResponseResult { IsSuccess = false, Message = "NotFound" };
        }
       Remove(model);
        return new ResponseResult { IsSuccess = true, Message = "Delete Success" };
    }
    public bool DeleteRange(List<T> entity)
    {
        try
        {
            Table.RemoveRange(entity);
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }
    }
    public IQueryable<T> OrderByDescending(Expression<Func<T, object>> expression)
    {
        return Table.Where(e => e.IsActive == true).OrderByDescending(expression);
    }
    public async Task<int> MaxAsync(Expression<Func<T, int>> expression)
    {
        var result = 0;
        try
        {
            result = await Table.Where(e => e.IsActive == true).MaxAsync(expression);
        }
        catch (Exception)
        {
        }
        return result;
    }
    public async Task<int> CountAsync()
    {
        return await Table.Where(e => e.IsActive == true).CountAsync();
    }
    //
    public async Task<Paging<List<T>>> GetAll<R>(int pageNumber, int pageSize, Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy)
    {
        try
        {
            if (pageSize == 0)
                pageSize = 10;
            if (pageNumber == 0)
                pageNumber = 1;
            IQueryable<T> query = _context.Set<T>();
            query = query.Where(criteria);
            query = OrderBy(query);
            int recCount = query.Count();
            int totalPages = (int)Math.Ceiling((decimal)recCount / (decimal)pageSize);
            var skip = (pageNumber - 1) * pageSize;
            var data = await query.Skip(skip).Take(pageSize).AsNoTracking().ToListAsync();
            var finaldata = data;
            return new Paging<List<T>>(finaldata, totalPages, pageNumber, pageSize, recCount);
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}


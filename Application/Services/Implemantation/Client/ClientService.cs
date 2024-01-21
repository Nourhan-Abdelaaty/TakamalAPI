using Application.Services.Interfaces;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implemantation;
public class ClientService : IClientService
{
    private readonly IUnitOfWork UnitOFWork;
    public ClientService(IUnitOfWork unitOfWork)
    {
        UnitOFWork = unitOfWork;
    }
    public async Task<ResponseResult> Create(Client model)
    {
        if (model == null)
            return new ResponseResult { IsSuccess = false, Message = "SendNull" };

        if (model.NameAr == null || model.NameAr.Trim() == "")
            return new ResponseResult { IsSuccess = false, Message = "NameRequired" };

        await UnitOFWork.Repository<Client>().AddAsync(model);
        await UnitOFWork.CompleteAsync();
        return new ResponseResult
        {
            IsSuccess = true,
            Message = "AddSuccess",
            Obj = new { Id = model.Id, NameAr = model.NameAr, }
        };
    }
    public async Task<ResponseResult> Update(Client model)
    {
        #region Valdation
        if (model == null)
            return new ResponseResult { IsSuccess = false, Message = "SendNull" };

        if (model.Id <= 0)
            return new ResponseResult { IsSuccess = false, Message = "IdRequierd" };

        if (model.NameAr == null || model.NameAr.Trim() == "")
            return new ResponseResult { IsSuccess = false, Message = "NameRequired" };

        var modelExist = UnitOFWork.Repository<Client>().FirstOrDefault(e => e.Id == model.Id);
        if (modelExist == null)
            return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };
        #endregion

        #region Modify
        model.CreatedById = modelExist.CreatedById;
        model.CreatedByName = modelExist.CreatedByName;
        model.LastModifiedDate = DateTime.Now;
        model.ModifyCount = modelExist.ModifyCount + 1;
        #endregion
        var i = await UnitOFWork.Repository<Client>().UpdateAsync(model);
        var x = await UnitOFWork.CompleteAsync();

        return new ResponseResult
        {
            IsSuccess = true,
            Message = "UpdateSuccess",
            Obj =
            new
            {
                Id = model.Id,
                NameAr = model.NameAr,
            }
        };
    }
    public async Task<ResponseResult> Delete(int id)
    {
        if (id <= 0)
            return new ResponseResult { IsSuccess = false, Message = "IdRequierd" };
        var model = UnitOFWork.Repository<Client>().Include(e=>e.ClientCalls).FirstOrDefault(e => e.Id == id);
        if (model == null)
            return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };

        await UnitOFWork.Repository<Client>().DeleteAsync(id);
        await UnitOFWork.CompleteAsync();
        return new ResponseResult { IsSuccess = true, Message = "DeleteSuccess", Obj = model };

    }
    public async Task<ResponseResult> GetById(int id)
    {
        if (id <= 0)
            return new ResponseResult { IsSuccess = false, Message = "IdRequierd" };
        try
        {
            var model = UnitOFWork.Repository<Client>().Include(e=>e.ClientCalls)
               .FirstOrDefault(e => e.Id == id);
            if (model == null)
                return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };

            return new ResponseResult { IsSuccess = true, Message = "Success", Obj = model };
        }
        catch(Exception ex)
        {
            throw;
        }

    }
    // GetAll Pagination
    public async Task<ResponseResult> GetAll(Paging<Client> Paging)
    {
        try
        {
            var list = await UnitOFWork.Repository<Client>()
            .GetAll<Client>(Paging.CurrentPage, Paging.PageSize, e => e.Id > 0, OrderBy: e => e.OrderBy(e => e.Id));
            if (list == null)
                return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };
            return new ResponseResult { IsSuccess = true, Message = "Success", Obj = list };
        }
        catch(Exception ex)
        {
            throw;
        }
    }
    // GetAll For DropDown
    public async Task<ResponseResult> GetAllDropDown()
    {
        var All = UnitOFWork.Repository<Client>()
            .Select(e => new { Id = e.Id, NameAr = e.NameAr,  }).ToList(); 
        
        if (All == null)
            return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };

        return new ResponseResult { IsSuccess = true, Message = "Success", Obj = All };
    }
}


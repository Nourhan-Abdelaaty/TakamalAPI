using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces ;
public interface IClientService
{
    Task<ResponseResult> Create(Client model);
    Task<ResponseResult> Update(Client model);
    Task<ResponseResult> Delete(int id);
    Task<ResponseResult> GetAllDropDown();
    Task<ResponseResult> GetById(int id);
    Task<ResponseResult> GetAll(Paging<Client> Paging);
}

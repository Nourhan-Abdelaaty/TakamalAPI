using Application.Services.Interfaces;
using Domain.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;
    public ClientController(IClientService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IResult> Create(Client model) => Results.Ok(await _service.Create(model));
    [HttpPut]
    public async Task<IResult> Update(Client model) => Results.Ok(await _service.Update(model));
    [HttpDelete]
    public async Task<IResult> Delete([FromQuery] int id) => Results.Ok(await _service.Delete(id));
    [HttpGet("{id}")]
    public async Task<IResult> GetById([FromQuery] int id) => Results.Ok(await _service.GetById(id)); 
    [HttpPost]
    [Route("GetAll")]
    public async Task<IResult> GetAll(Paging<Client> Paging) => Results.Ok(await _service.GetAll(Paging));
    [HttpGet]
    [Route("GetAllDropDown")]
    public async Task<IResult> GetAllDropDown(IClientService service) => Results.Ok(await _service.GetAllDropDown());

}



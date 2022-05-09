using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _repository;
        private IMapper _mapper;

        public RoleController(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseDTO<List<RoleDTO>>();
            response.SetMeta(Request);

            var roles = await _repository.GetAllAsync();
            var lista = _mapper.Map<List<RoleDTO>>(roles);
            response.Data = lista;
            return Ok(response);
        }
    }
}

using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDTO item)
        {
            var response = new ResponseDTO<Course>();
            response.SetMeta(Request);

            var record = _mapper.Map<Course>(item);

            try
            {
                var result = await _repository.CreateAsync(record);
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CourseDTO item)
        {
            var response = new ResponseDTO<bool>();
            response.SetMeta(Request);
            var record = _mapper.Map<Course>(item);
            await _repository.UpdateAsync(id, record);
            response.Data = true;
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ResponseDTO<bool>();
            response.SetMeta(Request);
            await _repository.DeleteAsync(id);
            response.Data = true;
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseDTO<List<CourseDTO>>();
            response.SetMeta(Request);
            var courses = await _repository.GetAllAsync();
            response.Data = _mapper.Map<List<CourseDTO>>(courses);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseDTO<CourseDTO>();
            response.SetMeta(Request);
            var result = await _repository.GetAsync(id);
            response.Data = _mapper.Map<CourseDTO>(result);
            return Ok(response);
        }

    }
}

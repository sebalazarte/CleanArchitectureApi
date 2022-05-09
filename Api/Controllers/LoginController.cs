using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository _repository;
        private IMapper _mapper;

        public LoginController(IConfiguration config, IUserRepository repository, IMapper mapper)
        {
            _config = config;
            _repository = repository;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var response = new ResponseDTO<ResponseLoginDTO>();
            response.SetMeta(Request);
            var user = await _repository.ValidateAsync(login.UserName, login.Password);
            if (user == null)
            {
                response.Error = "Credenciales incorrectas";
                return BadRequest(response);
            }
            var token = GenerateToken(user);
            var data = new ResponseLoginDTO
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            };
            response.Data = data;
            return Ok(response);
        }

        private string GenerateToken(User item)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, item.UserName),
                new Claim(ClaimTypes.Email, item.Email),
                new Claim(ClaimTypes.Name, item.FullName),
                new Claim(ClaimTypes.Role, item.Role.RoleName),
                new Claim(ClaimTypes.Sid, item.UserId.ToString()),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDTO GetCurrentUser()
        {
            var identity = Request.HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return null;
            var userClaims = identity.Claims;
            return new UserDTO
            {
                UserName = userClaims.First(i => i.Type == ClaimTypes.NameIdentifier).Value,
                Email = userClaims.First(i => i.Type == ClaimTypes.Email).Value,
                FullName = userClaims.First(i => i.Type == ClaimTypes.Name).Value,
                Role = userClaims.First(i => i.Type == ClaimTypes.Role).Value,
                UserId = int.Parse(userClaims.First(i => i.Type == ClaimTypes.Sid).Value),
            };
        }
    }
}

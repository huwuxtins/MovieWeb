using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using MovieWeb.DTOs;
using MovieWeb.Models;
using MovieWeb.Services.Interfaces;
using MovieWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

namespace MovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly IMapper mapper;
        private readonly IConfiguration _config;

        public UserController(IUserService userService, IMailService mailService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mailService = mailService;
            this.mapper = mapper;
            _config = configuration;
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers(int page)
        {
            ICollection<UserModel> users = await _userService.GetUsers(page);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/api/[controller]/sign-in")]
        public async Task<ActionResult> GetUserByEmail([FromBody] UserDTO userDTO)
        {
            var user = await _userService.GetUser(userDTO.Email);
            if (user == null)
            {
                return NotFound("Email isn't correct, please check then try again!");
            }
            bool verified = BC.Verify(userDTO.Password, user.Password);
            if(!verified)
            {
                return BadRequest("Password isn't correct, please check then try again!");
            }
            var tokenString = (new JwtHelper(_config)).GenerateJsonWebToken(user);

            return Ok(new { user = mapper.Map<UserDTO>(user), token = tokenString });
        }

        //POST: api/user
        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserModel>>> AddUser(UserModel userModel)
        {
            userModel.Password = BC.HashPassword(userModel.Password);

            var isSaved = await _userService.AddUser(userModel);
            if (isSaved)
            {
                await _mailService.SendMailAsync(new MailData
                {
                    EmailToId = userModel.Email,
                    EmailToName = userModel.Name,
                    EmailSubject = "Congratulation on your joining with us",
                    EmailBody = "Happy to see you on board! Here’s how to get started"
                });
                return CreatedAtAction(nameof(AddUser), userModel);
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO userDTO)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound("Id isn't correct");
            }

            var isUpdated = await _userService.PutUser(id, mapper.Map<UserModel>(userDTO));
            if (isUpdated)
            {
                return Ok(userDTO);
            }

            return BadRequest("Cannot update user, please check then try again!");
        }


        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("/api/user/admin/{id}")]
        public async Task<IActionResult> PutUserAdmin(Guid id, UserModel userModel)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound("Id isn't correct");
            }

            var isUpdated = await _userService.PutUser(id, mapper.Map(userModel, user));
            if (isUpdated)
            {
                return Ok(userModel);
            }

            return BadRequest("Cannot update user, please check then try again!");
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUser(id);

            if(user == null)
            {
                return NotFound();
            }

            var isDeleted = await _userService.DeleteUser(user);
            if(isDeleted)
            {
                return Ok("Deleted successfully!");
            }
            return NoContent();
        }
    }
}

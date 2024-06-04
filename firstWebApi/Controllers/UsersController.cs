using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Post([FromBody] UserLoginDTO userLoginDTO)
        {
          
            User user = _mapper.Map<UserLoginDTO, User>(userLoginDTO);
            User loginUser = await _userService.GetUserByEmailAndPassword(user.UserName, user.Password);
            if(loginUser == null) 
                return NoContent();
            UserDTO userDTO = _mapper.Map<User, UserDTO>(loginUser);
            _logger.LogInformation($"Login attempted with User Name {loginUser.UserName.Trim()} and password {loginUser.Password.Trim()}");
            return Ok(userDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            int z = 0;
            int err = 8 / z;
            User user = await _userService.GetUserById(id);
            if (user == null)
                return NoContent();
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<UserDTO,User>(userDto);
                User newUser = await _userService.AddUser(user);
                if (newUser == null)
                    return BadRequest();
                UserDTO userDTO = _mapper.Map<User, UserDTO>(newUser);
                return CreatedAtAction(nameof(Get), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);// BadRequest(ex.Message);
            }
        }
        [Route("Pwd")]
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] string password)
        {
            int result = await _userService.CheckPwd(password);
            if (result <= 2)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UserDTO userDto)
        {
            User UserToUpdate = _mapper.Map<UserDTO, User>(userDto);
            User updatedUser = await _userService.UpdateUser(id, UserToUpdate);
            if(updatedUser == null)
                return NoContent();
            UserDTO userDTO = _mapper.Map<User, UserDTO>(updatedUser);
            return Ok(userDTO);
        }


    }
}

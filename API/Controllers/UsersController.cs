using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        return Ok(await _userRepository.GetMembersAsync());
        var users = await _userRepository.GetUsersAsync();
        var usersReturn = _mapper.Map<IEnumerable<MemberDTO>>(users);
        return Ok(usersReturn);
    }
   
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUserByName(string username)
    {
        return await _userRepository.GetMemberByNameAsync(username);      
    }

}

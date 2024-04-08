using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UsersController(IUserRepository userRepository, IMapper mapper,
    IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        return Ok(await _userRepository.GetMembersAsync());        
    }
   
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUserByName(string username)
    {
        return await _userRepository.GetMemberByNameAsync(username);      
    }

    [HttpPut]

    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
    {
        var username = User.GetUserName();
        var user = await this._userRepository.GetUserByUserNameAsync(username);
        if (user == null) return NotFound();
        _mapper.Map(memberUpdateDTO, user);
        if (await this._userRepository.SaveAllAsync())
            return NoContent();
        return BadRequest("Failed to updte user");
    }

    [HttpPost("add-photo")]

    public async Task<ActionResult<PhotoDTO>> UploadPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        if (user == null) return NotFound();
        var uploadresult = await _photoService.AddPhotoAsync(file);
        if (uploadresult.Error != null) 
        return BadRequest(uploadresult.Error.Message);
        var photo = new Photo {
            Url = uploadresult.SecureUrl.AbsoluteUri,
            PublicId = uploadresult.PublicId
        };

        if (user.Photos.Count == 0) 
            photo.isMain = true;

        user.Photos.Add(photo);
        if (await _userRepository.SaveAllAsync()) 
            return CreatedAtAction(nameof(GetUserByName),new {username = user.UserName}, _mapper.Map<PhotoDTO>(photo));

        return BadRequest("Problem uploading the photo");

    }

    [HttpPut("set-main-photo/{photoId}")]

    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        if (user == null) return NotFound("User not found");
        var photo = user.Photos.Find(x=> x.Id == photoId);
        if (photo == null) return NotFound("Photo not found");
        if (photo.isMain) return BadRequest("Photo is already the main photo");
    
        var currentMain = user.Photos.Find(x=>x.isMain == true);
        if (currentMain != null) currentMain.isMain = false;
        photo.isMain = true;
        if (await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Problem setting main Photo");
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await _userRepository.GetMemberByNameAsync(User.GetUserName());
        if (user == null) return NotFound("User not found!");
        var photo = user.Photos.FirstOrDefault(p=>p.Id == photoId);
        if (photo == null) return NotFound("Photo Not Found!");
        if (photo.isMain) return BadRequest("Main photo cannot be deleted");
        if(photo.PublicId != null) 
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }
        user.Photos.Remove(photo);
        if (await _userRepository.SaveAllAsync()) return Ok();
        return BadRequest("Problem deleting photo");
    }

}

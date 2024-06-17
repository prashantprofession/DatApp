using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class LikesRepository : ILikesRepository
{
    private DataContext _dataContext;
    public LikesRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
    {
        return await _dataContext.Likes.FindAsync(sourceUserId,targetUserId);
    }

    public async Task<PagedList<LikeDTO>> GetUserLikes(LikesParams likesParams)
    {
        var users = _dataContext.Users.OrderBy(u => u.UserName).AsQueryable();
        var likes = _dataContext.Likes.AsQueryable();
        if (likesParams.Predicate == "liked") 
        {
            likes = likes.Where(l => l.SourceUserId == likesParams.UserId);
            users = likes.Select(u => u.TargetUser);
        } else 
        if (likesParams.Predicate == "likedby") 
        {
            likes = likes.Where(l => l.TargetUserId == likesParams.UserId);
            users = likes.Select(u => u.SourceUser);
        }

        var likedUsers =  users.Select(user => new LikeDTO
        {
            UserName = user.UserName,
            KnownAs = user.KnownAs,
            Age = user.DateOfBirth.CalculatteAge(),
            PhotoUrl = user.Photos.FirstOrDefault(x=>x.isMain).Url, 
            City = user.City,
            Id = user.Id
        });

        return await PagedList<LikeDTO>.CreateAsync(likedUsers, likesParams.PageNumber,
         likesParams.PageSize);
    }

    public async Task<AppUser> GetUserWithLikes(int userId)
    {
        return await _dataContext.Users
        .Include(u => u.LikedUsers)
        .FirstOrDefaultAsync(u => u.Id == userId);
    }
}

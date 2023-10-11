﻿using dungeons_and_cards.Models.Contexts;
using dungeons_and_cards.Models.UserModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Services;

public class UserService : IUserService
{
    private Context _context;
    
    public UserService(Context context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllUser()
    {
        return await _context.Users.ToListAsync().ConfigureAwait(true);
    }

    public async Task<string> AddNewUser(User newUser)
    {
        string result;
        
        if (await checkUserIsBanned(newUser.EmailAddress))
        {
            result = "The user is banned"; 
            return result;
        }

        if (await checkUserIsExist(newUser.EmailAddress))
        {
            result = "The user is already exist";
            return result;
        }
            
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        result = $"User with {newUser.UserId} is registered";
            
        return result;
        
       
    }

    public async Task<Guid> BannedUser(BannedUser bannedUser)
    {
        try
        {
            User userForBanned = await _context.Users.FirstOrDefaultAsync(user => user.EmailAddress.Equals(bannedUser.EmailAddress));
            _context.Users.Remove(userForBanned);
            _context.BannedUsers.Add(bannedUser);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return bannedUser.UserId;

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<List<BannedUser>> GetAllBannedUser()
    {
        return await _context.BannedUsers.ToListAsync().ConfigureAwait(true);
    }

    private async Task<bool> checkUserIsBanned(string email)
    {
        var user = await _context.BannedUsers.FirstOrDefaultAsync(user => user.EmailAddress.Equals(email)).ConfigureAwait(true);

        if (user == null)
        {
            return false;
        }

        return true;
    }

    private async Task<bool> checkUserIsExist(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.EmailAddress.Equals(email)).ConfigureAwait(true);

        if (user == null)
        {
            return false;
        }

        return true;
    }
}
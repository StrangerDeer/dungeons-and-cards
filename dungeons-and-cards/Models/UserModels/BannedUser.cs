﻿using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;

public class BannedUser : BaseUser
{
    public DateTime BannedStart { get; set; }
    public DateTime BannedEnd { get; set; }

    public BannedUser(Guid userId, string username, string emailAddress, Role userRole, DateTime registrationDate, DateTime bannedEnd)
        : base(userId, username, GenerateRandomPassword(), emailAddress, userRole, registrationDate)
    {
        BannedStart = DateTime.Now;
        BannedEnd = bannedEnd;
    }

    private static string GenerateRandomPassword()
    {
        Random random = new Random();
        
        string result = "";
        int resultLength = random.Next(10, 50);
        int asciiLimitValue = 33;
        int asciiMaximumValue = 126;

        for (int i = 0; i < resultLength; i++)
        {
            int randomValue = random.Next(asciiLimitValue, asciiMaximumValue);
            char letter = Convert.ToChar(randomValue);
            result += letter;
        }
        
        return BCrypt.Net.BCrypt.EnhancedHashPassword(result, 13);
    }
    
}
﻿namespace dungeons_and_cards.Models.UserModels;

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }

    public UserLogin(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
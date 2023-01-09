﻿using System.Text.Json.Serialization;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.ApiEntityModels;

public class UserModel
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string EncryptedPassword { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
    [JsonPropertyName("role")]
    public UserRole Role { get; set; }
        
    private UserModel(User user)
    {
        UserId = user.UserId.Value;
        Login = user.Login;
        EncryptedPassword = user.EncryptedPassword;
        Name = user.Name;
        Email = user.Email;
        Phone = user.Phone;
        Role = user.Role;
    }
        
    public static implicit operator UserModel(User u) => new (u);
    public static implicit operator User(UserModel um) => new ()
    {
        UserId = um.UserId,
        Login = um.Login,
        EncryptedPassword = um.EncryptedPassword,
        Name = um.Name,
        Email = um.Email,
        Phone = um.Phone,
        Role = um.Role
    };
}
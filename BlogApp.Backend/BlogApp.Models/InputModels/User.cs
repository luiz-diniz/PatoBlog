﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.Models.InputModels;

public class User
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string PasswordConfirmation { get; set; }

    [Required]
    public string Email { get; set; }

    public string? ProfileImageContent { get; set; }

    [JsonIgnore]
    public string? ProfileImageName { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public int IdRole {  get; set; }
}
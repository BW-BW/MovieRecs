﻿using System.ComponentModel.DataAnnotations;

namespace MovieRecs.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}

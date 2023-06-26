using System.ComponentModel.DataAnnotations;

namespace MovieRecs.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public string? Genre1 { get; set; }

    public string? Genre2 { get; set; }

    public string? Genre3 { get; set; }
}

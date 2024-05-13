using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using winui_cooler;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
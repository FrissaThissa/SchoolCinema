using Microsoft.AspNetCore.Identity;

namespace cinema.Identity;

public class CinemaIdentityUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    [PersonalData]
    public string? LastName { get; set; }
}
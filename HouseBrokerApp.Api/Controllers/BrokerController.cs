using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BrokerController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public BrokerController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult GetBrokers()
    {
        var brokers = _userManager.Users
            .Where(u => _userManager.IsInRoleAsync(u, "Broker").Result)
            .Select(u => new { u.UserName, u.Email })
            .ToList();

        return Ok(brokers);
    }
}

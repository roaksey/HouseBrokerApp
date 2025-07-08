using Microsoft.AspNetCore.Identity;

namespace HouseBrokerApp.Infrastructure.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1️⃣ Create roles if they don’t exist
            string[] roles = { "Broker", "HouseSeeker" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 2️⃣ Create default Broker user
            string brokerEmail = "broker1@example.com";
            var brokerUser = await userManager.FindByEmailAsync(brokerEmail);
            if (brokerUser == null)
            {
                brokerUser = new IdentityUser
                {
                    UserName = "broker1",
                    Email = brokerEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(brokerUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(brokerUser, "Broker");
                }
            }

            // 3️⃣ Create default HouseSeeker user
            string seekerEmail = "seeker1@example.com";
            var seekerUser = await userManager.FindByEmailAsync(seekerEmail);
            if (seekerUser == null)
            {
                seekerUser = new IdentityUser
                {
                    UserName = "seeker1",
                    Email = seekerEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(seekerUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(seekerUser, "HouseSeeker");
                }
            }
        }
    }
}

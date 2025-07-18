using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class VocContextSeed
{
    public static async Task SeedAsync(VocContext context)
    {

        // Check if super admin role exists
        if (!await context.Role.AnyAsync(r => r.Name == "SuperAdmin"))
        {
            // Create super admin role first
            var superAdminRole = new Role
            {
                Name = "SuperAdmin",
                CreatedDate = DateTime.UtcNow
            };
            context.Role.Add(superAdminRole);
            await context.SaveChangesAsync();

            // Now create super admin user
            var superAdmin = new User
            {
                Name = "Super Admin",
                Email = "superadmin@voc.com",
                Mobile = "+1234567890",
                JoiningDate = DateTime.UtcNow,
                Status = UserStatus.Active,
                RoleId = superAdminRole.Id
            };

            // Hash password
            //superAdmin.PasswordHash = passwordHasher.HashPassword(superAdmin, "Admin@123");

            context.User.Add(superAdmin);
            await context.SaveChangesAsync();

            // Update role's CreatedBy
            //superAdminRole.Create = superAdmin.UserId;
            await context.SaveChangesAsync();
        }
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Merchants.Any())
        {
            var MerchantData = await File
              .ReadAllTextAsync(path + @"/Data/SeedData/merchants.json");

            var merchants = JsonSerializer.Deserialize<List<Merchant>>(MerchantData);

            if (merchants == null) return;

            // Ensure all DateTime properties are UTC
            foreach (var merchant in merchants)
            {
                if (merchant.LastTransaction != null)
                    merchant.LastTransaction = DateTime.SpecifyKind(merchant.LastTransaction.Value, DateTimeKind.Utc);
                if (merchant.LastTicket != null)
                    merchant.LastTicket = DateTime.SpecifyKind(merchant.LastTicket.Value, DateTimeKind.Utc);
                if (merchant.LastSurvey != null)
                    merchant.LastSurvey = DateTime.SpecifyKind(merchant.LastSurvey.Value, DateTimeKind.Utc);
                if (merchant.LastFeedback != null)
                    merchant.LastFeedback = DateTime.SpecifyKind(merchant.LastFeedback.Value, DateTimeKind.Utc);
                if (merchant.LastEscalation != null)
                    merchant.LastEscalation = DateTime.SpecifyKind(merchant.LastEscalation.Value, DateTimeKind.Utc);
                if (merchant.CreatedOn != null)
                    merchant.CreatedOn = DateTime.SpecifyKind(merchant.CreatedOn, DateTimeKind.Utc);
            }

            context.Merchants.AddRange(merchants);
            await context.SaveChangesAsync();

        }
    }
}

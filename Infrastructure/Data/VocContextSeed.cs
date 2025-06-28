using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class VocContextSeed
{
    public static async Task SeedAsync(VocContext context, UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
        {
            var user = new AppUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Admin");
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

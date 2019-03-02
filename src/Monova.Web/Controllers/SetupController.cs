using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monova.Entity;

namespace Monova.Web.Controllers
{
    public class SetupController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            if (!await Db.SubscriptionTypes.AnyAsync())
            {
                // Free
                var subscriptionTypeFree = new MVDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = false,
                    Name = "free",
                    Price = 0,
                    Title = "Free",
                    Description = "Free for starter users."
                };
                await Db.AddAsync(subscriptionTypeFree);
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, "monitor", "MONITOR", "1");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, "monitor step", "MONITOR_STEP", "1");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, "user", "USER", "1");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, "interval", "INTERVAL", "300");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, "alert channel", "ALERT_CHANNEL", "1");
                // Startup
                var subscriptionTypeStartup = new MVDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = true,
                    Name = "startup",
                    Price = 4.99m,
                    Title = "Startup",
                    Description = "For startups."
                };
                await Db.AddAsync(subscriptionTypeStartup);
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, "monitor", "MONITOR", "5");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, "monitor step", "MONITOR_STEP", "10");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, "user", "USER", "1");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, "interval", "INTERVAL", "300");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, "alert channel", "ALERT_CHANNEL", "2");
                // Premium
                var subscriptionTypePremium = new MVDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = true,
                    Name = "premium",
                    Price = 49.99m,
                    Title = "Premium",
                    Description = "For growing companies."
                };
                await Db.AddAsync(subscriptionTypePremium);
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, "monitor", "MONITOR", "25");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, "monitor step", "MONITOR_STEP", "100");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, "user", "USER", "5");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, "interval", "INTERVAL", "60");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, "alert channel", "ALERT_CHANNEL", "5");
                // Enterprise
                var subscriptionTypeEnterprise = new MVDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = true,
                    Name = "enterprise",
                    Price = 149.99m,
                    Title = "Enterprise",
                    Description = "For enterprise companies."
                };
                await Db.AddAsync(subscriptionTypeEnterprise);
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, "monitor", "MONITOR", "100");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, "monitor step", "MONITOR_STEP", "250");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, "user", "USER", "25");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, "interval", "INTERVAL", "60");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, "alert channel", "ALERT_CHANNEL", "*");

                if (await Db.SaveChangesAsync() > 0)
                {
                    return Success("I have bootstrapped successfuly.");
                }
            }
            return BadRequest("Done.");
        }

        [NonAction]
        private async Task AddFeature(Guid typeId, string title, string name, string value)
        {
            await Db.AddAsync(new MVDSubscriptionTypeFeature
            {
                SubscriptionTypeFeatureId = Guid.NewGuid(),
                SubscriptionTypeId = typeId,
                Description = string.Empty,
                IsFeature = true,
                Name = name,
                Title = title,
                Value = value
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DateSelector.Persistence;
using DateSelector.Persistence.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DateSelector.API.Helpers {
    public class MigrationHelper {
        public static void SeedAndMigrate(IWebHost host) {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                using (var serviceScope = services.GetService<IServiceScopeFactory>().CreateScope()) {
                    var context = serviceScope.ServiceProvider.GetRequiredService<DateContext>();
                    AddInitialData(context);
                    context.Database.Migrate();
                }
            }
        }
        
        private static void AddInitialData(DateContext context) {
            if (!context.DateComparisonObjects.Any()) {
                context.DateComparisonObjects.AddRange(new List<DateComparisonObject> {
                    new DateComparisonObject(DateTime.UtcNow.EndOfDay().Ticks, DateTime.UtcNow.EndOfDay().AddDays(1).Ticks),
                    new DateComparisonObject(DateTime.UtcNow.EndOfDay().Ticks, DateTime.UtcNow.EndOfDay().AddDays(10).Ticks)
                });
            }

            context.SaveChanges();
        }

    }
}

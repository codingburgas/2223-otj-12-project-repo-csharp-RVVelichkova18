using FluentAssertions;
using ForestrySystem.Controllers;
using ForestrySystem.Data;
using ForestrySystem.Enums;
using ForestrySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace UnitTests
{
    public class CRUDTests
    {
        async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.TypeOfTimber.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.TypeOfTimber.Add(
                        new TypeOfTimber()
                        {
                            TimberName = TypeOfTimberEnum.Sanitary,
                            AmountForLogging = 1,
                            YearOfLogging = DateTime.Now
                        });
                }
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        async void UnitTestCreate()
        {
            var TypeOfTimber = new TypeOfTimber()
            {
                TimberName = TypeOfTimberEnum.Sanitary,
                AmountForLogging = 1,
                YearOfLogging = DateTime.Now
            };
            var databaseContext = await GetDbContext();
            var controller = new TypeOfTimbersController(databaseContext);
            controller.Create(TypeOfTimber);
            Assert.Equal(1, databaseContext.TypeOfTimber.Count());
        }

        [Fact]
        async void UnitTestRead()
        {
            var databaseContext = await GetDbContext();
            var controller = new TypeOfTimbersController(databaseContext);
            var result = controller.Details(1);
            result.Equals(true);
        }

        [Fact]
        async void UnitTestUpdate()
        {
            var TypeOfTimber = new TypeOfTimber()
            {
                TimberName = TypeOfTimberEnum.Sanitary,
                AmountForLogging = 1,
                YearOfLogging = DateTime.Now
            };
            var databaseContext = await GetDbContext();
            var controller = new TypeOfTimbersController(databaseContext);
            var result = controller.Edit(11,TypeOfTimber);
            result.Equals(true);
        }

        [Fact]
        async void UnitTestDelete()
        {
            var TypeOfTimber = new TypeOfTimber()
            {
                TimberName = TypeOfTimberEnum.Sanitary,
                AmountForLogging = 1,
                YearOfLogging = DateTime.Now
            };
            var databaseContext = await GetDbContext();
            var controller = new TypeOfTimbersController(databaseContext);
            controller.Create(TypeOfTimber);
            await controller.DeleteConfirmed(TypeOfTimber.Id);
            databaseContext.SaveChanges();
            Assert.Equal(0, databaseContext.TypeOfTimber.Count());
        }
    }
}
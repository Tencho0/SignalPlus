﻿namespace SignalPlus.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Data;
    using SignalPlus.Models.Enums;
    using SignalPlus.Models;
    using Microsoft.AspNetCore.Identity;

    using static Constants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            try
            {
                var serviceProvider = scopedServices.ServiceProvider;
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();

                SeedUsers(context);
                SeedSignals(context);
                SeedAdministrator(serviceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
            }

            return app;
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var user1 = new User
                {
                    Id = "eceed62a-8a49-422d-9417-0a7008ea95ff",
                    UserName = "ivan.popov@example.com",
                    Email = "ivan.popov@example.com",
                    Name = "Иван Попов",
                    PhoneNumber = "+359888123456"
                };

                var user2 = new User
                {
                    Id = "b11f4443-0e58-4bc1-8c91-0d18e12b7f02",
                    UserName = "maria.georgieva@example.com",
                    Email = "maria.georgieva@example.com",
                    Name = "Мария Георгиева",
                    PhoneNumber = "+359887987654"
                };

                context.Users.AddRange(user1, user2);
                context.SaveChanges();
            }
        }

        private static void SeedSignals(ApplicationDbContext context)
        {
            if (!context.Signals.Any())
            {
                // Create sample signals
                var signal1 = new Signal
                {
                    Title = "Счупена улична лампа",
                    Description = "Лампата пред блока не работи от няколко дни.",
                    Category = Category.Осветление,
                    Status = Status.Регистриран,
                    LocationAddress = "ул. Витошка 15, София",
                    Latitude = 42.6977M,
                    Longitude = 23.3242M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "eceed62a-8a49-422d-9417-0a7008ea95ff",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal2 = new Signal
                {
                    Title = "Дупка на пътя",
                    Description = "Голяма дупка на кръстовището, която създава опасност за шофьорите.",
                    Category = Category.УлициИТротоари,
                    Status = Status.Регистриран,
                    LocationAddress = "бул. България 10, София",
                    Latitude = 42.6937M,
                    Longitude = 23.3199M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "eceed62a-8a49-422d-9417-0a7008ea95ff",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal3 = new Signal
                {
                    Title = "Прелял контейнер за боклук",
                    Description = "Контейнерът е препълнен и боклукът се разпилява по улицата.",
                    Category = Category.ОколнаСреда,
                    Status = Status.Приключен,
                    LocationAddress = "ул. Шипка 22, София",
                    Latitude = 42.6965M,
                    Longitude = 23.3350M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "eceed62a-8a49-422d-9417-0a7008ea95ff",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal4 = new Signal
                {
                    Title = "Неравности по тротоара",
                    Description = "Тротоарът е напукан и представлява опасност за пешеходците.",
                    Category = Category.УлициИТротоари,
                    Status = Status.Регистриран,
                    LocationAddress = "ул. Граф Игнатиев 5, София",
                    Latitude = 42.6901M,
                    Longitude = 23.3274M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "b11f4443-0e58-4bc1-8c91-0d18e12b7f02",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal5 = new Signal
                {
                    Title = "Счупена пейка в парка",
                    Description = "Пейката до детската площадка е счупена и представлява опасност за децата.",
                    Category = Category.Площадки,
                    Status = Status.Приключен,
                    LocationAddress = "Южен парк, вход от ул. Нишава, София",
                    Latitude = 42.6758M,
                    Longitude = 23.3190M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "eceed62a-8a49-422d-9417-0a7008ea95ff",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal6 = new Signal
                {
                    Title = "Счупен светофар",
                    Description = "Светофарът на кръстовището не работи, създава хаос в трафика.",
                    Category = Category.ПътнаСигнализация,
                    Status = Status.Регистриран,
                    LocationAddress = "бул. Цар Борис III и бул. Акад. Иван Гешов, София",
                    Latitude = 42.6845M,
                    Longitude = 23.3092M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "b11f4443-0e58-4bc1-8c91-0d18e12b7f02",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal7 = new Signal
                {
                    Title = "Изоставен автомобил",
                    Description = "Стар автомобил е паркиран от месеци на улицата и пречи на движението.",
                    Category = Category.Паркиране,
                    Status = Status.Приключен,
                    LocationAddress = "ул. Оборище 18, София",
                    Latitude = 42.6980M,
                    Longitude = 23.3421M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "eceed62a-8a49-422d-9417-0a7008ea95ff",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                var signal8 = new Signal
                {
                    Title = "Запушен канал",
                    Description = "Канализацията е запушена и водата се връща по улицата при дъжд.",
                    Category = Category.Строителство,
                    Status = Status.Регистриран,
                    LocationAddress = "ул. Пиротска 25, София",
                    Latitude = 42.7012M,
                    Longitude = 23.3133M,
                    CreatedAt = DateTime.UtcNow,
                    UserId = "eceed62a-8a49-422d-9417-0a7008ea95ff",

                    SenderName = "Ivan petkov",
                    SenderPhone = "08976326270",
                    SenderEmail = "vankata69@gmail.com"
                };

                // Add signals to the database
                context.Signals
                    .AddRange(signal1, signal2, signal3, signal4, signal5, signal6, signal7, signal8);
                context.SaveChanges();
            }
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };
                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@crs.com";
                const string adminPassword = "Admin123@";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = "Admin",
                    PhoneNumber = "08888888"
                };

                await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, role.Name);
            }).GetAwaiter().GetResult();
        }
    }
}

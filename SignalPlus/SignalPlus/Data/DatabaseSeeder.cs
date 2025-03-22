namespace SignalPlus.Data
{
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Models.Enums;
    using SignalPlus.Models;

    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.Migrate();

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
                    CreatedAt = DateTime.UtcNow
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
                    CreatedAt = DateTime.UtcNow
                };

                // Add signals to the database
                context.Signals.AddRange(signal1, signal2);
                context.SaveChanges();
            }
        }
    }

}

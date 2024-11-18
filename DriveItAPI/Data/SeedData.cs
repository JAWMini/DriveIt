using DriveItAPI.Model;

namespace DriveItAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(CarRentalContext db)
        {
            Car[] cars = [
                new("Toyota", "Corolla", 2010, new Guid(), true, "Warszawa", 100, 10),
                new("Toyota", "Camry", 2012, new Guid(), true, "Warszawa", 150, 15),
                new ("Honda", "Civic", 2015, new Guid(), true, "Warszawa", 120, 12),
                new ("Honda", "Accord", 2018, new Guid(), true, "Warszawa", 180, 18),
                new ("Honda", "CR-V", 2021, new Guid(), true, "Warszawa", 200, 20),
                new ("Ford", "Focus", 2018, new Guid(), true, "Warszawa", 130, 13),
                new ("Ford", "Fiesta", 2016, new Guid(), true, "Warszawa", 110, 11),
                new ("Ford", "Mustang", 2020, new Guid(), true, "Warszawa", 250, 25),
                new ("BMW", "X5", 2020, new Guid(), true, "Warszawa", 300, 30),
                new ("BMW", "X3", 2017, new Guid(), true, "Warszawa", 200, 20),
                new ("BMW", "3 Series", 2015, new Guid(), true, "Warszawa", 150, 15),
                new ("Audi", "A4", 2022, new Guid(), true, "Warszawa", 220, 22),
                new ("Audi", "Q5", 2019, new Guid(), true, "Warszawa", 250, 25),
                new ("Audi", "A6", 2016, new Guid(), true, "Warszawa", 200, 20),
                new ("Mercedes", "C-Class", 2019, new Guid(), true, "Warszawa", 200, 20),
                new ("Mercedes", "E-Class", 2021, new Guid(), true, "Warszawa", 250, 25),
                new ("Mercedes", "GLC", 2020, new Guid(), true, "Warszawa", 300, 30),
                new ("Volkswagen", "Golf", 2018, new Guid(), true, "Warszawa", 130, 13),
                new ("Volkswagen", "Passat", 2015, new Guid(), true, "Warszawa", 150, 15),
                new ("Volkswagen", "Tiguan", 2017, new Guid(), true, "Warszawa", 180, 18),
                new ("Toyota", "Yaris", 2021, new Guid(), true, "Warszawa", 100, 10),
                new ("Toyota", "Highlander", 2019, new Guid(), true, "Warszawa", 200, 20),
                new ("Ford", "Explorer", 2022, new Guid(), true, "Warszawa", 300, 30),
                new ("Honda", "Pilot", 2023, new Guid(), true, "Warszawa", 250, 25),
                new ("Audi", "A3", 2020, new Guid(), true, "Warszawa", 180, 18)
                ];

            db.Cars.AddRange(cars);
            db.SaveChanges();
        }

    }
}

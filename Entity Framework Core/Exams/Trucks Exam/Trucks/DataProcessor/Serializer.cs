namespace Trucks.DataProcessor
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            throw new NotImplementedException();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context.Clients.AsNoTracking()
                .Where(c=>c.ClientsTrucks.Any(ct=>ct.Truck.TankCapacity>=capacity))
                .Select(c=>new 
                {
                    Name = c.Name,
                    Trucks = c.ClientsTrucks
                            .Where(ct => ct.Truck.TankCapacity >= capacity)
                            .OrderBy(t=>t.Truck.MakeType)
                            .ThenByDescending(ct=>ct.Truck.CargoCapacity)
                            .ToArray()
                            .Select(ct=>new 
                            {
                                TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                                VinNumber = ct.Truck.VinNumber,
                                TankCapacity = ct.Truck.TankCapacity,
                                CargoCapacity = ct.Truck.CargoCapacity,
                                CategoryType = ct.Truck.CategoryType.ToString(),
                                MakeType = ct.Truck.MakeType.ToString(),
                            })
                            .ToArray()
                })
                .OrderByDescending(c=>c.Trucks.Length)
                .ThenBy(c=>c.Name)
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(clients,Formatting.Indented);
        }
    }
}

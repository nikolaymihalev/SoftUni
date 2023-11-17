using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSupplierDTO[]), new XmlRootAttribute("Suppliers"));

            using var reader = new StringReader(inputXml);

            ImportSupplierDTO[] importSupplierDTOs = (ImportSupplierDTO[])xmlSerializer.Deserialize(reader);

            var mapper = GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(importSupplierDTOs);

            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }
        
        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportPartsDTO[]), new XmlRootAttribute("Parts"));

            using var reader = new StringReader(inputXml);

            ImportPartsDTO[] importPartDTOs = (ImportPartsDTO[])xmlSerializer.Deserialize(reader);

            var supplierIds = context.Suppliers.Select(x => x.Id).ToArray();

            var mapper = GetMapper();
            Part[] parts = mapper.Map<Part[]>(importPartDTOs.Where(p=>supplierIds.Contains(p.SupplierId)));

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }
        
        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCarDTO[]), new XmlRootAttribute("Cars"));

            using var reader = new StringReader(inputXml);

            ImportCarDTO[] importCarDTOs = (ImportCarDTO[])xmlSerializer.Deserialize(reader);

            var mapper = GetMapper();
            List<Car> cars = new List<Car>();

            foreach (var carDTO in importCarDTOs) 
            {
                Car car = mapper.Map<Car>(carDTO);

                int[] carPartIds = carDTO.PartsIds.Select(x => x.Id).Distinct().ToArray();

                var carParts = new List<PartCar>();

                foreach (var id in carPartIds) 
                {
                    carParts.Add(new PartCar
                    {
                        Car = car,
                        PartId = id
                    });
                }

                car.PartsCars = carParts;

                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCustomerDTO[]), new XmlRootAttribute("Customers"));

            using var reader = new StringReader(inputXml);

            ImportCustomerDTO[] importCustomerDTOs = (ImportCustomerDTO[])xmlSerializer.Deserialize(reader);


            var mapper = GetMapper();
            Customer[] customers = mapper.Map<Customer[]>(importCustomerDTOs);

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }
        
        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSaleDTO[]), new XmlRootAttribute("Sales"));

            using var reader = new StringReader(inputXml);

            ImportSaleDTO[] importSaleDTOs = (ImportSaleDTO[])xmlSerializer.Deserialize(reader);

            int[] carIds = context.Cars.Select(c=>c.Id).ToArray();

            var mapper = GetMapper();
            Sale[] sales = mapper.Map<Sale[]>(importSaleDTOs)
                                    .Where(s=>carIds.Contains(s.CarId)).ToArray();

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        //14. Cars with Distance
        public static string GetCarsWithDistance(CarDealerContext context) 
        {
            var mapper = GetMapper();

            var cars = context.Cars
                .Where(c=>c.TraveledDistance>2_000_000)
                .OrderBy(c=>c.Make)
                    .ThenBy(c=>c.Model)
                .Take(10)
                .ProjectTo<ExportCarsWithDistance>(mapper.ConfigurationProvider)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportCarsWithDistance[]),new XmlRootAttribute("Cars"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(String.Empty, String.Empty);


            StringBuilder sb = new();
            using (StringWriter sw = new StringWriter(sb)) 
            {
                xmlSerializer.Serialize(sw, cars, xsn);
            }

            return sb.ToString().TrimEnd();
        }

        static Mapper GetMapper() 
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());
            return new Mapper(cfg);
        }
    }
}
namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            DateTime givenDate;

            if (!DateTime.TryParse(date, out givenDate))
            {
                throw new ArgumentException("Invalid date format!");
            }

            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerial = new XmlSerializer(typeof(ExportPatientDTO[]), new XmlRootAttribute("Patients"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter sw = new StringWriter(sb);

            ExportPatientDTO[] dtos = context.Patients.AsNoTracking()
                    .Where(p=>p.PatientsMedicines.Any(pm=>pm.Medicine.ProductionDate >= givenDate))
                    .Select(p=>new ExportPatientDTO 
                    {
                        Name = p.FullName,
                        AgeGroup = p.AgeGroup.ToString(),
                        Gender = p.Gender.ToString().ToLower(),
                        Medicines = p.PatientsMedicines
                                    .Where(pm => pm.Medicine.ProductionDate >= givenDate)
                                    .Select(pm => pm.Medicine)
                                    .OrderByDescending(m => m.ExpiryDate)
                                    .ThenBy(m => m.Price)
                                    .Select(m => new ExportMedicineDTO
                                    {
                                        Name = m.Name,
                                        Price = m.Price.ToString("F2"),
                                        Category = m.Category.ToString().ToLower(),
                                        Producer = m.Producer,
                                        BestBefore = m.ExpiryDate.ToString("yyyy-MM-dd")
                                    })
                                    .ToArray()
                    })
                    .OrderByDescending(x=>x.Medicines.Length)
                    .ThenBy(x=>x.Name)
                    .ToArray();

            xmlSerial.Serialize(sw, dtos, namespaces);


            return sb.ToString().TrimEnd();
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicines = context.Medicines.AsNoTracking()
                .Where(m => m.Category == (Category)medicineCategory && m.Pharmacy.IsNonStop)
                .Select(m => new
                {
                    Name = m.Name,
                    Price = m.Price.ToString("f2"),
                    Pharmacy = new
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }
                })
                .OrderBy(m => m.Price)
                .ThenBy(m => m.Name)
                .ToList();

            return JsonConvert.SerializeObject(medicines, Formatting.Indented);
        }
    }
}

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder(); 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPharmacyDTO[]), new XmlRootAttribute("Pharmacies"));

            using StringReader xmlReader = new StringReader(xmlString);
            ImportPharmacyDTO[] importPharmacyDTOs = xmlSerializer.Deserialize(xmlReader) as ImportPharmacyDTO[];

            List<Pharmacy> pharmacies = new List<Pharmacy>();

            foreach (var dto in importPharmacyDTOs) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy pharmacyToAdd = new Pharmacy() 
                {
                    Name = dto.Name,
                    PhoneNumber = dto.PhoneNumber,
                    IsNonStop = dto.IsNonStop == "true"?true:false
                };

                foreach(var medicineDto in dto.Medicines) 
                {
                    if (!IsValid(medicineDto)) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime productionDate;
                    bool isProductionValid = DateTime.TryParseExact(
                        medicineDto.ProductionDate, 
                        "yyyy-MM-dd", 
                        CultureInfo.InvariantCulture, 
                        DateTimeStyles.None, 
                        out productionDate);

                    if (!isProductionValid) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime expiryDate;
                    bool isExpiryDateValid = DateTime.TryParseExact(
                        medicineDto.ExpiryDate,
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out expiryDate);

                    if (!isExpiryDateValid) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (productionDate >= expiryDate) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (dto.Medicines.Any(m => m.Name == medicineDto.Name && m.Producer == medicineDto.Producer)) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    pharmacyToAdd.Medicines.Add(new Medicine 
                    {
                        Name = medicineDto.Name,
                        Price = medicineDto.Price,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        Category = (Category)medicineDto.Category,
                        Producer = medicineDto.Producer
                    });
                }
                pharmacies.Add(pharmacyToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, dto.Name, dto.Medicines.Length));
            }
            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}

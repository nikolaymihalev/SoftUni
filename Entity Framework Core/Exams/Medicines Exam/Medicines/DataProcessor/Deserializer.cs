namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Newtonsoft.Json;
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
            StringBuilder sb = new StringBuilder();

            ImportPatientsDTO[] importPatientsDTO = JsonConvert.DeserializeObject<ImportPatientsDTO[]>(jsonString);

            List<Patient> patients = new List<Patient>();

            foreach (var dto in importPatientsDTO) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Patient patient = new Patient()
                {
                    FullName = dto.FullName,
                    AgeGroup = (AgeGroup)dto.AgeGroup,
                    Gender = (Gender)dto.Gender
                };

                foreach (var id in dto.Medicines) 
                {
                    if (patient.PatientsMedicines.Any(x => x.MedicineId == id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    patient.PatientsMedicines.Add(new PatientMedicine() { Patient = patient, MedicineId = id });
                }

                patients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }

            context.Patients.AddRange(patients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder(); 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPharmacyDTO[]), new XmlRootAttribute("Pharmacies"));

            using StringReader xmlReader = new StringReader(xmlString);
            ImportPharmacyDTO[] importPharmacyDTOs = xmlSerializer.Deserialize(xmlReader) as ImportPharmacyDTO[];

            List<Pharmacy> pharmacies = new List<Pharmacy>();

            int counter= 0; 

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

                    if (pharmacyToAdd.Medicines.Any(m => m.Name == medicineDto.Name && m.Producer == medicineDto.Producer)) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    pharmacyToAdd.Medicines.Add(new Medicine 
                    {
                        Name = medicineDto.Name,
                        Price = (decimal)medicineDto.Price,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        Category = (Category)medicineDto.Category,
                        Producer = medicineDto.Producer
                    });
                    counter++;
                }
                pharmacies.Add(pharmacyToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacyToAdd.Name, pharmacyToAdd.Medicines.Count));
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

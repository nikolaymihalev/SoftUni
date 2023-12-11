namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
             StringBuilder sb =new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportDistrictDto[]), new XmlRootAttribute("Districts"));
            using StringReader stringReader = new StringReader(xmlDocument);

            ImportDistrictDto[] importDistrictDtos = xmlSerializer.Deserialize(stringReader) as ImportDistrictDto[];
            List<District> districts = new List<District>();

            foreach (var dto in importDistrictDtos) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                District distInDB = dbContext.Districts.FirstOrDefault(x => x.Name == dto.Name);
                if (distInDB != null) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                District district = new District() 
                {
                    Name = dto.Name,
                    PostalCode = dto.PostalCode,
                    Region = dto.Region
                };

                foreach(var property in dto.Properties) 
                {
                    if (!IsValid(property)) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime dateOfAcquisition;
                    bool isDateValid = DateTime.TryParseExact(
                        property.DateOfAcquisition,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dateOfAcquisition);
                    if (!isDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //----------PropertyIdentifier
                    if (district.Properties.Any(x => x.PropertyIdentifier == property.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Property propDB = dbContext.Properties.FirstOrDefault(x => x.PropertyIdentifier == property.PropertyIdentifier);
                    if (propDB != null) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //----------Address
                    if (district.Properties.Any(x => x.Address == property.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Property propDB_A = dbContext.Properties.FirstOrDefault(x => x.Address == property.Address);
                    if (propDB_A != null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    district.Properties.Add(new Property 
                    {
                        PropertyIdentifier = property.PropertyIdentifier,
                        Area = property.Area,
                        Details = property.Details,
                        Address = property.Address,
                        DateOfAcquisition = dateOfAcquisition,
                        District = district,
                    });
                }
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict,district.Name, district.Properties.Count));
                districts.Add(district);
            }

            dbContext.Districts.AddRange(districts);
            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();

            ImportCitizensDto[] importCitizensDtos = JsonConvert.DeserializeObject<ImportCitizensDto[]>(jsonDocument);
            List<Citizen> citizens = new List<Citizen>();

            foreach (var dto in importCitizensDtos) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (dto.MaritalStatus != "Unmarried" &&
                    dto.MaritalStatus!= "Married" &&
                    dto.MaritalStatus != "Divorced" &&
                    dto.MaritalStatus != "Widowed") 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime birthDate;
                bool isDateValid = DateTime.TryParseExact(
                    dto.BirthDate,
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out birthDate);
                if (!isDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                MaritalStatus maritalStatus=MaritalStatus.Unmarried;

                switch (dto.MaritalStatus) 
                {
                    case "Unmarried":maritalStatus=MaritalStatus.Unmarried; break;
                    case "Married": maritalStatus=MaritalStatus.Married; break;
                    case "Divorced": maritalStatus=MaritalStatus.Divorced; break;
                    case "Widowed": maritalStatus=MaritalStatus.Widowed; break;
                }

                Citizen citizen = new Citizen() 
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    BirthDate = birthDate,
                    MaritalStatus = maritalStatus
                };

                foreach (int id in dto.Properties) 
                {
                    citizen.PropertiesCitizens.Add(new PropertyCitizen { PropertyId = id , Citizen = citizen});
                }

                sb.AppendLine(string.Format(SuccessfullyImportedCitizen, citizen.FirstName, citizen.LastName, citizen.PropertiesCitizens.Count));
                citizens.Add(citizen);
            }

            dbContext.Citizens.AddRange(citizens);
            dbContext.SaveChanges();

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

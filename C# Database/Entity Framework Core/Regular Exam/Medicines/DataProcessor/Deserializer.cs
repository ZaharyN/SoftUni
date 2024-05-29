namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlTypes;
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
            StringBuilder sb = new();

            ImportPatientDTO[] patientsDTO = JsonConvert.DeserializeObject<ImportPatientDTO[]>(jsonString);
            List<Patient> patients = new List<Patient>();



            foreach (var patientDTO in patientsDTO)
            {
                if (!IsValid(patientDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Patient patient = new Patient()
                {
                    FullName = patientDTO.FullName,
                    AgeGroup = (AgeGroup)patientDTO.AgeGroup,
                    Gender = (Gender)patientDTO.Gender,
                };
                
                foreach (int medicineId in patientDTO.Medicines)
                {
                    int[] patientMedicinesIds = patient.PatientsMedicines.Select(pm => pm.MedicineId).ToArray();

                    if (patientMedicinesIds.Contains(medicineId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    patient.PatientsMedicines.Add(new PatientMedicine
                    {
                        MedicineId = medicineId
                    });
                }

                patients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }
            context.AddRange(patients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            StringReader reader = new StringReader(xmlString);

            XmlSerializer serializer =
                new XmlSerializer(typeof(ImportPharmacyDTO[]), new XmlRootAttribute("Pharmacies"));

            ImportPharmacyDTO[] pharmaciesDTO = serializer.Deserialize(reader) as ImportPharmacyDTO[];

            List<Pharmacy> pharmacies = new List<Pharmacy>();

            foreach (var pharmacyDTO in pharmaciesDTO)
            {
                if (!IsValid(pharmacyDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if(pharmacyDTO.IsNonStop != "true" && pharmacyDTO.IsNonStop != "false")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy pharmacy = new Pharmacy()
                {
                    Name = pharmacyDTO.Name,
                    PhoneNumber = pharmacyDTO.PhoneNumber,
                    IsNonStop = bool.Parse(pharmacyDTO.IsNonStop)
                };

                foreach (var medicineDTO in pharmacyDTO.Medicines)
                {
                    if (!IsValid(medicineDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if(medicineDTO.Producer == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime productionDate;
                    bool validate = DateTime.TryParseExact
                        (medicineDTO.ProductionDate, 
                        "yyyy-MM-dd",CultureInfo.InvariantCulture,
                        DateTimeStyles.None, 
                        out productionDate);

                    DateTime expiryDate;
                    bool validateExpiry = DateTime.TryParseExact
                        (medicineDTO.ExpiryDate,
                        "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out expiryDate);

                    if(productionDate.Year == expiryDate.Year)
                    {
                        if(productionDate.Day >= expiryDate.Day)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                    }

                    if(pharmacy.Medicines.Any(m => m.Name == medicineDTO.Name 
                        && m.Producer == medicineDTO.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    pharmacy.Medicines.Add(new Medicine
                    {
                        Name = medicineDTO.Name,
                        Category = (Category)medicineDTO.Category,
                        Price = medicineDTO.Price,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        Producer = medicineDTO.Producer
                    });

                }

                pharmacies.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
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

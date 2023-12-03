namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            StringBuilder sb = new();

            //bool validate = DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime inputDate);

            var parsedDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            ExportPatientDTO[] patients = context.Patients
                .Where(p => p.PatientsMedicines.Any(pm => pm.Medicine.ProductionDate > parsedDate)) 
                .ToArray()
                .Select(p => new ExportPatientDTO
                {
                    Gender = p.Gender.ToString(),
                    Name = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Medicines = p.PatientsMedicines
                    .Where(pm => pm.Medicine.ProductionDate > parsedDate)
                    .OrderByDescending(pm => pm.Medicine.ExpiryDate)
                        .ThenBy(pm => pm.Medicine.Price)
                    .Select(pm => new ExportMedicineDTO
                    {
                        Category = pm.Medicine.Category.ToString().ToLower(),
                        Name = pm.Medicine.Name,
                        Price = pm.Medicine.Price.ToString("f2"),
                        Producer = pm.Medicine.Producer,
                        BestBefore = pm.Medicine.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    })
                    .ToArray()
                })
                .OrderByDescending(p => p.Medicines.Count())
                    .ThenBy(p => p.Name)
                .ToArray();

            XmlSerializer serializer =
                new XmlSerializer(typeof(ExportPatientDTO[]), new XmlRootAttribute("Patients"));

            XmlSerializerNamespaces xmlns = new();
            xmlns.Add(string.Empty, string.Empty);

            using (StringWriter sw = new(sb))
            {
                serializer.Serialize(sw, patients, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {

            var medicines = context.Medicines
                .Where(m => m.Category == (Category)medicineCategory
                && m.Pharmacy.IsNonStop == true)
                .ToArray()
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
                .ToArray();

            return JsonConvert.SerializeObject(medicines, Formatting.Indented);
        }
    }
}

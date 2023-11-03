using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            Console.WriteLine(RemoveTown(context));
        }

        //03.
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employeesInfo = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            return string.Join(Environment.NewLine, employeesInfo.Select(e =>
                    $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}"));
        }
        //04.
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .ToList();

            return string.Join(Environment.NewLine, employees.Select(e =>
                    $"{e.FirstName} - {e.Salary:f2}"));
        }
        //05.
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department,
                    e.Salary
                })
              .Where(d => d.Department.Name == "Research and Development")
              .OrderBy(s => s.Salary)
              .ThenByDescending(f => f.FirstName)
              .ToList();

            return string.Join(Environment.NewLine, employees.Select(e =>
                    $"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}"));
        }
        //06.
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                AddressId = 4
            };

            var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;
            context.SaveChanges();

            var employeesAddress = context.Employees
                .Select(e => new
                {
                    e.Address
                })
                .OrderByDescending(e => e.Address.AddressId)
                .Take(10)
                .ToList();

            return string.Join(Environment.NewLine, employeesAddress.Select(e =>
                    e.Address.AddressText));
        }
        //07.
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                //.Include(e => e.EmployeesProjects)
                //.ThenInclude(ep => ep.Project)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Manager,
                    Projects = e.EmployeesProjects
                        .Select(ep => new
                        {
                            ep.Project.Name,
                            ep.Project.StartDate,
                            ep.Project.EndDate
                        }).ToList()
                })
                .Take(10)
                .ToList();

            StringBuilder result = new();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - " +
                    $"Manager: {employee.Manager.FirstName} {employee.Manager.LastName}");

                if (!employee.Projects.Any())
                {
                    continue;
                }
                else
                {
                    foreach (var project in employee.Projects)
                    {

                        string projectStartDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                        string projectEndDate = project.EndDate.HasValue
                            ? project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                            : "not finished";
                        if (project.StartDate.Year >= 2001 && project.StartDate.Year <= 2003)
                        {
                            result.AppendLine($"--{project.Name} - {projectStartDate} - {projectEndDate}");
                        }
                    }
                }
            }

            return result.ToString().Trim();
        }
        //08.
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Select(ad => new
                {
                    ad.AddressText,
                    ad.Town,
                    ad.Employees
                })
                .OrderByDescending(ad => ad.Employees.Count)
                .ThenBy(ad => ad.Town.Name)
                .ThenBy(ad => ad.AddressText)
                .Take(10)
                .ToList();

            return string.Join(Environment.NewLine, addresses.Select(ad =>
                    $"{ad.AddressText}, {ad?.Town?.Name} - {ad?.Employees.Count} employees"));
        }
        //09.
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Include(e => e.EmployeesProjects)
                    .ThenInclude(ep => ep.Project)
                 .Where(e => e.EmployeeId == 147)
                 .FirstOrDefault();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var employeeProject in employee.EmployeesProjects.OrderBy(ep => ep.Project.Name))
            {
                sb.AppendLine(employeeProject.Project.Name);
            }

            return sb.ToString().Trim();
        }
        //10.
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Employees)
                .Where(d => d.Employees.Count > 5)
                .ToList();

            StringBuilder sb = new();

            foreach (var department in departments
                        .OrderBy(d => d.Employees.Count)
                        .ThenBy(d => d.Name))
            {
                sb.AppendLine($"{department.Name} - {department.Manager.FirstName} {department.Manager.LastName}");

                foreach (var employee in department.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return sb.ToString().Trim();
        }
        //11.
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            StringBuilder sb = new();

            foreach (var project in projects)//.OrderBy(p => p.Name))
            {
                sb.AppendLine(project.Name);
                sb.AppendLine(project.Description);
                sb.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return sb.ToString().Trim();
        }
        //12.
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                .Include(e => e.Department)
                .Where(e => e.Department.Name == "Engineering"
                    || e.Department.Name == "Tool Design"
                    || e.Department.Name == "Marketing"
                    || e.Department.Name == "Information Services")
                .ToList();

            StringBuilder sb = new();

            foreach (var employee in employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName))
            {
                employee.Salary *= 1.12m;

                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }
            context.SaveChanges();

            return sb.ToString().Trim();
        }
        //13.
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .ToList();

            return string.Join(Environment.NewLine, employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})"));
        }
        //14.
        public static string DeleteProjectById(SoftUniContext context)
        {

            foreach (var employeeProject in context.EmployeesProjects)
            {
                if(employeeProject.ProjectId == 2)
                {
                    context.EmployeesProjects.Remove(employeeProject);
                }
            }
            context.SaveChanges();

            context.Projects.Remove(context.Projects.SingleOrDefault(p => p.ProjectId == 2));
            context.SaveChanges();

            var projects = context.Projects
                .Take(10)
                .ToList();

            return string.Join(Environment.NewLine, projects.Select(p =>
                        $"{p.Name}"));
        }
        //15.
        public static string RemoveTown(SoftUniContext context)
        {
            foreach (var employee in context.Employees
                .Include(e => e.Address)
                .ThenInclude(t => t.Town))
            {
                if(employee.Address.Town.Name == "Seattle")
                {
                    employee.AddressId = null;
                }
            }
            context.SaveChanges();

            int addressesRemoved = 0;
            foreach (var address in context.Addresses
                .Include(a => a.Town))
            {
                if(address.Town.Name == "Seattle")
                {
                    context.Addresses.Remove(address);
                    addressesRemoved++;
                }
            }
            context.SaveChanges();

            context.Towns.Remove(context.Towns.SingleOrDefault(t => t.Name == "Seattle"));
            context.SaveChanges();

            return $"{addressesRemoved} addresses in Seattle were deleted";
        }
    }
}

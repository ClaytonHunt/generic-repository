using System;
using System.Linq;
using Payroll.Data.Entities;

namespace Payroll.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PayrollContext context)
        {
            context.Database.EnsureCreated();

            // Look for any employees.
            if (context.Employees.Any()) return;   // DB has been seeded

            var employees = new[]
            {
                new Employee { SocialSecurityNumber = "161-12-1091", FirstName = "Mufutau", LastName = "Fowler", Type = EmployeeType.Hourly },
                new Employee { SocialSecurityNumber = "165-10-4580", FirstName = "Cathleen", LastName = "Turner", Type = EmployeeType.Commission },
                new Employee { SocialSecurityNumber = "160-06-4579", FirstName = "Jessamine", LastName = "Weber", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "164-08-9266", FirstName = "Deirdre", LastName = "Bailey", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "163-12-9538", FirstName = "Fuller", LastName = "Hammond", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "160-10-8558", FirstName = "Wing", LastName = "Shaffer", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "167-08-0478", FirstName = "Driscoll", LastName = "Carr", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "169-08-3780", FirstName = "Florence", LastName = "Tran", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-03-3150", FirstName = "Laurel", LastName = "Scott", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "164-12-9071", FirstName = "Eagan", LastName = "Shelton", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "164-10-2486", FirstName = "Kylee", LastName = "Dyer", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-08-7241", FirstName = "Lewis", LastName = "Lawrence", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "167-06-5455", FirstName = "Jerry", LastName = "Gallagher", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "164-01-2201", FirstName = "Trevor", LastName = "Warren", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "161-08-7582", FirstName = "Calvin", LastName = "Levine", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "160-05-9326", FirstName = "Nissim", LastName = "Curry", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "162-05-3758", FirstName = "Stephanie", LastName = "Clay", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "164-01-3388", FirstName = "Jordan", LastName = "Case", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "169-04-2444", FirstName = "Kenyon", LastName = "Orr", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-01-8941", FirstName = "Minerva", LastName = "Sargent", Type = EmployeeType.Hourly },
                new Employee { SocialSecurityNumber = "160-10-2533", FirstName = "Jonah", LastName = "Banks", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "168-06-1898", FirstName = "Leilani", LastName = "Kerr", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "161-04-9789", FirstName = "Kenyon", LastName = "Tran", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "164-12-2774", FirstName = "Baxter", LastName = "Cook", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "161-11-6088", FirstName = "Destiny", LastName = "Pennington", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "162-01-1569", FirstName = "Carter", LastName = "Fitzpatrick", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "164-07-3638", FirstName = "Charlotte", LastName = "Casey", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "167-11-9849", FirstName = "Signe", LastName = "Pennington", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-12-2220", FirstName = "Basia", LastName = "Mcguire", Type = EmployeeType.Salary },
                new Employee { SocialSecurityNumber = "166-08-0521", FirstName = "Petra", LastName = "Bond", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "163-06-2030", FirstName = "Marah", LastName = "Walter", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "168-05-9618", FirstName = "Petra", LastName = "Simon", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-02-6879", FirstName = "Brennan", LastName = "Johnston", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "166-06-7460", FirstName = "Leslie", LastName = "Hyde", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "161-01-1776", FirstName = "Oscar", LastName = "Osborn", Type = EmployeeType.Hourly  },
                new Employee { SocialSecurityNumber = "168-01-8317", FirstName = "Rhonda", LastName = "Cummings", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "164-07-5835", FirstName = "Florence", LastName = "Camacho", Type = EmployeeType.Commission },
                new Employee { SocialSecurityNumber = "160-01-0183", FirstName = "Jamal", LastName = "Snider", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "168-04-6869", FirstName = "Addison", LastName = "Brennan", Type = EmployeeType.Hourly },
                new Employee { SocialSecurityNumber = "162-07-6843", FirstName = "Xandra", LastName = "Jacobson", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "160-05-3705", FirstName = "Evangeline", LastName = "Mercado", Type = EmployeeType.Hourly },
                new Employee { SocialSecurityNumber = "165-07-4440", FirstName = "Barbara", LastName = "Oliver", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "162-01-0722", FirstName = "Ima", LastName = "Lang", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-05-7369", FirstName = "Maite", LastName = "Charles", Type = EmployeeType.Salary },
                new Employee { SocialSecurityNumber = "167-08-6431", FirstName = "Herrod", LastName = "Rutledge", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "161-08-2148", FirstName = "Logan", LastName = "Melendez", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "168-06-8310", FirstName = "Ivan", LastName = "Brewer", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "166-01-8283", FirstName = "Jana", LastName = "Sykes", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "169-01-0234", FirstName = "Kimberley ", LastName = "Hendricks", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "160-12-5525", FirstName = "Cathleen  ", LastName = "Sanders", Type = EmployeeType.Salary },
                new Employee { SocialSecurityNumber = "165-02-9690", FirstName = "Connor", LastName = "Rios", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "163-07-4549", FirstName = "Madaline", LastName = "Medina", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "165-02-5128", FirstName = "Maxwell", LastName = "Drake", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "162-01-7699", FirstName = "Ahmed", LastName = "Gray", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "167-09-7007", FirstName = "Edward", LastName = "Hanson", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "160-01-0201", FirstName = "Brielle", LastName = "Hodges", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "163-01-2677", FirstName = "Leo", LastName = "Branch", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "168-06-3431", FirstName = "Odessa", LastName = "Sutton", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "166-02-0886", FirstName = "Anne", LastName = "Blackburn", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "167-10-6739", FirstName = "Cooper", LastName = "Cook", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "167-09-9818", FirstName = "Jeanette", LastName = "Franks", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "169-07-6691", FirstName = "Alana", LastName = "Watson", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "164-12-4897", FirstName = "Callum", LastName = "Guzman", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "163-02-6228", FirstName = "Bruno", LastName = "Matthews", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "166-06-6482", FirstName = "Kirk", LastName = "Franklin", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "166-12-6178", FirstName = "Abdul", LastName = "Singleton", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "168-11-4806", FirstName = "Moses", LastName = "Peters", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "162-03-0624", FirstName = "Thaddeus", LastName = "Johns", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "166-04-4747", FirstName = "Giacomo", LastName = "Barker", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "163-09-1512", FirstName = "Tad", LastName = "Keith", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "162-11-8723", FirstName = "Dominic", LastName = "Daniels", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "165-02-9604", FirstName = "Noble", LastName = "Cummings", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "161-11-6683", FirstName = "Ebony", LastName = "Mccoy", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "167-01-3858", FirstName = "Sara", LastName = "Acevedo", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "163-11-9386", FirstName = "Samantha", LastName = "Morales", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "162-01-9946", FirstName = "Connor", LastName = "Flores", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "162-02-0886", FirstName = "Abraham", LastName = "Taylor", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "168-01-5540", FirstName = "Cecilia", LastName = "Mcclure", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "161-01-8562", FirstName = "Scarlett", LastName = "Gamble", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "165-08-6455", FirstName = "Wesley", LastName = "Rice", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "169-07-2212", FirstName = "Leigh", LastName = "Hobbs", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "161-08-0930", FirstName = "Riley", LastName = "Emerson", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "168-04-0345", FirstName = "Elliott", LastName = "Walker", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "161-05-9125", FirstName = "Hammett", LastName = "Puckett", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "167-07-3582", FirstName = "Leonard", LastName = "Park", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "160-12-2736", FirstName = "Rae", LastName = "Fletcher", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "168-04-6104", FirstName = "Patience", LastName = "Hutchinson", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "165-03-9234", FirstName = "Scarlett", LastName = "Witt", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "163-03-6416", FirstName = "Aaron", LastName = "Harrison", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "163-03-6605", FirstName = "Kim", LastName = "Ratliff", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "162-06-7863", FirstName = "Keegan", LastName = "Oneal", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "162-08-0701", FirstName = "Gloria", LastName = "Herman", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "169-05-4162", FirstName = "Stacy", LastName = "Mcdowell", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "168-09-3745", FirstName = "Hayley", LastName = "Hicks", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "167-07-4830", FirstName = "Levi", LastName = "Mckee", Type = EmployeeType.Salary},
                new Employee { SocialSecurityNumber = "161-11-1041", FirstName = "Walker", LastName = "Johnston", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "165-03-9847", FirstName = "Wayne", LastName = "Adams", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "163-10-4874", FirstName = "Mari", LastName = "Rogers", Type = EmployeeType.Commission},
                new Employee { SocialSecurityNumber = "167-11-0679", FirstName = "Katell", LastName = "Boone", Type = EmployeeType.Hourly},
                new Employee { SocialSecurityNumber = "166-05-4041", FirstName = "Yeo", LastName = "Juarez", Type = EmployeeType.Commission}
            };

            foreach (var employee in employees)
            {
                context.Employees.Add(employee);
            }

            context.SaveChanges();

            AddHourlyEmployeeData(context, employees);
            AddSalaryEmployeeData(context, employees);
            AddCommissionEmployeeData(context, employees);
        }

        private static void AddHourlyEmployeeData(PayrollContext context, Employee[] employees)
        {
            foreach (var employee in employees.Where(x => x.Type == EmployeeType.Hourly))
            {
                var rate = new HourlyPayRate { Rate = new Random().Next(8, 15) };
                employee.HourlyPayRate = rate;

                context.HourlyPayRates.Add(rate);

                var range = Enumerable.Range(0, DateTime.Now.DayOfYear - 1);

                var firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                foreach (var day in range)
                {
                    var currentDay = firstDayOfYear.AddDays(day);

                    if (!IsWeekend(currentDay))
                    {
                        var hours = new EmployeeHour { Date = currentDay, Employee = employee, Hours = 8 };

                        context.EmployeeHours.Add(hours);
                    }
                }
            }

            context.SaveChanges();
        }

        private static void AddSalaryEmployeeData(PayrollContext context, Employee[] employees)
        {
            foreach (var employee in employees.Where(x => x.Type == EmployeeType.Salary))
            {
                var rate = new SalaryPayRate { AnnualRate = new Random().Next(60, 115) * 1000 };
                employee.SalaryPayRate = rate;

                context.SalaryPayRates.Add(rate);
                context.SaveChanges();

                employee.SalaryPayRateId = rate.Id;                
            }

            context.SaveChanges();
        }

        private static void AddCommissionEmployeeData(PayrollContext context, Employee[] employees)
        {
            foreach (var employee in employees.Where(x => x.Type == EmployeeType.Commission))
            {
                var salaryPayRate = new SalaryPayRate { AnnualRate = new Random().Next(40, 85) * 1000 };
                employee.SalaryPayRate = salaryPayRate;

                context.SalaryPayRates.Add(salaryPayRate);

                var commissionPayRate = new CommissionPayRate { CommissionRate = new Random().Next(5, 20) / 100F };
                employee.CommissionPayRate = commissionPayRate;

                context.CommissionPayRates.Add(commissionPayRate);

                var range = Enumerable.Range(0, DateTime.Now.DayOfYear - 1);

                var firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                foreach (var day in range)
                {
                    var currentDay = firstDayOfYear.AddDays(day);

                    if (!IsWeekend(currentDay) && new Random().Next(100) >= 80)
                    {
                        var sale = new EmployeeSale { Date = currentDay, Employee = employee, Amount = new Random().Next(1, 50) * 1000 };

                        context.EmployeeSales.Add(sale);
                    }
                }
            }

            context.SaveChanges();
        }

        private static bool IsWeekend(DateTime day)
        {
            return day.DayOfWeek == DayOfWeek.Saturday ||
                   day.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}

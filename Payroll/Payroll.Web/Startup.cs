using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Business;
using Payroll.Business.interfaces;
using Payroll.Business.Models;
using Payroll.Data;
using Payroll.Data.Mappers;
using EmployeeDto = Payroll.Business.Models.Employee;
using EmployeeEntity = Payroll.Data.Entities.Employee;

namespace Payroll.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PayrollContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.AddScoped<DbContext, PayrollContext>();
            services.AddScoped<EmployeeService, EmployeeService>();
            services.AddScoped<IMapper<EmployeeDto, EmployeeEntity>, EmployeeMapper>();
            services.AddScoped<IMapper<HourlyEmployee, EmployeeEntity>, HourlyEmployeeMapper>();
            services.AddScoped<IMapper<SalaryEmployee, EmployeeEntity>, SalaryEmployeeMapper>();
            services.AddScoped<IMapper<CommissionEmployee, EmployeeEntity>, CommissionEmployeeMapper>();
            services.AddScoped<IRepository<EmployeeDto>, GenericRepository<EmployeeDto, EmployeeEntity>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

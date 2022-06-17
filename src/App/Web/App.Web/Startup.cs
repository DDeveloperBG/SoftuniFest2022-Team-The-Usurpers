namespace App.Web
{
    using System;
    using System.Reflection;

    using App.Common;
    using App.Data;
    using App.Data.Common;
    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Data.Repositories;
    using App.Data.Seeding;
    using App.Services.Data.UpdateRecords;
    using App.Services.Mapping;
    using App.Services.Messaging;
    using App.Web.ViewModels;

    using Hangfire;
    using Hangfire.Dashboard;
    using Hangfire.SqlServer;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment currentEnvironment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment currentEnvironment)
        {
            this.configuration = configuration;
            this.currentEnvironment = currentEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHangfire(
               config => config
                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseRecommendedSerializerSettings()
                   .UseSqlServerStorage(
                       this.configuration.GetConnectionString("DefaultConnection"),
                       new SqlServerStorageOptions
                       {
                           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                           QueuePollInterval = TimeSpan.Zero,
                           UseRecommendedIsolationLevel = true,
                           UsePageLocksOnDequeue = true,
                           DisableGlobalLocks = true,
                       }));
            services.AddHangfireServer();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IUpdateRecordsService>(_
                => new UpdateRecordsService(
                        this.configuration["DWH:Url"],
                        this.configuration["DWH:AccessToken"]));
        }

        public void Configure(IApplicationBuilder app, IRecurringJobManager recurringJobManager)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                this.SeedHangfireJobs(recurringJobManager);

                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (this.currentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHangfireDashboard(
                "/hangfire",
                new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
              endpoints =>
              {
                  endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                  endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                  endpoints.MapRazorPages();
              });
        }

        private void SeedHangfireJobs(IRecurringJobManager recurringJobManager)
        {
            //recurringJobManager
            //    .AddOrUpdate<IMontlyPaymentsService>(
            //        "UpdateShopkeepers",
            //        x => x.PayMontlySalariesAsync(),
            //        Cron.Hourly);

            //recurringJobManager
            //    .AddOrUpdate<IMontlyPaymentsService>(
            //        "UpdateBankEmployees",
            //        x => x.PayMontlySalariesAsync(),
            //        Cron.Hourly);

            //recurringJobManager
            //    .AddOrUpdate<IMontlyPaymentsService>(
            //        "UpdateTerminals",
            //        x => x.PayMontlySalariesAsync(),
            //        Cron.Hourly);
        }

        private class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            }
        }
    }
}

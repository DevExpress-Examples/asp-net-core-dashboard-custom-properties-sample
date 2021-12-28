using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;

namespace CustomPropertiesSample {
    public class Startup {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) {
            Configuration = configuration;
            FileProvider = hostingEnvironment.ContentRootFileProvider;
            DashboardExportSettings.CompatibilityMode = DashboardExportCompatibilityMode.Restricted;
        }

        public IFileProvider FileProvider { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services
                .AddResponseCompression()
                .AddDevExpressControls()
                .AddMvc();

            services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {
                DashboardConfigurator configurator = new DashboardConfigurator();
                configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(Configuration));

                DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(FileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
                configurator.SetDashboardStorage(dashboardFileStorage);

                DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

                // Registers an SQL data source.
                DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionString");
                sqlDataSource.DataProcessingMode = DataProcessingMode.Client;
                SelectQuery query = SelectQueryFluentBuilder
                    .AddTable("Categories")
                    .Join("Products", "CategoryID")
                    .SelectAllColumns()
                    .Build("Products_Categories");
                sqlDataSource.Queries.Add(query);
                dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

                configurator.SetDataSourceStorage(dataSourceStorage);

                configurator.ConfigureItemDataCalculation += (s, e) => {
                    e.CalculateAllTotals = true;
                };
                return configurator;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDevExpressControls();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                EndpointRouteBuilderExtension.MapDashboardRoute(endpoints, "api/dashboard", "DefaultDashboard");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using System.Reflection;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQL"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                }));
            
            services.AddTransient<IWarehouseAdminService, WarehouseAdminService>();
            services.AddTransient<IWarehouseManagerService, WarehouseManagerService>();
            services.AddTransient<IWarehouseCustomersService, WarehouseCustomersService>();
            services.AddTransient<IWarehouseAuthService, WarehouseAuthService>();
            services.AddTransient<IWarehouseUserService, WarehouseUserService>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<ISessionsRepository, SessionsRepository>();
            services.AddControllers();

            services.AddApplicationInsightsTelemetry();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context!.Database.Migrate();
        }
    }
}
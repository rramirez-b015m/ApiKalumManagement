using Microsoft.EntityFrameworkCore;
using KalumManagement.Helpers;
using KalumManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KalumManagement
{
    public class Startup
    {
        private readonly string OriginKalum = "OriginKalum";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(name: OriginKalum,builder =>{
                   builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey
              (
                Encoding.UTF8.GetBytes(this.Configuration["JWT:key"])

              ),
                ClockSkew = TimeSpan.Zero

            });
            services.AddTransient<IQueueService,QueueAspiranteService>();
            services.AddControllers();
            services.AddDbContext<KalumDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddResponseCaching();
            services.AddControllers(options => options.Filters.Add(typeof(ErrorFilterException)));
            services.AddControllers(options => options.Filters.Add(typeof(ErrorFilterResponseException)));
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseCors(this.OriginKalum);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            { endpoints.MapControllers(); });
        }
    }
}
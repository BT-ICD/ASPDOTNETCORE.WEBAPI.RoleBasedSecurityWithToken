using System.Text;
using AspNetCore3JWT.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

//Deployed on local IIS - http://localhost:7458/Api/ServerType/GetServerTypes
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//https://dotnetdetail.net/asp-net-core-3-0-web-api-token-based-authentication-example-using-jwt-in-vs2019/
//To solve error: System.ArgumentOutOfRangeException: IDX10603: Decryption failed. Keys tried: '[PII is hidden: https://thecodebuzz.com/idx10603-decryption-failed-keys-tried-pii-hidden-keysize/
//https://www.youtube.com/watch?v=MGCC2zTb0t4
//https://www.youtube.com/watch?v=vEU9SDmIvVY
//https://www.youtube.com/watch?v=M6AkbBaDGJE
//https://www.youtube.com/watch?v=LKveAwao9HA
//https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x?view=aspnetcore-3.1
//To enable CORS - https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1
namespace AspNetCore3JWT
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://github.com/dotnet/aspnetcore/issues/13564

            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc();
            string cnnString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(cnnString));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cnnString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //To enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder=>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey=true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidAudience = "http://abc.net",
                    //ValidIssuer = "http://abc.net",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaa"))
                };
            });
        }
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xecretKeywqejaneHelloWorkdSecre"))
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            SeedDB.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();
            //To enable CORS
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

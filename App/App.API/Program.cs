using App.Repositories;
using APP.Domain.Entities;
using App.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.Service;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Http.Headers;
using System.Net;
using App.API.Helper;
using System.Reflection;
using App.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. ( configure services )

#region configure_services
#region Cors 

//enable cors origin to accept any request from any calller
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
// to customize 
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.WithOrigins("http://localhost:4200")
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});
//then use this 
// app.UseCors();
#endregion

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));






builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryBase<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();



builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<IClassRoomService, ClassRoomService>();
builder.Services.AddTransient<IUserService, UserService>();





builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<IVirtualFileProvider>(new VirtualFileProvider(builder.Configuration["PhysicalFilesUploadPath"]/*, env.EnvironmentName == "Development", env.ContentRootPath*/));

builder.Services.AddSwaggerGen(c =>
               {
                   c.SchemaFilter<EnumSchemaFilter>();

                   c.SwaggerDoc("Auth", new OpenApiInfo { Title = "Auth", Version = "v1" });
                   c.SwaggerDoc("ClassRoom", new OpenApiInfo { Title = "ClassRoom", Version = "v1" });
                   c.SwaggerDoc("Files", new OpenApiInfo { Title = "Files", Version = "v1" });
                   c.SwaggerDoc("Student", new OpenApiInfo { Title = "Student", Version = "v1" });
                   c.SwaggerDoc("Subject", new OpenApiInfo { Title = "Subject", Version = "v1" });
                   ////////setupAction.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                   // Include 'SecurityScheme' to use JWT Authentication
                   var jwtSecurityScheme = new OpenApiSecurityScheme
                   {
                       Scheme = "bearer",
                       BearerFormat = "JWT",
                       Name = "JWT Authentication",
                       In = ParameterLocation.Header,
                       Type = SecuritySchemeType.Http,
                       Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                       Reference = new OpenApiReference
                       {
                           Id = JwtBearerDefaults.AuthenticationScheme,
                           Type = ReferenceType.SecurityScheme
                       }
                   };
                   c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                   c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });

               }

                              );

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline. (configure) 
#region configure

app.UseCors("MyCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerAuthorized();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/Auth/swagger.json", "Auth");
        c.SwaggerEndpoint("/swagger/ClassRoom/swagger.json", "ClassRoom");
        c.SwaggerEndpoint("/swagger/Files/swagger.json", "Files");
        c.SwaggerEndpoint("/swagger/Student/swagger.json", "Student");
        c.SwaggerEndpoint("/swagger/Subject/swagger.json", "Subject");

    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();





public class SwaggerBasicAuthMiddleware
{
    private readonly RequestDelegate next;
    public SwaggerBasicAuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the credentials from request header
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var inBytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                // validate credentials
                if (username.Equals("Admin") && password.Equals("Admin"))
                {
                    await next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}
public static class AuthorizedSampleClass
{
    public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
    }
}
public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumValues = schema.Enum.ToArray();
            var i = 0;
            foreach (var n in Enum.GetNames(context.Type).ToList())
            {
                schema.Description += n + $" = {((OpenApiPrimitive<int>)enumValues[i]).Value}, ";
                i++;
            }
            schema.Description = schema.Description.Substring(0, schema.Description.Length - 2);
        }
    }
}

#endregion

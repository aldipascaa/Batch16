
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HospitalPatient.Data;
using HospitalPatient.Mapping;
using HospitalPatient.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// EF Core SQLite
builder.Services.AddDbContext<HospitalDb>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Controllers
builder.Services.AddControllers();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// JWT
var jwt = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = true;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Secret"]!))
        };
    });
builder.Services.AddAuthorization();

// Swagger (Swashbuckle v10)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HospitalPatient API", Version = "v1" });

    var scheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    };
    c.AddSecurityDefinition("bearer", scheme);
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});

builder.Services.AddIdentityCore<IdentityUser>(options=>
{
    options.User.RequireUniqueEmail = true;
    options.Password = new PasswordOptions
    {
            RequireDigit = true,
            RequiredLength = 8,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = false

    };
})
.AddRoles<IdentityRole>();

// DI: Services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

// Apply migrations + seed sample data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HospitalDb>();
    db.Database.Migrate();

    if (!db.Departments.Any())
    {
        db.Departments.AddRange(
            new HospitalPatient.Models.IDepartment { Name = "Internal Medicine" },
            new HospitalPatient.Models.IDepartment { Name = "Cardiology" });
        db.SaveChanges();
    }

    if (!db.Doctors.Any())
    {
        var dep = db.Departments.First();
        db.Doctors.AddRange(
            new HospitalPatient.Models.IDoctor { FullName = "Dr. Raka", LicenseNumber = "LIC-IM-001", DepartmentId = dep.Id },
            new HospitalPatient.Models.IDoctor { FullName = "Dr. Sari", LicenseNumber = "LIC-CARD-002", DepartmentId = dep.Id });
        db.SaveChanges();
    }

    if (!db.Patients.Any())
    {
        db.Patients.Add(new HospitalPatient.Models.IPatient
        {
            MedicalRecordNumber = "MRN-0001",
            FullName = "Aldi P. N.",
            DateOfBirth = new DateTime(1997, 1, 10),
            Phone = "0812-xxx-xxx",
            Email = "aldi@example.com"
        });
        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalPatient API v1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

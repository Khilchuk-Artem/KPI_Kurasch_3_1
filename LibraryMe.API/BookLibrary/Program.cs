using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Quartz;
using BookLibrary.Jobs;
//using BookLibrary.BAL.Services.Interfaces;
//using BookLibrary.DAL.Repositories.Interfaces;
//using BookLibrary.BAL.Services.Implementations;
//using BookLibrary.DAL.Repositories.Implementations;
using BookLibrary.DAL.Repositories.Interfaces;
using BookLibrary.DAL.Repositories.Implementations;
using BookLibrary.DAL.Mappings;

using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.BAL.Services.Implementations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Goods Storage Api", Version = "v1" });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
            });
});

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookLibrary.DAL.Data.BookLibraryDbContext>(options =>
{
    options.UseSqlServer(connection, b => b.MigrationsAssembly("BookLibrary"));
});

builder.Services.AddQuartz(configure =>
{
    var borrowingsJobKey = new JobKey(nameof(ExpireBorrowingsJob));
    configure
        .AddJob<ExpireBorrowingsJob>(borrowingsJobKey)
        .AddTrigger(
            trigger => trigger.ForJob(borrowingsJobKey).WithSimpleSchedule(
                schedule => schedule.WithIntervalInHours(6).RepeatForever()));

    var reservationsJobKey = new JobKey(nameof(ExpireReservationsJob));
    configure
        .AddJob<ExpireReservationsJob>(reservationsJobKey)
        .AddTrigger(
            trigger => trigger.ForJob(reservationsJobKey).WithSimpleSchedule(
                schedule => schedule.WithIntervalInHours(2).RepeatForever()));

    configure.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfiles)));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUserSummaryService,UserSummaryService>();

builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementService,AnnouncementService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IBorrowingService, BorrowingService>();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<IVisitorCardRepository, VisitorCardRepository>();
builder.Services.AddScoped<IVisitorCardService, VisitorCardService>();

builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("BookLibrary")
    .AddEntityFrameworkStores<BookLibrary.DAL.Data.BookLibraryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            AuthenticationType="Jwt",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath="/Images"
});
app.MapControllers();

app.Run();

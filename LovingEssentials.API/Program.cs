using LovingEssentials.API.Helpers;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.Seed;
using LovingEssentials.Repository.IRepository;
using LovingEssentials.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Net.payOS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ssZ"));
    });
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<BrandDAO>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();

builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<OrderDAO>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<CartDAO>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<AddressDAO>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
builder.Services.AddSingleton(payOS);

builder.Services.AddScoped<StoreDAO>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher<User>>();

    await context.Database.MigrateAsync();
    await Seed.SeedUser(context, passwordHasher);
    await Seed.SeedBrand(context);
    await Seed.SeedCategory(context);
    await Seed.SeedProduct(context);
    await Seed.SeedOrders(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding data");
}
app.Run();


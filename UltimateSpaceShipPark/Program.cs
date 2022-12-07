using CarAccessService;
using Microsoft.EntityFrameworkCore;
using UltimateSpaceShipPark;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContextPool<ApplicationDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IParkingLotRepository, SQLParkingSpotRepository>();
builder.Services.AddScoped<ISpaceShipRepository, SQLSpaceShipRepository>();
builder.Services.AddScoped<SeedData>();
builder.Services.Configure<RouteOptions>(option =>
{
    option.LowercaseUrls = true;
    option.LowercaseQueryStrings = true;
    option.AppendTrailingSlash = true;
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login/IndexEntre";
});
var app = builder.Build();
database();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
void database()
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<SeedData>();
        seeder.seedData();
    }
}
using cinema.Data;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;
using cinema.Repositories;
using Microsoft.AspNetCore.Identity;
using cinema.Identity;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
// load .env file
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, "../../Env.env");
DotEnv.Load(dotenv);

// Add DbContext

var env = Environment.GetEnvironmentVariables();

var connectionString = "Data Source=" + env["hostname"] + ";" +
                       "Initial Catalog=" + env["database"] + ";" +
                       "Integrated Security=SSPI;";

builder.Services
    .AddDbContext<CinemaContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<CinemaIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CinemaContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("rolecreation", policy =>
    policy.RequireRole("Admin")
    );

    options.AddPolicy("usermanagement", policy =>
    policy.RequireRole("Admin")
    );

    options.AddPolicy("employee", policy =>
    policy.RequireRole("Admin", "Kassamedewerker", "Back-office medewerker", "Manager")
    );
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IPriceCalculatingService, PriceCalculatingService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IFoundItemService, FoundItemService>();
builder.Services.AddScoped<IAuthorizationService, DefaultAuthorizationService>();
builder.Services.AddScoped<IPaymentAdapter, PaymentAdapter>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTemplatesRepository, RoomTemplatesRepository>();
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //running migrations at startup
    var db = scope.ServiceProvider.GetRequiredService<CinemaContext>();
    
    db.Database.Migrate();
    //adding seeddata
    if ((string) env["seeddata"]! ==  "true")
    {
        var services = scope.ServiceProvider;
        SeedData.Initialize(services);
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();

app.MapControllerRoute(
    name: "Movies",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "RoomTemplates",
    pattern: "{controller=RoomTemplates}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Rooms",
    pattern: "{controller=Rooms}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Shows",
    pattern: "{controller=Shows}/{action=Index}/{id?}");

app.Run();

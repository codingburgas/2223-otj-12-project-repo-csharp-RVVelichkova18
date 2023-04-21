using ForestrySystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ForestrySystem.Models;
using ForestrySystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
	builder.Configuration.GetConnectionString("localDb")));

builder.Services.AddDefaultIdentity<AppUser>().AddDefaultTokenProviders().
	 AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<UserRolesService>();
builder.Services.AddScoped<CategoryOfTimbersService>();
builder.Services.AddScoped<EventsService>();
builder.Services.AddScoped<ForestryInstitutionsService>();
builder.Services.AddScoped<PurposeOfCutOffsService>();
builder.Services.AddScoped<TypeOfTimbersService>();
builder.Services.AddScoped<TypeOfWoodsService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoPv.Areas.Identity.Data;
using ProjetoPv.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProjetoPvContextConnection") ?? throw new InvalidOperationException("Connection string 'ProjetoPvContextConnection' not found.");
builder.Services.AddDbContext<ProjetoPvContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ProjetoPvUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProjetoPvContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())

{

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();



    var roles = new[] { "Admin", "User", "Treinadores", "Atletas" };



    foreach (var role in roles)

    {

        if (!await roleManager.RoleExistsAsync(role))

        {

            await roleManager.CreateAsync(new IdentityRole(role));

        }

    }

}



using (var scope = app.Services.CreateScope())

{

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ProjetoPvUser>>();



    string email = "admin@admin.com";

    string password = "Admin123@";

    if (await userManager.FindByEmailAsync(email) == null)

    {

        var user = new ProjetoPvUser();

        user.PhoneNumber = "933418182";

        user.Email = email;

        user.UserName = email;

        user.EmailConfirmed = true;



        await userManager.CreateAsync(user, password);



        await userManager.AddToRoleAsync(user, "Admin");

    }



}
app.Run();

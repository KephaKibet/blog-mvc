using blog.Data.FileManager;
using blog.Data.Repository;
using Blog.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
}
)
    //.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Auth/Login";
});

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IFileManager, FileManager>();


var app = builder.Build();

try
{

    var scope = app.Services.CreateScope();

    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    ctx.Database.EnsureCreated();

    var adminRole = new IdentityRole("Admin");

    if (!ctx.Roles.Any())
    {
        //create a role 
        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
    }


    if (!ctx.Users.Any(u => u.UserName == "admin"))
    {
        //create an admin
        var adminUser = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@test.com"
        };
        var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
        
        //add role to user
		userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
	}

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.UseMvcWithDefaultRoute();
app.Run();
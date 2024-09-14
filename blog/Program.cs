using blog.Data.Repository;
using Blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
}
)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddTransient<IRepository, Repository>();


var app = builder.Build();

try
{

    var scope = app.Services.CreateScope();

    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    ctx.Database.EnsureCreated();

    var adminRole = new IdentityRole("admin");
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
    }

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}




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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.UseMvcWithDefaultRoute();
app.Run();
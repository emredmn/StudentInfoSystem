using StudentInfoSystem.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;
using StudentInfoSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<StudentInfoSystem.Data.SchoolDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<StudentInfoSystem.Models.ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<StudentInfoSystem.Data.SchoolDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddScoped<StudentInfoSystem.Services.IStudentService, StudentInfoSystem.Services.StudentService>();
builder.Services.AddScoped<StudentInfoSystem.Services.ICourseService, StudentInfoSystem.Services.CourseService>();
builder.Services.AddScoped<StudentInfoSystem.Services.IDepartmentService, StudentInfoSystem.Services.DepartmentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<StudentInfoSystem.Data.SchoolDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseStaticFiles();

app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
});

app.MapPost("/auth/login", async (HttpContext httpContext, SignInManager<ApplicationUser> signInManager) =>
{
    var form = await httpContext.Request.ReadFormAsync();
    var email = form["email"].ToString();
    var password = form["password"].ToString();

    var result = await signInManager.PasswordSignInAsync(email, password, false, false);
    if (result.Succeeded)
    {
        return Results.Redirect("/");
    }
    return Results.Redirect("/login?IsInvalid=true");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

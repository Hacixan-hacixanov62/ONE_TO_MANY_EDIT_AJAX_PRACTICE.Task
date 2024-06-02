using Microsoft.EntityFrameworkCore;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Data;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<ICategoryService,  CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );
   

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


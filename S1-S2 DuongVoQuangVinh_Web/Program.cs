using Microsoft.EntityFrameworkCore;
using S1_S2_DuongVoQuangVinh__Web__.Data;
using S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces;
using S1_S2_DuongVoQuangVinh__Web__.Models.Services;
using S1_S2_DuongVoQuangVinh__Web__.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductRepository,
    S1_S2_DuongVoQuangVinh__Web__.Repositories.ProductRepository>();

builder.Services.AddScoped<IOrderRepository,OrderRepository>();

builder.Services.AddScoped<IShoppingCartRepository>(sp =>
    ShoppingCartRepository.GetCart(sp));

builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
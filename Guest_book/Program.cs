using Microsoft.EntityFrameworkCore;
using Guest_book.Models;
using GuestBook.DAL.Context;
using GuestBook.DAL.Repositories;
using GuestBook.DAL.Interfaces;

using GuestBook.BLL.Interfaces;
using GuestBook.BLL.Services;
using GuestBook.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddGuestBookContext(connection);
builder.Services.AddUnitOfWorksService();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // ������������ ������ (����-��� ���������� ������)
    options.Cookie.Name = "Session"; // ������ ������ ����� ���� �������������, ������� ����������� � �����.

});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWorks, ContextUnitOfWorks>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

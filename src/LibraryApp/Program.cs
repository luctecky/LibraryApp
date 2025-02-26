using LibraryApp.Components;
using LibraryAppBlazor.Application.Services;
using LibraryAppBlazor.Domain.Interfaces;
using LibraryAppBlazor.Infrastructure.Data;
using LibraryAppBlazor.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddDbContext<LibraryAppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

//Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
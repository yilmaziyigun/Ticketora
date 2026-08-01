using Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Handlers;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Application.Interfaces;
using Ticketora.Persistence.Context;
using Ticketora.Persistence.Identity;
using Ticketora.Persistence.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<GetByIdCategoryQueryHandler>();
builder.Services.AddDbContext<TicketoraContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<TicketoraContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTicketQuery).Assembly));
builder.Services.AddScoped<CreateEventCommandHandler>();
builder.Services.AddScoped<GetEventQueryHandler>();
builder.Services.AddScoped<RemoveEventCommandHandler>();
builder.Services.AddScoped<UpdateEventCommandHandler>();
builder.Services.AddScoped<GetByIdQueryHandler>();
builder.Services.AddScoped<ITicketNumberGenerator, TicketNumberGenerator>();
builder.Services.AddScoped<CreateTicketCommandHandler>();
builder.Services.AddScoped<GetTicketQueryHandler>();
builder.Services.AddScoped<RemoveTicketCommandHandler>();
builder.Services.AddScoped<UpdateTicketCommandHandler>();
builder.Services.AddScoped<GetByIdTicketQueryHandler>();
builder.Services.AddScoped<BookTicketCommandHandler>();
builder.Services.AddScoped<GetUserTicketsQueryHandler>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

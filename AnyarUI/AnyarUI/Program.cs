using Anyar.Business.Validations.TeamItem;
using Anyar.Business.ViewModels.TeamViewModel;
using Anyar.Core.Entities;
using Anyor.DataAccess.Contexts;
using Anyor.DataAccess.Repositories.Implementations;
using Anyor.DataAccess.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var conString = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conString));
builder.Services.AddScoped<ITeamItemRepository, TeamItemRepository>();
builder.Services.AddScoped<IValidator<UpdateTeamItemVM>, UpdateTeamValidators>();
builder.Services.AddScoped<IValidator<CreateTeamItemVm>, CreateTeamValidators>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTeamValidators>();
var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.Run();

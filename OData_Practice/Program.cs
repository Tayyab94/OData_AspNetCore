using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OData_Practice.Models;
using OData_Practice.Models.Repos;
using OData_Practice.Seeder;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Company>("Companies");
    
    return builder.GetEdmModel();
}




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddOData(options => options.AddRouteComponents("odata", GetEdmModel())
    .Select()
    .Filter().OrderBy()
    .SetMaxTop(20)
    .Count()
    .Expand()
    );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "CompaniesDB"));
builder.Services.AddScoped<ICompanyRepo, CompanyRepo>();


var app = builder.Build();

DBSeeder.AddCompaniesData(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

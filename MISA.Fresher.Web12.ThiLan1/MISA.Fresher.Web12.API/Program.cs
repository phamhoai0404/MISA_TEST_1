using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using MISA.Fresher.Web12.Core.Services;
using MISA.Fresher.Web12.Infrastructure.Repository;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


/// <summary>
/// Add CORS
/// </summary>
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      }));

//Đúng định dạng của các property
builder.Services.AddMvc()
        .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Xử lý DI: Thực hiện tiêm Repository với Service mong muốn cho interface
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// <summary>
/// Add CORS
/// </summary>
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

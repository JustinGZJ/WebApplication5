using System.Reflection;
using FluentValidation.AspNetCore;
using WebApplication5.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "测试", Version = "v1" });
    //添加注解
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApplication5.xml"));
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(x =>
        x.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

//添加验证
builder.Services.AddFluentValidation(fv =>
{
    Assembly assembly = Assembly.GetExecutingAssembly();
    fv.RegisterValidatorsFromAssembly(assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
//注册SignalR
app.MapHub<ChatRoomHub>("hubs/chatroom");
app.MapControllers();

app.Run();
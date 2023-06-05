using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SignalRServer.auth;
using SignalRServer.Hubs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var MyAllowSpecificOrigins = "Policy1";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:8081","http://localhost:8080")
            //builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
            //.SetIsOriginAllowed(origin => true)
            // .AllowCredentials();
        });
});

#region jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    //�]�w����
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
        ValidateIssuerSigningKey = true,//����ñ�W�K�_
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey"))),
        ValidateAudience = false,//���ǫȤ�(client)�i�H�ϥ�
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived=context=>
        {
            var accessToken = context.Request.Query["access_token"];
            if (string.IsNullOrEmpty(accessToken) == false) {
                context.Token = accessToken;
            }
            return Task.CompletedTask;

        }
    };
});
builder.Services.AddSingleton<JwtAuthenticationManager>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);//�ݦb���ҩM���v�e��

app.UseAuthentication();//��������

app.UseAuthorization();//���v


app.UseEndpoints(x =>
{
    x.MapHub<ProgressHub>("/ProgressHub");
    x.MapHub<ProgressHub2>("/ProgressHub2");
});

app.MapControllers();

app.Run();

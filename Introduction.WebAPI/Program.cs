using Autofac;
using Introduction.Repository.Common;
using Introduction.Repository;
using Introduction.Service.Common;
using Introduction.Service;
using Autofac.Extensions.DependencyInjection;
using MimeKit;
using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //added for DI:
        builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
        builder.Services.AddSingleton<IBookRepository, BookRepository>();
        builder.Services.AddTransient<IAuthorService, AuthorService>();
        builder.Services.AddTransient<IBookService, BookService>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IRoleService, RoleService>();
        builder.Services.AddTransient<IEmailService, EmailService>();


        var app = builder.Build();

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
    }

    
}
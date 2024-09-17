using Autofac;
using Introduction.Repository.Common;
using Introduction.Repository;
using Introduction.Service.Common;
using Introduction.Service;
using Autofac.Extensions.DependencyInjection;
using MimeKit;
using System;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Core;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterInstance("your-email@example.com").As<string>();
            containerBuilder.RegisterType<AuthorRepository>().As<IAuthorRepository>().SingleInstance();
            containerBuilder.RegisterType<BookRepository>().As<IBookRepository>().SingleInstance();
            containerBuilder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            containerBuilder.RegisterType<RoleRepository>().As<IRoleRepository>().SingleInstance();
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ReservationRepository>().As<IReservationRepository>().SingleInstance();
            containerBuilder.RegisterType<ReservationService>().As<IReservationService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        });

        // Configure SMTP Client
        builder.Services.AddSingleton(new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new NetworkCredential("hookingapp2@gmail.com", "nccj qnfm iqkj ujuz"),
            EnableSsl = true
        });
        builder.Services.AddControllers();

        
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
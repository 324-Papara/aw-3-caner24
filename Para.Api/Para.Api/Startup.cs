using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Api.Attribute;
using Para.Api.GlobalExceptionHandler;
using Para.Api.Middleware;
using Para.Bussiness;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Validation.FluentValidation;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Api;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }


    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para.Api", Version = "v1" });
        });

        var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<ParaDbContext>(options => options.UseSqlServer(connectionStringSql));
        //services.AddDbContext<ParaDbContext>(options => options.UseNpgsql(connectionStringPostgre));

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        services.AddSingleton(config.CreateMapper());

        services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
        services.AddScoped<ValidatorAttribute>();

        #region Autofac
        var builder = new ContainerBuilder();
        builder.Populate(services);
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        builder.RegisterType<CustomerRequestValidatior>().As<IValidator<CustomerRequest>>();
        builder.RegisterType<CustomerAdressRequestValidator>().As<IValidator<CustomerAddressRequest>>();
        builder.RegisterType<CustomerDetailRequestValidator>().As<IValidator<CustomerDetailRequest>>();
        builder.RegisterType<CustomerPhoneRequestValidator>().As<IValidator<CustomerPhoneRequest>>();

        var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
        builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

        return new AutofacServiceProvider(builder.Build());
        #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
        }


        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
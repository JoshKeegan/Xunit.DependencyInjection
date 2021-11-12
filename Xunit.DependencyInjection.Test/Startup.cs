﻿using Autofac.Extensions.DependencyInjection;
using Xunit.DependencyInjection.Demystifier;

namespace Xunit.DependencyInjection.Test;

public class Startup
{
    public static int Counter { get; set; }

    public Startup() => Counter++;

    public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureAppConfiguration(lb => lb.AddJsonFile("appsettings.json", false, true))
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());

    public void ConfigureServices(IServiceCollection services) =>
        services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug))
            .AddScoped<IDependency, DependencyClass>()
            .AddScoped<IDependencyWithManagedLifetime, DependencyWithManagedLifetime>()
            .AddHostedService<HostServiceTest>()
            .AddSkippableFactSupport()
            .AddSingleton<IAsyncExceptionFilter, DemystifyExceptionFilter>();

    public void Configure(IServiceProvider provider, ITestOutputHelperAccessor accessor)
    {
        Assert.NotNull(accessor);

        XunitTestOutputLoggerProvider.Register(provider);
    }
}

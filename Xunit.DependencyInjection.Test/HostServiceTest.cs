﻿namespace Xunit.DependencyInjection.Test;

public class HostServiceTest : IHostedService
{
    private static bool HostedServiceInvoked { get; set; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        HostedServiceInvoked = true;
            
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [Fact]
    public void Test() => Assert.True(HostedServiceInvoked);
}

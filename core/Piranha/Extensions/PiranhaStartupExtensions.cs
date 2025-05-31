/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license. See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.Extensions.DependencyInjection;
using Piranha;
using Piranha.Models;
using Piranha.Services;
using Piranha.Events;
using Microsoft.Extensions.Http;

public static class PiranhaStartupExtensions
{
    public static IServiceCollection AddPiranha(this IServiceCollection services,
        Action<PiranhaServiceBuilder> options = null, ServiceLifetime scope = ServiceLifetime.Scoped)
    {
        var serviceBuilder = new PiranhaServiceBuilder(services);

        options?.Invoke(serviceBuilder);

        services.AddSingleton<IContentFactory, ContentFactory>();
        services.AddSingleton<EventBus>(sp => EventBus.CreateAsync().GetAwaiter().GetResult());
        services.AddHttpClient<NotificationService>();

        services.AddHostedService<EventConsumer>();

        services.AddScoped<IApi, Api>();
        services.AddScoped<Config>();

        return services;
    }
}
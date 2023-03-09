﻿using TestIdp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var isBuilder = builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;

        // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
        options.EmitStaticAudienceClaim = true;
    })
    .AddTestUsers(TestUsers.Users);

// in-memory, code config
isBuilder.AddInMemoryIdentityResources(Config.IdentityResources);
isBuilder.AddInMemoryApiScopes(Config.ApiScopes);
isBuilder.AddInMemoryClients(Config.Clients);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
        
app.MapRazorPages()
    .RequireAuthorization();

app.Run();
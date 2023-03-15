using Bogus;
using TestIdp;
using TestIdp.Factories;

// The seed that is used to generate users
Randomizer.Seed = new Random(1337);
var testUsers = UsersFactory.Faker.Generate(200);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var isBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
});

isBuilder.AddTestUsers(testUsers);
isBuilder.AddInMemoryIdentityResources(Config.IdentityResources);
isBuilder.AddInMemoryApiScopes(Config.ApiScopes);
isBuilder.AddInMemoryClients(Config.Clients);

builder.Services.AddSingleton(testUsers);
builder.Services.AddSingleton(Config.Clients);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
        
app.MapRazorPages()
    .RequireAuthorization();

app.Run();
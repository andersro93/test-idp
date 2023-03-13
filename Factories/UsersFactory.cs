using System.Security.Claims;
using Bogus;
using Bogus.Extensions.Norway;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace TestIdp.Factories;

public static class UsersFactory
{
    public static Faker<TestUser> Faker => new Faker<TestUser>("nb_NO")
        .RuleFor(u => u.SubjectId, faker => faker.Random.Guid().ToString())
        .RuleFor(u => u.Username, faker => faker.Person.Email)
        .RuleFor(u => u.Password, faker => faker.Internet.Password(faker.Random.Int(4, 16)))
        .RuleFor(u => u.IsActive, faker => true)
        .RuleFor(u => u.Claims, faker => new List<Claim>()
        {
            new ("ssn", faker.Person.Fødselsnummer()),
            new (JwtClaimTypes.Name, $"{faker.Person.LastName}, {faker.Person.FirstName}"),
            new (JwtClaimTypes.GivenName, faker.Person.FirstName),
            new (JwtClaimTypes.FamilyName, faker.Person.LastName),
            new (JwtClaimTypes.Email, faker.Person.Email),
            new (JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            new (JwtClaimTypes.PhoneNumber, faker.Phone.PhoneNumberFormat(3).Replace(" ", "")), // Creates E164 numbers
            new (JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
            new (JwtClaimTypes.Picture, faker.Internet.Avatar()),
            new (JwtClaimTypes.Gender, faker.Person.Gender.ToString()),
            new (JwtClaimTypes.WebSite, faker.Person.Website),
            new (JwtClaimTypes.BirthDate, faker.Person.DateOfBirth.ToShortDateString()),
            new (JwtClaimTypes.ZoneInfo, "Europe/Oslo"),
            new (JwtClaimTypes.Locale, "nb_NO"),
        });
}
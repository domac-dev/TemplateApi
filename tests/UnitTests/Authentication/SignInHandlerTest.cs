using Application.Modules.Authentication.Commands;
using Application.Modules.Authentication.DTOs.Request;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Enumerations;
using Domain.ValueObjects;
using NSubstitute;
using Ardalis.Specification;

namespace UnitTests.Authentication;

public class SignInHandlerTest
{
    private readonly IRepository<User> _repository = Substitute.For<IRepository<User>>();
    private readonly IHttpContextService _httpContextService = Substitute.For<IHttpContextService>();
    private readonly IAuthenticationManager _authenticationManager = Substitute.For<IAuthenticationManager>();
    private readonly SignInHandler _handler;

    public SignInHandlerTest()
    {
        _handler = new SignInHandler(_repository, _httpContextService, _authenticationManager);
    }

    private static User CreateUser(string email, string password, bool confirmed = true)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User(
            email: email,
            passwordHash: hash,
            fullName: "Test User",
            cultureType: (int)CultureTypeEnum.English,
            telephone: "0991234567",
            address: new Address("Street", "Country", "City", "00000")
        )
        {
            EmailConfirmed = confirmed
        };
        return user;
    }

    [Fact]
    public async Task Handle_WhenCorrectEmailAndPassword_ShouldReturnSuccess()
    {
        var password = "password123";
        var user = CreateUser("test@email.hr", password);

        var request = new SignInCommand(new SignInRequestDTO(user.Email, password, true));

        var accessToken = Substitute.For<IToken>();
        var refreshToken = Substitute.For<IToken>();
        accessToken.Value.Returns("access-token");
        refreshToken.Value.Returns("refresh-token");
        refreshToken.ExpiresAt.Returns(DateTime.UtcNow.AddDays(7));

        _repository.FirstOrDefaultAsync(Arg.Any<Specification<User>>(), Arg.Any<CancellationToken>())
            .Returns(user);
        _authenticationManager.GenerateTokens(user).Returns((accessToken, refreshToken));
        _httpContextService.IPAddress().Returns("127.0.0.1");

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("access-token", result.Data!.AccessToken.Value);
        Assert.Equal("refresh-token", result.Data.RefreshToken.Value);
        await _repository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenEmailNotFound_ShouldReturnBadRequest()
    {
        var request = new SignInCommand(new SignInRequestDTO("notfound@email.hr", "any", true));

        _repository.FirstOrDefaultAsync(Arg.Any<Specification<User>>(), Arg.Any<CancellationToken>())
            .Returns((User?)null);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("ERROR_INVALID_EMAIL", result.Message);
    }

    [Fact]
    public async Task Handle_WhenEmailNotConfirmed_ShouldReturnBadRequest()
    {
        var user = CreateUser("unconfirmed@email.hr", "pass", confirmed: false);
        var request = new SignInCommand(new SignInRequestDTO(user.Email, "pass", true));

        _repository.FirstOrDefaultAsync(Arg.Any<Specification<User>>(), Arg.Any<CancellationToken>())
            .Returns(user);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("ERROR_EMAIL_NOT_CONFIRMED", result.Message);
    }

    [Fact]
    public async Task Handle_WhenInvalidPassword_ShouldReturnBadRequest()
    {
        var user = CreateUser("valid@email.hr", "correctpass");
        var request = new SignInCommand(new SignInRequestDTO(user.Email, "wrongpass", true));

        _repository.FirstOrDefaultAsync(Arg.Any<Specification<User>>(), Arg.Any<CancellationToken>())
            .Returns(user);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("ERROR_INVALID_PASSWORD", result.Message);
    }
}

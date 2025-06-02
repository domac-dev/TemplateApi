//using Application.Modules.Authentication.Commands;
//using Application.Modules.Authentication.DTOs.Request;
//using Domain.Abstraction;
//using Domain.Abstraction.Security;
//using Domain.Entities.Core;
//using Domain.Entities.Core.Specifications;
//using Domain.Enumerations;
//using NSubstitute;

//namespace UnitTests.Authentication
//{
//    public class SignInHandlerTest
//    {
//        private readonly IRepository<User> _repository = Substitute.For<IRepository<User>>();
//        private readonly IHttpContextService _httpContextService = Substitute.For<IHttpContextService>();
//        private readonly IAuthenticationManager _authenticationManager = Substitute.For<IAuthenticationManager>();
//        private readonly SignInHandler _handler;
//        public SignInHandlerTest()
//        {
//            _handler = new SignInHandler(_repository, _httpContextService, _authenticationManager);
//        }

//        [Fact]
//        public async Task Handle_WhenCorrectEmailAndPassword_ShouldReturnSuccess()
//        {
//            var user = new ApplicationUser(
//                "test@email.hr",
//                BCrypt.Net.BCrypt.HashPassword("password123"),
//                "Test User",
//                CultureType.English)
//            {
//                Id = 1,
//                EmailConfirmed = true
//            };
//            var request = new SignInCommand(new SignInRequestDTO { EmailAddress = "test@email.hr", Password = "password123" });
//            var accessToken = Substitute.For<IToken>();
//            var refreshToken = Substitute.For<IToken>();
//            accessToken.Value.Returns("access-token");
//            refreshToken.Value.Returns("refresh-token");
//            refreshToken.ExpiresAt.Returns(DateTime.UtcNow.AddDays(7));

//            _repository.FirstOrDefaultAsync(Arg.Any<UserByEmailUnconfirmedSpec>(), Arg.Any<CancellationToken>())
//                .Returns<Task<User>>(Task.FromResult<User?>((User)user));
//            _authenticationManager.GenerateTokens(user).Returns((accessToken, refreshToken));
//            _httpContextService.IPAddress().Returns("192.168.1.1");

//            var result = await _handler.Handle(request, CancellationToken.None);

//            Assert.True(result.IsSuccess);
//            Assert.NotNull(result.Data);
//            Assert.Equal("access-token", result.Data.AccessToken.Token);
//            Assert.Equal("refresh-token", result.Data.RefreshToken.Token);
//            await _repository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
//        }

//        [Fact]
//        public async Task Handle_WhenEmailNotFound_ShouldReturnBadRequest()
//        {
//            var request = new SignInCommand(new SignInRequestDTO { EmailAddress = "nonexistent@email.hr", Password = "password" });
//            _repository.FirstOrDefaultAsync(Arg.Any<UserByEmailUnconfirmedSpec>(), Arg.Any<CancellationToken>())
//                .Returns(Task.FromResult<User?>(null));

//            var result = await _handler.Handle(request, CancellationToken.None);

//            Assert.False(result.IsSuccess);
//            Assert.Equal("ERROR_INVALID_EMAIL", result.Message);
//        }

//        [Fact]
//        public async Task Handle_WhenIncorrectPassword_ShouldReturnBadRequest()
//        {
//            var user = new ApplicationUser(
//                "test@email.hr",
//                BCrypt.Net.BCrypt.HashPassword("password123"),
//                "Test User",
//                CultureType.English)
//            {
//                Id = 1,
//                EmailConfirmed = true
//            };
//            var request = new SignInCommand(new SignInRequestDTO { EmailAddress = "test@email.hr", Password = "wrongpassword" });
//            _repository.FirstOrDefaultAsync(Arg.Any<UserByEmailUnconfirmedSpec>(), Arg.Any<CancellationToken>())
//                .Returns<Task<User>>(Task.FromResult<User?>((User)user));

//            var result = await _handler.Handle(request, CancellationToken.None);

//            Assert.False(result.IsSuccess);
//            Assert.Equal("ERROR_INVALID_PASSWORD", result.Message);
//        }

//        [Fact]
//        public async Task Handle_WhenEmailNotConfirmed_ShouldReturnBadRequest()
//        {
//            var user = new ApplicationUser(
//                "unconfirmed@email.hr",
//                BCrypt.Net.BCrypt.HashPassword("unconfirmed"),
//                "Unconfirmed User",
//                CultureType.English)
//            {
//                Id = 2,
//                EmailConfirmed = false
//            };
//            var request = new SignInCommand(new SignInRequestDTO { EmailAddress = "unconfirmed@email.hr", Password = "unconfirmed" });
//            _repository.FirstOrDefaultAsync(Arg.Any<UserByEmailUnconfirmedSpec>(), Arg.Any<CancellationToken>())
//                .Returns<Task<User>>(Task.FromResult<User?>((User)user));

//            var result = await _handler.Handle(request, CancellationToken.None);

//            Assert.False(result.IsSuccess);
//            Assert.Equal("ERROR_EMAIL_NOT_CONFIRMED", result.Message);
//        }

//        [Fact]
//        public async Task Handle_WhenClientUserLogin_ShouldReturnSuccess()
//        {
//            var user = new ApplicationUser(
//                "client@email.hr",
//                BCrypt.Net.BCrypt.HashPassword("client123"),
//                "Client User",
//                CultureType.English)
//            {
//                Id = 3,
//                EmailConfirmed = true
//            };
//            var request = new SignInCommand(new SignInRequestDTO { EmailAddress = "client@email.hr", Password = "client123" });
//            var accessToken = Substitute.For<IToken>();
//            var refreshToken = Substitute.For<IToken>();
//            accessToken.Value.Returns("client-access-token");
//            refreshToken.Value.Returns("client-refresh-token");
//            refreshToken.ExpiresAt.Returns(DateTime.UtcNow.AddDays(7));

//            _repository.FirstOrDefaultAsync(Arg.Any<UserByEmailUnconfirmedSpec>(), Arg.Any<CancellationToken>())
//                .Returns<Task<User>>(Task.FromResult<User?>((User)user));
//            _authenticationManager.GenerateTokens(user).Returns((accessToken, refreshToken));
//            _httpContextService.IPAddress().Returns("192.168.1.1");

//            var result = await _handler.Handle(request, CancellationToken.None);

//            Assert.True(result.IsSuccess);
//            Assert.NotNull(result.Data);
//            Assert.Equal("client-access-token", result.Data.AccessToken.Token);
//            Assert.Equal("client-refresh-token", result.Data.RefreshToken.Token);
//            await _repository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
//        }
//    }
//}
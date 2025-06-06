﻿using Api;
using Application.Modules.Authentication.DTOs.Request;
using Newtonsoft.Json;
using System.Text;

namespace FunctionalTests.Authentication
{
    [Collection("Sequential")]
    public class SignInEndpointTest(CustomWebAppFactory<Program> factory) : IClassFixture<CustomWebAppFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private const string _uri = Routes.AUTH;

        [Fact]
        public async Task TestSignInEndpoint()
        {
            var request = new SignInRequestDTO("admin@email.hr", "admin", true);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_uri, content);
            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}

using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Helpers;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Options;
using UsersApi.Config;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
	[Fact]
	public async Task GetAllUsers_When_Called_Invokes_HTTP_Request()
	{
		//Arrage
		var endpoint = "https://example.com/users";
		var expectedResponse = UsersFixture.GetTestUsers();
		var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
		var httpClient = new HttpClient(handlerMock.Object);
		var config = Options.Create(new UsersApiOptions {
			Endpoint = endpoint
		});
		var mockUserService = new UsersService(httpClient, config);

		//Act
		await mockUserService.GetAllUsers();

		//Assert
		handlerMock.Protected().Verify("SendAsync", 
								Times.Exactly(1), 
								ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), 
								ItExpr.IsAny<CancellationToken>());

	}



	[Fact]
	public async Task GetAllUsers_When_Hits_404_Returns_Empty_List_of_Users()
	{
		//Arrage
		var endpoint = "https://example.com/users";
		var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
		var httpClient = new HttpClient(handlerMock.Object);
		var config = Options.Create(new UsersApiOptions {
			Endpoint = endpoint
		});
		var mockUserService = new UsersService(httpClient, config);

		//Act
		var result = await mockUserService.GetAllUsers();

		//Assert
		result.Count().Should().Be(0);
	}



	[Fact]
	public async Task GetAllUsers_When_Called_Returns_List_of_Users_Of_Expected_Size()
	{
		//Arrage
		var endpoint = "https://example.com/users";
		var expectedResponse = UsersFixture.GetTestUsers();
		var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
		var httpClient = new HttpClient(handlerMock.Object);
		var config = Options.Create(new UsersApiOptions {
			Endpoint = endpoint
		});
		var mockUserService = new UsersService(httpClient, config);

		//Act
		var result = await mockUserService.GetAllUsers();

		//Assert
		result.Count().Should().Be(expectedResponse.Count);
	}


	[Fact]
	public async Task GetAllUsers_When_Called_Invokes_External_Configured_URL()
	{
		//Arrage
		var endpoint = "https://example.com/users";
		var expectedResponse = UsersFixture.GetTestUsers();
		var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
		var httpClient = new HttpClient(handlerMock.Object);

		var config = Options.Create(
			new UsersApiOptions {
			Endpoint = endpoint
		});

		var mockUserService = new UsersService(httpClient, config);

		//Act
		var result = await mockUserService.GetAllUsers();

		//Assert
		handlerMock.Protected()
						.Verify("SendAsync", 
						Times.Exactly(1), 
						ItExpr.Is<HttpRequestMessage>(
							req => 
								req.Method == HttpMethod.Get &&
								req.RequestUri.ToString() == endpoint),
						ItExpr.IsAny<CancellationToken>());
	}
}

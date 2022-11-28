using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Helpers;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
	[Fact]
	public async Task GetAllUsers_When_Called_Invokes_HTTP_Request()
	{
		//Arrage
		var expectedResponse = UsersFixture.GetTestUsers();
		var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
		var httpClient = new HttpClient(handlerMock.Object);
		var mockUserService = new UsersService(httpClient);

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
		var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
		var httpClient = new HttpClient(handlerMock.Object);
		var mockUserService = new UsersService(httpClient);

		//Act
		var result = await mockUserService.GetAllUsers();

		//Assert
		result.Count().Should().Be(0);
	}



	[Fact]
	public async Task GetAllUsers_When_Called_Returns_List_of_Users_Of_Expected_Size()
	{
		//Arrage
		var expectedResponse = UsersFixture.GetTestUsers();
		var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
		var httpClient = new HttpClient(handlerMock.Object);
		var mockUserService = new UsersService(httpClient);

		//Act
		var result = await mockUserService.GetAllUsers();

		//Assert
		result.Count().Should().Be(expectedResponse.Count);
	}
}

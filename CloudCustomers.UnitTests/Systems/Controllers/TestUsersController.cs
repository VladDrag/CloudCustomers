using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;


public class TestUsersController
{
    [Fact]
    public async Task Get_On_Success_Returns_Code_200()
    {
        //Arrange
        var mockUserService = new Mock<IUsersService>();
        var usersController = new UsersController(mockUserService.Object);

        //Act
        var result = (OkObjectResult) await usersController.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }


	[Fact]
	public async Task Get_On_Success_Invokes_User_Service()
	{
		//Arrange
		var mockUserService = new Mock<IUsersService>();
		mockUserService
			.Setup(service => service.GetAllUsers())
			.ReturnsAsync(new List<User>());
		
        var usersController = new UsersController(mockUserService.Object);

        //Act
        var result = await usersController.Get();

		//Assert
		mockUserService.Verify(service => service.GetAllUsers(), Times.Once);
	}
	
	
}

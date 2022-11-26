using CloudCustomers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.UnitTests.Systems.Controllers;


public class TestUsersController
{
    [Fact]
    public async Task Get_On_Success_Returns_Code_200()
    {
        //Arrange
        var usersController = new UsersController();

        //Act
        var result = (OkObjectResult) await usersController.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }
}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using vetappback.Controllers;
using vetappback.Entities;
using vetappback.Helpers;
using Xunit;

namespace VetAppTest
{
    public class AccountControllerTest
    {

       
        private readonly Mock<IUserHelper> userHelper = new Mock<IUserHelper>();
  

        [Fact]
        public async void GGetUserById_WithUnexitingItem_ReturnNull()
        {
            //Arrange

            
            var id = 0;
            var controller = new AccountController(userHelper.Object);

            //Act
            var result = await controller.GetUserById(id);

            //Assert
           Assert.IsType<BadRequestObjectResult>(result.Result);

        }
    }
}
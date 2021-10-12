using Microsoft.AspNetCore.Mvc;
using Moq;
using vetappApi.Repositories;
using vetappback.Controllers;
using vetappback.Entities;
using Xunit;

namespace VetAppTest
{
    public class PetTypeControllerTest
    {

        private readonly Mock<IPetTypeRepository> repositoryStub = new Mock<IPetTypeRepository>();


        [Fact]
        public async void GetPetTypeById_WithexitingItem_ReturnOkResult()
        {
            var idsp = 1;
            //Arrange

            repositoryStub.Setup(sp => sp.GetPetTypeByIdAsync(idsp))
             .ReturnsAsync(new PetType() { Id = 1, Name = "Shower" });
            var controller = new PetTypeController(repositoryStub.Object);

            //Act
            var result = await controller.GetPetTypeById(1);

            //Assert
            Assert.NotNull(result);

        }


        [Fact]
        public async void GetPetTypeById_WithUnexitingItem_ReturnStatus404()
        {
            //Arrange
            repositoryStub.Setup(sp => sp.GetPetTypeByIdAsync(It.IsAny<int>()))
             .ReturnsAsync((PetType)null);
            var controller = new PetTypeController(repositoryStub.Object);

            //Act
            var result = await controller.GetPetTypeById(0);

            //Assert
            var res = result.Result as StatusCodeResult;
            Assert.Equal(404, res.StatusCode);


        }
       


        [Fact]
        public async void PostPetType_WitNewItem_RetunActionResultPetType()
        {

            var PetType = new PetType() { Name = "Care" };

            repositoryStub.Setup(sp => sp.AddPetTypeAsync(PetType))
           .Returns(async () => new PetType() { Id = 1, Name = "Care" });
            //Arrange
            var controller = new PetTypeController(repositoryStub.Object);

            //Act
            var result = await controller.PostPetType(PetType);
            var res = result.Result as StatusCodeResult;
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }


        [Fact]
        public async void PostPetType_WithNullItem_RetunBadR()
        {
            var PetType = new PetType();
            PetType = null;
            //Arrange

            repositoryStub.Setup(sp => sp.AddPetTypeAsync(PetType))
             .Returns(async () => new PetType());
            var controller = new PetTypeController(repositoryStub.Object);

            //Act
            var result = await controller.PostPetType(PetType);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }


        [Fact]
        public async void PutPetType_WithNullItem_BadRequest()
        {
            var PetType = new PetType() { Id = 0, Name = "" };

            //Arrange

            repositoryStub.Setup(sp => sp.UpdatePetTypeAsync(PetType))
             .Returns(async () => new PetType());
            var controller = new PetTypeController(repositoryStub.Object);

            //Act
            var result = await controller.PutPetType(1, PetType);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async void PutPetType_Return_OkResult()
        {
            var PetType = new PetType() { Id = 1, Name = "" };

            //Arrange
            repositoryStub.Setup(sp => sp.UpdatePetTypeAsync(PetType))
             .Returns(async () => new PetType());
            var controller = new PetTypeController(repositoryStub.Object);

            //Act
            var result = await controller.PutPetType(PetType.Id, PetType);

            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
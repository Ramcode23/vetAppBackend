using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using vetappApi.Repositories;
using vetappback.Controllers;
using vetappback.DTOs;
using vetappback.Entities;
using vetappback.Utilities;
using Xunit;

namespace VetAppTest
{
    public class PetControllerTest
    {

        private readonly Mock<IPetRepository> repositoryStub = new Mock<IPetRepository>();
        private static IMapper _mapper;
        private readonly IFileStorage fileStorage;
        private readonly string container = "Pets";

        public PetControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void GetPetById_WithUnexitingItem_ReturnNull()
        {
            //Arrange
            var id = 0;
            var controller = new PetController(repositoryStub.Object, _mapper, fileStorage);

            //Act
            var result = await controller.GetPetById(id);

            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async void GetPetById_WithexitingItem_ReturnOkResult()
        {
            //Arrange
            var id = 1;
               repositoryStub.Setup(sp => sp.GetPetByIdAsync(id))
             .ReturnsAsync(new Pet() { Id = 1, Name = "Shower" });

            var controller = new PetController(repositoryStub.Object, _mapper, fileStorage);

            //Act
            var result = await controller.GetPetById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }


        [Fact]
        public async void PostPet_WitNewItem_RetunOkResult()
        {
            //Arrange
            var PetCreateDTO = new PetCreateDTO() { Name = "Boby" };

            var controller = new PetController(repositoryStub.Object, _mapper, fileStorage);

            //Act
            var result = await controller.PostPet(PetCreateDTO);

            //Assert
            Assert.IsType<OkResult>(result);

        }


        [Fact]
        public async void PostPet_WithNullItem_RetunBadRequest()
        {
            //Arrange
            var controller = new PetController(repositoryStub.Object, _mapper, fileStorage);

            //Act
            var result = await controller.PostPet(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async void PutPet_WithNullItem_BadRequest()
        {
            //Arrange
            var id = 0;
            var petCreateDTO = new PetCreateDTO() { Name = "Dingo", Born = System.DateTime.Today, OwnerId = 1, PetTypeId = 1, Race = "chiguagua", Remarks = "Notes" };
            var controller = new PetController(repositoryStub.Object, _mapper, fileStorage);

            //Act
            var result = await controller.PutPet(id, petCreateDTO);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async void PutPet_Return_OkResult()
        {
            //Arrange
            var id = 1;
              repositoryStub.Setup(sp => sp.GetPetByIdAsync(id))
             .ReturnsAsync(new Pet() { Id = 1, Name = "Shower" });
            var petCreateDTO = new PetCreateDTO() { Name = "Dingo", Born = System.DateTime.Today, OwnerId = 1, PetTypeId = 1, Race = "chiguagua", Remarks = "Notes" };
            var controller = new PetController(repositoryStub.Object, _mapper, fileStorage);

            //Act
            var result = await controller.PutPet(id, petCreateDTO);

            //Assert
            Assert.IsType<OkResult>(result);
        }
    }

}

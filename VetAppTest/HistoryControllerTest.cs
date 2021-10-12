using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using vetappApi.Repositories;
using vetappback.Controllers;
using vetappback.Entities;
using vetappback.Utilities;
using vetappback.DTOs;
using Xunit;

namespace VetAppTest
{
    public class HistoryControllerTest
    {
        private readonly Mock<IHistoryRepository> repositoryStub = new Mock<IHistoryRepository>();
        private static IMapper _mapper;

        public HistoryControllerTest()
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
        public async void GetHistoryById_WithUnexitingItem_ReturnNull()
        {
            //Arrange
            var id = 0;
            var controller = new HistoryController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.GetHistoryById(id);

            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async void GetHistoryById_WithexitingItem_ReturnOkResult()
        {
            //Arrange
            var id = 1;
            repositoryStub.Setup(sp => sp.GetHistoryByIdAsync(id))
          .ReturnsAsync(new History { Id = 1 });

            var controller = new HistoryController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.GetHistoryById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }


        [Fact]
        public async void PostHistory_WitNewItem_RetunOkResult()
        {
            //Arrange
            var historyCreateDTO = new HistoryCreateDTO()
            {
                Date = System.DateTime.Now,
                Description = "Pet testing",
                PetId = 1,
                Remarks = "Coment to test",
                ServiceTypeId = 1

            };

            var controller = new HistoryController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PostHistory(historyCreateDTO);

            //Assert
            Assert.IsType<NoContentResult>(result.Result);

        }


        [Fact]
        public async void PostHistory_WithNullItem_RetunBadRequest()
        {
            //Arrange
            var controller = new HistoryController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PostHistory(null);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }


        [Fact]
        public async void PutHistory_WithNullItem_BadRequest()
        {
            //Arrange
            var id = 0;
            var HistoryCreateDTO = new HistoryCreateDTO()
            {
                Date = System.DateTime.Now,
                Description = "Pet testing",
                PetId = 1,
                Remarks = "Coment to test",
                ServiceTypeId = 1

            };

            var controller = new HistoryController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PutHistory(id, HistoryCreateDTO);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async void PutHistory_Return_OkResult()
        {
            //Arrange
            var id = 1;
            repositoryStub.Setup(sp => sp.GetHistoryByIdAsync(id))
           .ReturnsAsync(new History { Id = 1, Description = "Test history" });
            var HistoryCreateDTO = new HistoryCreateDTO()
            {
                Date = System.DateTime.Now,
                Description = "History testing",
                PetId = 1,
                Remarks = "Coment to test",
                ServiceTypeId = 1

            };
            var controller = new HistoryController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PutHistory(id, HistoryCreateDTO);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }



}

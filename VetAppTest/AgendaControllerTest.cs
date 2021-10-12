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
    public class AgendaControllerTest
    {
        private readonly Mock<IAgendaRepository> repositoryStub = new Mock<IAgendaRepository>();
        private static IMapper _mapper;

        public AgendaControllerTest()
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
        public async void GetAgendaById_WithUnexitingItem_ReturnNull()
        {
            //Arrange
            var id = 0;
            var controller = new AgendaController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.GetAgendaById(id);

            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async void GetAgendaById_WithexitingItem_ReturnOkResult()
        {
            //Arrange
            var id = 1;
            repositoryStub.Setup(sp => sp.GetAgendaByIdAsync(id))
          .ReturnsAsync(new Agenda { Id = 1 });

            var controller = new AgendaController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.GetAgendaById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }


        [Fact]
        public async void PostAgenda_WitNewItem_RetunOkResult()
        {
            //Arrange
            var agendaCreateDTO = new AgendaCreateDTO()
            {
                Date = System.DateTime.Now,
                PetId = 1,
                Remarks = "Coment to test",
            

            };

            var controller = new AgendaController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PostAgenda(agendaCreateDTO);

            //Assert
            Assert.IsType<NoContentResult>(result.Result);

        }


        [Fact]
        public async void PostAgenda_WithNullItem_RetunBadRequest()
        {
            //Arrange
            var controller = new AgendaController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PostAgenda(null);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }


        [Fact]
        public async void PutAgenda_WithNullItem_BadRequest()
        {
            //Arrange
            var id = 0;
            var agendaCreateDTO = new AgendaCreateDTO()
            {
                Date = System.DateTime.Now,
                PetId = 1,
                Remarks = "Coment to test",


            };

            var controller = new AgendaController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PutAgenda(id, agendaCreateDTO);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async void PutAgenda_Return_OkResult()
        {
            //Arrange
            var id = 1;
            repositoryStub.Setup(sp => sp.GetAgendaByIdAsync(id))
           .ReturnsAsync(new Agenda { Id = 1 });
            var agendaCreateDTO = new AgendaCreateDTO()
            {
                Date = System.DateTime.Now,
                PetId = 1,
                Remarks = "Coment to test",
          

            };
            var controller = new AgendaController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PutAgenda(id, agendaCreateDTO);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }

    }

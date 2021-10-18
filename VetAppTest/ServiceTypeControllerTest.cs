using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using veapp.Api.Repositories;
using vetappApi.DTOs;
using vetappback.Controllers;
using vetappback.Entities;
using vetappback.Utilities;
using Xunit;


namespace VetAppTest
{
    public class ServiceTypeControllerTest
    {
        private readonly Mock<IServicesTypesRepository> repositoryStub = new Mock<IServicesTypesRepository>();
        private static IMapper _mapper;
        public ServiceTypeControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void GetServiceTypeById_WithUnexitingItem_ReturnNotFound()
        {
            //Arrange
            repositoryStub.Setup(sp => sp.GetServiceTypeByIdAsync(It.IsAny<int>()))
             .ReturnsAsync((ServiceType)null);
            var controller = new ServiceTypeController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.GetServiceTypeById(0);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }
        [Fact]
        public async void GetServiceTypeById_WithexitingItem_ReturnOkResult()
        {
            var idsp = 1;
            //Arrange

            repositoryStub.Setup(sp => sp.GetServiceTypeByIdAsync(idsp))
             .ReturnsAsync(new ServiceType() { Id = 1, Name = "Shower" });
            var controller = new ServiceTypeController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.GetServiceTypeById(1);

            //Assert
            Assert.NotNull(result);

        }


        [Fact]
        public async void PostServiceType_WitNewItem_RetunActionResultServiceType()
        {
            //Arrange
            var ServiceTypeCreateDTO = new ServiceTypeCreateDTO() { Name = "Care" };
            var serviceType = _mapper.Map<ServiceType>(ServiceTypeCreateDTO);
            repositoryStub.Setup(sp => sp.AddServiceTypeAsync(serviceType))
             .Returns(async () => new ServiceType() { Id = 1, Name = "Care" });
            var controller = new ServiceTypeController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PostServiceType(ServiceTypeCreateDTO);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async void PostServiceType_WithNullItem_RetunBadR()
        {
             //Arrange
            var ServiceTypeCreateDTO = new ServiceTypeCreateDTO();
            var serviceType = _mapper.Map<ServiceType>(ServiceTypeCreateDTO);
            repositoryStub.Setup(sp => sp.AddServiceTypeAsync(serviceType))
             .Returns(async () => new ServiceType() { Id = 1, Name = "Care" });
            var controller = new ServiceTypeController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PostServiceType(null);


            //Assert
            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async void PutServiceType_WithNullItem_BadRequest()
        {
            var serviceType = new ServiceType() { Id = 0, Name = "" };

            //Arrange

            repositoryStub.Setup(sp => sp.UpdateServiceTypeAsync(serviceType))
             .Returns(async () => new ServiceType());
            var controller = new ServiceTypeController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PutServiceType(1, serviceType);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async void PutServiceType_Return_OkResult()
        {
            var serviceType = new ServiceType() { Id = 1, Name = "" };

            //Arrange
            repositoryStub.Setup(sp => sp.UpdateServiceTypeAsync(serviceType))
             .Returns(async () => new ServiceType());
            var controller = new ServiceTypeController(repositoryStub.Object, _mapper);

            //Act
            var result = await controller.PutServiceType(serviceType.Id, serviceType);

            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
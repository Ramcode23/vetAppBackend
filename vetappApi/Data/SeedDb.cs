using System;
using System.Linq;
using System.Threading.Tasks;
using veapp.Api.Repositories;
using vetappApi.Repositories;
using vetappback.DTOs;
using vetappback.Entities;
using vetappback.Helpers;

namespace vetappback.Data
{
    public class SeedDb
    {
        private readonly IUserHelper userHelper;
        private readonly IPetTypeRepository petTypeRepository;
        private readonly IServicesTypesRepository servicesTypesRepository;

        public SeedDb(IUserHelper userHelper,
IPetTypeRepository petTypeRepository,
IServicesTypesRepository servicesTypesRepository)
        {
            this.servicesTypesRepository = servicesTypesRepository;
            this.petTypeRepository = petTypeRepository;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await CheckServiceTypesAsync();
            await CheckPetTypesAsync();
            await CheckAdminUserAsync();

        }

        private async Task CheckServiceTypesAsync()
        {
            if (!await servicesTypesRepository.AnyServiceExists())
            {
                await servicesTypesRepository.AddServiceTypeAsync(new ServiceType { Name = "Haircut" });
                await servicesTypesRepository.AddServiceTypeAsync(new ServiceType { Name = "Hairstyle" });
                await servicesTypesRepository.AddServiceTypeAsync(new ServiceType { Name = "Vaccination" });
            }
        }

        private async Task CheckPetTypesAsync()
        {
            if (!await petTypeRepository.AnyPetTypeExists())
            {
                await petTypeRepository.AddPetTypeAsync(new PetType { Name = "Dog" });
                await petTypeRepository.AddPetTypeAsync(new PetType { Name = "Cat" });
                await petTypeRepository.AddPetTypeAsync(new PetType { Name = "Other" });

            }


        }

        private async Task CheckAdminUserAsync()
        {
            if (!await userHelper.AdminUserExists())
            {
                await userHelper.CreateAdminAsync(new RegisterUser
                {
                    Email = "manager@gmail.com",
                    Password = "AS1234567@",
                    Document = "12121212121",
                    FirstName = "Bette",
                    LastName = "Hubberstey",
                    Address = "9 Dorton Park"

                });
            }
        }

    }
}
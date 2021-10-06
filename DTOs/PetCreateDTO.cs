using System;
using Microsoft.AspNetCore.Http;

namespace vetappback.DTOs
{
    public class PetCreateDTO
    {
        
        public string Name { get; set; }


        public IFormFile ImageUrl { get; set; }

        public string Race { get; set; }


        public DateTime Born { get; set; }
        public string Remarks { get; set; }

        public int OwnerId { get; set; }

        public int PetTypeId { get; set; }

    }
}
using System;
using Microsoft.AspNetCore.Http;

namespace vetappback.DTOs
{
    public class PetDTO
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public String ImageUrl { get; set; }

        public string Race { get; set; }


        public DateTime Born { get; set; }
        public string Remarks { get; set; }

        public int OwnerId { get; set; }
        public string OwnerUserFullName { get; set; }

        public int PetTypeId { get; set; }
       public string PetTypeName { get; set; }

    }
}
using System;

namespace vetappback.DTOs
{
    public class HistoryDTO
    {
        public int Id { get; set; }

        public int ServiceTypeId { get; set; }
        public int ServiceTypeName { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Remarks { get; set; }

        public int PetId { get; set; }
        public int PetName { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();

    }
}
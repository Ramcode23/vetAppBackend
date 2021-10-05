using System;

namespace vetappback.DTOs
{
    public class HistoryCreateDTO
    {
    

        public int ServiceTypeId { get; set; }


        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Remarks { get; set; }

        public int PetId { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

    }
}
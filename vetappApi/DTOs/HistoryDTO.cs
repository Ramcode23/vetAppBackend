using System;

namespace vetappback.DTOs
{
    public class HistoryDTO
    {
        public int Id { get; set; }

        public int ServiceTypeId { get; set; }
<<<<<<< HEAD
        public int ServiceTypeName { get; set; }
=======

>>>>>>> origin/main

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Remarks { get; set; }

        public int PetId { get; set; }
<<<<<<< HEAD
        public int PetName { get; set; }
=======

>>>>>>> origin/main
        public DateTime DateLocal => Date.ToLocalTime();

    }
}
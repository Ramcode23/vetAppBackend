using System;
using System.ComponentModel.DataAnnotations;

namespace vetappback.DTOs
{
    public class AgendaDTO
    {

        public int Id { get; set; }

    	
    	public DateTime Date { get; set; }

    	public int OwnerId { get; set; }

    	public int PetId { get; set; }

    	public string Remarks { get; set; }

    	[Display(Name = "Is Available?")]
    	public bool IsAvailable { get; set; }
        
    }
}
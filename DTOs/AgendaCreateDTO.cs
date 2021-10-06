using System;

namespace vetappback.DTOs
{
    public class AgendaCreateDTO
    {
        	
    	public DateTime Date { get; set; }

    	public int OwnerId { get; set; }

    	public int PetId { get; set; }

    	public string Remarks { get; set; }

    	public bool IsAvailable { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vetappback.Entities
{
   public class Owner
	{
    public int Id { get; set; }

    	public User User { get; set; }

    	public ICollection<Pet> Pets { get; set; }

    	public ICollection<Agenda> Agendas { get; set; }
	}


}
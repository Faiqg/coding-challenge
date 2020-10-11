using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
	public class State
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StateId { get; set; }
		[DisplayName("State name")]
		public string StateName { get; set; }
		public Decimal Median { get; set; }
	}
}

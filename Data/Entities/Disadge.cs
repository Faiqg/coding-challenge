using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entites
{
    public class Disadge
	{
		[Key]
		public Guid ScoreId { get; set; }
		public int? Disadvantage2011 { get; set; }
		public int? Disadvantage2016 { get; set; }
		public string PlaceName { get; set; }
		public string StateName { get; set; }
	}
}

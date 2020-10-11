using Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImportData
{
	public class SeedingContext
	{
		public List<Location> Locations { get; set; }
		public List<State> States { get; set; }
		public List<Score> Scores { get; set; }
		public List<ScoreDetail> ScoreDetails { get; set; }
		public SeedingContext()
		{
			Locations = new List<Location>();
			States = new List<State>();
			Scores = new List<Score>();
			ScoreDetails = new List<ScoreDetail>();
		}
	}
}

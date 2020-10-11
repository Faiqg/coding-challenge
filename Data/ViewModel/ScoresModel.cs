using System.ComponentModel;

namespace Data.ViewModel
{
	public class ScoresModel
	{
		public string PlaceName { get; set; }
		public string StateName { get; set; }
		public int Year { get; set; }
		[DisplayName("Disadvantage")]
		public int? DisadvantageScore { get; set; }
		[DisplayName("Advantage")]
		public int? AdvantageDisadvantageScore { get; set; }
	}
}

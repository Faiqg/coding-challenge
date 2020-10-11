using System.ComponentModel;

namespace Data.ViewModel
{
	public class ReportModel
	{
		public int? DisadvantageFirstYear { get; set; }
		public int? DisadvantageSecondYear { get; set; }
		[DisplayName("Compare 2016 to 2011")]
		public int Comparison
		{
			get
			{
				return DisadvantageSecondYear.GetValueOrDefault() - DisadvantageFirstYear.GetValueOrDefault();
			}
		}
		public string PlaceName { get; set; }
		public string StateName { get; set; }
	}
}

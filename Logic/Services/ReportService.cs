using Data.ViewModel;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services
{
    public class ReportService : IReportService
    {
		private readonly LGAContext _context;
		private readonly IScoreService _scoreService;
		private readonly StateService _stateService;
		public ReportService(LGAContext context)
        {
			_context = context;
			//Report Service is acting like a logic layer to combine data contexts and present the desired output,
			//hence added concrete coupling to ScoreService. 
			_scoreService = new ScoreService(context);
			_stateService = new StateService(context);
		}

		/// <summary>
		/// Get disadvantages for the given search criteria
		/// </summary>
		/// <param name="searchParams">StateID, FirstYear, SecondYear</param>
		/// <returns>Disadvantages per place</returns>
        public IEnumerable<ReportModel> GetDisadges(ReportSearchModel searchParams)
		{
			// Default state
			if (!searchParams.StateId.HasValue)
			{
				searchParams.StateId = _stateService.VicStateId;
			}

			if (searchParams.StateId != -1)
			{
				var dataFirstYear = _scoreService.GetScores(searchParams.StateId, searchParams.FirstYear).
					Select(p => new { p.Location.LocationId, p.DisadvantageScore });
				var dataSecondYear = _scoreService.GetScores(searchParams.StateId, searchParams.FirstYear).
					Select(p => new { p.Location.LocationId, p.DisadvantageScore });
				var both = dataFirstYear.Join(
					dataSecondYear,
					d11 => d11.LocationId,
					d16 => d16.LocationId,
					(d11, d16) => new
					{
						d11.LocationId,
						FirstYear = d11.DisadvantageScore,
						SecondYear = d16.DisadvantageScore
					});
				var result = both.Join(_context.Locations,
						s => s.LocationId,
						l => l.LocationId,
						(s, l) => new { s.FirstYear, s.SecondYear, s.LocationId, l.PlaceName, l.State.StateId })
						.Join(_context.States,
						s => s.StateId,
						st => st.StateId,
						(s, st) => new ReportModel()
						{
							DisadvantageFirstYear = s.FirstYear,
							DisadvantageSecondYear = s.SecondYear,
							PlaceName = s.PlaceName,
							StateName = st.StateName
						});
				return result.ToList();
			}

			return Enumerable.Empty<ReportModel>();
		}
    }
}
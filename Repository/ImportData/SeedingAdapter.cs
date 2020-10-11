using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImportData
{
	public class SeedingAdapter
	{
		public void MergeContext(LGAContext context, SeedingContext seedingContext)
		{
			context.Locations.AddRange(seedingContext.Locations);
			context.States.AddRange(seedingContext.States);
			context.Scores.AddRange(seedingContext.Scores);
			context.ScoreDetails.AddRange(seedingContext.ScoreDetails);
		}
	}
}

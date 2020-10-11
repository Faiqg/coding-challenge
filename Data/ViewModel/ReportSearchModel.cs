
namespace Data.ViewModel
{
    public class ReportSearchModel
    {
        public ReportSearchModel()
        {
            //setting default values
            FirstYear = 2011;
            SecondYear = 2016;
        }
        public int? StateId { get; set; }
        public int? FirstYear { get; set; }
        public int? SecondYear { get; set; }
    }
}
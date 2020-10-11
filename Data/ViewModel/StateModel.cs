using System;
using System.ComponentModel;

namespace Data.ViewModel
{
    public class StateModel
    {
        public int StateId { get; set; }
        [DisplayName("State name")]
        public string StateName { get; set; }
        public Decimal Median { get; set; }
    }
}
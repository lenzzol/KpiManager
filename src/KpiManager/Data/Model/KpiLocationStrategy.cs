using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class KpiLocationStrategy
    {
        public int KpiId { get; set; }
        public string LocationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int KpiStrategyId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Kpi Kpi { get; set; }
        public virtual KpiStrategy KpiStrategy { get; set; }
        public virtual Location Location { get; set; }
    }
}

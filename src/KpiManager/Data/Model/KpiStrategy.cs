using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class KpiStrategy
    {
        public KpiStrategy()
        {
            KpiLocationStrategy = new HashSet<KpiLocationStrategy>();
            MetricScheme = new HashSet<MetricScheme>();
        }

        public int KpiStrategyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public int KpiId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int PointofsaleId { get; set; }

        public virtual ICollection<KpiLocationStrategy> KpiLocationStrategy { get; set; }
        public virtual ICollection<MetricScheme> MetricScheme { get; set; }
        public virtual Kpi Kpi { get; set; }
        public virtual Pointofsale Pointofsale { get; set; }
    }
}

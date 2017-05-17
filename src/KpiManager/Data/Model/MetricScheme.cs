using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class MetricScheme
    {
        public int MetricSchemeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int KpiStrategyId { get; set; }
        public int MetricId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int OperationOrder { get; set; }
        public int OperatorId { get; set; }

        public virtual KpiStrategy KpiStrategy { get; set; }
        public virtual Metric Metric { get; set; }
        public virtual Operator Operator { get; set; }
    }
}

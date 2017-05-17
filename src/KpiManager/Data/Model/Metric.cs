using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class Metric
    {
        public Metric()
        {
            MetricFormulaScheme = new HashSet<MetricFormulaScheme>();
            MetricScheme = new HashSet<MetricScheme>();
        }

        public int MetricId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DefaultResultValue { get; set; }
        public string Metric1 { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ResultDataTypeId { get; set; }

        public virtual ICollection<MetricFormulaScheme> MetricFormulaScheme { get; set; }
        public virtual ICollection<MetricScheme> MetricScheme { get; set; }
        public virtual DataType ResultDataType { get; set; }
    }
}

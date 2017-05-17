using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class MetricFormulaScheme
    {
        public int MetricFormulaSchemeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MetricId { get; set; }
        public int MetricFormulaId { get; set; }
        public int FormulaOrder { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Metric Metric { get; set; }
        public virtual MetricFormula MetricFormula { get; set; }
    }
}

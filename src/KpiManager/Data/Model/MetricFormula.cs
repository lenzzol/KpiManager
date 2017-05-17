using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class MetricFormula
    {
        public MetricFormula()
        {
            FormulaOperation = new HashSet<FormulaOperation>();
            MetricFormulaScheme = new HashSet<MetricFormulaScheme>();
        }

        public int MetricFormulaId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<FormulaOperation> FormulaOperation { get; set; }
        public virtual ICollection<MetricFormulaScheme> MetricFormulaScheme { get; set; }
    }
}

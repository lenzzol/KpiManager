using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class Operator
    {
        public Operator()
        {
            FormulaOperation = new HashSet<FormulaOperation>();
            MetricScheme = new HashSet<MetricScheme>();
        }

        public int OperatorId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Operator1 { get; set; }
        public string OperatorType { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<FormulaOperation> FormulaOperation { get; set; }
        public virtual ICollection<MetricScheme> MetricScheme { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class FormulaOperation
    {
        public int FormulaOperationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MetricFormulaId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int OperandId { get; set; }
        public int OperationOrder { get; set; }
        public int OperatorId { get; set; }

        public virtual MetricFormula MetricFormula { get; set; }
        public virtual Operand Operand { get; set; }
        public virtual Operator Operator { get; set; }
    }
}

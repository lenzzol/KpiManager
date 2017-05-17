using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class Operand
    {
        public Operand()
        {
            FormulaOperation = new HashSet<FormulaOperation>();
        }

        public int OperandId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? OperandDataFieldId { get; set; }
        public int OperandDataTypeId { get; set; }
        public string OperandValue { get; set; }

        public virtual ICollection<FormulaOperation> FormulaOperation { get; set; }
        public virtual DataField OperandDataField { get; set; }
        public virtual DataType OperandDataType { get; set; }
    }
}

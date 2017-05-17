using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class DataType
    {
        public DataType()
        {
            DataField = new HashSet<DataField>();
            Metric = new HashSet<Metric>();
            Operand = new HashSet<Operand>();
        }

        public int DataTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DataType1 { get; set; }
        public byte FloatPrecision { get; set; }
        public bool IsNumeric { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<DataField> DataField { get; set; }
        public virtual ICollection<Metric> Metric { get; set; }
        public virtual ICollection<Operand> Operand { get; set; }
    }
}

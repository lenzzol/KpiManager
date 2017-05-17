using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class DataNode
    {
        public DataNode()
        {
            DataField = new HashSet<DataField>();
        }

        public int DataNodeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DataCategoryId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string NodeName { get; set; }
        public int PointofsaleId { get; set; }

        public virtual ICollection<DataField> DataField { get; set; }
        public virtual DataCategory DataCategory { get; set; }
        public virtual Pointofsale Pointofsale { get; set; }
    }
}

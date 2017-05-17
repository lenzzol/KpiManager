using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class DataField
    {
        public DataField()
        {
            DataFieldRelationshipFieldSource = new HashSet<DataFieldRelationship>();
            DataFieldRelationshipFieldTarget = new HashSet<DataFieldRelationship>();
            Operand = new HashSet<Operand>();
        }

        public int DataFieldId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DataFieldName { get; set; }
        public int? DataFieldRelationshipId { get; set; }
        public int DataNodeId { get; set; }
        public int DataTypeId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<DataFieldRelationship> DataFieldRelationshipFieldSource { get; set; }
        public virtual ICollection<DataFieldRelationship> DataFieldRelationshipFieldTarget { get; set; }
        public virtual ICollection<Operand> Operand { get; set; }
        public virtual DataFieldRelationship DataFieldRelationship { get; set; }
        public virtual DataNode DataNode { get; set; }
        public virtual DataType DataType { get; set; }
    }
}

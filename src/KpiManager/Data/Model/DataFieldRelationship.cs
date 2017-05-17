using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class DataFieldRelationship
    {
        public DataFieldRelationship()
        {
            DataField = new HashSet<DataField>();
        }

        public int DataFieldRelationshipId { get; set; }
        public int FieldSourceId { get; set; }
        public int FieldTargetId { get; set; }

        public virtual ICollection<DataField> DataField { get; set; }
        public virtual DataField FieldSource { get; set; }
        public virtual DataField FieldTarget { get; set; }
    }
}

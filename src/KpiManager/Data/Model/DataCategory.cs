using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class DataCategory
    {
        public DataCategory()
        {
            DataNode = new HashSet<DataNode>();
        }

        public int DataCategoryId { get; set; }
        public String Category { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<DataNode> DataNode { get; set; }
    }
}

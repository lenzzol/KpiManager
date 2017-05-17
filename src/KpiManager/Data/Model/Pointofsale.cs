using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class Pointofsale
    {
        public Pointofsale()
        {
            DataNode = new HashSet<DataNode>();
            KpiStrategy = new HashSet<KpiStrategy>();
        }

        public int PointofsaleId { get; set; }

        public virtual ICollection<DataNode> DataNode { get; set; }
        public virtual ICollection<KpiStrategy> KpiStrategy { get; set; }
    }
}

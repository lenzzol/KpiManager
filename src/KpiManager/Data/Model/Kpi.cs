using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class Kpi
    {
        public Kpi()
        {
            KpiLocationStrategy = new HashSet<KpiLocationStrategy>();
            KpiStrategy = new HashSet<KpiStrategy>();
        }

        public int KpiId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KpiLocationStrategy> KpiLocationStrategy { get; set; }
        public virtual ICollection<KpiStrategy> KpiStrategy { get; set; }
    }
}

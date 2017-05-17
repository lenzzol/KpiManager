using System;
using System.Collections.Generic;

namespace KpiManager
{
    public partial class Location
    {
        public Location()
        {
            KpiLocationStrategy = new HashSet<KpiLocationStrategy>();
        }

        public string LocationId { get; set; }

        public virtual ICollection<KpiLocationStrategy> KpiLocationStrategy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceBaseClass;

namespace MiddleLayer
{
    public class baseClass:IbaseClass
    {
        public string name { get; set; }
        public string shortCode { get; set; }
        public string description { get; set; }
        public DateTime createdOn { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public int ?primaryCategoryId { get; set; }
        public int ?secondaryCategoryId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public char type { get; set; }
        public int isExpired { get; set; }
        public decimal discountPercent { get; set; }
        public int unitsBooked { get; set; }
        public decimal revenueGenerated { get; set; }
        public int ?productId { get; set; }
        public string expiredBy { get; set; }
        public decimal price { get; set; }

    }
    public class primaryCategory : baseClass
    {

    }
    public class secondaryCategory : baseClass
    {
    }

    public class product : baseClass
    {
    }

    public class Scheme : baseClass
    {

    }
}

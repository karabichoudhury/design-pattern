using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InterfaceBaseClass
{
    public interface IbaseClass
    {
        string name { get; set; }
        string shortCode { get; set; }
        string description { get; set; }
        DateTime createdOn { get; set; }
        string createdBy { get; set; }
        DateTime modifiedOn { get; set; }
        string modifiedBy { get; set; }
        int? primaryCategoryId { get; set; }
        int? secondaryCategoryId { get; set; }
        DateTime startDate { get; set; }
        DateTime endDate { get; set; }
        char type { get; set; }
        int isExpired { get; set; }
        decimal discountPercent { get; set; }
        int unitsBooked { get; set; }
        decimal revenueGenerated { get; set; }
        int? productId { get; set; }
        string expiredBy { get; set; }
        decimal price { get; set; }
    }

    public abstract class baseCategory
    {
        [Key]
        int id { get; set; }
        string name { get; set; }
        string shortCode { get; set; }
        string description { get; set; }
    }
}

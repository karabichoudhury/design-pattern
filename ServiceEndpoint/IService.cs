using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace ServiceEndpoint
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        //Add
        [OperationContract]
        void Add(string name, string shortCode, string description, decimal price, int primaryCategoryId, int secondaryCategoryId, string model);

        [OperationContract]
        void Delete(string shortCode, string model);

        [OperationContract]
        void Update(string name, string shortCode, string description, decimal price, int primaryCategoryId, int secondaryCategoryId, string model);

        
        [OperationContract]
        void GetPrimaryCategories();

           
        //Scheme operations
        [OperationContract]
        void AddScheme(string name, string shortCode, string description, DateTime startdate, DateTime endDate, bool isExpired,decimal discountPercent,int unitsBooked,decimal revenueGenerated, int primaryCategoryId, int secondaryCategoryId, int productId, string expiredBy);

        [OperationContract]
        List<SchemeObj> GetScheme(int primaryCategoryId, int secondaryCategoryId, int productId);

        [OperationContract]
        List<ProductsAndSchemes> GetSchemeForProducts(List<ProductsAndQty> products);
    }

    [DataContract]
    public class SchemeObj
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string shortCode { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public DateTime startDate { get; set; }
        [DataMember]
        public DateTime endDate { get; set; }
        [DataMember]
        public char type { get; set; }
        [DataMember]
        public int isExpired { get; set; }
        [DataMember]
        public decimal discountPercent { get; set; }
        [DataMember]
        public int unitsBooked { get; set; }
        [DataMember]
        public decimal revenueGenerated { get; set; }
        [DataMember]
        public int? primaryCategoryId { get; set; }
        [DataMember]
        public int? secondaryCategoryId { get; set; }
        [DataMember]
        public int? productId { get; set; }
        [DataMember]
        public string expiredBy { get; set; }
        [DataMember]
        public DateTime createdOn { get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public DateTime modifiedOn { get; set; }
        [DataMember]
        public string modifiedBy { get; set; }
        
        
        
        
       
        
       
        
        
        
    }

    [DataContract]
    public class ProductsAndQty
    {
        [DataMember]
        public int productId { get; set; }
        [DataMember]
        public int orderedQty { get; set; }
    }

    [DataContract]
    public class ProductsAndSchemes
    {
        [DataMember]
        public ProductsAndQty productqty { get; set; }
        
        [DataMember]
        public List<SchemeObj> schemes { get; set; }
    }

}

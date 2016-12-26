using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FactoryDal;
using InterfaceBaseClass;
using InterfaceDal;
using FactoryEntity;

namespace ServiceEndpoint
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        IbaseClass obj = null;

        public void Add(string name, string shortCode, string description, decimal price, int primaryCategoryId, int secondaryCategoryId, string model)
        {
            try
            {
                IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");


                switch (model)
                {
                    case "PC":
                        obj = Factory<IbaseClass>.Create("PC");
                        obj.primaryCategoryId = 0;
                        obj.secondaryCategoryId = 0;
                        break;
                    case "SC":
                        obj = Factory<IbaseClass>.Create("SC");
                        obj.secondaryCategoryId = 0;
                        obj.primaryCategoryId = primaryCategoryId;
                        break;
                    case "PR":
                        obj = Factory<IbaseClass>.Create("PR");
                        obj.primaryCategoryId = 0;
                        obj.secondaryCategoryId = secondaryCategoryId;
                        obj.price = price;
                        break;

                }



                obj.name = name;
                obj.shortCode = shortCode;
                obj.description = description;
                dal.Add(obj);
                dal.Save(model);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);

            }


        }

        public void Delete(string shortCode, string model)
        {
            try
            {
                IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");

                switch (model)
                {
                    case "PC":
                        obj = Factory<IbaseClass>.Create("PC");
                        break;
                    case "SC":
                        obj = Factory<IbaseClass>.Create("SC");
                        break;
                    case "PR":
                        obj = Factory<IbaseClass>.Create("PR");
                        break;

                }

                obj.shortCode = shortCode;
                dal.Delete(obj, model);
            }

            catch (Exception e)
            {
                throw new FaultException(e.Message);

            }




        }

        public void Update(string name, string shortCode, string description, decimal price, int primaryCategoryId, int secondaryCategoryId, string model)
        {
            try
            {
                IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");

                switch (model)
                {
                    case "PC":
                        obj = Factory<IbaseClass>.Create("PC");
                        obj.primaryCategoryId = 0;
                        obj.secondaryCategoryId = 0;
                        break;
                    case "SC":
                        obj = Factory<IbaseClass>.Create("SC");
                        obj.secondaryCategoryId = 0;
                        obj.primaryCategoryId = primaryCategoryId;
                        break;
                    case "PR":
                        obj = Factory<IbaseClass>.Create("PR");
                        obj.primaryCategoryId = 0;
                        obj.secondaryCategoryId = secondaryCategoryId;
                        obj.price = price;
                        break;

                }


                obj.name = name;
                obj.shortCode = shortCode;
                obj.description = description;
                dal.Update(obj, model);
            }

            catch (Exception e)
            {
                throw new FaultException(e.Message);

            }
        }


        public void GetPrimaryCategories()
        {
            IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");
            obj = Factory<IbaseClass>.Create("PC");

            List<IbaseClass> categories = dal.Search();
        }

        public void AddScheme(string name, string shortCode, string description, DateTime startdate, DateTime endDate, bool isExpired, decimal discountPercent, int unitsBooked,decimal revenueGenerated, int primaryCategoryId, int secondaryCategoryId, int productId, string expiredBy)
        {
            try
            {
                IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");
                obj = Factory<IbaseClass>.Create("SH");
                if (primaryCategoryId == 0)
                    obj.primaryCategoryId = null;
                else
                    obj.primaryCategoryId = primaryCategoryId;
                if (secondaryCategoryId == 0)
                    obj.secondaryCategoryId = null;
                else
                    obj.secondaryCategoryId = secondaryCategoryId;
                if (productId == 0)
                    obj.productId = null;
                else
                    obj.productId = productId;

                obj.name = name;
                obj.shortCode = shortCode;
                obj.description = description;
                obj.startDate = startdate;
                obj.endDate = endDate;
                obj.isExpired = isExpired?1:0 ;
                obj.type = unitsBooked > 0 ? 'U' : 'R';
                obj.discountPercent = discountPercent ;
                obj.unitsBooked =unitsBooked;
                obj.revenueGenerated = revenueGenerated;

                obj.expiredBy = expiredBy;
                dal.Add(obj);
                dal.Save("SH");
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);

            }



        }

        public List<SchemeObj> GetScheme(int primaryCategoryId, int secondaryCategoryId, int productId)
        {
            try
            {
                IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");
                obj = Factory<IbaseClass>.Create("SH");

                List<IbaseClass> schemes = dal.SearchObj(primaryCategoryId, secondaryCategoryId, productId);
                List<SchemeObj> Schemes = new List<SchemeObj>();
                foreach (var o in schemes)
                {
                    SchemeObj s = transform(o);
                    Schemes.Add(s);
                }
                return Schemes;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            
        }

        public List<ProductsAndSchemes> GetSchemeForProducts(List<ProductsAndQty> products)
        {
            try
            {
                IDal<IbaseClass> dal = FactoryDALLayer<IDal<IbaseClass>>.Create("ADODal");
                obj = Factory<IbaseClass>.Create("SH");

                List<ProductsAndSchemes> productsSchemes = new List<ProductsAndSchemes>();
                foreach (var item in products)
                {

                    List<IbaseClass> schemes = dal.SearchScheme(item.productId, item.orderedQty);
                    List<SchemeObj> Schemes = new List<SchemeObj>();
                    ProductsAndSchemes ps = new ProductsAndSchemes();
                    foreach (var o in schemes)
                    {
                        SchemeObj s = transform(o);
                        Schemes.Add(s);
                    }
                    ps.productqty = item;
                    ps.schemes = Schemes;
                    productsSchemes.Add(ps);
                }
                return productsSchemes;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            
        }

        private SchemeObj transform(IbaseClass obj)
        {
            SchemeObj schemeObj = new SchemeObj();
            schemeObj.name = obj.name;
            schemeObj.shortCode = obj.shortCode;
            schemeObj.description = obj.description;
            schemeObj.startDate = obj.startDate;
            schemeObj.endDate = obj.endDate;
            schemeObj.type = obj.type;
            schemeObj.isExpired = obj.isExpired;
            schemeObj.discountPercent = obj.discountPercent;
            schemeObj.unitsBooked = obj.unitsBooked;
            schemeObj.revenueGenerated = obj.revenueGenerated;
            schemeObj.primaryCategoryId = obj.primaryCategoryId;
            schemeObj.secondaryCategoryId = obj.secondaryCategoryId;
            schemeObj.productId = obj.productId;
            schemeObj.expiredBy = obj.expiredBy;
            schemeObj.createdOn = obj.createdOn;
            schemeObj.createdBy = obj.createdBy;
            schemeObj.modifiedOn = obj.modifiedOn;
            schemeObj.modifiedBy = obj.modifiedBy;

            return schemeObj;

        }
    }
}

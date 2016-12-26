using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceDal;
using CommonDAL;
using System.Data;
using System.Data.SqlClient;
using InterfaceBaseClass;
using FactoryEntity;


namespace ADODAL
{
    public abstract class TemplateADO<TEntity> : AbstractDAL<TEntity>
    {
        protected SqlConnection objConnection = null;
        protected SqlCommand objCommand = null;

        public TemplateADO(string _ConnectionString)
            : base(_ConnectionString)
        {

        }
        private void Open()
        {
            objConnection = new SqlConnection(ConnectionString);
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.Connection = objConnection;

        }

        protected abstract List<TEntity> ExecuteCommand();
        protected abstract void ExecuteCommand(TEntity obj, string operation, string model);
        protected abstract List<TEntity> ExecuteCommand(int primaryId, int secondaryId, int productId);
        protected abstract List<TEntity> ExecuteCommand(int productId,int qty);


        private void Close()
        {
            objConnection.Close();
        }


        public void Execute(TEntity obj, string operation, string model)
        {
            Open();
            ExecuteCommand(obj, operation, model);
            Close();
        }

        public List<TEntity> Execute() // Fixed Sequence select
        {
            List<TEntity> objTypes = null;
            Open();
            objTypes = ExecuteCommand();
            Close();
            return objTypes;
        }

        public List<TEntity> Execute(int primaryId, int secondaryId, int productId)
        {
            List<TEntity> objTypes = null;
            Open();
            objTypes = ExecuteCommand(primaryId, secondaryId, productId);
            Close();
            return objTypes;
        }

        public List<TEntity> Execute(int productId,int qty)
        {
            List<TEntity> objTypes = null;
            Open();
            objTypes = ExecuteCommand(productId,qty);
            Close();
            return objTypes;
        }


        public override void Save(string model)
        {
            foreach (TEntity o in anyTypes)
            {
                Execute(o, "C", model);
            }
        }

        public override List<TEntity> Search()
        {
            return Execute();
        }

        public override List<TEntity> SearchObj(int primaryId, int secondaryId, int productId)
        {
            return Execute(primaryId, secondaryId, productId);
        }

        public override List<TEntity> SearchScheme(int productId, int qty)
        {
            return Execute(productId,qty);
        }

        public override void Delete(TEntity obj, string model)
        {

            Execute(obj, "D", model);

        }

        public override void Update(TEntity obj, string model)
        {

            Execute(obj, "U", model);

        }

    }

    public class BaseDAL : TemplateADO<IbaseClass>
    {
        public BaseDAL(string _ConnectionString)
            : base(_ConnectionString)
        {

        }

        protected override List<IbaseClass> ExecuteCommand()
        {
            objCommand.CommandText = "select * from tblPrimaryCategory";
            SqlDataReader dr = null;
            dr = objCommand.ExecuteReader();
            List<IbaseClass> categories = new List<IbaseClass>();
            while (dr.Read())
            {
                IbaseClass category = Factory<IbaseClass>.Create("PC");
                category.name = dr["Name"].ToString();
                category.shortCode = dr["ShortCode"].ToString();
                category.description = dr["Description"].ToString();
                categories.Add(category);
            }
            return categories;
        }


        protected override void ExecuteCommand(IbaseClass obj, string operation, string model)
        {
            // try
            // {
            string table = "";
            switch (model)
            {
                case "PC":
                    table = "tblPrimaryCategory";
                    break;
                case "SC":
                    table = "tblSecondaryCategory";
                    break;
                case "PR":
                    table = "tblProduct";
                    break;
                case "SH":
                    table = "tblScheme";
                    break;
            }

            if (operation == "C")
            {
                if (model == "PC")
                {
                    objCommand.CommandText = "Insert into " + table + " (Name,ShortCode,Description,CreatedBy) values('" + obj.name + "','" + obj.shortCode + "','" + obj.description + "','" + Environment.UserName + "')";
                }
                else
                {
                    if (model == "SC")
                    {
                        objCommand.CommandText = "Insert into " + table + " (Name,ShortCode,Description,PrimaryCategoryId,CreatedBy) values('" + obj.name + "','" + obj.shortCode + "','" + obj.description + "','" + obj.primaryCategoryId + "','" + Environment.UserName + "')";

                    }
                    else
                    {
                        if (model == "PR")
                        {
                            objCommand.CommandText = "Insert into " + table + " (Name,ShortCode,Description,Price,SecondaryCategoryId,CreatedBy) values('" + obj.name + "','" + obj.shortCode + "','" + obj.description + "','" + obj.price + "','" + obj.secondaryCategoryId + "','" + Environment.UserName + "')";

                        }
                        else
                        {
                            string format = "yyyy-MM-dd HH:mm:ss";
                            var pcId = "";
                            var scId = "";
                            var pdId = "";
                            if (obj.primaryCategoryId == null)
                                pcId = "NULL";
                            else
                                pcId = obj.primaryCategoryId.ToString();

                            if (obj.secondaryCategoryId == null)
                                scId = "NULL";
                            else
                                scId = obj.secondaryCategoryId.ToString();
                            if (obj.productId == null)
                                pdId = "NULL";
                            else
                                pdId = obj.productId.ToString();

                            objCommand.CommandText = "Insert into " + table + " (Name,ShortCode,Description,StartDate,EndDate,type,IsExpired,DiscountPercent,UnitsBooked,RevenueGenerated,PrimaryCategoryId,SecondaryCategoryId,ProductId,ExpiredBy,CreatedBy) values('" + obj.name + "','" + obj.shortCode + "','" + obj.description + "','" + obj.startDate.ToString(format) + "','" + obj.endDate.ToString(format) + "','" + obj.type + "'," + obj.isExpired + "," + obj.discountPercent + "," + obj.unitsBooked + "," + obj.revenueGenerated + "," + pcId + "," + scId + "," + pdId + ",'" + obj.expiredBy + "','" + Environment.UserName + "')";

                        }

                    }
                }

                objCommand.ExecuteNonQuery();
            }
            else
            {
                if (operation == "U")
                {
                    if (model == "PC")
                    {
                        DateTime time = DateTime.Now;              // Use current time
                        string format = "yyyy-MM-dd HH:mm:ss";
                        objCommand.CommandText = "Update " + table + " set Name = '" + obj.name + "',Description='" + obj.description + "',ModifiedOn='" + time.ToString(format) + "',ModifiedBy='" + Environment.UserName + "' where ShortCode='" + obj.shortCode + "'";
                        objCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        if (model == "SC")
                        {
                            DateTime time = DateTime.Now;              // Use current time
                            string format = "yyyy-MM-dd HH:mm:ss";
                            objCommand.CommandText = "Update " + table + " set Name = '" + obj.name + "',Description='" + obj.description + "',ModifiedOn='" + time.ToString(format) + "',ModifiedBy='" + Environment.UserName + "',PrimaryCategoryId=" + obj.primaryCategoryId + " where ShortCode='" + obj.shortCode + "'";
                            objCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            DateTime time = DateTime.Now;              // Use current time
                            string format = "yyyy-MM-dd HH:mm:ss";
                            objCommand.CommandText = "Update " + table + " set Name = '" + obj.name + "',Description='" + obj.description + "',Price='" + obj.price + "',ModifiedOn='" + time.ToString(format) + "',ModifiedBy='" + Environment.UserName + "',SecondaryCategoryId=" + obj.secondaryCategoryId + " where ShortCode='" + obj.shortCode + "'";
                            objCommand.ExecuteNonQuery();
                        }
                    }

                }
                else
                {
                    if (operation == "D")
                    {
                        objCommand.CommandText = "Delete from " + table + " where ShortCode='" + obj.shortCode + "'";
                        objCommand.ExecuteNonQuery();
                    }
                }
            }


            // }
            //catch (Exception ex)
            //{
            //    throw new Exception();

            //}








        }

        protected override List<IbaseClass> ExecuteCommand(int primaryId, int secondaryId, int productId)
        {
            objCommand.CommandText = "select * from tblScheme where (PrimaryCategoryId=" + primaryId + " or SecondarycategoryId=" + secondaryId + " or ProductId=" + productId + ") and IsExpired=0";
            SqlDataReader dr = null;
            dr = objCommand.ExecuteReader();
            List<IbaseClass> schemes = new List<IbaseClass>();
            while (dr.Read())
            {
                IbaseClass scheme = Factory<IbaseClass>.Create("SH");
                scheme.name = dr["Name"].ToString();
                scheme.shortCode = dr["ShortCode"].ToString();
                scheme.description = dr["Description"].ToString();
                scheme.startDate =Convert.ToDateTime( dr["StartDate"]);
                scheme.endDate = Convert.ToDateTime(dr["EndDate"]);
                scheme.discountPercent = Convert.ToInt32( dr["DiscountPercent"]);
                schemes.Add(scheme);
            }
            return schemes;
        }

        protected override List<IbaseClass> ExecuteCommand(int productId,int qty)
        {
            objCommand.CommandText = "select * from tblScheme where  ProductId=" + productId + " and UnitsBooked<=" + qty + " and IsExpired=0 and type='U'";
            SqlDataReader dr = null;
            dr = objCommand.ExecuteReader();
            List<IbaseClass> schemes = new List<IbaseClass>();
            while (dr.Read())
            {
                IbaseClass scheme = Factory<IbaseClass>.Create("SH");
                scheme.name = dr["Name"].ToString();
                scheme.shortCode = dr["ShortCode"].ToString();
                scheme.description = dr["Description"].ToString();
                scheme.startDate = Convert.ToDateTime(dr["StartDate"]);
                scheme.endDate = Convert.ToDateTime(dr["EndDate"]);
                scheme.discountPercent = Convert.ToInt32(dr["DiscountPercent"]);
                schemes.Add(scheme);
            }
            return schemes;
        }

    }
}

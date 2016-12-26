using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceDal;

namespace CommonDAL
{
    public abstract class AbstractDAL<TEntity>:IDal<TEntity>
    {
        protected string ConnectionString = "";
        protected List<TEntity> anyTypes = new List<TEntity>();

        public AbstractDAL(string _connectionString)
        {
            ConnectionString = _connectionString;
        }

        public virtual void Add(TEntity obj)
        {
            anyTypes.Add(obj);
        }

        public virtual void Update(TEntity obj,string model)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity obj,string model)
        {
            throw new NotImplementedException();
        }

        public virtual List<TEntity> Search()
        {
            throw new NotImplementedException();
        }

        public virtual List<TEntity> SearchObj(int primaryCategoryId, int secondaryId, int productId)
        {
            throw new NotImplementedException();
        }

        public virtual List<TEntity> SearchScheme(int productId, int qty)
        {
            throw new NotImplementedException();
        }


        public virtual void Save(string model)
        {
            throw new NotImplementedException();
        }
    }
}

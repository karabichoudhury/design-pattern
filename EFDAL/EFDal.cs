using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceDal;
using System.Data.Entity;
using InterfaceBaseClass;

namespace EFDAL
{
    public class EFDalAbstract<AnyType>: DbContext,IDal<AnyType>
        where AnyType:class
    {
        public EFDalAbstract():base("name=Conn")
        {

        }
        public void Add(AnyType obj)
        {
            Set<AnyType>().Add(obj);
        }

        public void Update(AnyType obj, string model)
        {
            throw new NotImplementedException();
        }

        public void Delete(AnyType obj, string model)
        {
            throw new NotImplementedException();
        }

        public List<AnyType> Search()
        {
            throw new NotImplementedException();
        }

        public List<AnyType> SearchObj(int primaryCategoryId, int secondaryId, int productId)
        {
            throw new NotImplementedException();
        }

        public List<AnyType> SearchScheme(int productId, int qty)
        {
            throw new NotImplementedException();
        }

        public void Save(string model)
        {
            SaveChanges();
        }
    }

    public class EFbaseDal : EFDalAbstract<baseCategory>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<baseCategory>()
                .ToTable("tblPrimaryCategory");
        }
    }
}

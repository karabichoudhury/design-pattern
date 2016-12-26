using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal
{
    public interface IDal<TEntitiy>
    {
        void Add(TEntitiy obj);//In memory addition
        void Update(TEntitiy obj,string model);//In memory updation
        void Delete(TEntitiy obj,string model);
        List<TEntitiy> Search();
        List<TEntitiy> SearchObj(int primaryCategoryId,int secondaryId,int productId);
        List<TEntitiy> SearchScheme(int productId,int qty);
        void Save(string model);//physical commit

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MiddleLayer;
using CommonDAL;
using InterfaceDal;
using InterfaceBaseClass;

namespace FactoryEntity
{
    public static class Factory<AnyType>
    {
        private static IUnityContainer container = null;

        public static AnyType Create(string type)
        {
            if (container == null)
            {
                container = new UnityContainer();
                container.RegisterType<IbaseClass, primaryCategory>("PC");
                container.RegisterType<IbaseClass, secondaryCategory>("SC");
                container.RegisterType<IbaseClass, product>("PR");
                container.RegisterType<IbaseClass, Scheme>("SH");
            }
            
            


            return container.Resolve<AnyType>(type);
        }

        
    
    }
}

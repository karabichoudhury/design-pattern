using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADODAL;
using InterfaceBaseClass;
using InterfaceDal;
using CommonDAL;
using Microsoft.Practices.Unity;
using EFDAL;

namespace FactoryDal
{
    public class FactoryDALLayer<AnyType>
    {
        private static IUnityContainer container = null;

        public static AnyType Create(string type)
        {
            if (container == null)
            {
                container = new UnityContainer();
                container.RegisterType<IDal<IbaseClass>, BaseDAL>("ADODal");
                container.RegisterType<IDal<baseCategory>, EFbaseDal>("EFDal");
            }

            return container.Resolve<AnyType>(type,
               new ResolverOverride[]
                {
                    new ParameterOverride("_ConnectionString",@"Data Source=TOSIBA-PC\SQLEXPRESS;Initial Catalog=FieldAssist;Integrated Security=true")
                }
               );

        }

    }
}

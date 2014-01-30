using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemBuckets.Types;
using Sitecore.Reflection;

namespace Sitecore.ItemBuckets.TypeCreator
{
    public class ObjectCreator<T> : IObjectCreator<T>
    {
        public virtual T Create(ITypeDefinition def)
        {
            var objectCreated = ReflectionUtil.CreateObject(def.TypeName);
            if (objectCreated is T)
            {
                AssignParameters(objectCreated, def);
                return (T) objectCreated;
            }
            throw new InvalidCastException();
        }

        public virtual void AssignParameters(object o, ITypeDefinition def)
        {
            foreach (var key in def.Parameters.AllKeys)
            {
                var propInfo = ReflectionUtil.GetPropertyInfo(o, key);
                if (propInfo != null)
                {
                    propInfo.SetValue(o, def.Parameters[key]);
                }
            }
        }
    }
}

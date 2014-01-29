using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Reflection;

namespace Sitecore.ItemBuckets.TypeCreator
{
    public class TypeCreator<T> : ITypeCreator<T>
    {
        public virtual T Create(TypeDefinition def)
        {
            var objectCreated = ReflectionUtil.CreateObject(def.TypeName);
            if (objectCreated is T)
            {
                AssignParameters(objectCreated, def);
                return (T) objectCreated;
            }
            throw new InvalidCastException();
        }

        public virtual void AssignParameters(object o, TypeDefinition def)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemBuckets.TypeCreator
{
    public interface ITypeCreator<T>
    {
        T Create(TypeDefinition def);
    }
}

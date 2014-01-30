using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemBuckets.TypeCreator
{
    public interface IObjectCreator<T>
    {
        T Create(ITypeDefinition def);
    }
}

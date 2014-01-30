using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.ItemBuckets.TypeCreator;

namespace Sitecore.ItemBuckets.Types
{
    public interface IBucket
    {
        ITypeDefinition DynamicFolderPath { get;  }
        Guid Id { get; }
    }
}

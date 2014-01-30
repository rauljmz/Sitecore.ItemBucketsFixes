using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Util;
using Sitecore.ItemBuckets.Types;

namespace Sitecore.ItemBuckets.DynamicFolderPathResolvers
{
    public interface IDynamicFolderResolver
    {
        string GetFolderPath(IItem newItemId, IBucket parentItemId, DateTime creationDateOfNewItem);
    }
}

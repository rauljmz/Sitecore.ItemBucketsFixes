using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Util;

namespace Sitecore.ItemBuckets.Types
{
    public interface IDynamicFolderPath
    {
        string GetFolderPath(Guid newItemId, Guid parentItemId, DateTime creationDateOfNewItem);
    }
}

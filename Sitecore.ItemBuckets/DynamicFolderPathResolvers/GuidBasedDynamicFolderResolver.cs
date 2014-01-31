using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemBuckets.DynamicFolderPathResolvers
{
    public class GuidBasedDynamicFolderResolver : IDynamicFolderResolver
    {
        public string Depth { get; set; }

        public GuidBasedDynamicFolderResolver()
        {
            Depth = "6";
        }
        public string GetFolderPath(Types.IItem newItemId, Types.IBucket parentItemId, DateTime creationDateOfNewItem)
        {
            var i = int.Parse(Depth);
            return newItemId.Id.ToString().ToString().Take(i).Zip(new String('/', i), (a, b) => string.Concat(a, b)).Aggregate((a, c) => a += c).TrimEnd('/');
            
        }
    }
}

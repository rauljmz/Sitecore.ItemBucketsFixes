using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.ItemBuckets.Helpers
{
    public static class Extensions
    {
        public static CheckboxField IsBucketItemCheckBox(this Item item)
        {
            return item.Fields[Sitecore.Buckets.Util.Constants.IsBucket];
        }

        public static bool IsLockedChildRelationship(this Item item)
        {
            if ((item != null) && (item.Fields[Sitecore.Buckets.Util.Constants.ShouldNotOrganizeInBucket] != null))
            {
                return (item.Fields[Sitecore.Buckets.Util.Constants.ShouldNotOrganizeInBucket].Value == "1");
            }
            return true;
        }

 

 

    }
}

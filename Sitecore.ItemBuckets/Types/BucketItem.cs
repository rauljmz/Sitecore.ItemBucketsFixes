using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ItemBuckets.Helpers;
using Sitecore.ItemBuckets.TypeCreator;

namespace Sitecore.ItemBuckets.Types
{
    public class BucketItem : SitecoreItem, IBucket
    {
        public static BucketItem Create(ID id)
        {
            return new BucketItem(MakeItemFromID(id));
        } 

        public BucketItem(Item item) : base(item) { }

        public ITypeDefinition DynamicFolderPath
        {
            get
            {
                ReferenceField field = InnerItem.Fields[References.__DynamicFolderPath];
                if (field != null && field.InnerField.HasValue)
                {
                    return new TypeDefinition(field.TargetItem);
                }
                return null;
            }
           
        }      
    }
}

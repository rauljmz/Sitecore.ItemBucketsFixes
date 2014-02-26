using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ItemBuckets.Helpers;
using Sitecore.ItemBuckets.Types;

namespace Sitecore.ItemBuckets.Types
{
    public class TypeDefinition : CustomItem, ITypeDefinition
    {
        public TypeDefinition(Item item):base(item) {}

        public string TypeName
        {
            get
            {
                return InnerItem[References.TypeName];
            }
        }
        public NameValueCollection Parameters
        {
            get
            {
                NameValueListField parameters = InnerItem.Fields[References.Parameters];
                if (parameters != null)
                {
                    return parameters.NameValues;
                }
                return new NameValueCollection();
            }
        }
    }
}

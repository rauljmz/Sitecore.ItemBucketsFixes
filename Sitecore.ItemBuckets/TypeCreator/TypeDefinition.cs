using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.ItemBuckets.TypeCreator
{
    public class TypeDefinition : CustomItem, ITypeDefinition
    {
        public TypeDefinition(Item item):base(item) {}

        public string TypeName
        {
            get
            {
                return InnerItem["{5D12AB04-12F5-4A9C-B8D7-4A0889FD4E49}"];
            }
        }
        public NameValueCollection Parameters
        {
            get
            {
                NameValueListField parameters = InnerItem.Fields["{8C0043E6-DC96-4381-BDA7-16E660EC67BB}"];
                if (parameters != null)
                {
                    return parameters.NameValues;
                }
                return new NameValueCollection();
            }
        }
    }
}

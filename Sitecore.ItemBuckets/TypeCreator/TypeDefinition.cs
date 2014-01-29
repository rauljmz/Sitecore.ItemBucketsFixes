using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemBuckets.TypeCreator
{
    public class TypeDefinition
    {
        public string TypeName { get; set; }
        public NameValueCollection Parameters { get; set; }
    }
}

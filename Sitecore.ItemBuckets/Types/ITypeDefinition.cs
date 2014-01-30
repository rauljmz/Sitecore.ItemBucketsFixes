using System;
using System.Collections.Specialized;
namespace Sitecore.ItemBuckets.Types
{
   public interface ITypeDefinition
    {
        NameValueCollection Parameters { get; }
        string TypeName { get;}
    }
}

using System;
using System.Collections.Specialized;
namespace Sitecore.ItemBuckets.TypeCreator
{
   public interface ITypeDefinition
    {
        NameValueCollection Parameters { get; }
        string TypeName { get;}
    }
}

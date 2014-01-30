using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.ItemBuckets.Types
{
    public interface IItem
    {
        DateTime GetDateField(string idOrName);
        Guid Id { get; }
    }
}

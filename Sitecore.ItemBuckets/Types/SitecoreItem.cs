using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ItemBuckets.Types;

namespace Sitecore.ItemBuckets.Types
{
    public class SitecoreItem : CustomItem, IItem
    {

        protected static Item MakeItemFromID(ID id)
        {
            var db = Sitecore.Context.ContentDatabase ?? Sitecore.Context.Database;
            return db.GetItem(id);
        }
        public static SitecoreItem Create(ID id)
        {
            return new SitecoreItem(MakeItemFromID(id));
        } 

        public SitecoreItem(Item i):base(i) {}

        public Guid Id
        {
            get { return ID.ToGuid(); }
        }

        public DateTime GetDateField(string idOrName)
        {
            DateField dateField = InnerItem.Fields[idOrName];
            return dateField != null ? dateField.DateTime : DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Util;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.ItemBuckets.TypeCreator;


namespace Sitecore.ItemBuckets.DynamicFolders
{
    public class DynamicFolderSwitcher : IDynamicBucketFolderPath
    {
        private static DynamicFolderCache _cache;        

        public DynamicFolderCache Cache
        {
            get { return DynamicFolderSwitcher._cache; }
            set { DynamicFolderSwitcher._cache = value; }
        }

        public ITypeCreator<IDynamicBucketFolderPath> TypeCreator { get; set; }

        public DynamicFolderSwitcher() : this(new DynamicFolderCache(), new TypeCreator<IDynamicBucketFolderPath>()) { }
        public DynamicFolderSwitcher(DynamicFolderCache cache, ITypeCreator<IDynamicBucketFolderPath> creator) 
        {
            Sitecore.Events.Event.Subscribe("item:saved", SavedItem);
            if (Cache == null)
            {
                Cache = cache;
            }
        }

        protected virtual void SavedItem(object sender, EventArgs e)
        {
            var savedItem = Event.ExtractParameter<Item>(e, 0);
            if (savedItem == null) return;
            
            Cache.Remove(savedItem.ID);
        }

        public virtual string GetFolderPath(Data.ID newItemId, Data.ID parentItemId, DateTime creationDateOfNewItem)
        {
            
            throw new NotImplementedException();
        }
    }
}

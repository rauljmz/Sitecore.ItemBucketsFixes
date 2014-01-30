using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Util;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.ItemBuckets.TypeCreator;
using Sitecore.ItemBuckets.Types;


namespace Sitecore.ItemBuckets.DynamicFolders
{
    public class DynamicFolderSwitcher
    {
        private DynamicFolderCache _cache;


        public DynamicFolderCache Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }

        public IObjectCreator<IDynamicFolderPath> TypeCreator { get; set; }

        public DynamicFolderSwitcher() : this(new DynamicFolderCache(), new ObjectCreator<IDynamicFolderPath>()) { }
        public DynamicFolderSwitcher(DynamicFolderCache cache, IObjectCreator<IDynamicFolderPath> creator)
        {
            Sitecore.Events.Event.Subscribe("item:saved", SavedItem);
            TypeCreator = creator;
            Cache = cache;
            
        }

        protected virtual void SavedItem(object sender, EventArgs e)
        {
            var savedItem = Event.ExtractParameter<Item>(e, 0);
            if (savedItem == null) return;

            Cache.Remove(savedItem.ID.Guid);
        }

        public virtual IDynamicFolderPath GetFolderPath(IBucket bucket)
        {

            if (Cache.ContainsKey(bucket.Id))
            {
                return Cache[bucket.Id];
            }
            //may throw null ref if item not found

            var iDynamicBucketFolderPath = this.TypeCreator.Create(bucket.DynamicFolderPath);

            Cache.Add(bucket.Id, iDynamicBucketFolderPath);


            return iDynamicBucketFolderPath;
        }
    }
}

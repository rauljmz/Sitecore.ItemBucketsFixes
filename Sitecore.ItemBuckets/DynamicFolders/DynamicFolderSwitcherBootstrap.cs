﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Util;
using Sitecore.ItemBuckets.Types;

namespace Sitecore.ItemBuckets.DynamicFolders
{
    public class DynamicFolderSwitcherBootstrap : IDynamicBucketFolderPath
    {
        private static DynamicFolderSwitcher switcher;

        public DynamicFolderSwitcherBootstrap()
        {
            if (DynamicFolderSwitcherBootstrap.switcher == null)
            {
                DynamicFolderSwitcherBootstrap.switcher = new DynamicFolderSwitcher();
            }
        }

        public string GetFolderPath(Data.ID newItemId, Data.ID parentItemId, DateTime creationDateOfNewItem)
        {
            var bucket = Types.BucketItem.Create(parentItemId);

            var dynamicFolderPathResolver = switcher.GetFolderPathResolver(bucket);

            return dynamicFolderPathResolver.GetFolderPath( SitecoreItem.Create(newItemId), bucket, creationDateOfNewItem);
        }
    }
}

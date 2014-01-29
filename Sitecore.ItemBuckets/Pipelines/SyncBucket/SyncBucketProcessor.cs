using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Pipelines.BucketOperations.CreateBucket;
using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using Sitecore.ItemBuckets.Helpers;
using Sitecore.Buckets.Pipelines.BucketOperations.SyncBucket;
using Sitecore.Buckets.Extensions;

namespace Sitecore.ItemBuckets.Pipelines.SyncBucket
{
    public class SyncBucketProcessor : BucketOperationProcessor<SyncBucketArgs> 
    {
         private bool IsAlreadyOnItsPlace(Item itemToCheck, Item root)
    {
        return itemToCheck.Paths.Path.Equals(this.GetDestinationFolderPath(root, itemToCheck.Statistics.Created, itemToCheck.ID), StringComparison.OrdinalIgnoreCase);
    }

    public override void Process(SyncBucketArgs args)
    {
        if ((args != null) && !args.BucketSynced)
        {
            Item item = args.Item;
            if (item != null)
            {
                using (new SecurityDisabler())
                {
                    args.BucketSynced = this.SyncBucket(item, args);
                }
            }
        }
    }

    private bool ShouldBeMovedToRoot(Item item)
    {
        return ((!this.ShouldDeleteInCreationOfBucket(item) && !item.IsItemBucketable()) && !item.Parent.IsLockedChildRelationship());
    }

    protected virtual bool SyncBucket(Item item, SyncBucketArgs args)
    {
        if (!item.IsABucket())
        {
            return false;
        }
        foreach (Item item2 in item.GetChildren(ChildListOptions.SkipSorting))
        {
            this.SyncRec(item, item2);
        }
        return true;
    }

    private void SyncRec(Item root, Item current)
    {
        foreach (Item item in current.GetChildren(ChildListOptions.SkipSorting))
        {
            this.SyncRec(root, item);
        }
        if (this.ShouldBeMovedToRoot(current))
        {
            base.MoveItem(current, root);
        }
        if (this.ShouldMoveToDateFolder(current))
        {
            if (!this.IsAlreadyOnItsPlace(current, root))
            {
                base.MoveSingleItemToDynamicFolder(root, current);
            }
        }
        else if (this.ShouldDeleteInCreationOfBucket(current) && !current.GetChildren(ChildListOptions.SkipSorting).Any<Item>())
        {
            current.Delete();
        }
    }
    }
}

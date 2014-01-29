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

namespace Sitecore.ItemBuckets.Pipelines.CreateBucket
{
    public class CreateBucketProcessor : BucketOperationProcessor<CreateBucketArgs>
    {
            // Methods
    protected virtual bool CreateBucket(Item item, Action<Item> callBack, CreateBucketArgs args)
    {
        Assert.ArgumentNotNull(item, "item");
        Assert.ArgumentNotNull(args, "args");
        using (new EditContext(item, SecurityCheck.Disable))
        {
            item.IsBucketItemCheckBox().Checked = true;
        }
        string str = Translate.Text("Processing Item");
        long num = 0L;
        foreach (Item item2 in item.GetChildren(ChildListOptions.SkipSorting))
        {
            bool flag = Context.Job != null;
            if (flag)
            {
                Context.Job.Status.Messages.Add(str + " " + item2.Paths.FullPath);
                Context.Job.Status.Processed = num;
            }
            if (this.ShouldDeleteInCreationOfBucket(item2))
            {
                Parallel.ForEach<Item>(item2.GetChildren(ChildListOptions.SkipSorting), new Action<Item>(this.MakeIntoBucket));
                if (flag)
                {
                    Context.Job.Status.Messages.Add(Translate.Text("Deleting item {0}", new object[] { item2.Paths.FullPath }));
                }
            }
            else
            {
                this.MoveItemToDateFolder(item, item2);
                if (flag)
                {
                    Context.Job.Status.Messages.Add(Translate.Text("Moving item {0}", new object[] { item2.Paths.FullPath }));
                }
            }
            num += 1L;
        }
        if (callBack != null)
        {
            callBack(item);
        }
        return true;
    }

    public override void Process(CreateBucketArgs args)
    {
        if ((args != null) && !args.BucketCreated)
        {
            Item item = args.Item;
            Action<Item> callBack = args.CallBack;
            if (item != null)
            {
                args.BucketCreated = this.CreateBucket(item, callBack, args);
            }
        }
    }
    }
}

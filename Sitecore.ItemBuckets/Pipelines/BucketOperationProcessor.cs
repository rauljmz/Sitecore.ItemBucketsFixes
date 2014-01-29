using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Buckets.Pipelines;
using Sitecore.Buckets.Util;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.ItemBuckets.Helpers;
using Sitecore.Reflection;
using Sitecore.SecurityModel;

namespace Sitecore.ItemBuckets.Pipelines
{
    public abstract class BucketOperationProcessor<TArgs> : Sitecore.Buckets.Pipelines.BucketOperations.BucketOperationProcessor<TArgs> where TArgs : BucketsPipelineArgs

    {
        protected override string GetDestinationFolderPath(Data.Items.Item topParent, DateTime childItemCreationDateTime, Data.ID itemToMove)
        {
            Type type = Type.GetType(GetDynamicFolderPathType(topParent));
            IDynamicBucketFolderPath path = ReflectionUtil.CreateObject(type) as IDynamicBucketFolderPath;
            if (path == null)
            {
                Log.Fatal("Could not instantiate DynamicBucketFolderPath of type " + type, this);
                throw new ConfigurationException("Could not instantiate DynamicBucketFolderPath of type " + type);
            }
            string str = path.GetFolderPath(itemToMove, topParent.ID, childItemCreationDateTime);
            if ((BucketConfigurationSettings.BucketFolderPath == string.Empty) && (path is DateBasedFolderPath))
            {
                str = "Repository";
            }
            return (topParent.Paths.FullPath + Sitecore.Buckets.Util.Constants.ContentPathSeperator + str);
        }

        protected virtual string GetDynamicFolderPathType(Data.Items.Item topParent)
        {
            return StringUtil.GetString(topParent[References.__DynamicFolderPath], BucketConfigurationSettings.DynamicBucketFolderPath);
        }

        protected virtual bool MoveItem(Item item, Item destination)
        {
            return ItemManager.MoveItem(item, destination, SecurityCheck.Disable);
        }

 



    }
}

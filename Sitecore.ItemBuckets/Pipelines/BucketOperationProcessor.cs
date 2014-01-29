using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemBuckets.Helpers;

namespace Sitecore.ItemBuckets.Pipelines
{
    public class BucketOperationProcessor<T> : Sitecore.Buckets.Pipelines.BucketOperations.BucketOperationProcessor<T>
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
            return (topParent.Paths.FullPath + Constants.ContentPathSeperator + str);
        }

        protected virtual string GetDynamicFolderPathType(Data.Items.Item topParent)
        {
            return StringUtil.GetString(topParent[References.__DynamicFolderPath], BucketConfigurationSettings.DynamicBucketFolderPath);
        }



    }
}

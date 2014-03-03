namespace Sitecore.ItemBuckets.Commands
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Shell.Applications.ContentManager;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using System.Linq;
    using Sitecore.ItemBuckets.Helpers;

    public class EditFrameButton : Command
    {
        public override void Execute([NotNull] CommandContext context)
        {
            var nvc = new NameValueCollection();
            nvc.Add("uri", context.Items[0].Uri.ToString());
            Context.ClientPage.Start(this, "Run", nvc);
        }

        public void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                var item = Sitecore.Data.Database.GetItem(ItemUri.Parse(args.Parameters["uri"]));
                var fields = new List<FieldDescriptor>();
                FieldEditorOptions options = new FieldEditorOptions(fields);
                options.SaveItem = true;
                options.DialogTitle = "Item Bucket settings";
                foreach (var fieldDescriptor in GetFields(item).Select(s => new FieldDescriptor(item, s)))
                {
                    options.Fields.Add(fieldDescriptor);
                }
                Context.ClientPage.ClientResponse.ShowModalDialog(options.ToUrlString().ToString(), true);

                args.WaitForPostBack();
            }


        }

        protected virtual IEnumerable<string> GetFields(Item item)
        {
            if (item[Sitecore.Buckets.Util.Constants.IsBucket] == "1")
            {
                return new string[] {   
                    References.__DynamicFolderPath.ToString(),
                    Sitecore.Buckets.Util.Constants.DefaultQuery,
                    Sitecore.Buckets.Util.Constants.DefaultFilter,
                    Sitecore.Buckets.Util.Constants.FacetsField,
                    Sitecore.Buckets.Util.Constants.EnabledView,
                    Sitecore.Buckets.Util.Constants.QuickActionField,                    
                };
            }
            return new string[]{ 
                Sitecore.Buckets.Util.Constants.BucketableField,
                Sitecore.Buckets.Util.Constants.ShouldNotOrganizeInBucket
            };
        }

    }
}
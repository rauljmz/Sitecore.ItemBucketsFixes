using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemBuckets.DynamicFolderPathResolvers
{
    public class DateBasedDynamicFolderResolver : IDynamicFolderResolver
    {
        public string Field { get; set; }
        public string Format { get; set; }

        public DateBasedDynamicFolderResolver()
        {
            Field = Sitecore.FieldIDs.Created.ToString();
            Format = "yyyy/MM/dd/HH/mm"; 
        }



        public string GetFolderPath(Types.IItem newItemId, Types.IBucket parentItemId, DateTime creationDateOfNewItem)
        {
            return newItemId.GetDateField(Field).ToString(Format, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}

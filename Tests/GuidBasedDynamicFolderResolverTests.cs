using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using Sitecore.ItemBuckets.DynamicFolderPathResolvers;
using Sitecore.ItemBuckets.Types;
using Xunit;

namespace Tests
{
    public class BasedDynamicFolderResolverTests
    {
        [Fact]
        public void Guid_Should_Return_Correct_Path()
        {
            var guidBasedDynamicFolderResolver = new GuidBasedDynamicFolderResolver()
            {
                Depth = "5"
            };
            var testItem = new TestItem()
            {
                Id = new Guid("1d8e49ca-2a02-4b78-8339-6e6bf8bf5687")
            };

            var bucket = new BucketItemTest();
            var result = guidBasedDynamicFolderResolver.GetFolderPath(testItem, bucket, DateTime.Now);

            result.ShouldEqual("1/d/8/e/4");
        }

        [Fact]
        public void DateBased_Should_Return_Correct_Path()
        {
            var dateBasedBasedDynamicFolderResolver = new DateBasedDynamicFolderResolver()
            {
                Format = "dd/MM/yyyy"
            };
            var testItem = new TestItem(new DateTime(1998,12,23,12,23,23));

            var bucket = new BucketItemTest();
            var result = dateBasedBasedDynamicFolderResolver.GetFolderPath(testItem, bucket, DateTime.Now);

            result.ShouldEqual("23/12/1998");

        }
        

        public class TestItem : IItem
        {
            private DateTime date;
            public TestItem()
            {
                date = DateTime.Now;
            }
            public TestItem(DateTime datetime)
            {
                date = datetime;
            }
            public DateTime GetDateField(string idOrName)
            {
                return date;
            }

            public Guid Id
            {
                get;
                set;
            }
        }
    }
}

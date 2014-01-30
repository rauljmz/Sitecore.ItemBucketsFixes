using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemBuckets.DynamicFolders;
using Sitecore.ItemBuckets.TypeCreator;
using Sitecore.ItemBuckets.Types;
using Xunit;
using Should;
using Sitecore.ItemBuckets.DynamicFolderPathResolvers;

namespace Tests
{
    public class DynamicFolderSwitcherTests
    {        
        [Fact]
        public void Should_Return_Same_Object()
        {
            var testDynamicFolderPath = new TestDynamicFolderPath();
            var switcher = new DynamicFolderSwitcher(new DynamicFolderCache(), new TestObjectCreator<IDynamicFolderResolver>(testDynamicFolderPath));
            var bucket = new BucketItemTest() { Id = Guid.NewGuid(), DynamicFolderPath = new TestTypeDefinition(testDynamicFolderPath) };

            var result1 = switcher.GetFolderPathResolver(bucket);
            var result2 = switcher.GetFolderPathResolver(bucket);

            result1.ShouldImplement<IDynamicFolderResolver>();         
            result1.ShouldBeSameAs(result2);
        }        
    }


    public class TestObjectCreator<T> : IObjectCreator<T>
    {
        private T _object;
        public TestObjectCreator(T o)
        {
            _object = o;
        }
        public T Create(ITypeDefinition def)
        {
            return _object;
        }
    }


    

    public class TestDynamicFolderPath : IDynamicFolderResolver
    {
        public string GetFolderPath(Guid newItemId, Guid parentItemId, DateTime creationDateOfNewItem)
        {
            throw new NotImplementedException();
        }
    }

    public class BucketItemTest : IBucket
    {
        public ITypeDefinition DynamicFolderPath
        {
            get;
            set;
        }

        public Guid Id
        {
            get ; set;
        }
    }


}

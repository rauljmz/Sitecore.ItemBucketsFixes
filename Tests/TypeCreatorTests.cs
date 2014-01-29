using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemBuckets.TypeCreator;
using Xunit;
using Should;

namespace Tests
{
    public class TypeCreatorTests
    {
        [Fact]
        public void Should_return_valid_type()
        {
            var parameters = new System.Collections.Specialized.NameValueCollection();
          
            var tdef = new TypeDefinition()
            {
               TypeName = string.Format("{0},{1}",typeof(TestObject).FullName,typeof(TestObject).Assembly.GetName().Name ),
               Parameters = parameters                
            }; 
           
            ITypeCreator<TestObject> creator = new TypeCreator<TestObject>();

            var result = creator.Create(tdef);

            result.ShouldBeType<TestObject>();          
        }

        [Fact]
        public void Should_return_populated_parameters()
        {
            var parameters = new System.Collections.Specialized.NameValueCollection();
            parameters.Add("MyProperty","5");
            var tdef = new TypeDefinition()
            {
                TypeName = string.Format("{0},{1}", typeof(TestObject).FullName, typeof(TestObject).Assembly.GetName().Name),
               Parameters = parameters                
            }; 
           
            ITypeCreator<TestObject> creator = new TypeCreator<TestObject>();

            var result = creator.Create(tdef);

            result.MyProperty.ShouldEqual("5");
           

        }
        [Fact]
        public void Should_Throw_Exception_With_Type_Mismatch()
        {
            var parameters = new System.Collections.Specialized.NameValueCollection();
            
            var tdef = new TypeDefinition()
            {
                TypeName = string.Format("{0},{1}", typeof(TestObject).FullName, typeof(TestObject).Assembly.GetName().Name),
                Parameters = parameters
            };

            ITypeCreator<string> creator = new TypeCreator<string>();

            Action action = () => creator.Create(tdef);

            action.ShouldThrow<InvalidCastException>();     
        }
    }
    s
    public class TestObject
    {
        public string MyProperty { get; set; }
    }
}

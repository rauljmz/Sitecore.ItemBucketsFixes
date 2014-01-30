using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemBuckets.TypeCreator;
using Xunit;
using Should;
using System.Collections.Specialized;

namespace Tests
{
    public class TypeCreatorTests
    {
        [Fact]
        public void Should_return_valid_type()
        {
            var parameters = new System.Collections.Specialized.NameValueCollection();
            var testObject = new TestObject();
            var tdef = new TestTypeDefinition(testObject)
            {            
               Parameters = parameters                
            }; 
           
            IObjectCreator<TestObject> creator = new ObjectCreator<TestObject>();

            var result = creator.Create(tdef);

            result.ShouldBeType(testObject.GetType());          
        }

        [Fact]
        public void Should_return_populated_parameters()
        {
            var parameters = new System.Collections.Specialized.NameValueCollection();
            parameters.Add("MyProperty","5");
            var tdef = new TestTypeDefinition(new TestObject())
            {                
               Parameters = parameters                
            }; 
           
            IObjectCreator<TestObject> creator = new ObjectCreator<TestObject>();

            var result = creator.Create(tdef);

            result.MyProperty.ShouldEqual("5");
           

        }
        [Fact]
        public void Should_Throw_Exception_With_Type_Mismatch()
        {
            var parameters = new System.Collections.Specialized.NameValueCollection();
            
            var tdef = new TestTypeDefinition(new TestObject())
            {               
                Parameters = parameters
            };

            IObjectCreator<string> creator = new ObjectCreator<string>();

            Action action = () => creator.Create(tdef);

            action.ShouldThrow<InvalidCastException>();     
        }
    }
    
    public class TestObject
    {
        public string MyProperty { get; set; }
    }

    public class TestTypeDefinition : ITypeDefinition
    {
        public TestTypeDefinition(Object o)
        {
            TypeName = string.Format("{0},{1}", o.GetType().FullName, o.GetType().Assembly.GetName().Name);
        }
        public NameValueCollection Parameters
        {
            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }
    }
}

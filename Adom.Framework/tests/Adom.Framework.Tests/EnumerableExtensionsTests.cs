using Adom.Framework.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Adom.Framework.Tests
{
    public class EnumerableExtensionsTests
    {
        private const string MSG_FORMAT_NEGATIVE_CAPACITY = "Capacity must be greater than 0";
        private const string MSG_FORMAT_ENUM_FAILED = "Enumeration has failed, items has been added during enumeration";
        private const string MSG_FORMAT_ENUM_CANNOT_HAPPEN = "Enumeration cannot be done";
        private const string MSG_FORMAT_COUNT_ITEM_GREATHER_THAN_SIZE = "Count parameter is greather than of the LimitedList size";
        private const string MSG_FORMAT_INDEX_OUTOFRANGE = "Index parameter must be greather than 0 and less than the LimitedList size";

        private const FakeObject _nullFakeObject = null;
        private static readonly FakeObject _fakeObject2 = FakeObject.ConstFakeObject;

        IList<FakeObject> listFakeObjects = new List<FakeObject>()
        {
            FakeObject.Create(1_000, "one thousand"),
            FakeObject.Create(10_000, "ten thousand"),
            FakeObject.Create(100_000, "one hundred thousand"),
            FakeObject.Create(1_000_000, "one million")
        };

        internal class FakeObject
        {
            public int Prop1 { get; set; } = 100;
            public string Prop2 { get; set; } = "EnumerationExtension";

            public static FakeObject Create(int prop1, string prop2) => new() { Prop1 = prop1, Prop2 = prop2 };

            public readonly static FakeObject ConstFakeObject = new() { Prop1 = 1_000, Prop2 = "IEnumerable.Extension" };
        }

        [Fact]
        public void ReplaceShouldThrowSpecificException()
        {
            List<FakeObject> nullList = null;
            FakeObject fakeObjectToReplace = FakeObject.Create(100_000_000, "one hundred million");
            Assert.Throws<ArgumentNullException>(() => nullList.Replace(_nullFakeObject, fakeObjectToReplace));

            Assert.Throws<ArgumentOutOfRangeException>(() => listFakeObjects.Replace(_fakeObject2, fakeObjectToReplace));
        }

        [Fact]
        public void AddOrReplaceShouldAddTheItem()
        {
            FakeObject fakeObjectToReplace = FakeObject.Create(100_000_000, "one hundred million");
            int existingCount = listFakeObjects.Count;
            listFakeObjects.AddOrReplace(_nullFakeObject, fakeObjectToReplace);

            Assert.True(listFakeObjects.Count == existingCount + 1);
            Assert.Contains(fakeObjectToReplace, listFakeObjects);
            var obj = listFakeObjects.FirstOrDefault(o => o.Prop1 == fakeObjectToReplace.Prop1 && o.Prop2 == fakeObjectToReplace.Prop2);
            Assert.Equal(obj, fakeObjectToReplace);
        }

        [Fact]
        public void ReplaceShouldReplaceTheExistingItem()
        {
            FakeObject fakeObjectToReplace = FakeObject.Create(100_000_000, "one hundred million");
            int existingCount = listFakeObjects.Count;
            FakeObject firstFakeObject = listFakeObjects.First();

            listFakeObjects.Replace(listFakeObjects.First(), fakeObjectToReplace);

            Assert.True(listFakeObjects.Count == existingCount);
            Assert.DoesNotContain(firstFakeObject, listFakeObjects);
            Assert.Contains(fakeObjectToReplace, listFakeObjects);
            Assert.True(listFakeObjects.IndexOf(fakeObjectToReplace) == 0);
        }
    }
}

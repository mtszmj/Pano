using NUnit.Framework;
using Pano.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.UnitTests.IO
{
    [TestFixture]
    public class JsonSerializerTests
    {
        private class TestObject
        {
            public TestObject(string text)
            {
                Text = text;
            }

            public string Text { get; set; }
        }

        [Test]
        public void SerializeEmptyObject()
        {
            JsonSerializer serializer = new JsonSerializer();

            string json = serializer.Serialize(new object());

            Assert.That(json, Is.EqualTo("{}"));
        }

        [TestCase("test")]
        public void SerializeTestObject(string text)
        {
            JsonSerializer serializer = new JsonSerializer();

            string json = serializer.Serialize(new TestObject(text));

            Assert.That(json, Is.EqualTo("{\r\n  \"text\": \"test\"\r\n}"));
        }

        [TestCase("{\r\n  \"text\": \"test\"\r\n}", "test")]
        public void DeserializeTestObject(string json, string propValue)
        {
            JsonSerializer serializer = new JsonSerializer();

            var obj = serializer.Deserialize<TestObject>(json);

            Assert.That(obj.Text, Is.EqualTo(propValue));
        }


    }
}

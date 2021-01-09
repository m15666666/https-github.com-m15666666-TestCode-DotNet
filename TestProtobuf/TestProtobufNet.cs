using ProtoBuf;
using System.IO;
using INT = System.Int32;

namespace TestProtobuf
{
    /// <summary>
    /// 参考：https://github.com/protobuf-net/protobuf-net
    /// https://www.cnblogs.com/autyinjing/p/6495103.html
    /// https://protobuf-net.github.io/protobuf-net/3_0
    /// https://www.shisujie.com/blog/Google-ProtoBuf-vs-ProtoBuf-Net
    /// 
    /// </summary>
    internal class TestProtobufNet
    {
        public void Test()
        {
            var b = new INT[1024];
            System.Array.Fill(b, (INT)1);
            var person = new Person
            {
                Id = 12345,
                Name = "Fred",
                Address = new Address
                {
                    Line1 = "Flat 1",
                    Line2 = "The Meadows",
                    Bytes = b,
                }
            };
            //using (var file = File.Create("person.bin"))
            byte[] bytes;
            using (var file = new MemoryStream())
            {
                Serializer.Serialize(file, person);
                bytes = file.ToArray();
            }
            using (var file = new MemoryStream(bytes))
            {
                Person newPerson = Serializer.Deserialize<Person>(file);
            }

        }
    }

    [ProtoContract]
    internal class Person
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public Address Address { get; set; }
    }

    [ProtoContract]
    internal class Address
    {
        [ProtoMember(1)]
        public string Line1 { get; set; }

        [ProtoMember(2)]
        public string Line2 { get; set; }

        [ProtoMember(3)]
        public INT[] Bytes { get; set; }
    }
}
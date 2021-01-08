using Google.Protobuf.Examples.AddressBook;
using System.IO;
using System;

namespace TestProtobuf
{
    public class TestGoogleProtobuf
    {
        public void Test()
        {
            //StringBuilder builder = new StringBuilder();
            //for( int i = 0; i < 16384; i++) builder.
            char[] chars = new char[16384];
            for (int i = 0; i < chars.Length; i++) chars[i] = 'a';
            //AddressBook.Parser.ParseFrom()
            var address = new AddressBook()
            {
            };
            address.People.Add(
                new Google.Protobuf.Examples.AddressBook.Person { Id = 1, Name = "david",  });
            address.People[0].Name = new string(chars);
            address.People[0].Phones.Add(new Google.Protobuf.Examples.AddressBook.Person.Types.PhoneNumber { Number = "135", Type = 0 });

            var size = address.CalculateSize();
            byte[] bytes;
            using (var stream = new MemoryStream())
            using (var m = new Google.Protobuf.CodedOutputStream(stream))
            {
                address.WriteTo(m);
                m.Flush();
                
                bytes = stream.ToArray();
            }

            var newAddress = AddressBook.Parser.ParseFrom(bytes);
        }
    }
}
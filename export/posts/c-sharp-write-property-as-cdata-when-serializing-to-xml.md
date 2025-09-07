# C# Write property as CDATA when serializing to XML

Author: Dan Dumitru; Created: May 7, 2020; Last Edit: February 7, 2021  
Tags: C#; Views: 680

## Problem

I have a simple `CronItem` object, I am serializing it to XML, and I want that its `Info` value to be wrapped inside a `CDATA` section when serializing, to obtain something like:

```
<?xml version="1.0" encoding="utf-16"?>
<CronItem>
  <Id>42</Id>
  <Name>Bloat</Name>
  <Info><![CDATA[gibberish]]></Info>
</CronItem>
```

This is the `CronItem` class:

```
public partial class CronItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }
}
```

## Solution

I had to make `CronItem` implement the `IXmlSerializable` interface, and use the `WriteCData` method on the `Info` property. Here is the full class: 

```csharp
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DummyConsoleApp
{
    public partial class CronItem : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Id", Id.ToString());
            writer.WriteElementString("Name", Name);

            writer.WriteStartElement("Info");

            if (Info != null)
            {
                writer.WriteCData(Info);
            }

            writer.WriteEndElement();
        }
    }
}
```

This is the full program that calls the serialization process:

```
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DummyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cronItem = new CronItem
            {
                Id = 42,
                Name = "Bloat",
                Info = "gibberish"
            };

            Console.Write(Serialize(cronItem));
            Console.Read();
        }

        public static string Serialize<T>(T value)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlSerializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }
    }
}
```

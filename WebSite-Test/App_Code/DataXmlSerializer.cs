using System;
using System.IO;
using System.Xml.Serialization;

public class DataXmlSerializer
{
    public string Serializer(object objectInstance)
    {
        var serializer = new XmlSerializer(objectInstance.GetType());

        var sb = new System.Text.StringBuilder();

        using (var writer = new StringWriter(sb))
        {
            serializer.Serialize(writer, serializer);
        }

        return sb.ToString();
    }

    //public PerfomanceInfoData Deserialize<PerfomanceInfoData>(string xmlString)
    //{
    //    return (PerfomanceInfoData)Deserialize(xmlString, typeof(PerfomanceInfoData));
    //}

    private object Deserialize(string xmlString, Type type)
    {
        var serializer = new XmlSerializer(type);
        object result;

        using (var reader = new StringReader(xmlString))
        {
            result = serializer.Deserialize(reader);
        }

        return result;
    }
}
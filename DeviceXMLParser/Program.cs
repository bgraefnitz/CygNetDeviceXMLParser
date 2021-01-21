using System.IO;
using System.Xml.Linq;

namespace DeviceXMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter file = new StreamWriter(@"E:\Downloads\CHKDeviceFiles\facs.csv"))
            {
                var directories = Directory.GetDirectories(@"E:\Downloads\CHKDeviceFiles\DeviceFiles");
                foreach (var xmlFile in Directory.GetFiles(@"E:\Downloads\CHKDeviceFiles\DeviceFiles", "*.txt", SearchOption.AllDirectories))
                {
                    try
                    {
                        var xml = XDocument.Load(xmlFile);
                        if (xml.Root.Element("Device").Attribute("category").Value == "RD")
                        {
                            var devName = xml.Root.Element("Device").Attribute("device_id").Value;


                            var facLinks = xml.Root.Element("Device").Element("FacilityLinks").Elements("FacilityLink");
                            foreach (var facLink in facLinks)
                            {
                                var ordinal = facLink.Attribute("ordinal").Value;
                                var facId = facLink.Attribute("id").Value;
                                file.WriteLine(xmlFile + ", " + devName + ", " + ordinal + ", " + facId);
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace GraphLib.DTOs
{
    [DataContract(Name = "AdjacentNodes")]
    [XmlRoot(ElementName = "adjacentNodes")]
    public class AdjacentNodesDTO
    {
        public AdjacentNodesDTO()
        {
            Ids = new List<int>();
        }

        [DataMember]
        [XmlElement(ElementName = "id")]
        public List<int> Ids { get; set; }
    }
}

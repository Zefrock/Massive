using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace GraphLib.DTOs
{
    [DataContract(Name = "Node")]
    [XmlRoot(ElementName = "node")]
    public class NodeDTO
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement(ElementName = "id")]
        public int NodeId { get; set; }

        [DataMember(Name = "label", Order = 2)]
        [XmlElement(ElementName = "label")]
        public string Label { get; set; }

        [DataMember(Name = "adjacentNodes", Order = 3)]
        [XmlElement(ElementName = "adjacentNodes")]
        public AdjacentNodesDTO AdjacentNodes { get; set; }
    }
}

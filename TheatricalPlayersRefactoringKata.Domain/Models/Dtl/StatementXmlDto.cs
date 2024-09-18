using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.Models.Dtl
{
    [XmlRoot("Statement")]
    public class StatementXmlDto
    {
        [XmlElement("Customer")]
        public string Customer { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<StatementXmlItemDto> Items { get; set; }

        [XmlElement("AmountOwed")]
        public decimal AmountOwed { get; set; }

        [XmlElement("EarnedCredits")]
        public int EarnedCredits { get; set; }
    }

    public class StatementXmlItemDto
    {
        [XmlElement("AmountOwed")]
        public decimal AmountOwed { get; set; }

        [XmlElement("EarnedCredits")]
        public int EarnedCredits { get; set; }

        [XmlElement("Seats")]
        public int Seats { get; set; }
    }
}

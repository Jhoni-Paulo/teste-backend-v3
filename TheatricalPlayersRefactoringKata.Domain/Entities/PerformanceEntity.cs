using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    [Table("performance")]
    public class PerformanceEntity
    {
        private PerformanceEntity() { }
        public PerformanceEntity(string fkPlaySlug, int audience)
        {
            FkPlaySlug = fkPlaySlug;
            Audience = audience;
        }

        [Column("fk_play_name")]
        [JsonPropertyName("PlayId")]
        public string FkPlaySlug { get; private set; }
        [Column("audience")]
        public int Audience { get; private set; }

    }
}

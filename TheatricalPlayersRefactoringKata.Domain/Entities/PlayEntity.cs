using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    [Table("play")]
    public class PlayEntity
    {
        private PlayEntity() { }

        public PlayEntity(string name, string slug, int lines, PlayType type)
        {
            Name = name;
            Slug = slug;
            Lines = lines;
            Type = type;
        }

        [Column("id_play")]
        public int IdPlay { get; private set; }
        [Column("name")]
        public string Name { get; private set; }
        public string Slug { get; private set; }
        [Column("lines")]
        public int Lines { get; private set; }
        [Column("type")]
        public PlayType Type { get; private set; }
    }

    public enum PlayType
    {
        Tragedy,
        Comedy,
        History
    }
}

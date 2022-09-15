using System.ComponentModel.DataAnnotations;

namespace MusicStoreAppTwo.Models
{
    public abstract class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}

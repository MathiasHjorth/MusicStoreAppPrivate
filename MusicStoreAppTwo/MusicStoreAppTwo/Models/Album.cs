using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreAppTwo.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string AlbumArtUrl { get; set; }

        //Navigation Properties
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public ICollection<SongAlbum> SongAlbums { get; set; }

    }
}

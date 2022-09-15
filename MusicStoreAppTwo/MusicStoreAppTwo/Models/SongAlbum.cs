namespace MusicStoreAppTwo.Models
{
    public class SongAlbum
    {
        public int SongAlbumId { get; set; }
        public int SongId { get; set; }
        public int AlbumId { get; set; }

        //Navigation Properties
        public Song Song { get; set; }
        public Album Album { get; set; }

    }
}

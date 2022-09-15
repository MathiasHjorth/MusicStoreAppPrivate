namespace MusicStoreAppTwo.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public ICollection<SongAlbum> SongAlbums { get; set; }
        public ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}

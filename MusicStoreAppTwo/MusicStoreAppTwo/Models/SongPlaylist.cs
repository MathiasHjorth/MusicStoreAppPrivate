namespace MusicStoreAppTwo.Models
{
    public class SongPlaylist
    {
        public int SongPlaylistId { get; set; }
        public int SongId { get; set; }
        public int PlaylistId { get; set; }

        //Navigation Properties
        public Song Song { get; set; }
        public Playlist Playlist { get; set; }

    }
}

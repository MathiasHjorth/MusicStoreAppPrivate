namespace MusicStoreAppTwo.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }

        public string Title { get; set; }

        //Navigation Properties
        public ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using MusicStoreAppTwo.Models;

namespace MusicStoreAppTwo.Data
{
    public class MusicStoreAppContext : DbContext
    {
        public MusicStoreAppContext(DbContextOptions<MusicStoreAppContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(s => s.Price).HasColumnType("decimal(18, 2)");
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Soloartist> SoloArtists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongPlaylist> SongPlaylists { get; set; }
        public DbSet<SongAlbum> SongAlbums { get; set; }

    }
}

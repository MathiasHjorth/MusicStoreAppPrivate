using Microsoft.EntityFrameworkCore;
using MusicStoreAppTwo.Models;
using System.ComponentModel;

namespace MusicStoreAppTwo.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MusicStoreAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MusicStoreAppContext>>()))
            {

                if (context.Genres.Any())
                {
                    return;   // DB has been seeded
                }

                //Adding Genres
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Blues",
                        Description = "Founded in America in the 1920's"
                    },
                    new Genre
                    {
                        Name = "Rock",
                        Description = "Popular since the 80's"
                    }
                );
                context.SaveChanges();

                //Adding Soloartists
                context.SoloArtists.AddRange(
                    new Soloartist
                    {
                        Name = "Elton John",
                        DateOfBirth = new DateTime(1950,1,2)
                    },
                    new Soloartist
                    {
                        Name = "Bruce Springsteen",
                        DateOfBirth = new DateTime(1965, 3, 23)
                    },
                    new Soloartist
                    {
                        Name = "Judy McDonald",
                        DateOfBirth = new DateTime(1992, 8, 12)
                    }
                    );
                context.SaveChanges();

                //Adding Groups
                context.Groups.AddRange(
                    new Group
                    {
                        Name = "U2",
                        FoundingDate = new DateTime(1988, 5, 5)
                    },
                    new Group
                    {
                        Name = "Guns & Roses",
                        FoundingDate = new DateTime(2000,11,4)
                    }
                    );
                context.SaveChanges();

                //Adding Songs
                context.Songs.AddRange(
                    new Song
                    {
                        Name = "All I want"
                    },
                    new Song
                    {
                        Name = "With or Without you"
                    },
                    new Song
                    {
                        Name = "Paradise City"
                    }
                    );
                context.SaveChanges();

                //Adding Playlist
                context.Playlists.AddRange(
                    new Playlist
                    {
                        Title = "My favorite playlist"
                    }
                    );
                context.SaveChanges();

                //Adding Albums
                context.Albums.AddRange(
                    new Album
                    {
                        Title = "Best of U2",
                        Price = 120,
                        AlbumArtUrl = "https://www.udiscovermusic.com/wp-content/uploads/2021/12/U2-album-covers-October.jpg",
                        ArtistId = context.Groups.Single(g => g.Name == "U2").ArtistId,
                        GenreId = context.Genres.Single(g => g.Name == "Rock").GenreId,
                    },
                    new Album
                    {
                        Title = "Eltons Best",
                        Price = 300,
                        AlbumArtUrl = "aewt",
                        ArtistId = context.SoloArtists.Single(g => g.Name == "Elton John").ArtistId,
                        GenreId = context.Genres.Single(g => g.Name == "Blues").GenreId,
                    }
                    );
                context.SaveChanges();

                //Adding SongPlaylists
                context.SongPlaylists.AddRange(
                    new SongPlaylist
                    {
                       SongId = context.Songs.Single(s => s.Name == "All I want").SongId,
                       PlaylistId = context.Playlists.Single(p => p.Title == "My favorite playlist").PlaylistId
                    }
                    );
                context.SaveChanges();

                //Adding SongAlbums
                context.SongAlbums.AddRange(
                    new SongAlbum
                    {
                        AlbumId = context.Albums.Single(a => a.Title == "Eltons Best").AlbumId,
                        SongId = context.Songs.Single(s => s.Name == "Paradise City").SongId
                    },
                    new SongAlbum
                    {
                        AlbumId = context.Albums.Single(a => a.Title == "Eltons Best").AlbumId,
                        SongId = context.Songs.Single(s => s.Name == "All I want").SongId
                    },
                    new SongAlbum
                    {
                        AlbumId = context.Albums.Single(a => a.Title == "Best of U2").AlbumId,
                        SongId = context.Songs.Single(s => s.Name == "With or Without you").SongId
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}

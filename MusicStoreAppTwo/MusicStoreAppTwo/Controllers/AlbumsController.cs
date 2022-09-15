using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStoreAppTwo.Data;
using MusicStoreAppTwo.Models;
using MusicStoreAppTwo.Models.ViewModels;

namespace MusicStoreAppTwo.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicStoreAppContext _context;

        public AlbumsController(MusicStoreAppContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = "Albums";

            var musicStoreAppContext = _context.Albums.Include(a => a.Artist)
                .Include(a => a.Genre);

            return View(await musicStoreAppContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.SongAlbums).ThenInclude(a => a.Song)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Discriminator");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "SongId");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,GenreId,ArtistId,SongId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Discriminator", album.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", album.GenreId);
            //ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "SongId", album.SongId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.Include(a => a.SongAlbums)
                .ThenInclude(a => a.Song)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            PopulateAssignedSongData(album);

            return View(album);
        }

        private void PopulateAssignedSongData(Album album)
        {
            var allSongs = _context.Songs;
            var albumSongs = new HashSet<int>(album.SongAlbums.Select(c => c.SongId));
            var viewModel = new List<AssignedSongData>();
            foreach (var song in allSongs)
            {
                viewModel.Add(new AssignedSongData
                {
                    SongId = song.SongId,
                    Name = song.Name,
                    Assigned = albumSongs.Contains(song.SongId)
                });
            }
            ViewData["Songs"] = viewModel;
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,GenreId,ArtistId,Title,Price,SongAlbums,AlbumArtUrl")] Album album)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Discriminator", album.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", album.GenreId);
            //ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "SongId", album.SongId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                //.Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'MusicStoreAppContext.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
          return (_context.Albums?.Any(e => e.AlbumId == id)).GetValueOrDefault();
        }
    }
}

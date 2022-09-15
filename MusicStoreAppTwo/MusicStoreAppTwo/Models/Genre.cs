﻿namespace MusicStoreAppTwo.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<Album> Albums { get; set; }

    }
}

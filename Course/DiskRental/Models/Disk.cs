using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.Models
{
    public class Disk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfCreating { get; set; }
        public int ManufacturerId { get; set; }
        public int CountryId { get; set; }
        public int ActorId { get; set; }
        public int FilmGenreId { get; set; }
        public int DiskTypeId { get; set; }
        public decimal Price { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Country Country { get; set; }
        public Actor Actor { get; set; }
        public FilmGenre FilmGenre { get; set; }
        public DiskType DiskType { get; set; }
    }
}

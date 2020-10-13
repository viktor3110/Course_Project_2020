using System;
using System.Collections.Generic;

namespace VideoRental
{
    public partial class Genre
    {
        public Genre()
        {
            Discs = new HashSet<Disc>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Disc> Discs { get; set; }
        public override string ToString()
        {
            return $"{Id}, {Name}, {Description}";
        }
    }
}

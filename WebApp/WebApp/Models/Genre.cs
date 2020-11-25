using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Genre
    {
        public Genre() {}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

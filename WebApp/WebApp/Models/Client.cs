using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Client
    {
        public Client() {}
        public int Id { get; set; }
        public string Fio { get; set; }
        public int Number { get; set; }
        public string Pasport { get; set; }
    }
}

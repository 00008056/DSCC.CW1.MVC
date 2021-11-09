using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DSCC.CW_1._00008056MVC.Models
{
    public class Album
    {
        //model class with releveant properties
        public int AlbumId { get; set; }

        public string Name { get; set; }

        public int ReleaseYear { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
        
        public int AlbumArtistId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public class MovieResponse
    {
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please select a rating")]
        public string Rating { get; set; }

        public bool? Edited { get; set; } //bool? vs bool

        public string LentTo { get; set; }

        [MaxLength(25)]
        public string Notes { get; set; }
    }
}

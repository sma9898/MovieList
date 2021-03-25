using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public static class Repository
    {
        //
        private static List<MovieResponse> allMovies = new List<MovieResponse>();
        public static IEnumerable<MovieResponse> AllMovies
        {
            get { return allMovies; }
        }
        public static void Create(MovieResponse movie)
        {
            allMovies.Add(movie);
        }

        //Delete
        public static void Delete(MovieResponse movie)
        {
            allMovies.Remove(movie);
        }
    }
}

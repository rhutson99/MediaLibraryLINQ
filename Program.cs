using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            MovieFile movieFile = new MovieFile(scrubbedFile);

     Console.ForegroundColor = ConsoleColor.Green;

            // LINQ - Where filter operator & Contains quantifier operator
            var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
            // LINQ - Count aggregation method
            Console.WriteLine($"There are {Movies.Count()} movies from 1990");

            // LINQ - Any quantifier operator & Contains quantifier operator
            var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
            Console.WriteLine($"Any movies from 1921? {validate}");

            // LINQ - Where filter operator & Contains quantifier operator & Count aggregation method
            int num = movieFile.Movies.Where(m => m.title.Contains("(1921)")).Count();
            Console.WriteLine($"There are {num} movies from 1921");

            // LINQ - Where filter operator & Contains quantifier operator
            var Movies1921 = movieFile.Movies.Where(m => m.title.Contains("(1921)"));
            foreach(Movie m in Movies1921)
            {
                Console.WriteLine($"  {m.title}");
            }
            // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
            var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
            // LINQ - Count aggregation method
            Console.WriteLine($"There are {titles.Count()} movies with \"Shark\" in the title:");
            foreach(string t in titles)
            {
                Console.WriteLine($"  {t}");
            }

            // LINQ - First element operator
            var FirstMovie = movieFile.Movies.First(m => m.title.StartsWith("Z", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"First movie that starts with letter 'Z': {FirstMovie.title}");

            Console.WriteLine("1) Add a movie.");
            Console.WriteLine("2) Display all movies.");
            Console.WriteLine("3) Search for by movie title.");
            string input = Console.ReadLine();
            if(input == "3")
            {
                Console.WriteLine("Please enter a title to search for");
                string search = Console.ReadLine();
                var titlesearch = movieFile.Movies.Where(m => m.title.Contains(search, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine($"There are {titlesearch.Count()} movies with the search query in the title");
                foreach(Movie m in titlesearch)
                {
                    Console.WriteLine($"{Convert.ToString(m.mediaId)}, {m.title}, {string.Join(", ", m.genres)}, {m.director}, {m.runningTime}");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            logger.Info("Program ended");
        }
    }
}

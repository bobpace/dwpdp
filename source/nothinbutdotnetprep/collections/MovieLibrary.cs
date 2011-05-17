using System;
using System.Collections.Generic;
using nothinbutdotnetprep.infrastructure;
using nothinbutdotnetprep.infrastructure.filtering;

namespace nothinbutdotnetprep.collections
{
  public class MovieLibrary
  {
    readonly IList<Movie> movies;

    public MovieLibrary(IList<Movie> list_of_movies)
    {
      movies = list_of_movies;
    }

    public IEnumerable<Movie> all_movies()
    {
      return movies.one_at_a_time();
    }

    public void add(Movie movie)
    {
      if (already_contains(movie)) return;

      movies.Add(movie);
    }

    bool already_contains(Movie movie)
    {
      return movies.Contains(movie);
    }

    public IEnumerable<Movie> sort_all_movies_by_movie_studio_and_year_published()
    {
      var rankings =
        new[]
        {
          ProductionStudio.MGM,
          ProductionStudio.Pixar,
          ProductionStudio.Dreamworks,
          ProductionStudio.Universal,
          ProductionStudio.Disney,
          ProductionStudio.Paramount,
        };

      Func<ProductionStudio, int> indexOf = x =>
      {
        for (var i = 0; i < rankings.Length; i++)
        {
          if (rankings[i] == x)
          {
            return i;
          }
        }
        return -1;
      };

      Comparison<ProductionStudio> studioRankings = (x, y) => { return indexOf(x).CompareTo(indexOf(y)); };

      return sortBy(
        (x, y) => studioRankings(x.production_studio, y.production_studio),
        (x, y) => x.date_published.Year.CompareTo(y.date_published.Year)
        );
    }

    IEnumerable<Movie> sortBy(params Comparison<Movie>[] comparison)
    {
      var list = new List<Movie>(movies);

      Comparison<Movie> comparer = (x, y) =>
      {
        for (var i = 0; i < comparison.Length; i++)
        {
          var result = comparison[i](x, y);
          if (result != 0)
          {
            return result;
          }
        }
        return 0;
      };

      list.Sort(comparer);

      foreach (var movie in list)
      {
        yield return movie;
      }
    }
  }
}
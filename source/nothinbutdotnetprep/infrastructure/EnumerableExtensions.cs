using System;
using System.Collections.Generic;
using nothinbutdotnetprep.infrastructure.filtering;

namespace nothinbutdotnetprep.infrastructure
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
    {
      foreach (T item in items)
      {
        yield return item;
      }
    }

    public static IEnumerable<T> all_matching<T>(this IEnumerable<T> items, IMatchAn<T> condition) 
    {
      return all_matching(items, condition.matches);
    }

    static IEnumerable<T> all_matching<T>(this IEnumerable<T> items, Matches<T> condition) 
    {
      foreach (T item in items) if (condition(item)) yield return item;
    }

    static Comparison<T> getComparison<T, PropertyType>(PropertyAccessor<T, PropertyType> accessor) where PropertyType : IComparable<PropertyType>
    {
      return (x, y) =>
      {
        var xValue = accessor(x);
        var yValue = accessor(y);
        return xValue.CompareTo(yValue);
      };
    }

    public static IEnumerable<T> sort<T, PropertyType>(this IEnumerable<T> collection, PropertyAccessor<T, PropertyType> accessor) where PropertyType : IComparable<PropertyType>
    {
      var list = new List<T>(collection);
      var comparison = getComparison(accessor);
      list.Sort(comparison);
      return list.all_matching(x => true);
    }

    public static IEnumerable<T> sortDescending<T, PropertyType>(this IEnumerable<T> collection, PropertyAccessor<T, PropertyType> accessor) where PropertyType : IComparable<PropertyType>
    {
      var list = new List<T>(collection);

      Comparison<T> negatedComparison = (x, y) =>
      {
        var wrappedComparison = getComparison(accessor);
        return wrappedComparison(x, y)*-1;
      };

      list.Sort(negatedComparison);
      return list.all_matching(x => true);
    }
  }
}
using System;
using System.Collections.Generic;

namespace nothinbutdotnetprep.infrastructure
{
  public interface ICriteriaFactory<ItemToFilter, PropertyType>
  {
    IMatchAn<ItemToFilter> equal_to(PropertyType value);
    IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] value);
    IMatchAn<ItemToFilter> not_equal_to(PropertyType value);
  }

  public class CriteriaFactory<ItemToFilter, PropertyType> : ICriteriaFactory<ItemToFilter, PropertyType>
  {
    PropertyAccessor<ItemToFilter, PropertyType> accessor;

    public CriteriaFactory(PropertyAccessor<ItemToFilter, PropertyType> accessor)
    {
      this.accessor = accessor;
    }

    public IMatchAn<ItemToFilter> equal_to(PropertyType value)
    {
      return equal_to_any(value);
    }

    public IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] values)
    {
      return new AnonymousCriteria<ItemToFilter>(x => new List<PropertyType>(values).Contains(accessor(x)));
    }

    public IMatchAn<ItemToFilter> not_equal_to(PropertyType value)
    {
      return new NegatingCriteria<ItemToFilter>(equal_to(value));
    }
  }

  public class NeverMatches<T> : IMatchAn<T>
  {
    public bool matches(T item)
    {
      return false;
    }
  }
}
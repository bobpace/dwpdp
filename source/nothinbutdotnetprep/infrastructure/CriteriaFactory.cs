using System.Collections.Generic;

namespace nothinbutdotnetprep.infrastructure
{
  public class CriteriaFactory<Item, PropertyType> : ICreateSpecifications<Item, PropertyType>
  {
    PropertyAccessor<Item, PropertyType> accessor;

    public CriteriaFactory(PropertyAccessor<Item, PropertyType> accessor)
    {
      this.accessor = accessor;
    }

    public IMatchAn<Item> equal_to(PropertyType value)
    {
      return equal_to_any(value);
    }

    public IMatchAn<Item> equal_to_any(params PropertyType[] values)
    {
      return AnonymousCriteria<Item>.Create(x => new List<PropertyType>(values).Contains(accessor(x)));
    }

    public IMatchAn<Item> not_equal_to(PropertyType value)
    {
      return new NegatingCriteria<Item>(equal_to(value));
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
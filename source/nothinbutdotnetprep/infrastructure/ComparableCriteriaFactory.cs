using System;

namespace nothinbutdotnetprep.infrastructure
{
  public class ComparableCriteriaFactory<ItemToFilter, PropertyType> : ICreateSpecifications<ItemToFilter, PropertyType>
    where PropertyType : IComparable<PropertyType>

  {
    ICreateSpecifications<ItemToFilter, PropertyType> original;

    public ComparableCriteriaFactory(ICreateSpecifications<ItemToFilter, PropertyType> original)
    {
      this.original = original;
    }

    public IMatchAn<ItemToFilter> greater_than(PropertyType value)
    {
      return create_using(x => Accessor(x).CompareTo(value) > 0);
    }

    public IMatchAn<ItemToFilter> equal_to(PropertyType value)
    {
      return original.equal_to(value);
    }

    public IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] values)
    {
      return original.equal_to_any(values);
    }

    public IMatchAn<ItemToFilter> not_equal_to(PropertyType value)
    {
      return original.not_equal_to(value);
    }

    public IMatchAn<ItemToFilter> between(PropertyType start, PropertyType end)
    {
      return
        create_using(
          x => Accessor(x).CompareTo(start) >= 0 && Accessor(x).CompareTo(end) <= 0);
    }

    public IMatchAn<ItemToFilter> create_using(Matches<ItemToFilter> condition)
    {
      return original.create_using(condition);
    }

    public PropertyAccessor<ItemToFilter, PropertyType> Accessor
    {
      get { return original.Accessor; }
    }
  }
}
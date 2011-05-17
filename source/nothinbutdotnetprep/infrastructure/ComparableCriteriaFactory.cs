using System;

namespace nothinbutdotnetprep.infrastructure
{
  public class ComparableCriteriaFactory<ItemToFilter, PropertyType> : ICreateSpecifications<ItemToFilter, PropertyType>
    where PropertyType : IComparable<PropertyType>

  {
    PropertyAccessor<ItemToFilter, PropertyType> accessor;
    ICreateSpecifications<ItemToFilter, PropertyType> original;

    public ComparableCriteriaFactory(PropertyAccessor<ItemToFilter, PropertyType> accessor,
                                     ICreateSpecifications<ItemToFilter, PropertyType> original)
    {
      this.accessor = accessor;
      this.original = original;
    }

    public IMatchAn<ItemToFilter> greater_than(PropertyType value)
    {
      return AnonymousCriteria<ItemToFilter>.Create(x => accessor(x).CompareTo(value) > 0);
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
        AnonymousCriteria<ItemToFilter>.Create(
          x => accessor(x).CompareTo(start) >= 0 && accessor(x).CompareTo(end) <= 0);
    }
  }
}
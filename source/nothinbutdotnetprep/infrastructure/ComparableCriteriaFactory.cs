using System;

namespace nothinbutdotnetprep.infrastructure
{
  public class ComparableCriteriaFactory<ItemToFilter, PropertyType> : ICriteriaFactory<ItemToFilter, PropertyType> where PropertyType : IComparable<PropertyType>
  {
    PropertyAccessor<ItemToFilter, PropertyType> accessor;
    private readonly ICriteriaFactory<ItemToFilter, PropertyType> deferToCriteriaFactory;

    public ComparableCriteriaFactory(PropertyAccessor<ItemToFilter, PropertyType> accessor, ICriteriaFactory<ItemToFilter, PropertyType> deferToCriteriaFactory)
    {
      this.accessor = accessor;
      this.deferToCriteriaFactory = deferToCriteriaFactory;
    }

    public IMatchAn<ItemToFilter> greater_than(PropertyType value)
    {
      return new AnonymousCriteria<ItemToFilter>(x => accessor(x).CompareTo(value) > 0);
    }

    public IMatchAn<ItemToFilter> equal_to(PropertyType value)
    {
      return deferToCriteriaFactory.equal_to(value);
    }

    public IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] value)
    {
      return deferToCriteriaFactory.equal_to_any(value);
    }

    public IMatchAn<ItemToFilter> not_equal_to(PropertyType value)
    {
      return deferToCriteriaFactory.not_equal_to(value);
    }
  }
}
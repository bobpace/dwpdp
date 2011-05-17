using System;
using System.Collections.Generic;

namespace nothinbutdotnetprep.infrastructure
{
  public class CriteriaFactory<ItemToFilter, PropertyType> : ICreateSpecifications<ItemToFilter, PropertyType>
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

    public IMatchAn<ItemToFilter> create_using(Matches<ItemToFilter> condition)
    {
      return new AnonymousCriteria<ItemToFilter>(condition);
    }

    public PropertyAccessor<ItemToFilter, PropertyType> Accessor
    {
      get { return accessor; }
    }

    public IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] values)
    {
        return create_using(x => new List<PropertyType>(values).Contains(accessor(x)));
    }

    public IMatchAn<ItemToFilter> not_equal_to(PropertyType value)
    {
      return new NegatingCriteria<ItemToFilter>(equal_to(value));
    }
  }
}
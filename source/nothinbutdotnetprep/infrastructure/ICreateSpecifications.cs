﻿namespace nothinbutdotnetprep.infrastructure
{
  public interface ICreateSpecifications<ItemToFilter, PropertyType>
  {
    IMatchAn<ItemToFilter> equal_to(PropertyType value);
    IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] values);
    IMatchAn<ItemToFilter> create_using(IMatchAn<PropertyType> criteria);
    NegatedCriteriaFactory<ItemToFilter, PropertyType> not { get; }
  }
}
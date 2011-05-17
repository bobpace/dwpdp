using System;
using System.Collections.Generic;
using nothinbutdotnetprep.infrastructure.filtering;

namespace nothinbutdotnetprep.infrastructure.sorting
{
  public class ComparerBuilder<ItemThatWillBeSorted> : IComparer<ItemThatWillBeSorted>
  {
    IComparer<ItemThatWillBeSorted> initial;

    public ComparerBuilder(IComparer<ItemThatWillBeSorted> initial)
    {
      this.initial = initial;
    }

    public int Compare(ItemThatWillBeSorted x, ItemThatWillBeSorted y)
    {
      return initial.Compare(x, y);
    }

    public ComparerBuilder<ItemThatWillBeSorted> then_by_descending<PropertyType>(PropertyAccessor<ItemThatWillBeSorted, PropertyType> accessor)
      where PropertyType : IComparable<PropertyType>
    {
      initial = chain_with(new ReverseComparer<PropertyType>(new ComparableComparer<PropertyType>()), accessor);
      return this;
    }

    public ComparerBuilder<ItemThatWillBeSorted> then_by<PropertyType>(PropertyAccessor<ItemThatWillBeSorted, PropertyType> accessor)
      where PropertyType : IComparable<PropertyType>
    {
      initial = chain_with(new ComparableComparer<PropertyType>(), accessor);
      return this;
    }

    public ComparerBuilder<ItemThatWillBeSorted> then_by<PropertyType>(PropertyAccessor<ItemThatWillBeSorted, PropertyType> accessor,
      params PropertyType[] fixed_order)
    {
      initial = chain_with(new FixedOrderComparer<PropertyType>(fixed_order), accessor);
      return this;
    }

    IComparer<ItemThatWillBeSorted> chain_with<PropertyType>(IComparer<PropertyType> comparer,
      PropertyAccessor<ItemThatWillBeSorted,PropertyType> accessor)
    {
      return new ChainedComparer<ItemThatWillBeSorted>(initial, new
        PropertyComparer<ItemThatWillBeSorted,PropertyType>(accessor, comparer));
    }
  }
}
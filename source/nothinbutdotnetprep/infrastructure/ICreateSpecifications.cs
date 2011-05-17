namespace nothinbutdotnetprep.infrastructure
{
  public interface ICreateSpecifications<ItemToFilter, PropertyType>
  {
    IMatchAn<ItemToFilter> equal_to(PropertyType value);
    IMatchAn<ItemToFilter> equal_to_any(params PropertyType[] values);
    IMatchAn<ItemToFilter> not_equal_to(PropertyType value);
    IMatchAn<ItemToFilter> create_using(Matches<ItemToFilter> condition);
    PropertyAccessor<ItemToFilter, PropertyType> Accessor { get; }
  }
}
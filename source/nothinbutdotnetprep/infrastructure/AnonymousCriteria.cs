namespace nothinbutdotnetprep.infrastructure
{
  public class AnonymousCriteria<ItemToMatch> : IMatchAn<ItemToMatch>
  {
    Matches<ItemToMatch> condition;

    private AnonymousCriteria(Matches<ItemToMatch> condition)
    {
      this.condition = condition;
    }

    public bool matches(ItemToMatch item)
    {
      return condition(item);
    }

    public static AnonymousCriteria<ItemToMatch> Create(Matches<ItemToMatch> condition)
    {
      return new AnonymousCriteria<ItemToMatch>(condition);
    }
  }
}
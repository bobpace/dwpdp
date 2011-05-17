using System.Collections.Generic;

namespace nothinbutdotnetprep.infrastructure.sorting
{
  public class ChainedComparer<ItemToSort> : IComparer<ItemToSort>
  {
    IComparer<ItemToSort> first;
    IComparer<ItemToSort> second;

    public ChainedComparer(IComparer<ItemToSort> first, IComparer<ItemToSort> second)
    {
      this.first = first;
      this.second = second;
    }

    public int Compare(ItemToSort x, ItemToSort y)
    {
      var result = first.Compare(x, y);
      if (result == 0) return second.Compare(x, y);
      return result;
    }
  }
}
using System.CodeDom;

namespace GildedRose.Main
{
    public class AgedBrieUpdateStrategy : UpdateStrategy
    {
        public override void UpdateQuality(Item item)
        {
            item.Quality++;

            if (item.SellIn < 0)
            {
                item.Quality++;
            }

            item.Quality = GuardValueWithinRange(item);
        }

    }
}
namespace GildedRose.Main
{
    public class StandardItemUpdateStrategy : UpdateStrategy
    {
        public override void UpdateQuality(Item item)
        {
            item.Quality--;

            if (item.SellIn < 0)
            {
                item.Quality--;
            }

            item.Quality = GuardValueWithinRange(item);
     
        }

    }
}

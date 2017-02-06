namespace GildedRose.Main
{
    public class BackstagePassUpdateStrategy : UpdateStrategy
    {
        public override void UpdateQuality(Item item)
        {
                item.Quality++;

                if (item.SellIn < 10)
                {
                        item.Quality++;
                }

                if (item.SellIn < 5)
                {
                        item.Quality++;
                }
            
            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }

            item.Quality = GuardValueWithinRange(item);
        }

    }
}

namespace GildedRose.Main
{
    public class UpdateStrategyFactory : IUpdateStrategyFactory
    {
        public UpdateStrategy Create(Item item)
        {
            if (item.Name.Equals("Aged Brie"))
            {
                return new AgedBrieUpdateStrategy();
            }
            else if (item.Name.StartsWith("Backstage pass"))
            {
                return new BackstagePassUpdateStrategy();
            }
            else if (item.Name.StartsWith("Conjured"))
            {
                return new ConjuredUpdateStrategy();
            }
            else if (item.Name.Equals("Sulfuras, Hand of Ragnaros") || item.Name.StartsWith("Legendary"))
            {
                return new LegendaryItemUpdateStrategy();
            }
            else
            {
                return new StandardItemUpdateStrategy();
            }
        }
    }
}
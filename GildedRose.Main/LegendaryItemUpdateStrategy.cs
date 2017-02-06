namespace GildedRose.Main
{
    public class LegendaryItemUpdateStrategy : UpdateStrategy
    {
        private new const int MaxQuality = 80;

        public override void UpdateQuality(Item item)
        {
            item.Quality = MaxQuality;
        }

        public override void UpdateSellIn(Item item)
        {
            //noop  
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Main
{
    public class ConjuredUpdateStrategy : UpdateStrategy
    {
        public override void UpdateQuality(Item item)
        {
            item.Quality -= 2;

            if (item.SellIn < 0)
            {
                item.Quality -= 2;
            }

            item.Quality = GuardValueWithinRange(item);
        }
    }
}

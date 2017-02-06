using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Main
{
    public abstract class UpdateStrategy
    {
        public const int MaxQuality = 50;
        public const int MinQuality = 0;

        public void Update(Item item)
        {
            UpdateSellIn(item);
            UpdateQuality(item);
        }

        public abstract void UpdateQuality(Item item);

        public virtual void UpdateSellIn(Item item)
        {
            item.SellIn--;
        }

        public virtual int GuardValueWithinRange(Item item)
        {
            item.Quality = Math.Min(MaxQuality,item.Quality);
            item.Quality = Math.Max(MinQuality, item.Quality);

            return item.Quality;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GildedRose.Main;
using Xunit;

namespace GildedRose.Tests
{
    public class GildedRoseTests
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IList<Item> _myItemList;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Main.GildedRose _gildedRose;

        public GildedRoseTests()
        {
            _myItemList = new List<Item>();
            _gildedRose = new Main.GildedRose(_myItemList);
        }

        [Fact]
        public void LegendaryItems_DoNotChangeQualityAndIsAlways80()
        {
            _myItemList.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 1});
            _myItemList.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 50});
            _myItemList.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 100});
            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(80);
            _myItemList[1].Quality.Should().Be(80);
            _myItemList[2].Quality.Should().Be(80);
        }

        [Fact]
        public void LegendaryItems_DoNotChangeSellIn()
        {
            _myItemList.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -10, Quality = 1});
            _myItemList.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 111, Quality = 1});
            _myItemList.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 1});

            _gildedRose.UpdateQuality();

            _myItemList[0].SellIn.Should().Be(-10);
            _myItemList[1].SellIn.Should().Be(111);
            _myItemList[2].SellIn.Should().Be(1);
        }

        [Fact]
        public void AllNonLegendaryItemsDecreaseSellInByOne()
        {
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = 1, Quality = 0});
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 48
            });
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = -1,
                Quality = 50
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].SellIn.Should().Be(0);
            _myItemList[1].SellIn.Should().Be(9);
            _myItemList[2].SellIn.Should().Be(-2);
        }

        [Fact]
        public void StandardItems_NotExpiredWithPositiveQuality_ReduceQualityBy1()
        {
            _myItemList.Add(new Item {Name = "Basic Item", SellIn = 10, Quality = 10});

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(9);
        }

        [Fact]
        public void StandardItems_OnceExpired_ReduceQualityBy2()
        {
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = -1, Quality = 20});
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = -1, Quality = 2});
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = 0, Quality = 2});

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(18);
            _myItemList[1].Quality.Should().Be(0);
            _myItemList[2].Quality.Should().Be(0);
        }

        [Fact]
        public void StandardItems_withQualityLessThanOne_QualityIsAlwaysMinValue()
        {
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = -1, Quality = 0});
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = 11, Quality = 0});
            _myItemList.Add(new Item {Name = "StandardItem", SellIn = 10, Quality = -1});

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(0);
            _myItemList[1].Quality.Should().Be(0);
            _myItemList[2].Quality.Should().Be(0);
        }

        [Fact]
        public void AgedBrie_AtOrAboveMaxQuality_QualityIsAlwaysMaxQuality()
        {
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = 0,
                Quality = 50
            });
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = 1,
                Quality = 52
            });
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = -10,
                Quality = 54
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(50);
            _myItemList[1].Quality.Should().Be(50);
            _myItemList[2].Quality.Should().Be(50);

        }

        [Fact]
        public void AgedBrie_NotExpiredNotMaxQuality_QualityIncreasesBy1()
        {
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = 10,
                Quality = 40
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(41);
        }

        [Fact]
        public void AgedBrie_OnceExpiredNotMaxQuality_QualityIncreasesBy2()
        {
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = 0,
                Quality = 40
            });
            _myItemList.Add(new Item
            {
                Name = "Aged Brie",
                SellIn = -10,
                Quality = 20
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(42);
            _myItemList[1].Quality.Should().Be(22);
        }

        [Fact]
        public void BackstagePass_AtOrAboveMaxQuality_QualityIsALwaysMaxQuality()
        {
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 50
            });
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 54
            });
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 60
            });

            _gildedRose.UpdateQuality();
            _myItemList[0].Quality.Should().Be(50);
            _myItemList[1].Quality.Should().Be(50);
            _myItemList[2].Quality.Should().Be(50);
        }

        [Fact]
        public void BackstagePass_QualityDoesNotIncreasePastMaxQuality()
        {
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            });

            _gildedRose.UpdateQuality();
            _myItemList[0].Quality.Should().Be(50);
        }

        [Fact]
        public void BackstagePass_SellInGreaterThan10_QualityIncreasesBy1()
        {
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 11,
                Quality = 44
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(45);
        }

        [Fact]
        public void BackstagePass_SellInGreaterThan5LessThan10_QualityIncreasesBy2()
        {
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 8,
                Quality = 44
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(46);
        }

        [Fact]
        public void BackstagePass_SellInLessThan6GreaterThan0_QualityIncreasesBy3()
        {
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 3,
                Quality = 44
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(47);
        }

        [Fact]
        public void BackstagePass_OnceExpired_QualityEquals0()
        {
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 0,
                Quality = 44
            });
            _myItemList.Add(new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = -10,
                Quality = -34
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(0);
            _myItemList[1].Quality.Should().Be(0);
        }

        [Fact]
        public void ConjuredItems__NotExpired_QualityDecreasesBy2()
        {
            _myItemList.Add(new Item
            {
                Name = "Conjured bread",
                SellIn = 10,
                Quality = 20
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured water",
                SellIn = 20,
                Quality = 12
            });

            _gildedRose.UpdateQuality();

            _myItemList[0].Quality.Should().Be(18);
            _myItemList[1].Quality.Should().Be(10);
        }

        [Fact]
        public void ConjuredItems_AtOrAboveMaxQuality_QualityIsALwaysMaxQuality()
        {
            _myItemList.Add(new Item
            {
                Name = "Conjured bread",
                SellIn = 10,
                Quality = 52
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured water",
                SellIn = 10,
                Quality = 54
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured wine",
                SellIn = 10,
                Quality = 60
            });

            _gildedRose.UpdateQuality();
            _myItemList[0].Quality.Should().Be(50);
            _myItemList[1].Quality.Should().Be(50);
            _myItemList[2].Quality.Should().Be(50);
        }

        [Fact]
        public void ConjuredItems_OnceExpired_QualityDecreasesBy4()
        {
            _myItemList.Add(new Item
            {
                Name = "Conjured bread",
                SellIn = 0,
                Quality = 52
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured water",
                SellIn = -1,
                Quality = 44
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured wine",
                SellIn = -10,
                Quality = 10
            });

            _gildedRose.UpdateQuality();
            _myItemList[0].Quality.Should().Be(48);
            _myItemList[1].Quality.Should().Be(40);
            _myItemList[2].Quality.Should().Be(6);
        }

        [Fact]
        public void ConjuredItems_WithQualityLessThanOne_QualityIsAlways0()
        {
            _myItemList.Add(new Item
            {
                Name = "Conjured bread",
                SellIn = 0,
                Quality =0
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured water",
                SellIn = 10,
                Quality = 0
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured wine",
                SellIn = 10,
                Quality = -10
            });
            _myItemList.Add(new Item
            {
                Name = "Conjured wine",
                SellIn = -10,
                Quality = -10
            });

            _gildedRose.UpdateQuality();
            _myItemList[0].Quality.Should().Be(0);
            _myItemList[1].Quality.Should().Be(0);
            _myItemList[2].Quality.Should().Be(0);
            _myItemList[3].Quality.Should().Be(0);
        }
    }
}
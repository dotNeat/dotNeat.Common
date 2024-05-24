using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNeat.Common.DataAccess.Criteria;

namespace UnitTest.dotNeat.Common.DataAccess.Criteria
{
    [TestClass()]
    public class CriteriaFixture
    {
        [TestMethod()]
        public void BasicsTest()
        {
            List<Mobile> mobiles = new List<Mobile>
            {
                new Mobile(BrandName.Samsung, PhoneType.Smart, 700),
                new Mobile(BrandName.Apple, PhoneType.Smart, 650),
                new Mobile(BrandName.Htc, PhoneType.Basic, 15),
                new Mobile(BrandName.Samsung, PhoneType.Basic)
            };

            //LINQ expressions:
            ICriteria<Mobile> samsungExpSpec =
               new ExpressionCriteria<Mobile>(o => o.Brand == BrandName.Samsung);
            Assert.IsTrue(samsungExpSpec.IsSatisfiedBy(mobiles[0]));
            Assert.IsFalse(samsungExpSpec.IsSatisfiedBy(mobiles[1]));
            Assert.IsFalse(samsungExpSpec.IsSatisfiedBy(mobiles[2]));
            Assert.IsTrue(samsungExpSpec.IsSatisfiedBy(mobiles[3]));

            ICriteria<Mobile> htcExpSpec =
               new ExpressionCriteria<Mobile>(o => o.Brand == BrandName.Htc);
            Assert.IsFalse(htcExpSpec.IsSatisfiedBy(mobiles[0]));
            Assert.IsFalse(htcExpSpec.IsSatisfiedBy(mobiles[1]));
            Assert.IsTrue(htcExpSpec.IsSatisfiedBy(mobiles[2]));
            Assert.IsFalse(htcExpSpec.IsSatisfiedBy(mobiles[3]));

            ICriteria<Mobile> samsungHtcExpSpec =
                samsungExpSpec.Or(htcExpSpec);
            Assert.IsTrue(samsungHtcExpSpec.IsSatisfiedBy(mobiles[0]));
            Assert.IsFalse(samsungHtcExpSpec.IsSatisfiedBy(mobiles[1]));
            Assert.IsTrue(samsungHtcExpSpec.IsSatisfiedBy(mobiles[2]));
            Assert.IsTrue(samsungHtcExpSpec.IsSatisfiedBy(mobiles[3]));

            ICriteria<Mobile> notSamsungExpSpec =
              new ExpressionCriteria<Mobile>(o => o.Brand != BrandName.Samsung);
            Assert.IsFalse(notSamsungExpSpec.IsSatisfiedBy(mobiles[0]));
            Assert.IsTrue(notSamsungExpSpec.IsSatisfiedBy(mobiles[1]));
            Assert.IsTrue(notSamsungExpSpec.IsSatisfiedBy(mobiles[2]));
            Assert.IsFalse(notSamsungExpSpec.IsSatisfiedBy(mobiles[3]));

            Assert.AreEqual(2, mobiles.FindAll(o => samsungExpSpec.IsSatisfiedBy(o)).Count(), "total Samsung mobiles");
            Assert.AreEqual(1, mobiles.FindAll(o => htcExpSpec.IsSatisfiedBy(o)).Count(), "total HTC mobiles");
            Assert.AreEqual(3, mobiles.FindAll(o => samsungHtcExpSpec.IsSatisfiedBy(o)).Count(), "total Samsung or HTC mobiles");
            Assert.AreEqual(2, mobiles.FindAll(o => notSamsungExpSpec.IsSatisfiedBy(o)).Count(), "total non-Samsung mobiles");

            //non-LINQ expressions:
            ICriteria<Mobile> premiumMobilesSpec = new PremiumCriteria(600);
            Assert.AreEqual(2, mobiles.FindAll(o => premiumMobilesSpec.IsSatisfiedBy(o)).Count(), "total premium mobiles");
            //mixing it up:
            ICriteria<Mobile> linqAndNonLinqExpSpec = notSamsungExpSpec.And(premiumMobilesSpec);
            Assert.AreEqual(1, mobiles.FindAll(o => linqAndNonLinqExpSpec.IsSatisfiedBy(o)).Count(), "total non-Samsung premium mobiles");
        }

        #region mocks

        private enum BrandName
        {
            Samsung,
            Apple,
            Htc,
        }

        private enum PhoneType
        {
            Basic,
            Smart,
        }

        private class Mobile
        {
            public BrandName Brand { get; set; }
            public PhoneType Type { get; set; }
            public int Cost { get; set; }

            public Mobile(BrandName brand, PhoneType type, int cost = 0)
            {
                this.Brand = brand;
                this.Type = type;
                this.Cost = cost;
            }

            public override string ToString()
            {
                return $"{nameof(Mobile)}: brand = {this.Brand}, type = {this.Type}, cost = ${this.Cost}";
            }
        }

        private class PremiumCriteria
            : CompositeCriteria<Mobile>
        {
            private int cost;
            public PremiumCriteria(int cost)
            {
                this.cost = cost;
            }

            public override bool IsSatisfiedBy(Mobile o)
            {
                return (o.Cost >= this.cost);
            }
        }

        #endregion mocks
    }
}
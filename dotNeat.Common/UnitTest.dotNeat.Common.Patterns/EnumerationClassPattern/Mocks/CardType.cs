namespace UnitTest.dotNeat.Common.Patterns.EnumerationClassPattern.Mocks
{
    using global::dotNeat.Common.Patterns.EnumerationClassPattern;

    public class CardType
		: EnumerationBase<CardType>
	{
        public static readonly CardType Amex;
        public static readonly CardType Visa;
        public static readonly CardType MasterCard;

        static CardType()
        {
            int enumValue = 0;
            CardType.Amex = new(++enumValue, nameof(Amex));
            CardType.Visa = new(++enumValue, nameof(Visa));
            CardType.MasterCard = new(++enumValue, nameof(MasterCard));
        }

        private CardType(int id, string name)
            : base(id, name)
        {
        }
    }
}


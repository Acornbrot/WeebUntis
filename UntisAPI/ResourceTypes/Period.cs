namespace UntisAPI.ResourceTypes
{
    public struct Period
    {
        public PeriodType Type;
        public PeriodStatus Status;
        public DateTime Start;
        public DateTime End;

        public uint LayoutStartPosition;
        public uint LayoutWidth;
        public uint LayoutGroup;
        public Color color;

        public string Notes;
        public List<Icon> Icons;

        public List<PeriodLine<GenericEntity>> Line1;
        public List<PeriodLine<GenericEntity>> Line2;
        public List<PeriodLine<GenericEntity>> Line3;
        public List<PeriodLine<GenericEntity>> Line4;
        public List<PeriodLine<GenericEntity>> Line5;
        public List<PeriodLine<GenericEntity>> Line6;
        public List<PeriodLine<GenericEntity>> Line7;
    }
}

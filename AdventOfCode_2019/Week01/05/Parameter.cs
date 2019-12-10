namespace AdventOfCode_2019.Week01
{
    public struct Parameter
    {
        public int Address;
        public int Value;
        public ParameterMode Mode;
        public int Order;

        public override string ToString()
        {
            var prefix = ParameterMode.Immediate == Mode ? "#" : "$";
            return prefix + Value;
        }
    }
}

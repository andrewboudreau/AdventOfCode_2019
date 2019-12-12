namespace AdventOfCode_2019.Week01
{
    public struct Parameter
    {
        public int Address;
        public int Value;
        public ParameterMode Mode;
        public int ResolvedValue;

        public override string ToString()
        {
            if(Mode == ParameterMode.Immediate)
            {
                return "#" + Value;
            }
            
            return $"[${Value}]=>{ResolvedValue}";
        }
    }
}

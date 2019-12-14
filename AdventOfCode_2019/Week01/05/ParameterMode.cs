using System.ComponentModel;

namespace AdventOfCode_2019.Week01
{
    public enum ParameterMode
    {
        [Description("Address")]
        Position = 0,

        [Description("Immediate")]
        Immediate = 1,

        [Description("Relative")]
        Relative = 2
    }
}
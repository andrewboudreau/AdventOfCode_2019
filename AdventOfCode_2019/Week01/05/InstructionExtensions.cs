namespace AdventOfCode_2019.Week01
{
    public static class InstructionExtensions
    {
        public static int ReadOperand1(this Instruction instruction, int[] memory)
        {
            int index = 0;
            return memory.Read(instruction.Parameters[index].Address, instruction.Parameters[index].Mode);
        }

        public static int ReadOperand2(this Instruction instruction, int[] memory)
        {
            int index = 1;
            return memory.Read(instruction.Parameters[index].Address, instruction.Parameters[index].Mode);
        }

        public static void WriteOperand1(this Instruction instruction, int value, int[] memory)
        {
            memory.Write(value, instruction.Parameters[0].Address, instruction.Parameters[0].Mode);
        }

        public static void WriteOperand3(this Instruction instruction, int value, int[] memory)
        {
            memory.Write(value, instruction.Parameters[2].Address, instruction.Parameters[2].Mode);
        }
    }
}


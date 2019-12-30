using System.Numerics;

namespace AdventOfCode_2019.Cpu
{
    public static class InstructionExtensions
    {
        public static BigInteger ReadOperand(this Instruction instruction, IntCodeCpuMemory memory, int index)
        {
            return memory.Read(instruction.Parameters[index].Address, instruction.Parameters[index].Mode);
        }

        public static BigInteger ReadOperand1(this Instruction instruction, IntCodeCpuMemory memory)
        {
            return ReadOperand(instruction, memory, 0);
        }

        public static BigInteger ReadOperand2(this Instruction instruction, IntCodeCpuMemory memory)
        {
            return ReadOperand(instruction, memory, 1);
        }

        public static void WriteOperand(this Instruction instruction, BigInteger value, IntCodeCpuMemory memory, int index)
        {
            memory.Write(value, instruction.Parameters[index].Address, instruction.Parameters[index].Mode);
        }

        public static void WriteOperand1(this Instruction instruction, BigInteger value, IntCodeCpuMemory memory)
        {
            WriteOperand(instruction, value, memory, 0);
        }

        public static void WriteOperand3(this Instruction instruction, BigInteger value, IntCodeCpuMemory memory)
        {
            WriteOperand(instruction, value, memory, 2);
        }
    }
}


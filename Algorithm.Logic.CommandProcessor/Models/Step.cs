namespace Algorithm.Logic.CommandProcessor.Models
{
    public struct Step
    {
        public char Command { get; set; }
        public int Repeat { get; set; }

        public Step(char Command, int repeat = 0)
        {
            this.Command = Command;
            this.Repeat = repeat;
        }
    }
}

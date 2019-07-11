namespace Algorithm.Logic.CommandProcessor.Models
{
    public struct Step
    {
        public char Command { get; set; }
        public int Times { get; set; }
        public int MinimumTimes { get => Times == 0 ? 1 : Times; }

        public Step(char Command, int times = 0)
        {
            this.Command = Command;
            this.Times = times;
        }
    }
}

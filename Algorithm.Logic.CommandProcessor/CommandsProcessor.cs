namespace Algorithm.Logic.CommandProcessor
{
    using Algorithm.Logic.CommandProcessor.Commands;
    using Algorithm.Logic.CommandProcessor.Models;
    using System.Collections.Generic;

    public class CommandsProcessor
    {
        private Dictionary<CommandsStep, ICommandStep> _commandSteps = new Dictionary<CommandsStep, ICommandStep>
            {
                { CommandsStep.Start, new VerifyIfIsEmptyCommandStep() },
                { CommandsStep.VerifyStep, new VerifyNextStepInCommandStep() },
                { CommandsStep.AddStep, new AddNewCommandStep() },
                { CommandsStep.CancelLastOperation, new CancelLastCommandStep() },
                { CommandsStep.AddRepeats, new AddNumberToRepeatStep() },
                { CommandsStep.IncreaseIndex, new IncreaseIndexStep() },
                { CommandsStep.InvalidEntrance, new InvalidEntranceStep() }
            };

        private string _command = string.Empty;

        public CommandsProcessor(string command)
        {
            this._command = command;
        }

        public string Process()
        {
            var stepCollection = new StepCollection(this._command);
            var step = CommandsStep.Start;

            do
            {
                _commandSteps[step].Execute(stepCollection, ref step);
            }
            while (step != CommandsStep.Finish);

            var output = stepCollection.GenerateOutput();

            return $"({output.Item2}, {output.Item1})";
        }
    }
}

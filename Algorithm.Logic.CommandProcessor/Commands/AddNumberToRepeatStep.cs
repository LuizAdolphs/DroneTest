namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;

    public class AddNumberToRepeatStep : ICommandStep
    {
        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {
            if (stepCollection.StepsSize == 0 || stepCollection.Command[stepCollection.Index - 1].Equals('X'))
            {
                currentStep = CommandsStep.InvalidEntrance;
            }
            else
            {
                stepCollection.AddNumberToLatestStepRepeat(stepCollection.CharAtIndex);

                if (stepCollection.LastStep().Repeat >= 2147483647 && !stepCollection.Command[stepCollection.Index + 1].Equals('X'))
                    currentStep = CommandsStep.InvalidEntrance;
                else
                    currentStep = CommandsStep.IncreaseIndex;
            }

            return stepCollection;
        }
    }
}

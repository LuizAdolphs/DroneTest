namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;

    public class CancelLastCommandStep : ICommandStep
    {
        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {
            stepCollection.RemoveLatestStep();

            currentStep = CommandsStep.IncreaseIndex;

            return stepCollection;
        }
    }
}

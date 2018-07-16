namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;

    public class IncreaseIndexStep : ICommandStep
    {
        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {
            stepCollection.IncreaseIndex();

            currentStep = CommandsStep.VerifyStep;

            return stepCollection;
        }
    }
}

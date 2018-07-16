namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;

    public class InvalidEntranceStep : ICommandStep
    {
        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {
            stepCollection.InvalidEntrance = true;

            currentStep = CommandsStep.Finish;

            return stepCollection;
        }
    }
}

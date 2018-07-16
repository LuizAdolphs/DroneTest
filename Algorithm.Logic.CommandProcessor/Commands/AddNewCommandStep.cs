namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;

    public class AddNewCommandStep : ICommandStep
    {
        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {
            stepCollection.AddStep(new Step(stepCollection.CharAtIndex));

            currentStep = CommandsStep.IncreaseIndex;

            return stepCollection;
        }
    }
}

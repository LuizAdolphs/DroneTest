namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;

    public interface ICommandStep
    {
        StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep);
    }
}

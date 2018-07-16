namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;
    using System;

    public class VerifyIfIsEmptyCommandStep : ICommandStep
    {
        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {
            if (String.IsNullOrEmpty(stepCollection.Command))
                currentStep = CommandsStep.InvalidEntrance;
            else
                currentStep = CommandsStep.VerifyStep;

            return stepCollection;
        }
    }
}

namespace Algorithm.Logic.CommandProcessor.Commands
{
    using Algorithm.Logic.CommandProcessor.Models;
    using System.Linq;

    public class VerifyNextStepInCommandStep : ICommandStep
    {
        private char[] _commands = new char[] { 'N', 'L', 'S', 'O' };
        private char cancelLastOperation = 'X';

        public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
        {

            if (stepCollection.VerifyIfCommandTerminated())
            {
                currentStep = CommandsStep.Finish;
            }
            else
            {
                var currentChar = stepCollection.CharAtIndex;

                if (_commands.Contains(currentChar))
                {
                    currentStep = CommandsStep.AddStep;
                }
                else if (currentChar.Equals(cancelLastOperation))
                {
                    currentStep = CommandsStep.CancelLastOperation;
                }
                else
                {
                    if (int.TryParse(currentChar.ToString(), out int numericChar))
                    {
                        if (currentStep == CommandsStep.CancelLastOperation)
                            currentStep = CommandsStep.InvalidEntrance;
                        else
                            currentStep = CommandsStep.AddRepeats;
                    }
                    else
                    {
                        currentStep = CommandsStep.InvalidEntrance;
                    }
                }
            }

            return stepCollection;
        }
    }
}

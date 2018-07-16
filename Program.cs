
namespace Algorithm.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        /// <summary>
        /// PROBLEMA:
        /// 
        /// Implementar um algoritmo para o controle de posição de um drone emum plano cartesiano (X, Y).
        /// 
        /// O ponto inicial do drone é "(0, 0)" para cada execução do método Evaluate ao ser executado cada teste unitário.
        /// 
        /// A string de entrada pode conter os seguintes caracteres N, S, L, e O representando Norte, Sul, Leste e Oeste respectivamente.
        /// Estes catacteres podem estar presentes aleatóriamente na string de entrada.
        /// Uma string de entrada "NNNLLL" irá resultar em uma posição final "(3, 3)", assim como uma string "NLNLNL" irá resultar em "(3, 3)".
        /// 
        /// Caso o caracter X esteja presente, o mesmo irá cancelar a operação anterior. 
        /// Caso houver mais de um caracter X consecutivo, o mesmo cancelará mais de uma ação na quantidade em que o X estiver presente.
        /// Uma string de entrada "NNNXLLLXX" irá resultar em uma posição final "(1, 2)" pois a string poderia ser simplificada para "NNL".
        /// 
        /// Além disso, um número pode estar presente após o caracter da operação, representando o "passo" que a operação deve acumular.
		/// Este número deve estar compreendido entre 1 e 2147483647.
		/// Deve-se observar que a operação 'X' não suporta opção de "passo" e deve ser considerado inválido. Uma string de entrada "NNX2" deve ser considerada inválida.
        /// Uma string de entrada "N123LSX" irá resultar em uma posição final "(1, 123)" pois a string pode ser simplificada para "N123L"
        /// Uma string de entrada "NLS3X" irá resultar em uma posição final "(1, 1)" pois a string pode ser siplificada para "NL".
        /// 
        /// Caso a string de entrada seja inválida ou tenha algum outro problema, o resultado deve ser "(999, 999)".
        /// 
        /// OBSERVAÇÕES:
        /// Realizar uma implementação com padrões de código para ambiente de "produção". 
        /// Comentar o código explicando o que for relevânte para a solução do problema.
        /// Adicionar testes unitários para alcançar uma cobertura de testes relevânte.
        /// </summary>
        /// <param name="input">String no padrão "N1N2S3S4L5L6O7O8X"</param>
        /// <returns>String representando o ponto cartesiano após a execução dos comandos (X, Y)</returns>
        public static string Evaluate(string input)
        {
            var commandProcessor = new CommandsProcessor(input);

            return commandProcessor.Process();
        }

        public enum CommandsStep
        {
            Begin,
            VerifyStep,
            AddStep,
            AddRepeats,
            CancelLastOperation,
            IncreaseIndex,
            End
        }

        public class CommandsProcessor
        {

            private Dictionary<CommandsStep, ICommandStep> _commandSteps = new Dictionary<CommandsStep, ICommandStep>
            {
                { CommandsStep.Begin, new VerifyNextStepInCommandStep() },
                { CommandsStep.VerifyStep, new VerifyNextStepInCommandStep() },
                { CommandsStep.AddStep, new AddNewCommandStep() },
                { CommandsStep.CancelLastOperation, new CancelLastCommandStep() },
                { CommandsStep.AddRepeats, new AddNumberToRepeatStep() },
                { CommandsStep.IncreaseIndex, new IncreaseIndexStep() }
            };

            private string _command = string.Empty;

            public CommandsProcessor(string command)
            {
                this._command = command;
            }

            public string Process()
            {
                var stepCollection = new StepCollection(this._command);
                var step = CommandsStep.Begin;

                do
                {
                    _commandSteps[step].Execute(stepCollection, ref step);
                }
                while (step != CommandsStep.End);

                var output = stepCollection.GenerateOutput();

                return $"({output.Item1}, {output.Item2})";
            }
        }

        public struct Step
        {
            public char Command { get; set; }
            public int Repeat { get; set; }

            public Step(char Command, int repeat = 1)
            {
                this.Command = Command;
                this.Repeat = repeat;
            }
        }

        public class StepCollection
        {
            private IList<Step> _steps = new List<Step>();
            public int Index { get; private set; } = 0;
            
            public string Command { get; }

            public StepCollection(string command)
            {
                this.Command = command;
            }

            public char CharAtIndex()
            {
                return this.Command[this.Index];
            }

            public StepCollection IncreaseIndex()
            {
                this.Index++;

                return this;
            }

            public StepCollection AddStep(Step step)
            {
                this._steps.Add(step);

                return this;
            }

            public StepCollection RemoveLatestStep()
            {
                this._steps.RemoveAt(this._steps.Count - 1);

                return this;
            }

            public StepCollection AddNumberToLatestStepRepeat(char number)
            {
                var last = this._steps.Last();
                last.Repeat = Convert.ToInt32(String.Concat(this._steps.Last().Repeat, number));

                return this;
            }

            public bool VerifyIfCommandTerminated()
            {
                return this.Index > this.Command.Length - 1;
            }

            public Tuple<int, int> GenerateOutput()
            {
                return this._steps.Aggregate<Step, Tuple<int, int>>(new Tuple<int, int>(0,0), (accumulated, step) =>
                {
                    int directionQuantity;

                    if (step.Command.Equals('N'))
                    {
                        directionQuantity = accumulated.Item1 + step.Repeat;
                        return new Tuple<int, int>(directionQuantity, accumulated.Item2);
                    }

                    if (step.Command.Equals('S'))
                    {
                        directionQuantity = accumulated.Item1 - step.Repeat;
                        return new Tuple<int, int>(directionQuantity, accumulated.Item2);
                    }

                    if (step.Command.Equals('L'))
                    {
                        directionQuantity = accumulated.Item2 + step.Repeat;
                        return new Tuple<int, int>(accumulated.Item1, directionQuantity);
                    }

                    if (step.Command.Equals('O'))
                    {
                        directionQuantity = accumulated.Item2 - step.Repeat;
                        return new Tuple<int, int>(accumulated.Item1, directionQuantity);
                    }

                    return accumulated;
                });

            }
        }

        public interface ICommandStep
        {
            StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep);
        }

        public class VerifyNextStepInCommandStep : ICommandStep
        {
            private char[] _commands = new char[] { 'N', 'L', 'S', 'O' };
            private char cancelLastOperation = 'X';

            public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
            {

                if (stepCollection.VerifyIfCommandTerminated())
                {
                    currentStep = CommandsStep.End;
                }
                else
                {

                    var currentChar = stepCollection.CharAtIndex();

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
                            currentStep = CommandsStep.AddRepeats;
                        }
                        else
                        {
                            currentStep = CommandsStep.IncreaseIndex;
                        }
                    }
                }

                return stepCollection;
            }
        }

        public class IncreaseIndexStep : ICommandStep
        {
            public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
            {
                stepCollection.IncreaseIndex();

                currentStep = CommandsStep.VerifyStep;

                return stepCollection;
            }
        }

        public class AddNewCommandStep : ICommandStep
        {
            public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
            {
                stepCollection.AddStep(new Step(stepCollection.CharAtIndex()));

                currentStep = CommandsStep.IncreaseIndex;
                                
                return stepCollection;
            }
        }

        public class CancelLastCommandStep : ICommandStep
        {
            public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
            {
                stepCollection.RemoveLatestStep();

                currentStep = CommandsStep.IncreaseIndex;

                return stepCollection;
            }
        }

        public class AddNumberToRepeatStep : ICommandStep
        {
            public StepCollection Execute(StepCollection stepCollection, ref CommandsStep currentStep)
            {
                stepCollection.AddNumberToLatestStepRepeat(stepCollection.CharAtIndex());

                currentStep = CommandsStep.IncreaseIndex;

                return stepCollection;
            }
        }

    }
}

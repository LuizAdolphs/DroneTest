
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
            // TODO: Este método é o ponto de entrada para a lógica.
            return "(X, Y)";
        }


        public enum DirectionsStep
        {
            Begin,
            VerifStep,
            AddStep,
            AddRepeats,
            End
        }



        public class DirectionsProcessor
        {

            private Dictionary<DirectionsStep, IDirectionStep> _directionSteps = new Dictionary<DirectionsStep, IDirectionStep>
            {
                { DirectionsStep.Begin, new VerifyPositionInCommandStep() },
                { DirectionsStep.VerifStep, new VerifyPositionInCommandStep() }
            };


            public string Process(string command)
            {
                var stepCollection = new StepCollection(command);
                var step = DirectionsStep.Begin;

                while (step != DirectionsStep.End)
                {
                    _directionSteps[step].Execute(stepCollection, ref step);
                }

                return "";
            }
        }

        public struct Step
        {
            public char Direction { get; set; }
            public int Repeat { get; set; }

            public Step(char direction, int repeat = 1)
            {
                this.Direction = direction;
                this.Repeat = repeat;
            }
        }

        public class StepCollection
        {
            public IList<Step> Steps { get; set; } = new List<Step>();
            public int Index { get; set; } = 0;
            
            public string Command { get; }

            public StepCollection(string command)
            {
                this.Command = command;
            }

            public char CharAtIndex()
            {
                return this.Command[this.Index];
            }
        }

        public interface IDirectionStep
        {
            StepCollection Execute(StepCollection stepCollection, ref DirectionsStep currentStep);
        }

        public abstract class StepBase
        {
            protected char[] _directions = new char[] { 'N', 'L', 'S', 'O' };
        }

        public class VerifyPositionInCommandStep : StepBase, IDirectionStep
        {
            public StepCollection Execute(StepCollection stepCollection, ref DirectionsStep currentStep)
            {
                var currentChar = stepCollection.CharAtIndex();

                if (_directions.Contains(currentChar))
                {
                    currentStep = DirectionsStep.AddStep;
                }

                return stepCollection;
            }
        }

        public class AddNewDirectionStep : StepBase, IDirectionStep
        {
            public StepCollection Execute(StepCollection stepCollection, ref DirectionsStep currentStep)
            {
                stepCollection.Steps.Add(new Step(stepCollection.CharAtIndex()));

                currentStep = DirectionsStep.VerifStep;

                return stepCollection;
            }
        }

    }
}

namespace Algorithm.Logic.CommandProcessor.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StepCollection
    {
        private IList<Step> _steps = new List<Step>();
        public int Index { get; private set; } = 0;
        public string Command { get; }
        public bool InvalidEntrance { get; set; } = false;
        public char CharAtIndex
        {
            get
            {
                return this.Command[this.Index];
            }
        }
        public int StepsSize
        {
            get
            {
                return this._steps.Count;
            }
        }

        public StepCollection(string command)
        {
            this.Command = command;
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

        public Step LastStep()
        {
            return this._steps.Last();
        }

        public StepCollection RemoveLatestStep()
        {
            this._steps.RemoveAt(this._steps.Count - 1);

            return this;
        }

        public StepCollection AddNumberToLatestStepRepeat(char number)
        {
            var last = this._steps[this._steps.Count - 1];

            last.Repeat = Convert.ToInt32(String.Concat(this._steps.Last().Repeat, number));

            this._steps.RemoveAt(this._steps.Count - 1);

            this._steps.Add(last);

            return this;
        }

        public bool VerifyIfCommandTerminated()
        {
            return this.Index > this.Command.Length - 1;
        }

        public Tuple<int, int> GenerateOutput()
        {
            if (this.InvalidEntrance)
            {
                return new Tuple<int, int>(999, 999);
            }

            return this._steps.Aggregate<Step, Tuple<int, int>>(new Tuple<int, int>(0, 0), (accumulated, step) =>
            {
                int directionQuantity;

                var repeat = step.Repeat == 0 ? 1 : step.Repeat;

                if (step.Command.Equals('N'))
                {
                    directionQuantity = accumulated.Item1 + repeat;
                    return new Tuple<int, int>(directionQuantity, accumulated.Item2);
                }

                if (step.Command.Equals('S'))
                {
                    directionQuantity = accumulated.Item1 - repeat;
                    return new Tuple<int, int>(directionQuantity, accumulated.Item2);
                }

                if (step.Command.Equals('L'))
                {
                    directionQuantity = accumulated.Item2 + repeat;
                    return new Tuple<int, int>(accumulated.Item1, directionQuantity);
                }

                if (step.Command.Equals('O'))
                {
                    directionQuantity = accumulated.Item2 - repeat;
                    return new Tuple<int, int>(accumulated.Item1, directionQuantity);
                }

                return accumulated;
            });
        }
    }
}

namespace Algorithm.Logic.CommandProcessor.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StepCollection
    {
        private const char _n = 'N';
        private const char _s = 'S';
        private const char _l = 'L';
        private const char _o = 'O';

        private static Dictionary<char, Func<Tuple<int, int>, Step, Tuple<int, int>>> _actions = new Dictionary<char, Func<Tuple<int, int>, Step, Tuple<int, int>>>
        {
            { _n, (Tuple<int, int> accumulated, Step step) => new Tuple<int, int>(accumulated.Item1 + step.MinimumTimes, accumulated.Item2) },
            { _s, (Tuple<int, int> accumulated, Step step) => new Tuple<int, int>(accumulated.Item1 - step.MinimumTimes, accumulated.Item2) },
            { _l, (Tuple<int, int> accumulated, Step step) => new Tuple<int, int>(accumulated.Item1, accumulated.Item2 + step.MinimumTimes) },
            { _o, (Tuple<int, int> accumulated, Step step) => new Tuple<int, int>(accumulated.Item1, accumulated.Item2 - step.MinimumTimes) }
        };

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

            last.Times = Convert.ToInt32(String.Concat(this._steps.Last().Times, number));

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
                return _actions[step.Command](accumulated, step);
            });
        }
    }
}

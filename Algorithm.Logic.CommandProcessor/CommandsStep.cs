using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Logic.CommandProcessor
{
    public enum CommandsStep
    {
        Start,
        VerifyStep,
        AddStep,
        AddRepeats,
        CancelLastOperation,
        IncreaseIndex,
        InvalidEntrance,
        Finish
    }
}

using Rowatch2.Enums;
using Rowatch2.Librarys;
using System;

namespace Rowatch2.Embeding
{
    public class MacroAction
    {
        public MacroAction()
        {
        }

        public MacroAction(MacroAction action)
        {
            Type = action.Type;
            Delay = action.Delay;
            Key = action.Key;
        }

        public MacroActionType Type { get; set; }
        public TimeSpan? Delay { get; set; }
        public VKey? Key { get; set; }

        public override string ToString()
        {
            switch (Type)
            {
                case MacroActionType.Delay:
                    return string.Format("{0:00}:{1:00}:{2:0000}",
                        Delay.Value.Minutes,
                        Delay.Value.Seconds,
                        Delay.Value.Milliseconds);

                case MacroActionType.Key:
                    return SendInputManager.GetKeyDescription(Key.Value);

                default:
                    return string.Empty;
            }
        }
    }
}
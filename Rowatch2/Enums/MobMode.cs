using System;

namespace Rowatch2.Enums
{
    [Flags]
    public enum MobMode
    {
        MD_CANMOVE = 0x0001,
        MD_LOOTER = 0x0002,
        MD_AGGRESSIVE = 0x0004,
        MD_ASSIST = 0x0008,
        MD_CASTSENSOR_IDLE = 0x0010,
        MD_BOSS = 0x0020,
        MD_PLANT = 0x0040,
        MD_CANATTACK = 0x0080,
        MD_DETECTOR = 0x0100,
        MD_CASTSENSOR_CHASE = 0x0200,
        MD_CHANGECHASE = 0x0400,
        MD_ANGRY = 0x0800,
        MD_CHANGETARGET_MELEE = 0x1000,
        MD_CHANGETARGET_CHASE = 0x2000,
        MD_TARGETWEAK = 0x4000,
        MD_MASK = 0xFFFF
    }
}
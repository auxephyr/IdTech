using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class UdmfLinedef
    {
        public int Id;
        public int V1;
        public int V2;
        public bool Blocking;
        public bool BlockMonsters;
        public bool TwoSided;
        public bool DontPegTop;
        public bool DontPegBottom;
        public bool Secret;
        public bool BlockSound;
        public bool DontDraw;
        public bool Mapped;
        public bool PassUse;
        public bool Translucent;
        public bool JumpOver;
        public bool BlockFloaters;
        public bool PlayerCross;
        public bool PlayerUse;
        public bool MonsterCross;
        public bool MonsterUse;
        public bool Impact;
        public bool PlayerPush;
        public bool MonsterPush;
        public bool MissileCross;
        public bool RepeatSpecial;
        public int Special;
        public int Arg0;
        public int Arg1;
        public int Arg2;
        public int Arg3;
        public int Arg4;
        public int SideFront;
        public int SideBack;
        public string Comment;
    }
}
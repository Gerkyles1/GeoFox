using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    public class UnknownSpell : Spell
    {
        public override void GetStats(ref string oldStat, ref string newStat)
        {
            oldStat = "???";
            newStat = "???";
        }

        public override void InitialiseSpell(Vector2 position, int direction)
        {
            return;
        }

        public override void UpgrateSpell()
        {
            return;
        }
    }
}
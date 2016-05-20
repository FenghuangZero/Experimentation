using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGOShared;

namespace YGOShared
{
    class DecisionMaking
    {
        Player p;
        Player o;

        public Card bestAttacker()
        {
            var normalSummonable = p.Hand.Where(m => m.monsterType != "" && m.level < 5);
            var tribute1Summonable = p.Hand.Where(m => m.monsterType != "" && m.level > 4 && m.level < 7);
            var tribute2summonable = p.Hand.Where(m => m.monsterType != "" && m.level > 6);
            var oppsAtkPosMons = o.MonsterZone.Where(m => m.monsterType != "" && m.Horizontal == false);
            var oppsDefPosMons = o.MonsterZone.Where(m => m.monsterType != "" && m.Horizontal);
            var normSumm = normalSummonable.Any();
            var trib1Summ = tribute1Summonable.Any();
            var trib2Summ = tribute2summonable.Any();
            var canTrib1 = p.MonsterZone.Any();
            var canTrib2 = (p.MonsterZone.Count > 1);
            normalSummonable.OrderBy(m => m.atkOnField);
            tribute1Summonable.OrderBy(m => m.atkOnField);
            tribute2summonable.OrderBy(m => m.atkOnField);

            if (normSumm)
                return normalSummonable.First();
            if (trib1Summ)
                return tribute1Summonable.First();
            if (trib2Summ)
                return tribute2summonable.First();
            throw new NoMonsterinHandException();
        }

        public DecisionMaking(Player player, Player opponent)
        {
            p = player;
            o = opponent;
        }
    }
}

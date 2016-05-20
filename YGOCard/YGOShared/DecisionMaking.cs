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
            var canTrib1 = p.MonsterZone.Any();
            var canTrib2 = (p.MonsterZone.Count > 1);
            normalSummonable.OrderBy(m => m.atkOnField);
            tribute1Summonable.OrderBy(m => m.atkOnField);
            tribute2summonable.OrderBy(m => m.atkOnField);

            if (normalSummonable.Any())
                return normalSummonable.First();
            if (tribute1Summonable.Any() && canTrib1)
                return tribute1Summonable.First();
            if (tribute2summonable.Any() && canTrib2)
                return tribute2summonable.First();
            throw new NoMonsterinHandException("No monsters in hand that can be summoned.");
        }

        public Card bestDefender()
        {
            var normalSummonable = p.Hand.Where(m => m.monsterType != "" && m.level < 5);
            var tribute1Summonable = p.Hand.Where(m => m.monsterType != "" && m.level > 4 && m.level < 7);
            var tribute2summonable = p.Hand.Where(m => m.monsterType != "" && m.level > 6);
            var oppsAtkPosMons = o.MonsterZone.Where(m => m.monsterType != "" && m.Horizontal == false);
            var oppsDefPosMons = o.MonsterZone.Where(m => m.monsterType != "" && m.Horizontal);
            var canTrib1 = p.MonsterZone.Any();
            var canTrib2 = (p.MonsterZone.Count > 1);
            normalSummonable.OrderBy(m => m.defOnField);
            tribute1Summonable.OrderBy(m => m.defOnField);
            tribute2summonable.OrderBy(m => m.defOnField);

            if (normalSummonable.Any())
                return normalSummonable.First();
            if (tribute1Summonable.Any() && canTrib1)
                return tribute1Summonable.First();
            if (tribute2summonable.Any() && canTrib2)
                return tribute2summonable.First();
            throw new NoMonsterinHandException("No monsters in hand that can be summoned.");
        }

        public bool shouldAttack()
        {
            if (!o.MonsterZone.Any())
                return true;
            p.MonsterZone.OrderBy(m => m.atkOnField);
            o.MonsterZone.OrderBy(m => m.atkOnField);
            p.Hand.OrderBy(m => m.atkOnField);
            if (!p.MonsterZone.Any())
                if (p.Hand.First().atkOnField > o.MonsterZone.Last().atkOnField)
                    return true;
            if (p.MonsterZone.Any() && o.MonsterZone.Any())
                if (p.MonsterZone.First().atkOnField > o.MonsterZone.Last().atkOnField)
                    return true;
            return false;
        }


        public DecisionMaking(Player player, Player opponent)
        {
            p = player;
            o = opponent;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using IodemBot.Modules.ColossoBattles;
using Newtonsoft.Json;

namespace IodemBot.Modules.GoldenSunMechanics
{
    internal class StatEffect : Effect
    {
        public override string Type { get; } = "Stat";
        [JsonProperty] private string Stat { get; set; } = "OH NO";
        [JsonProperty] private double Multiplier { get; set; } = 1;
        [JsonProperty] private int Probability { get; set; } = 100;
        [JsonProperty] private bool OnTarget { get; set; } = true;
        [JsonProperty] private int Turns { get; set; } = 7;

        public override string ToString()
        {
            return $"{(Probability == 100 ? $"{(Multiplier > 1 ? "Raise" : "Lower")}" : $"{Probability}% chance to {(Multiplier > 1 ? "raise" : "lower")}")} {Stat} of {(OnTarget ? "target" : "user")} to {Multiplier * 100}%";
        }

        public override List<string> Apply(ColossoFighter User, ColossoFighter Target)
        {
            List<string> log = new List<string>();
            if (!Target.IsAlive)
            {
                return log;
            }

            if (Stat == "OH NO")
            {
                Console.WriteLine(string.Join(", ", User.Moves.Select(s => s.Name)));
            }

            if (Global.RandomNumber(0, 100) <= Probability)
            {
                if (OnTarget)
                {
                    Target.ApplyBuff(new Buff(Stat, Multiplier, (uint)Turns));
                    log.Add($"{Target.Name}'s {Stat} {(Multiplier > 1 ? "rises" : "lowers")}.");
                }
                else
                {
                    User.ApplyBuff(new Buff(Stat, Multiplier, (uint)Turns));
                    log.Add($"{User.Name}'s {Stat} {(Multiplier > 1 ? "rises" : "lowers")}.");
                }
            }

            return log;
        }
    }
}
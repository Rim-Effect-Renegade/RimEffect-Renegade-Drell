using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace REDrell
{
    [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class Patch_MemoryThoughtHandler
    {
        [HarmonyPrefix]
        public static bool Prefix(Thought_Memory newThought, Pawn otherPawn, MemoryThoughtHandler __instance)
        {
            Pawn pawn = __instance?.pawn;
            Gene gene = pawn.genes?.GetGene(REDrellDefOf.RE_MindSeparation) ?? null;
            if (gene != null)
            {
                Gene_MindSeparation msGene = gene as Gene_MindSeparation;
                if (!msGene?.SoulActive ?? false)
                {
                    return false;
                }
            }
            else if (pawn.genes?.HasGene(REDrellDefOf.RE_LongMemory) ?? false)
            {
                newThought.durationTicksOverride = Mathf.RoundToInt(newThought.DurationTicks * 10f);
            }

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(Thought_Memory newThought, Pawn otherPawn)
        {
            // Drell Lovin additional thought.
            if(newThought.def == ThoughtDefOf.GotSomeLovin && (otherPawn?.genes?.HasGene(REDrellDefOf.RE_DrellSkin) ?? false))
            {
                Thought_MemorySocial drellLovin = (Thought_MemorySocial)ThoughtMaker.MakeThought(REDrellDefOf.RE_DrellLovin);
                otherPawn.needs.mood.thoughts.memories.TryGainMemory(drellLovin, otherPawn);
            }
        }
    }
}

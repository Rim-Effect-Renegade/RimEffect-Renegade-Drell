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
    public class Patch_Need_Mood
    {
        [HarmonyPatch(typeof(Need_Mood), "NeedInterval")]
        public static class Patch_NeedInterval
        {
            [HarmonyPostfix]
            public static void Postfix(Need_Mood __instance)
            {
                Pawn pawn = __instance.pawn;

                Gene gene = pawn.genes?.GetGene(REDrellDefOf.RE_MindSeparation) ?? null;
                if (gene != null)
                {
                    Gene_MindSeparation msGene = gene as Gene_MindSeparation;
                    if (!msGene?.SoulActive ?? false)
                    {
                        __instance.CurLevel = __instance.MaxLevel;
                    }
                }
            }
        }
    }
}

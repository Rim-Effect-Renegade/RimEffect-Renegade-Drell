using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace REDrell
{
    [DefOf]
    public class REDrellDefOf
    {
        public REDrellDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(REDrellDefOf));
        }

        public static GeneDef 
            RE_DrellSkin,
            RE_LongMemory,
            RE_KepralSyndrome,
            RE_MindSeparation;

        public static ThoughtDef 
            RE_DrellLovin;

        public static HediffDef 
            RE_KepralsSyndrome;

    }
}

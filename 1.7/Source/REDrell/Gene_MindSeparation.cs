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
    public class Gene_MindSeparation : Gene
    {
        private bool soulActive = true;
        private Texture2D soulIcon;
        private Texture2D bodyIcon;

        private float originalMood;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref soulActive, "soulActive", true);
            Scribe_Values.Look(ref originalMood, "originalMood", 1f);
        }

        public bool SoulActive
        {
            get
            {
                return soulActive;
            }
        }

        public Texture2D SoulIcon
        {
            get
            {
                if(soulIcon == null)
                {
                    soulIcon = ContentFinder<Texture2D>.Get("Things/Gizmo/DrellMode_Soul", true);
                }
                return soulIcon;
            }
        }

        public Texture2D BodyIcon
        {
            get
            {
                if (bodyIcon == null)
                {
                    bodyIcon = ContentFinder<Texture2D>.Get("Things/Gizmo/DrellMode_Body", true);
                }
                return bodyIcon;
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (!base.GetGizmos().EnumerableNullOrEmpty())
            {
                foreach (Gizmo current in base.GetGizmos())
                {
                    yield return current;
                }
            }

            if (soulActive)
            {
                yield return new Command_Action
                {
                    defaultLabel = "RE_DrellSoulModeLabel".Translate(),
                    defaultDesc = "RE_DrellSoulModeDesc".Translate(),
                    icon = SoulIcon,
                    action = delegate { 
                        soulActive = false;
                        originalMood = pawn.needs.mood.curLevelInt;
                        pawn.needs.mood.curLevelInt = pawn.needs.mood.MaxLevel;
                    }
                };
            }
            else
            {
                yield return new Command_Action
                {
                    defaultLabel = "RE_DrellBodyModeLabel".Translate(),
                    defaultDesc = "RE_DrellBodyModeDesc".Translate(),
                    icon = BodyIcon,
                    action = delegate { 
                        soulActive = true;
                        pawn.needs.mood.curLevelInt = originalMood;
                    }
                };
            }
        }
    }
}

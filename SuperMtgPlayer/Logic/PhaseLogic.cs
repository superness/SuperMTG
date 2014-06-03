using Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class PhaseLogic : Singleton<PhaseLogic>
    {
        public enum Phase
        {
            Beginning,
            PreCombatMain,
            Combat,
            PostCombatMain,
            End
        }

        public enum Step
        {
            Beginning_Untap,
            Beginning_Upkeep,
            Beginning_Draw,

            PreCombatMain,

            Combat_Beginning,
            Combat_DeclareAttackers,
            Combat_DeclareBlockers,
            Combat_Damage,
            Combat_End,

            PostCombatMain,

            End_End,
            End_CleanUp,

            Count
        }

        public Phase CurrentPhase = Phase.Beginning;
        public Step CurrentStep = Step.Beginning_Untap;

        private delegate void StepFunc();
        private StepFunc[] StepFunctions = { PhaseLogic.BeginningUntap,
                                             PhaseLogic.BeginningUpkeep,
                                             PhaseLogic.BeginningDraw,

                                             PhaseLogic.PreCombatMain,

                                             PhaseLogic.CombatBeginning,
                                             PhaseLogic.CombatDeclareAttackers,
                                             PhaseLogic.CombatDeclareBlockers,
                                             PhaseLogic.CombatDamage,
                                             PhaseLogic.CombatEnd,

                                             PhaseLogic.PostCombatMain,

                                             PhaseLogic.EndEnd,
                                             PhaseLogic.EndCleanup
                                           };

        public void ProcessStep()
        {
            this.StepFunctions[(int)this.CurrentStep]();
        }

        public void NextStep()
        {
            // Increment current step
            this.CurrentStep++;

            // If we completed the last step then start the next turn
            if(this.CurrentStep == Step.Count)
            {
                CurrentTurn.Global.NextTurn();
                this.CurrentStep = Step.Beginning_Untap;
            }
        }

        // Steps
        private static void BeginningUntap()
        {
            if(CurrentTurn.Global.CurrentActivePlayer == CurrentTurn.ActivePlayer.Local)
            {
                Battlefield.Global.UntapLocal();
            }
            else
            {
                Battlefield.Global.UntapRemote();
            }
        }

        private static void BeginningUpkeep()
        {
            Battlefield.Global.ProcessTriggerType(TriggeredAbility.TriggerType.AtBeginingOfUpkeep);
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            PriorityLayer.Global.GivePriority();
        }

        private static void BeginningDraw()
        {
            Game1.Global.player.DrawCard();
            Battlefield.Global.ProcessTriggerType(TriggeredAbility.TriggerType.AtBeginingOfDrawStep);
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            PriorityLayer.Global.GivePriority();
        }

        private static void PreCombatMain()
        {
            Battlefield.Global.ProcessTriggerType(TriggeredAbility.TriggerType.AtBeginningOfPreCombatMain);
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Sorcery;
            PriorityLayer.Global.GivePriority();
        }

        private static void CombatBeginning()
        {
            Battlefield.Global.ProcessTriggerType(TriggeredAbility.TriggerType.AtBeginningOfCombat);
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            PriorityLayer.Global.GivePriority();
        }

        private static void CombatDeclareAttackers()
        {
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            PriorityLayer.Global.GivePriority();
        }

        private static void CombatDeclareBlockers()
        {
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            PriorityLayer.Global.GivePriority();
        }

        private static void CombatDamage()
        {
            // Deal damage
        }

        private static void CombatEnd()
        {
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            PriorityLayer.Global.GivePriority();
        }

        private static void PostCombatMain()
        {
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Sorcery;
            Battlefield.Global.ProcessTriggerType(TriggeredAbility.TriggerType.AtBeginningOfPostCombatMain);
            PriorityLayer.Global.GivePriority();
        }

        private static void EndEnd()
        {
            CardPlayer.Global.CurrentPlaySpeed = CardPlayer.PlaySpeed.Instant;
            Battlefield.Global.ProcessTriggerType(TriggeredAbility.TriggerType.AtBeginningOfEndStep);
            PriorityLayer.Global.GivePriority();
        }

        private static void EndCleanup()
        {
            // Cleanup
        }
        // End Steps
    }
}

using SuperMtgPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class AIPlayer : Player
    {
        public AIPlayer()
        {
            this.Type = Common.PlayerType.Remote;
        }

        public override void OnPriority()
        {
            // Do something based on the phase
            switch(PhaseLogic.Global.CurrentStep)
            {
                case PhaseLogic.Step.PreCombatMain:
                case PhaseLogic.Step.PostCombatMain:
                    this.MainPhase();
                    break;
                default:
                    this.DoneActing();
                    break;
            }
        }

        private void DoneActing()
        {
            PriorityLayer.Global.PassPriorty();
        }

        private void MainPhase()
        {
            if(CurrentTurn.Global.CurrentActivePlayer == Common.PlayerType.Remote)
            {
                if (CurrentTurn.Global.CurrentLandsPlayed < CurrentTurn.Global.LandsPerTurn)
                {
                    // Play a land
                    foreach(PlayableCard card in this.hand.Hand)
                    {
                        if(card.card.types.Contains("Land"))
                        {
                            Battlefield.Global.PutCardIntoPlayForPlayer(card, this);
                            CurrentTurn.Global.CurrentLandsPlayed++;
                            this.hand.PlayCard(card);
                            break;
                        }
                    }
                }

                // Tap all lands for mana
                foreach(CardInPlay card in Battlefield.Global.remoteLandsInPlay)
                {
                    if(card.Tapped == false)
                        card.ActivateAbility(card.activatedAbilities[0]);
                }

                // Play the first thing in our hand that we can
                foreach (PlayableCard card in this.hand.Hand)
                {
                    // Keep it simple stupid 
                    if (card.card.types.Contains("Creature") && CardPlayer.Global.CanPlayCard(this, card))
                    {
                        CardPlayer.Global.TryPlayCard(this, card);
                        this.hand.PlayCard(card);
                        break;
                    }
                }
            }

            this.DoneActing();
        }
    }
}

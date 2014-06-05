using Super;
using SuperMtgPlayer.Data;
using SuperMtgPlayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Display
{
    public class ChooseAbility : Singleton<ChooseAbility>
    {
        public CardInPlay card = null;
        public CardDisplay mainCard = null;
        public ActivatedAbility abilityChosen = null;

        public List<ActivatedAbility> abilitiesToChoose = null;

        public void ShowCard(CardInPlay _card)
        {
            this.card = _card;

            this.abilitiesToChoose = this.card.activatedAbilities;

            // Create card display
            SuperTexture tex = DisplayFactory.Global.Create();
            tex.Load(string.Format("CardImgs\\{0}.jpg", card.cardData.multiverseid));

            this.mainCard = CardDisplayFactory.Global.Create();
            this.mainCard.Init(tex);
            this.mainCard.initScale = 2.0f;
            this.mainCard.scale.targetValue = 2.0f;
            this.mainCard.scale.currentValue = 2.0f;

            this.mainCard.TargetLocation.X.targetValue = Game1.Global.graphics.PreferredBackBufferWidth / 2 - this.mainCard.BaseDimWidth() / 2;
            this.mainCard.TargetLocation.Y.targetValue = Game1.Global.graphics.PreferredBackBufferHeight / 2 - this.mainCard.BaseDimHeight() / 2;
            this.mainCard.TargetLocation.X.currentValue = this.mainCard.TargetLocation.X.targetValue;
            this.mainCard.TargetLocation.Y.currentValue = this.mainCard.TargetLocation.Y.targetValue;

            UIFocus.Global.TakeFocus(this);

            // Auto activate if only one ability
            if(this.abilitiesToChoose.Count == 1)
            {
                if(this.CanActivateAbility(this.abilitiesToChoose[0]))
                {
                    this.card.ActivateAbility(this.abilitiesToChoose[0]);
                }

                this.Close();
            }
        }

        public void Close()
        {
            this.mainCard.Unload();

            this.card = null;
            this.mainCard = null;
            this.abilityChosen = null;
            this.abilitiesToChoose = null;

            UIFocus.Global.PopFocus(this);
        }

        public void Update()
        {
            if (UIFocus.Global.FocusObj != this)
                return;

            if(SuperMouse.Global.RightMouseClick())
            {
                this.Close();
                return;
            }

            if(SuperKeyboard.Global.KeyPress(Microsoft.Xna.Framework.Input.Keys.D1))
            {
                if(this.abilitiesToChoose.Count >= 1)
                {
                    this.ActivateAbility(this.abilitiesToChoose[0]);
                }
            }
            else if (SuperKeyboard.Global.KeyPress(Microsoft.Xna.Framework.Input.Keys.D2))
            {
                if (this.abilitiesToChoose.Count >= 2)
                {
                    this.ActivateAbility(this.abilitiesToChoose[1]);
                }
            } if (SuperKeyboard.Global.KeyPress(Microsoft.Xna.Framework.Input.Keys.D3))
            {
                if (this.abilitiesToChoose.Count >= 3)
                {
                    this.ActivateAbility(this.abilitiesToChoose[2]);
                }
            } if (SuperKeyboard.Global.KeyPress(Microsoft.Xna.Framework.Input.Keys.D4))
            {
                if (this.abilitiesToChoose.Count >= 4)
                {
                    this.ActivateAbility(this.abilitiesToChoose[3]);
                }
            } if (SuperKeyboard.Global.KeyPress(Microsoft.Xna.Framework.Input.Keys.D5))
            {
                if (this.abilitiesToChoose.Count >= 5)
                {
                    this.ActivateAbility(this.abilitiesToChoose[4]);
                }
            }
        }

        private void ActivateAbility(ActivatedAbility ability)
        {
            if(this.CanActivateAbility(ability))
            {
                this.card.ActivateAbility(ability);

                this.Close();
            }
        }

        private bool CanActivateAbility(ActivatedAbility ability)
        {
            switch (ability.type)
            {
                case ActivatedAbility.AbilityType.Pay:
                    if (Game1.Global.player.manaPool.HaveManaForSpell(ability.cost))
                    {
                        return true;
                    }
                    break;
                case ActivatedAbility.AbilityType.PayTap:
                    if (Game1.Global.player.manaPool.HaveManaForSpell(ability.cost) && this.card.Tapped == false)
                    {
                        return true;
                    }
                    break;
                case ActivatedAbility.AbilityType.Tap:
                    if (this.card.Tapped == false)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}

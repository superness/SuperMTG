using Microsoft.Xna.Framework;
using Super;
using SuperMtgPlayer.Display;
using SuperMtgPlayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Data
{
    public class CardInPlay
    {
        public List<StaticAbility> staticAbilities = new List<StaticAbility>();
        public List<ActivatedAbility> activatedAbilities = new List<ActivatedAbility>();
        public List<TriggeredAbility> triggeredAbilities = new List<TriggeredAbility>();

        public OutputCard cardData = null;
        public Player owner = null;
        public CardDisplay display = null;
        public Point startAt = new Point();
        public bool Tapped = false;

        public CardInPlay()
        {

        }

        public void Init(PlayableCard card, Player _owner)
        {
            this.cardData = card.card;
            this.owner = _owner;

            this.startAt.X = (int)card.display.TargetLocation.X.currentValue;
            this.startAt.Y = (int)card.display.TargetLocation.Y.currentValue;
        }

        public void Display()
        {
            this.display = CardDisplayFactory.Global.Create();
            this.display.Init(GetCardTexture());

            this.display.TargetLocation.X.BlendSpeed = 20.0f;
            this.display.TargetLocation.Y.BlendSpeed = 20.0f;
            this.display.TargetLocation.X.currentValue = this.startAt.Y;
            this.display.TargetLocation.Y.currentValue = this.startAt.Y;
        }

        public void Update(GameTime gt)
        {
            if (SuperMouse.Global.LeftMouseClick())
            {
                if(this.display.isHighlighted)
                {
                    // A card was clicked

                    // Handle declaring attackers

                    // Otherwise activate abilities
                    if(this.Tapped == false)
                    {
                        ActivatedAbility[] tapAbilities = this.GetActivatedAbilitiesOfType(ActivatedAbility.AbilityType.Tap, this.cardData);
                        if(tapAbilities != null)
                        {
                            // Tap dat ass
                            this.Tapped = true;
                            this.display.canFocus = false;
                            Battlefield.Global.ActivateAbility(this.cardData.activatedAbilities[0], this);
                        }
                    }
                }
            }
        }

        private ActivatedAbility[] GetActivatedAbilitiesOfType(ActivatedAbility.AbilityType type, OutputCard card)
        {
            if(card.activatedAbilities != null)
            {
                return (ActivatedAbility[])card.activatedAbilities.Where((x) => x.type == type).ToArray();
            }

            return null;
        }

        private SuperTexture GetCardTexture()
        {
            SuperTexture texture = DisplayFactory.Global.Create();
            texture.Load(string.Format("CardImgs\\{0}.jpg", this.cardData.multiverseid));

            return texture;
        }
    }
}

using Microsoft.Xna.Framework;
using Super;
using SuperMtgPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic.Player
{
    public interface IPlayer
    {
        // Deck management
        void AddCardToDeck(OutputCard card, bool mainboard);
        void AddCardsToDeck(List<OutputCard> cards, bool mainboard);
        void RemoveCardFromDeck(OutputCard card, bool mainboard);
        PlayerDeck GetDeck();

        // Gameplay
        void GameStart();
        void DrawHand();
        void Mulligan();
        List<PlayableCard> GetCardsInHand();

        // Update
        void Update(GameTime gt);
    }
}

using Nebula._79Nebula.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    class AutoBattle
    {
        public const int MaxRounds = 5;

        private List<Player> _players;

        public AutoBattle(Player player, Player opponent)
        {
            if ((player.Modules.Count < MaxRounds) || (player.Modules.Count < MaxRounds))
            {
                throw new Exception($"Player has less than {MaxRounds} Modules equipped.");
            }

            _players.Add(player);
            _players.Add(opponent);
        }

        private void Battle()
        {
            for (int round = 0; round < MaxRounds; round++ )
            {
                DoRound(round);
            }
        }

        private void DoRound(int round)
        {

            _players = SortPlayersByPriority(round, _players);
            _players[0].ApplyEffect(new InitiatorBonus());

            // TRIGGER OnRoundStart
            // Todo: Add Initiator Bonus


            UseModule(round, _players[0], _players[1]);
            UseModule(round, _players[1], _players[0]);

            // TRIGGER OnRoundEnd
            // Todo: Remove Initiator Bonus
            _players[0].RemoveEffect(new InitiatorBonus());
        }


        private void UseModule(int i, Player player, Player opponent)
        {
            // Todo: Before

            player.Modules[i].Activate(player, opponent);

            // Todo: After
        }


        /// <summary>
        /// Takes a List of Players and sorts them by Priority.
        /// Priority is sorted by (ASC Module Priority, DESC Player Init)
        /// </summary>
        private List<Player> SortPlayersByPriority(int round, List<Player> players)
        {
            players.OrderBy(o => o.GetPriority(round)).ThenByDescending(o => o.Initiative);
            return players;
        }

    }

}

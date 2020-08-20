using Nebula._79Nebula.Effects;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class AutoBattle
    {
        public const int MaxRounds = 5;

        public AutoBattleState State = AutoBattleState.IsPreparing;

        private Player _player;
        private Player _opponent;

        public AutoBattle()
        {
        }

        public AutoBattleState Battle(Player player, Player opponent)
        {
            _player = player;
            _opponent = opponent;

            State = AutoBattleState.IsRunning;
            bool lastRoundPlayerFirst = true;

            for (int round = 0; round < MaxRounds; round++)
            {   // Do rounds 0 to 4

                if (IsPlayerFaster(round, lastRoundPlayerFirst))
                {
                    // Player starts round
                    DoRound(round, _player, _opponent);
                    lastRoundPlayerFirst = true;
                }
                else
                {
                    // Opponent starts round
                    DoRound(round, _opponent, _player);
                    lastRoundPlayerFirst = false;
                }

                if (State != AutoBattleState.IsRunning)
                {   // Premature End
                    break;
                }
            }

            return State = GetBattleResult();
        }

        private void DoRound(int round, Player player1, Player player2)
        {
            // Apply Initiator Bonus to faster player
            player1.ApplyEffect(new InitiatorBonus());

            // Todo: Trigger OnRoundStart

            if (State == AutoBattleState.IsRunning)
            {
                // Player 1 Action
                UseModule(round, player1, player2);
                
                // Check if any player's health is zero or below
                CheckPrematureEnd();
            }

            if (State == AutoBattleState.IsRunning)
            {
                // Player 2 Action
                UseModule(round, player2, player1);

                // Check if any player's health is zero or below
                CheckPrematureEnd();
            }

            // Todo: Trigger OnRoundEnd
            OnRoundEnd();
            CheckPrematureEnd();

            // Remove Initiator Bonus from faster player
            player1.RemoveEffect(new InitiatorBonus());
        }


        private void UseModule(int i, Player player, Player opponent)
        {
            // Todo: Before

            player.Modules[i].Activate(player, opponent);

            // Todo: After
        }

        /// <summary>
        /// Compares both players Module Priority, Initiative, Health.
        /// </summary>
        /// <returns>True if player is faster. False if opponent is faster.</returns>
        private bool IsPlayerFaster(int round, bool resultIfEqual = true)
        {
            if (_player.GetModulePriority(round) < _opponent.GetModulePriority(round))
            {
                return true;
            } 
            else if (_player.GetModulePriority(round) > _opponent.GetModulePriority(round))
            {
                return false;
            } 
            else
            {
                if (_player.Initiative > _opponent.Initiative)
                {
                    return true;
                }
                else if(_player.Initiative < _opponent.Initiative)
                {
                    return false;
                } else
                {
                    if (_player.Health > _opponent.Health)
                    {
                        return true;
                    }
                    else if (_player.Health < _opponent.Health)
                    {
                        return false;
                    }
                    else
                    {
                        return resultIfEqual;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if any player's health is zero or below
        /// Sets Battle State accordingly to draw, lost, won.
        /// </summary>
        private void CheckPrematureEnd()
        {
            if (_player.Health <= 0 && _opponent.Health <= 0)
            {
                State = AutoBattleState.Draw;
            } 
            else if (_player.Health <= 0)
            {
                State = AutoBattleState.Lost;
            }
            else if (_opponent.Health <= 0)
            {
                State = AutoBattleState.Won;
            }
        }

        /// <summary>
        /// Sets State status to Draw, Won or Lost.
        /// </summary>
        private AutoBattleState GetBattleResult()
        {

            if ((_player.Health == _opponent.Health) || (_player.Health <= 0 && _opponent.Health <= 0))
            {
                return AutoBattleState.Draw;
            }
            else if ((_player.Health > _opponent.Health) || (_opponent.Health <= 0))
            {
                return AutoBattleState.Won;
            }
            else
            {
                return AutoBattleState.Lost;
            }
        }
        private void OnRoundEnd()
        {
            _player.Effects.ForEach(o => o.OnRoundEnd(_player));
            _opponent.Effects.ForEach(o => o.OnRoundEnd(_opponent));
        }

    }

}

using Godot;
using Nebula._79Nebula.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class BattleInstance
    {
        public Player Player;
        public Player Opponent;

        private List<Enemy> _enemies;
        private AutoBattle autoBattle = new AutoBattle();

        public BattleInstance(Player player, List<Enemy> enemies)
        {
            Player = player;
            _enemies = enemies;
        }

        public AutoBattleState Battle()
        {
            Player.Reset();
            Opponent = SelectEnemy().ToPlayer();
            return autoBattle.Battle(Player, Opponent);
        }

        private Enemy SelectEnemy()
        {
            Random random = new Random();
            return _enemies.ElementAt(random.Next(0, _enemies.Count - 1));
        }


    }
}

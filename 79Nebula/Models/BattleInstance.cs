using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    class BattleInstance
    {
        private Player _player;
        private List<Enemy> _enemies;

        private AutoBattle autoBattle = new AutoBattle();

        public BattleInstance(Player player, List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
        }

        public void Battle()
        {
            Player opponent = SelectEnemy().ToPlayer();
            autoBattle.Battle(_player, opponent);
        }

        private Enemy SelectEnemy()
        {
            Random random = new Random();
            return _enemies.ElementAt(random.Next(0, _enemies.Count - 1));
        }


    }
}

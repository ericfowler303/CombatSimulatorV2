using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulatorV2
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Enemy
    {
        private Random rng = new Random();
        public string Name { get; set; }
        public double HP { get; set; }
        public bool IsAlive
        {
            get { if (this.HP > 0.0) { return true; } else { return false; } }
        }
        
        public Enemy(string name, double health)
        {
            this.Name = name;
            this.HP = health;
        }

        public void DoAttack(Player player)
        {
            // Has a 80% chance of hitting
            if (rng.NextDouble() >= 0.2)
            {
                // A hit, between $5-$15 loss for the player
                player.HP -= rng.Next(5, 16);
            }
        }
    }
}

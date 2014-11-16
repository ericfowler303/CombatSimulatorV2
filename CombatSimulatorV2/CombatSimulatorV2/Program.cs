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
    class Player
    {
        enum AttackType
        {
            Tetris =1,
            BurnTPS,
            Heal,
            SmashPrinter,
            AccountingVirus,
        }
        private bool HasPrinterBeenSmashed { get; set; }
        private bool AccountingVirusRunning { get; set; }
        private double VirusMoney { get; set; }
        public double HP { get; set; }
        public bool IsAlive
        {
            get { if (this.HP > 0.0) { return true; } else { return false; } }
        }
        public Player(double health)
        {
            this.HP = health;
            this.HasPrinterBeenSmashed = false;
            this.AccountingVirusRunning = false;
        }
        public void DoAttack(Enemy enemy)
        {

        }
        private AttackType ChooseAttack()
        {
            // Print out the choices of attack
            Console.WriteLine("You have a few options to choose from:");
            Console.WriteLine("1. Play Tetris at Work (really hurts the boss's bottom line)");
            Console.WriteLine("2. Burn TPS Reports (wastes company money)");
            Console.WriteLine("3. Grab a beer at Chotchkie's with Joanna (heals the soul)");
            //If they have smashed the printer, they can wield a scrap of printer
            if (this.HasPrinterBeenSmashed)
            {
                Console.WriteLine("4. Wield a scrap of that damn printer (possible critical damage)");
            }
            else
            {
                Console.WriteLine("4. Bash that damn jammed up printer to peices (wastes a turn)");
            }
            // If they have initiated the fraction of a penny virus, add some change to the player's account
            if (this.AccountingVirusRunning)
            {
                Console.WriteLine("5. The accounting virus is already running, it stole $" + this.VirusMoney + " so far");
            }
            else
            {
                Console.WriteLine("5. Launch Michael Bolton's accounting virus onto the network (wastes a turn)");
            }

            // Ask for the user choice
            Console.Write("Which option do you want to perform: ");
            switch (Console.ReadLine())
            {
                case "1": return AttackType.Tetris;
                case "2": return AttackType.BurnTPS;
                case "3": return AttackType.Heal;
                case "4": return AttackType.SmashPrinter;
                case "5": return AttackType.AccountingVirus;
                    // Return 0 if the input was invalid
                default: return 0;
            }
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

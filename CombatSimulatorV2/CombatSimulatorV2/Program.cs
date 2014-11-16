﻿using System;
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
        private Random rng = new Random();
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
            double tempPlayerDamage = 0.0;
            switch (ChooseAttack())
            {
                case AttackType.Tetris:
                    if (rng.NextDouble() >= 0.3) { tempPlayerDamage = rng.Next(20, 36); enemy.HP -= tempPlayerDamage; }
                    if (tempPlayerDamage > 0.0) { Console.WriteLine("Playing Tetris hit {0} for ${1} of wasted company money", enemy.Name, tempPlayerDamage); }
                    else
                    {
                        Console.WriteLine("Somehow playing Tetris didn't waste any of {0}'s money today.", enemy.Name);
                    }
                    break;
                case AttackType.BurnTPS:
                    tempPlayerDamage = rng.Next(10, 16);
                    Console.WriteLine("Burning TPS Reports hit {0} for ${1} of wasted company money", enemy.Name, tempPlayerDamage);
                    break;
                case AttackType.Heal:
                    this.HP += rng.Next(10, 21);
                    Console.WriteLine("Spending time with Joanna has helped you heal your wounds from your miserable cubicle life.");
                    break;
                case AttackType.SmashPrinter:
                    if (this.HasPrinterBeenSmashed) { if (rng.NextDouble() >= 0.5) { tempPlayerDamage = rng.Next(20, 36) * (rng.NextDouble() + 1); } }
                    else
                    {
                        // The attack missed
                        Console.WriteLine("That damn printer failed you on a daily basis and how it's busted up part failed you as well.");
                    }
                    if (!this.HasPrinterBeenSmashed) 
                    {
                        // On first attack, smash the printer so the broken part can be used the next time
                        Console.WriteLine("You really showed that jammed up printer who's boss."); this.HasPrinterBeenSmashed = true;
                    }
                    else
                    {
                        // The attack hit, print the result
                        Console.WriteLine("That busted printer part hit {0} for ${1} of wasted company money", enemy.Name, tempPlayerDamage);
                    }
                    break;
                case AttackType.AccountingVirus:
                    if (!this.AccountingVirusRunning)
                    {
                        // Start the virus
                        this.AccountingVirusRunning = true;
                    } // Print this line if has been running already or it just started running
                    Console.WriteLine("The accounting virus is on the loose, hopefully it only steals a few fractions of a penny at a time.");
                    break;
                default:
                    Console.WriteLine("Invalid attack choice");
                    break;
            }

            // Check if the virus is running and have it run every turn
            if (this.AccountingVirusRunning) { VirusRunner(); }
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
        private void VirusRunner()
        {
            // Steal money from the company, a fraction of a penny at a time, which happens to be surprisingly more then expected
            double virusTemp = this.VirusMoney * (rng.NextDouble() + 2.1);
            this.VirusMoney = virusTemp;
            // Give the play the extra stolen money
            this.HP += virusTemp;

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

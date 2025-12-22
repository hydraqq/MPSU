using System;

namespace HealthMonitorGame
{
    public class HealthMonitor
    {
        private const int MaxHP = 100;
        private const int MinHP = 0;
        private const int CriticalThreshold = 20;
        private const int MinDamage = 5;
        private const int MaxDamage = 30;
        private const int MinHeal = 3;
        private const int MaxHeal = 20;
        private const int MinSteps = 10;
        private const int MaxSteps = 14;

        public delegate void HealthEventHandler(
            HealthMonitor sender,
            int hp
        );

        public event HealthEventHandler? HealthChanged;

        private EventHandler<int>? criticalHandlers;
        private EventHandler<int>? deathHandlers;

        public event EventHandler<int> CriticalStateReached
        {
            add
            {
                criticalHandlers += value;
                Console.WriteLine(
                    "[CriticalStateReached] Подписчик добавлен"
                );
            }
            remove
            {
                criticalHandlers -= value;
                Console.WriteLine(
                    "[CriticalStateReached] Подписчик удалён"
                );
            }
        }

        public event EventHandler<int> DeathOccurred
        {
            add
            {
                deathHandlers += value;
                Console.WriteLine("[DeathOccurred] Подписчик добавлен");
            }
            remove
            {
                deathHandlers -= value;
                Console.WriteLine("[DeathOccurred] Подписчик удалён");
            }
        }

        private int currentHP;
        private Random random;

        public HealthMonitor()
        {
            random = new Random();
            currentHP = MaxHP;
        }

        public void Start()
        {
            currentHP = MaxHP;
            Console.WriteLine(
                $"Игра начинается. Стартовое здоровье: {currentHP} HP"
            );
            Console.WriteLine();

            int steps = random.Next(MinSteps, MaxSteps + 1);

            for (int i = 0; i < steps; i++)
            {
                if (currentHP == MinHP)
                {
                    break;
                }

                bool takeDamage = random.Next(0, 2) == 0;
                int change;

                if (takeDamage)
                {
                    change = random.Next(MinDamage, MaxDamage + 1);
                    int newHP = currentHP - change;
                    if (newHP < MinHP)
                    {
                        newHP = MinHP;
                    }
                    Console.Write($"Получен урон: {change}. ");
                    currentHP = newHP;
                }
                else
                {
                    change = random.Next(MinHeal, MaxHeal + 1);
                    int newHP = currentHP + change;
                    if (newHP > MaxHP)
                    {
                        newHP = MaxHP;
                    }
                    Console.Write($"Лечение: +{change}. ");
                    currentHP = newHP;
                }

                HealthChanged?.Invoke(this, currentHP);

                if (currentHP == MinHP)
                {
                    deathHandlers?.Invoke(this, currentHP);
                    break;
                }
                else if (currentHP < CriticalThreshold)
                {
                    criticalHandlers?.Invoke(this, currentHP);
                }

                Console.WriteLine();
            }

            if (currentHP > MinHP)
            {
                Console.WriteLine();
                Console.WriteLine("Симуляция завершена.");
            }
        }
    }

    public class ConsoleHUD
    {
        public void OnHealthChanged(HealthMonitor sender, int hp)
        {
            Console.Write($"HP: {hp}");
        }

        public void OnCriticalState(object? sender, int hp)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"ВНИМАНИЕ! Критический уровень HP: {hp} " +
                "— используйте зелье!"
            );
        }

        public void OnDeath(object? sender, int hp)
        {
            Console.WriteLine();
            Console.WriteLine($"Игрок пал. HP: {hp}");
        }
    }

    public class SurvivalStats
    {
        private const int CriticalMin = 1;
        private const int CriticalMax = 19;

        private int criticalCount;

        public SurvivalStats()
        {
            criticalCount = 0;
        }

        public void OnHealthChanged(HealthMonitor sender, int hp)
        {
            if (hp >= CriticalMin && hp <= CriticalMax)
            {
                criticalCount++;
            }
        }

        public void Report()
        {
            Console.WriteLine();
            Console.WriteLine("=== СТАТИСТИКА ===");
            Console.WriteLine(
                $"Критических состояний ({CriticalMin}..{CriticalMax} " +
                $"HP): {criticalCount}"
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== МОНИТОР ЗДОРОВЬЯ ИГРОКА ===");
            Console.WriteLine();

            HealthMonitor monitor = new HealthMonitor();
            ConsoleHUD hud = new ConsoleHUD();
            SurvivalStats stats = new SurvivalStats();

            Console.WriteLine("Подписка обработчиков...");
            monitor.HealthChanged += hud.OnHealthChanged;
            monitor.HealthChanged += stats.OnHealthChanged;
            monitor.CriticalStateReached += hud.OnCriticalState;
            monitor.DeathOccurred += hud.OnDeath;
            Console.WriteLine();

            monitor.Start();

            Console.WriteLine();
            Console.WriteLine("Отписка ConsoleHUD от CriticalStateReached...");
            monitor.CriticalStateReached -= hud.OnCriticalState;
            Console.WriteLine();

            stats.Report();

            Console.WriteLine();
            Console.WriteLine("=== ПОВТОРНАЯ СИМУЛЯЦИЯ ===");
            Console.WriteLine();

            HealthMonitor monitor2 = new HealthMonitor();
            ConsoleHUD hud2 = new ConsoleHUD();
            SurvivalStats stats2 = new SurvivalStats();

            monitor2.HealthChanged += hud2.OnHealthChanged;
            monitor2.HealthChanged += stats2.OnHealthChanged;
            monitor2.CriticalStateReached += hud2.OnCriticalState;
            monitor2.DeathOccurred += hud2.OnDeath;

            monitor2.Start();

            stats2.Report();

            Console.ReadKey();
        }
    }
}
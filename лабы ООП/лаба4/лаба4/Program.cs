using System;

namespace GameCharactersAttack
{
    public class Character
    {
        private string name;
        private int level;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым!");
                }
                name = value;
            }
        }

        public int Level
        {
            get { return level; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Уровень должен быть >= 1!");
                }
                level = value;
            }
        }

        public Character(string name, int level)
        {
            Name = name;
            Level = level;
        }

        public void LevelUp()
        {
            Level++;
            Console.WriteLine($"{Name} повысил уровень! Теперь уровень: {Level}");
        }

        public string Stats()
        {
            return $"Персонаж: {Name}, Уровень: {Level}";
        }

        public virtual int Attack()
        {
            return Level;
        }
    }

    public class Warrior : Character
    {
        private int strength;

        public int Strength
        {
            get { return strength; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Сила не может быть отрицательной!");
                }
                strength = value;
            }
        }

        public Warrior(string name, int level, int strength) : base(name, level)
        {
            Strength = strength;
        }

        public void Train(int s)
        {
            Strength += s;
            Console.WriteLine($"{Name} тренируется! Сила увеличена на {s}. Текущая сила: {Strength}");
        }

        public override int Attack()
        {
            int damage = Level + Strength;
            Console.WriteLine($"{Name} (Воин) атакует с уроном: {damage}");
            return damage;
        }

        public override string ToString()
        {
            return $"Воин: {Name}, Уровень: {Level}, Сила: {Strength}";
        }
    }

    public class Novice : Character
    {
        private int luck;

        public int Luck
        {
            get { return luck; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Удача должна быть в диапазоне 0-100!");
                }
                luck = value;
            }
        }

        public Novice(string name, int level, int luck) : base(name, level)
        {
            Luck = luck;
        }

        public void Pray()
        {
            Random random = new Random();
            int increase = random.Next(1, 6);

            if (Luck + increase > 100)
            {
                Luck = 100;
            }
            else
            {
                Luck += increase;
            }

            Console.WriteLine($"{Name} молится! Удача увеличена на {increase}. Текущая удача: {Luck}");
        }

        public override string ToString()
        {
            return $"Новичок: {Name}, Уровень: {Level}, Удача: {Luck}";
        }
    }

    public class Rogue : Novice
    {
        private int agility;

        public int Agility
        {
            get { return agility; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ловкость не может быть отрицательной!");
                }
                agility = value;
            }
        }

        public Rogue(string name, int level, int luck, int agility) : base(name, level, luck)
        {
            Agility = agility;
        }

        public void Dodge()
        {
            Console.WriteLine($"{Name} уклоняется от атаки с ловкостью {Agility}!");
        }

        public override int Attack()
        {
            int damage = Level + Luck / 10 + Agility / 2;
            Console.WriteLine($"{Name} (Разбойник) атакует с уроном: {damage}");
            return damage;
        }

        public override string ToString()
        {
            return $"Разбойник: {Name}, Уровень: {Level}, Удача: {Luck}, Ловкость: {Agility}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== СОЗДАНИЕ ПЕРСОНАЖЕЙ ===\n");

            Character baseChar = new Character("Базовый герой", 5);
            Warrior warrior = new Warrior("Конан", 10, 25);
            Novice novice = new Novice("Ученик", 3, 50);
            Rogue rogue = new Rogue("Тень", 8, 60, 40);

            Console.WriteLine("=== СТАТИСТИКА ПЕРСОНАЖЕЙ ===\n");
            Console.WriteLine(baseChar.Stats());
            Console.WriteLine(warrior);
            Console.WriteLine(novice);
            Console.WriteLine(rogue);

            Console.WriteLine("\n=== АТАКИ ===\n");

            int damage1 = baseChar.Attack();
            Console.WriteLine($"Урон: {damage1}\n");

            int damage2 = warrior.Attack();
            Console.WriteLine();

            int damage3 = novice.Attack();
            Console.WriteLine($"Урон: {damage3}\n");

            int damage4 = rogue.Attack();
            Console.WriteLine();

            Console.WriteLine("=== ТРЕНИРОВКА И УЛУЧШЕНИЯ ===\n");

            warrior.Train(10);
            warrior.Attack();
            Console.WriteLine();

            novice.Pray();
            novice.Attack();
            Console.WriteLine($"Урон: {novice.Attack()}\n");

            rogue.Pray();
            rogue.Dodge();
            rogue.Attack();
            Console.WriteLine();

            Console.WriteLine("=== ПОВЫШЕНИЕ УРОВНЯ ===\n");

            baseChar.LevelUp();
            warrior.LevelUp();
            novice.LevelUp();
            rogue.LevelUp();

            Console.WriteLine("\n=== АТАКИ ПОСЛЕ ПОВЫШЕНИЯ УРОВНЯ ===\n");

            baseChar.Attack();
            Console.WriteLine($"Урон: {baseChar.Attack()}\n");

            warrior.Attack();
            Console.WriteLine();

            novice.Attack();
            Console.WriteLine($"Урон: {novice.Attack()}\n");

            rogue.Attack();

            Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ПОЛИМОРФИЗМА ===\n");

            Character[] party = { baseChar, warrior, novice, rogue };

            Console.WriteLine("Групповая атака:");
            foreach (var character in party)
            {
                int dmg = character.Attack();
                Console.WriteLine($"Нанесён урон: {dmg}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
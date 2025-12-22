using System;

namespace GameCharacters
{
    public class Character
    {
        public string CharacterName { get; set; }
        public int HealthPoints { get; set; }

        public Character(string characterName, int healthPoints)
        {
            CharacterName = characterName;
            HealthPoints = healthPoints;
        }

        public virtual string AttackDescription()
        {
            return $"{CharacterName} совершает атаку.";
        }

        public override string ToString()
        {
            return $"Персонаж: {CharacterName}, Здоровье: {HealthPoints} HP";
        }
    }

    public class Warrior : Character
    {
        public int Strength { get; set; }

        public Warrior(string characterName, int healthPoints, int strength)
            : base(characterName, healthPoints)
        {
            Strength = strength;
        }

        public override string AttackDescription()
        {
            return $"{CharacterName} атакует противника силой удара {Strength} единиц.";
        }

        public override string ToString()
        {
            return $"Воин: {CharacterName}, Здоровье: {HealthPoints} HP, Сила удара: {Strength}";
        }
    }

    public class Wizard : Character
    {
        public int MagicPower { get; set; }

        public Wizard(string characterName, int healthPoints, int magicPower)
            : base(characterName, healthPoints)
        {
            MagicPower = magicPower;
        }

        public override string AttackDescription()
        {
            return $"{CharacterName} накладывает заклинание с мощностью магии {MagicPower} единиц.";
        }

        public override string ToString()
        {
            return $"Волшебник: {CharacterName}, Здоровье: {HealthPoints} HP, Магическая мощь: {MagicPower}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== СОЗДАНИЕ ПЕРСОНАЖЕЙ ===\n");

            Warrior warrior1 = new Warrior("Конан", 150, 85);
            Warrior warrior2 = new Warrior("Торгрим", 180, 95);

            Wizard wizard1 = new Wizard("Гэндальф", 100, 120);
            Wizard wizard2 = new Wizard("Мерлин", 90, 140);

            Character[] characters = { warrior1, warrior2, wizard1, wizard2 };

            Console.WriteLine("--- Информация о персонажах ---");
            foreach (var character in characters)
            {
                Console.WriteLine(character);
            }

            Console.WriteLine("\n=== ОПИСАНИЕ АТАК ===\n");

            foreach (var character in characters)
            {
                Console.WriteLine(character.AttackDescription());
            }

            Console.WriteLine("\n=== БОЕВАЯ ДЕМОНСТРАЦИЯ ===\n");

            Console.WriteLine("--- Атаки воинов ---");
            Console.WriteLine(warrior1.AttackDescription());
            Console.WriteLine(warrior2.AttackDescription());

            Console.WriteLine("\n--- Атаки волшебников ---");
            Console.WriteLine(wizard1.AttackDescription());
            Console.WriteLine(wizard2.AttackDescription());

            Console.WriteLine("\n=== СОЗДАНИЕ ДОПОЛНИТЕЛЬНЫХ ПЕРСОНАЖЕЙ ===\n");

            Warrior berserker = new Warrior("Рагнар", 200, 110);
            Wizard necromancer = new Wizard("Некромант", 80, 160);

            Console.WriteLine(berserker);
            Console.WriteLine(berserker.AttackDescription());
            Console.WriteLine();
            Console.WriteLine(necromancer);
            Console.WriteLine(necromancer.AttackDescription());

            Console.ReadKey();
        }
    }
}
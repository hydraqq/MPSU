using System;
using System.Collections;
using System.Collections.Generic;

namespace QuestJournal
{
    public enum Difficulty
    {
        Trivial = 1,
        Easy = 2,
        Normal = 3,
        Hard = 4,
        Nightmare = 5
    }

    public class Objective
    {
        public string Code { get; }
        public string Description { get; }
        public int RequiredCount { get; }

        public Objective(string code, string description, int requiredCount)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException(
                    "Код цели не может быть пустым"
                );
            }

            if (requiredCount < 1)
            {
                throw new ArgumentException(
                    "Требуемое количество должно быть >= 1"
                );
            }

            Code = code;
            Description = description ?? string.Empty;
            RequiredCount = requiredCount;
        }

        public override string ToString()
        {
            return $"[{Code}] {Description} (x{RequiredCount})";
        }
    }

    public class Quest
    {
        private List<Objective> _objectives;

        public string Id { get; }
        public string Title { get; }
        public Difficulty Difficulty { get; }
        public IReadOnlyList<Objective> Objectives => _objectives;

        public Quest(string id, string title, Difficulty difficulty)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Id не может быть пустым");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(
                    "Название не может быть пустым"
                );
            }

            Id = id;
            Title = title;
            Difficulty = difficulty;
            _objectives = new List<Objective>();
        }

        public void AddObjective(Objective objective)
        {
            if (objective == null)
            {
                throw new ArgumentNullException(nameof(objective));
            }

            _objectives.Add(objective);
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} ({Difficulty}) - " +
                   $"Целей: {_objectives.Count}";
        }
    }

    public class QuestLog : IEnumerable<Quest>
    {
        private List<Quest> _quests;
        private Dictionary<string, Quest> _byId;

        public int Count => _quests.Count;

        public QuestLog()
        {
            _quests = new List<Quest>();
            _byId = new Dictionary<string, Quest>();
        }

        public Quest this[int index]
        {
            get
            {
                if (index < 0 || index >= _quests.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        "Индекс вне диапазона"
                    );
                }
                return _quests[index];
            }
        }

        public Quest this[string id]
        {
            get
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (!_byId.ContainsKey(id))
                {
                    throw new KeyNotFoundException(
                        $"Квест с Id '{id}' не найден"
                    );
                }

                return _byId[id];
            }
        }

        public void Add(Quest quest)
        {
            if (quest == null)
            {
                throw new ArgumentNullException(nameof(quest));
            }

            if (_byId.ContainsKey(quest.Id))
            {
                throw new ArgumentException(
                    $"Квест с Id '{quest.Id}' уже существует"
                );
            }

            _quests.Add(quest);
            _byId[quest.Id] = quest;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _quests.Count)
            {
                return false;
            }

            Quest quest = _quests[index];
            _quests.RemoveAt(index);
            _byId.Remove(quest.Id);

            return true;
        }

        public bool RemoveById(string id)
        {
            if (id == null || !_byId.ContainsKey(id))
            {
                return false;
            }

            Quest quest = _byId[id];
            _byId.Remove(id);
            _quests.Remove(quest);

            return true;
        }

        public IEnumerable<Quest> EnumerateByDifficulty(
            Difficulty minDifficulty
        )
        {
            foreach (Quest quest in _quests)
            {
                if (quest.Difficulty >= minDifficulty)
                {
                    yield return quest;
                }
            }
        }

        public IEnumerator<Quest> GetEnumerator()
        {
            return _quests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _quests.Clear();
            _byId.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЖУРНАЛ КВЕСТОВ ===");
            Console.WriteLine();

            QuestLog journal = new QuestLog();

            Quest q1 = new Quest("q001", "Охота на волков", Difficulty.Easy);
            q1.AddObjective(new Objective(
                "kill_wolves",
                "Убить 5 волков",
                5
            ));
            q1.AddObjective(new Objective(
                "return_pelts",
                "Вернуть шкуры старосте",
                1
            ));

            Quest q2 = new Quest(
                "q002",
                "Сбор трав",
                Difficulty.Trivial
            );
            q2.AddObjective(new Objective(
                "gather_herbs",
                "Собрать 10 лечебных трав",
                10
            ));

            Quest q3 = new Quest(
                "q003",
                "Логово дракона",
                Difficulty.Nightmare
            );
            q3.AddObjective(new Objective(
                "find_lair",
                "Найти логово дракона",
                1
            ));
            q3.AddObjective(new Objective(
                "defeat_dragon",
                "Победить дракона",
                1
            ));

            Quest q4 = new Quest(
                "q004",
                "Поиск артефакта",
                Difficulty.Hard
            );
            q4.AddObjective(new Objective(
                "explore_ruins",
                "Исследовать руины",
                1
            ));

            journal.Add(q1);
            journal.Add(q2);
            journal.Add(q3);
            journal.Add(q4);

            Console.WriteLine($"Всего квестов: {journal.Count}");
            Console.WriteLine();

            Console.WriteLine("--- Доступ по индексу ---");
            Console.WriteLine($"[0]: {journal[0]}");
            Console.WriteLine($"[2]: {journal[2]}");
            Console.WriteLine();

            Console.WriteLine("--- Доступ по Id ---");
            Console.WriteLine($"q001: {journal["q001"]}");
            Console.WriteLine($"q003: {journal["q003"]}");
            Console.WriteLine();

            Console.WriteLine("--- Цели квеста q001 ---");
            Quest targetQuest = journal["q001"];
            foreach (Objective obj in targetQuest.Objectives)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();

            Console.WriteLine("--- Квесты сложности >= Normal ---");
            foreach (Quest q in journal.EnumerateByDifficulty(
                Difficulty.Normal
            ))
            {
                Console.WriteLine(q);
            }
            Console.WriteLine();

            Console.WriteLine("--- Удаление квеста по индексу 1 ---");
            bool removed = journal.RemoveAt(1);
            Console.WriteLine($"Удалено: {removed}");
            Console.WriteLine($"Квестов осталось: {journal.Count}");
            Console.WriteLine();

            Console.WriteLine("--- Удаление квеста q004 ---");
            removed = journal.RemoveById("q004");
            Console.WriteLine($"Удалено: {removed}");
            Console.WriteLine($"Квестов осталось: {journal.Count}");
            Console.WriteLine();

            Console.WriteLine("--- Все оставшиеся квесты (foreach) ---");
            foreach (Quest q in journal)
            {
                Console.WriteLine(q);
            }
            Console.WriteLine();

            Console.WriteLine("--- Тест исключений ---");
            try
            {
                Quest duplicate = new Quest(
                    "q001",
                    "Дубликат",
                    Difficulty.Easy
                );
                journal.Add(duplicate);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка дубликата: {ex.Message}");
            }

            try
            {
                Quest notFound = journal["q999"];
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Ошибка поиска: {ex.Message}");
            }

            try
            {
                Quest outOfRange = journal[100];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка индекса: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
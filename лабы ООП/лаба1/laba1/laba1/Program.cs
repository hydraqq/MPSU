namespace PhoneApp
{
    public class Phone
    {
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public int BatteryLevel { get; private set; }

        public Phone(string brand, string serialNumber, int batteryLevel)
        {
            Brand = brand;
            SerialNumber = serialNumber;
            BatteryLevel = batteryLevel < 0 ? 0 : (batteryLevel > 100 ? 100 : batteryLevel);
        }

        public void ChargeBattery(int amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Нельзя зарядить на отрицательное значение!");
                return;
            }

            BatteryLevel += amount;

            if (BatteryLevel > 100)
            {
                BatteryLevel = 100;
                Console.WriteLine("Батарея заряжена до максимума (100%)");
            }
            else
            {
                Console.WriteLine($"Батарея заряжена. Текущий уровень: {BatteryLevel}%");
            }
        }

        public override string ToString()
        {
            return $"Телефон {Brand}, серийный номер: {SerialNumber}, заряд батареи: {BatteryLevel}%";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Phone myPhone = new Phone("Samsung", "SN123456789", 25);

            Console.WriteLine("=== Начальное состояние ===");
            Console.WriteLine(myPhone);
            Console.WriteLine();

            Console.WriteLine("=== Зарядка на 30% ===");
            myPhone.ChargeBattery(30);
            Console.WriteLine(myPhone);
            Console.WriteLine();

            Console.WriteLine("=== Попытка зарядить на 60% (превышение лимита) ===");
            myPhone.ChargeBattery(60);
            Console.WriteLine(myPhone);
            Console.WriteLine();

            Phone anotherPhone = new Phone("iPhone", "SN987654321", 80);
            Console.WriteLine("=== Второй телефон ===");
            Console.WriteLine(anotherPhone);

            Console.ReadKey();
        }
    }
}
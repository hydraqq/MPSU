using System;

namespace ShippingCostCalculator
{

    public class ShippingContext
    {
        public bool IsFragile { get; set; }
        public bool IsExpress { get; set; }
        public bool IsInternational { get; set; }

        public ShippingContext(bool isFragile = false, bool isExpress = false, bool isInternational = false)
        {
            IsFragile = isFragile;
            IsExpress = isExpress;
            IsInternational = isInternational;
        }

        public override string ToString()
        {
            return $"Хрупкое: {IsFragile}, Срочное: {IsExpress}, Международное: {IsInternational}";
        }
    }

    public enum PackageType
    {
        Letter,      
        Parcel,      
        Oversize,    
        FragileBox,  
        Tube         
    }

    public class ShippingCostCalculatorEnum
    {
        public int CalculateCost(PackageType packageType, ShippingContext context)
        {
            int baseCost = 0;
            bool applyModifiers = true;

            switch (packageType)
            {
                case PackageType.Letter:
                    baseCost = 2;
                    break;
                case PackageType.Parcel:
                    baseCost = 5;
                    break;
                case PackageType.Oversize:
                    baseCost = 10;
                    break;
                case PackageType.FragileBox:
                    baseCost = 8;
                    break;
                case PackageType.Tube:
                    baseCost = 4;
                    applyModifiers = false;
                    break;
                default:
                    throw new ArgumentException("Неизвестный тип посылки");
            }

            if (!applyModifiers)
            {
                return baseCost;
            }

            int totalCost = baseCost;

            if (context.IsFragile && packageType != PackageType.FragileBox)
            {
                totalCost += 2;
            }

            if (context.IsInternational)
            {
                totalCost += 4;
            }

            if (context.IsExpress)
            {
                totalCost = (int)Math.Ceiling(totalCost * 1.5);
            }

            return totalCost;
        }
    }

    public abstract class Package
    {
        private int baseCost;

        public int BaseCost
        {
            get { return baseCost; }
            protected set { baseCost = value; }
        }

        protected Package(int baseCost)
        {
            this.baseCost = baseCost;
        }

        public virtual int GetCost(ShippingContext context)
        {
            int totalCost = BaseCost;

            if (context.IsFragile)
            {
                totalCost += 2;
            }

            if (context.IsInternational)
            {
                totalCost += 4;
            }

            if (context.IsExpress)
            {
                totalCost = (int)Math.Ceiling(totalCost * 1.5);
            }

            return totalCost;
        }

        public abstract string GetPackageType();
    }

    public class Letter : Package
    {
        public Letter() : base(2) { }

        public override string GetPackageType()
        {
            return "Письмо";
        }
    }

    public class Parcel : Package
    {
        public Parcel() : base(5) { }

        public override string GetPackageType()
        {
            return "Посылка";
        }
    }

    public class Oversize : Package
    {
        public Oversize() : base(10) { }

        public override string GetPackageType()
        {
            return "Крупногабарит";
        }
    }

    public class FragileBox : Package
    {
        public FragileBox() : base(8) { }

        public override int GetCost(ShippingContext context)
        {
            int totalCost = BaseCost;

            if (context.IsInternational)
            {
                totalCost += 4;
            }

            if (context.IsExpress)
            {
                totalCost = (int)Math.Ceiling(totalCost * 1.5);
            }

            return totalCost;
        }

        public override string GetPackageType()
        {
            return "Хрупкий ящик";
        }
    }

    public class Tube : Package
    {
        public Tube() : base(4) { }

        public override int GetCost(ShippingContext context)
        {
            return BaseCost;
        }

        public override string GetPackageType()
        {
            return "Тубус";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== КАЛЬКУЛЯТОР СТОИМОСТИ ДОСТАВКИ ===\n");

            Console.WriteLine(">>> ЧАСТЬ A: Реализация через enum + switch <<<\n");

            ShippingCostCalculatorEnum calculatorEnum = new ShippingCostCalculatorEnum();

            Console.WriteLine("--- Контрольные примеры (enum) ---");

            ShippingContext ctx1 = new ShippingContext();
            int cost1 = calculatorEnum.CalculateCost(PackageType.Letter, ctx1);
            Console.WriteLine($"Letter, без модификаторов: {cost1} (ожидается: 2)");

            ShippingContext ctx2 = new ShippingContext(isInternational: true);
            int cost2 = calculatorEnum.CalculateCost(PackageType.Parcel, ctx2);
            Console.WriteLine($"Parcel, Международная: {cost2} (ожидается: 9)");

            ShippingContext ctx3 = new ShippingContext(isExpress: true);
            int cost3 = calculatorEnum.CalculateCost(PackageType.Oversize, ctx3);
            Console.WriteLine($"Oversize, Срочная: {cost3} (ожидается: 15)");

            ShippingContext ctx4 = new ShippingContext(isFragile: true);
            int cost4 = calculatorEnum.CalculateCost(PackageType.Letter, ctx4);
            Console.WriteLine($"Letter, Хрупкое: {cost4} (ожидается: 4)");

            ShippingContext ctx5 = new ShippingContext(isFragile: true);
            int cost5 = calculatorEnum.CalculateCost(PackageType.FragileBox, ctx5);
            Console.WriteLine($"FragileBox, Хрупкое: {cost5} (ожидается: 8)");

            ShippingContext ctx6 = new ShippingContext(true, true, true);
            int cost6 = calculatorEnum.CalculateCost(PackageType.Tube, ctx6);
            Console.WriteLine($"Tube, все модификаторы: {cost6} (ожидается: 4)");

            Console.WriteLine("\n>>> ЧАСТЬ B: Реализация через иерархию классов <<<\n");

            Console.WriteLine("--- Контрольные примеры (классы) ---");

            Package letter = new Letter();
            int costB1 = letter.GetCost(ctx1);
            Console.WriteLine($"{letter.GetPackageType()}, без модификаторов: {costB1} (ожидается: 2)");

            Package parcel = new Parcel();
            int costB2 = parcel.GetCost(ctx2);
            Console.WriteLine($"{parcel.GetPackageType()}, Международная: {costB2} (ожидается: 9)");

            Package oversize = new Oversize();
            int costB3 = oversize.GetCost(ctx3);
            Console.WriteLine($"{oversize.GetPackageType()}, Срочная: {costB3} (ожидается: 15)");

            int costB4 = letter.GetCost(ctx4);
            Console.WriteLine($"{letter.GetPackageType()}, Хрупкое: {costB4} (ожидается: 4)");

            Package fragileBox = new FragileBox();
            int costB5 = fragileBox.GetCost(ctx5);
            Console.WriteLine($"{fragileBox.GetPackageType()}, Хрупкое: {costB5} (ожидается: 8)");

            Package tube = new Tube();
            int costB6 = tube.GetCost(ctx6);
            Console.WriteLine($"{tube.GetPackageType()}, все модификаторы: {costB6} (ожидается: 4)");

            Console.WriteLine("\n--- Дополнительные тесты ---");

            ShippingContext ctxAll = new ShippingContext(true, true, true);
            int costAll = calculatorEnum.CalculateCost(PackageType.Parcel, ctxAll);
            Console.WriteLine($"Parcel (все модификаторы): {costAll}");
            Console.WriteLine($"  Расчёт: 5 (база) + 2 (хрупкое) + 4 (международное) = 11, × 1.5 (срочное) = 16.5 → 17");

            int costFBAll = calculatorEnum.CalculateCost(PackageType.FragileBox, ctxAll);
            Console.WriteLine($"FragileBox (все модификаторы): {costFBAll}");
            Console.WriteLine($"  Расчёт: 8 (база) + 4 (международное) = 12, × 1.5 (срочное) = 18");

            Console.ReadKey();
        }
    }
}
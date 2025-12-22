using System;

namespace ImageFilterSystem
{
    public class ImageContext
    {
        private int brightness;
        private int contrast;

        public int Brightness
        {
            get { return brightness; }
            set { brightness = value; }
        }

        public int Contrast
        {
            get { return contrast; }
            set { contrast = value; }
        }

        public bool IsBlurred { get; set; }

        public ImageContext(int brightness, int contrast, bool isBlurred = false)
        {
            Brightness = brightness;
            Contrast = contrast;
            IsBlurred = isBlurred;
        }

        public override string ToString()
        {
            return $"Яркость: {Brightness}, Контрастность: {Contrast}, " +
                   $"Размытие: {(IsBlurred ? "Да" : "Нет")}";
        }
    }

    public delegate void FilterHandler(ImageContext context);

    public class ImageProcessor
    {
        public FilterHandler Filter { get; set; }

        public void Run(ImageContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (Filter == null)
            {
                Console.WriteLine("Нет подключенных фильтров");
                return;
            }

            Console.WriteLine("Применение фильтров...");
            Filter(context);
        }

        public void AddFilter(FilterHandler handler)
        {
            Filter += handler;
        }

        public void RemoveFilter(FilterHandler handler)
        {
            Filter -= handler;
        }

        public int GetFilterCount()
        {
            return Filter?.GetInvocationList().Length ?? 0;
        }
    }

    public static class ImageFilters
    {
        private const int BrightnessStep = 10;
        private const int ContrastStep = 5;

        public static void IncreaseBrightness(ImageContext context)
        {
            int old = context.Brightness;
            context.Brightness += BrightnessStep;
            Console.WriteLine($"[Яркость] {old} → {context.Brightness}");
        }

        public static void IncreaseContrast(ImageContext context)
        {
            int old = context.Contrast;
            context.Contrast += ContrastStep;
            Console.WriteLine($"[Контраст] {old} → {context.Contrast}");
        }

        public static void DecreaseBrightness(ImageContext context)
        {
            int old = context.Brightness;
            context.Brightness -= BrightnessStep;
            if (context.Brightness < 0)
            {
                context.Brightness = 0;
            }
            Console.WriteLine($"[Затемнение] {old} → {context.Brightness}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ImageProcessor processor = new ImageProcessor();
            ImageContext image = new ImageContext(50, 30, false);

            Console.WriteLine("Исходное изображение:");
            Console.WriteLine(image);
            Console.WriteLine();

            processor.Filter += ImageFilters.IncreaseBrightness;
            processor.Filter += ImageFilters.IncreaseContrast;

            processor.Filter += (ImageContext ctx) =>
            {
                if (!ctx.IsBlurred)
                {
                    ctx.IsBlurred = true;
                    Console.WriteLine("[Размытие] Применено");
                }
            };

            Console.WriteLine($"Подключено фильтров: {processor.GetFilterCount()}");
            Console.WriteLine();

            processor.Run(image);

            Console.WriteLine();
            Console.WriteLine("Результат:");
            Console.WriteLine(image);
            Console.WriteLine();

            Console.WriteLine("=== Удаление фильтра IncreaseBrightness ===");
            Console.WriteLine();

            image = new ImageContext(50, 30, false);
            processor.Filter -= ImageFilters.IncreaseBrightness;

            Console.WriteLine($"Подключено фильтров: {processor.GetFilterCount()}");
            Console.WriteLine();

            processor.Run(image);

            Console.WriteLine();
            Console.WriteLine("Результат после удаления фильтра:");
            Console.WriteLine(image);
            Console.WriteLine();

            Console.WriteLine("=== Тест с другими фильтрами ===");
            Console.WriteLine();

            ImageContext image2 = new ImageContext(70, 40, true);
            ImageProcessor processor2 = new ImageProcessor();

            processor2.AddFilter(ImageFilters.DecreaseBrightness);
            processor2.AddFilter((ctx) =>
            {
                ctx.IsBlurred = false;
                Console.WriteLine("[Убрать размытие]");
            });

            Console.WriteLine("Исходное:");
            Console.WriteLine(image2);
            Console.WriteLine();

            processor2.Run(image2);

            Console.WriteLine();
            Console.WriteLine("Результат:");
            Console.WriteLine(image2);

            Console.ReadKey();
        }
    }
}
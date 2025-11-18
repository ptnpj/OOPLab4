using System;

namespace Lab4_Structs
{
    struct Date
    {
        public int Day;
        public int Month;
        public int Year;

        public Date(int day, int month, int year)
        {
            try
            {
                if (day <= 0 || month <= 0 || year <= 0 || month > 12)
                {
                    throw new ArgumentException("Числа мають бути додатними, а місяць не більше 12.");
                }

                int daysInMonth = DateTime.DaysInMonth(year, month);
                if (day > daysInMonth)
                {
                    throw new ArgumentException($"У {month} місяці {year} року не може бути {day} днів.");
                }

                this.Day = day;
                this.Month = month;
                this.Year = year;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка ініціалізації: {ex.Message}. Встановлено дату за замовчуванням (01.01.2000).");
                this.Day = 1;
                this.Month = 1;
                this.Year = 2000;
            }
        }

        public void AddDays(int days)
        {
            DateTime dt = new DateTime(Year, Month, Day);
            dt = dt.AddDays(days);
            this.Day = dt.Day;
            this.Month = dt.Month;
            this.Year = dt.Year;
        }

        public void SubtractDays(int days)
        {
            DateTime dt = new DateTime(Year, Month, Day);
            dt = dt.AddDays(-days);
            this.Day = dt.Day;
            this.Month = dt.Month;
            this.Year = dt.Year;
        }


        public bool IsLeapYear()
        {
            return DateTime.IsLeapYear(this.Year);
        }

        public int DifferenceInDays(Date other)
        {
            DateTime dt1 = new DateTime(this.Year, this.Month, this.Day);
            DateTime dt2 = new DateTime(other.Year, other.Month, other.Day);
            return Math.Abs((dt1 - dt2).Days);
        }

        public bool IsGreaterThan(Date other)
        {
            DateTime dt1 = new DateTime(this.Year, this.Month, this.Day);
            DateTime dt2 = new DateTime(other.Year, other.Month, other.Day);
            return dt1 > dt2;
        }

        public string ToUSFormat()
        {
            return $"{Month} {Day}, {Year}";
        }

        public override string ToString()
        {
            return $"{Day:D2}.{Month:D2}.{Year}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                Console.Write("Введіть кількість дат для масиву: ");
                int n = int.Parse(Console.ReadLine());

                Date[] dates = ReadDatesArray(n);

                Console.WriteLine("\n--- Початковий масив ---");
                PrintArray(dates);

                SortDates(dates);
                Console.WriteLine("\n--- Відсортований масив (за зростанням) ---");
                PrintArray(dates);

                Console.WriteLine("\n--- Модифікація першого елементу (ref +1 рік) ---");
                ModifyDateRef(ref dates[0]);
                PrintDate(dates[0]);

                int minYear, maxYear;
                GetMinMaxYears(dates, out minYear, out maxYear);
                Console.WriteLine($"\n--- Статистика (out) ---");
                Console.WriteLine($"Мінімальний рік: {minYear}");
                Console.WriteLine($"Максимальний рік: {maxYear}");

                Console.WriteLine("\n--- Тест методів структури ---");
                Date testDate = dates[0];
                Console.WriteLine($"Поточна дата: {testDate}");
                Console.WriteLine($"Чи високосний рік? {testDate.IsLeapYear()}");

                testDate.AddDays(10);
                Console.WriteLine($"Дата через 10 днів: {testDate}");

                Console.WriteLine($"Американський формат: {testDate.ToUSFormat()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критична помилка у Main: {ex.Message}");
            }

            Console.ReadLine();
        }


        static Date[] ReadDatesArray(int n)
        {
            Date[] arr = new Date[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nВведіть дані для дати №{i + 1}:");
                Console.Write("День: ");
                int d = int.Parse(Console.ReadLine());
                Console.Write("Місяць: ");
                int m = int.Parse(Console.ReadLine());
                Console.Write("Рік: ");
                int y = int.Parse(Console.ReadLine());

                arr[i] = new Date(d, m, y);
            }
            return arr;
        }

        static void PrintDate(Date d)
        {
            Console.WriteLine($"Дата: {d.ToString()}");
        }

        static void PrintArray(Date[] dates)
        {
            foreach (var d in dates)
            {
                PrintDate(d);
            }
        }

        static void SortDates(Date[] dates)
        {
            for (int i = 0; i < dates.Length - 1; i++)
            {
                for (int j = 0; j < dates.Length - i - 1; j++)
                {
                    if (dates[j].IsGreaterThan(dates[j + 1]))
                    {
                        Date temp = dates[j];
                        dates[j] = dates[j + 1];
                        dates[j + 1] = temp;
                    }
                }
            }
        }

        static void ModifyDateRef(ref Date d)
        {
            d.Year += 1;
        }

        static void GetMinMaxYears(Date[] dates, out int min, out int max)
        {
            if (dates.Length == 0)
            {
                min = 0;
                max = 0;
                return;
            }

            min = dates[0].Year;
            max = dates[0].Year;

            foreach (var d in dates)
            {
                if (d.Year < min) min = d.Year;
                if (d.Year > max) max = d.Year;
            }
        }
    }
}
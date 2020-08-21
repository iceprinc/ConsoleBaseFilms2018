using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Задание_1
{
    struct DiscribeFilm
    {
        public string film;
        public string produser;
        public string year;
        public string type;
    }
    class Program
    {
        static int countcheck = 0;
        static DiscribeFilm Discribe;
        static DiscribeFilm[] Discribes = new DiscribeFilm[999999];
        static string line = "_______________________________________________________________________________________________________________________________________";
        static int count = 0;
        static int counttime = 0;
        static int max = 0;
        static string[] LogTime = new string[999999];
        static DateTime[] datka = new DateTime[999999];
        static TimeSpan[] bezdeistvie = new TimeSpan[999999];
        static double[] sravn = new double[999999];
        static void ShowTable()
        {
            Console.WriteLine(line);
            Console.WriteLine("\t\t\t\t\t\tКИНОПРОДУКЦИЯ");
            Console.WriteLine(line);
            Console.WriteLine("      Фильм\t\t-\tРежиссер\t\t-\tГод\t\t-\tТип");
            Console.WriteLine(line);
            for (count = 0; count < max; count++)
            {
                if (Discribes[count].film.Length <= 6)
                {
                    if (Discribes[count].produser.Length <= 6)
                    {
                        Console.WriteLine("№ " + (count + 1) + ": {0}\t\t-\t{1}\t\t\t-\t{2}\t\t-\t{3}", Discribes[count].film, Discribes[count].produser, Discribes[count].year, Discribes[count].type);
                        goto Proverka;
                    }
                    Console.WriteLine("№ " + (count + 1) + ": {0}\t\t-\t{1}\t\t\t-\t{2}\t\t-\t{3}", Discribes[count].film, Discribes[count].produser, Discribes[count].year, Discribes[count].type);
                    Proverka:
                    goto EndWrite;
                }
                if (Discribes[count].film.Length > 6)
                {
                    if (Discribes[count].produser.Length > 6)
                    {
                        Console.WriteLine("№ " + (count + 1) + ": {0}\t\t-\t{1}\t\t\t-\t{2}\t\t-\t{3}", Discribes[count].film, Discribes[count].produser, Discribes[count].year, Discribes[count].type);
                        goto Proverka2;
                    }
                    Console.WriteLine("№ " + (count + 1) + ": {0}\t\t-\t{1}\t\t\t-\t{2}\t\t-\t{3}", Discribes[count].film, Discribes[count].produser, Discribes[count].year, Discribes[count].type);
                    Proverka2:
                    goto EndWrite;
                }
                Console.WriteLine("№ " + (count + 1) + ": {0}\t-{1}\t\t-{2}\t\t-\t{3}", Discribes[count].film, Discribes[count].produser, Discribes[count].year, Discribes[count].type);
                EndWrite:
                Console.WriteLine(line);
            }
            Console.WriteLine("Перечисляемый тип:\nД - драма, К – комедия, М – мелодрама, Б – боевик, А – мультфильм");
            Console.WriteLine(line);
            Console.ReadLine();
        }
        public static void AddFilmInTable()
        {
            Add:
            if (count <= 999998)
            {
                Console.WriteLine("Введите название фильма?");
                Discribe.film = Console.ReadLine();
                Console.WriteLine("Введите имя режиссера?");
                Discribe.produser = Console.ReadLine();
                Console.WriteLine("Введите год выпуска фильма?");
                Discribe.year = Console.ReadLine();
                Console.WriteLine("Введите тип фильма?\n(Д - драма, К – комедия, М – мелодрама, Б – боевик, А – мультфильм)");
                Discribe.type = Console.ReadLine();
                Discribes[count] = Discribe;
                Console.Clear();
                string successaddfilm = "№" + (count + 1) + ": " + Discribes[count].film + ", " + Discribes[count].produser + ", " + Discribes[count].year + ", " + Discribes[count].type + " - фильм был добавлен.";
                string successaddfilminfile = Discribes[count].film + "," + Discribes[count].produser + "," + Discribes[count].year + "," + Discribes[count].type + "," + ";";
                FileStream file = new FileStream("lab.dat", FileMode.OpenOrCreate);
                byte[] writearray = Encoding.Default.GetBytes(successaddfilminfile);// строку в массив байтов
                file.Seek(file.Length, SeekOrigin.Begin);// указатель в конец, что бы не стереть начало
                file.Write(writearray, 0, writearray.Length);// массив байт в файл
                file.Close();
                SaveToLog(successaddfilm);
                Console.WriteLine(successaddfilm);
                count++;
                Error:
                Console.WriteLine("Хотите добавить еще один фильм в таблицу?\n1 - Добавить\n2 - Нет, достаточно");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        goto Add;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Возврат в Гл.Меню");
                        Console.ReadLine();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Error!\nВводить нужно число от 1 до 2");
                        Console.ReadLine();
                        goto Error;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Уже добавлено максимальное кол-во фильмов в таблицу (999999 шт.)");
                Console.ReadLine();
            }
            max = count;
        }
        static void DeleteFilmFromTable()
        {
            if (count == 0)
            {
                Console.WriteLine("Фильмов нет, удалять нечего");
                goto End;
            }
            Console.WriteLine("Какой фильм по счету вы хотите удалить?\nВ таблице записаны фильмы в колличестве: {0}", count);
            int del = int.Parse(Console.ReadLine());
            if (del <= count)
            {
                string successdel = "Фильм №" + del + " " + Discribes[del - 1].film + ", " + Discribes[del - 1].produser + ", " + Discribes[del - 1].year + ", " + Discribes[del - 1].type + "  - удален.";
                SaveToLog(successdel);
                Console.WriteLine(successdel);
                for (int i = del; i < count; i++)
                {
                    Discribes[i - 1] = Discribes[i];
                }
                max = count - 1;
                count -= 1;
                goto End;
            }
            if (del > count)
                Console.WriteLine("Ошибка! В таблице нет фильма с номером {0}, в таблице всего записей - {1}!", del, count);
            End:
            Console.ReadLine();
        }
        static void RefreshFilmInTable()
        {
            int refresh = 0;
            Console.WriteLine("Какой по счету фильм вы хотите обновить?");
            refresh = int.Parse(Console.ReadLine());
            if (refresh == 0)
            {
                Console.WriteLine("Нулевой записи не существует, нуммерация начинается с единицы!");
                goto End;
            }
            if (refresh <= count)
            {
                Console.WriteLine("Вводите новые данные:\n");
                DiscribeFilm Discribe;
                Console.WriteLine("Введите название фильма?");
                Discribe.film = Console.ReadLine();
                Console.WriteLine("Введите имя режиссера?");
                Discribe.produser = Console.ReadLine();
                Console.WriteLine("Введите год выпуска фильма?");
                Discribe.year = Console.ReadLine();
                Console.WriteLine("Введите тип фильма?\n(Д - драма, К – комедия, М – мелодрама, Б – боевик, А – мультфильм)");
                Discribe.type = Console.ReadLine();
                Discribes[refresh - 1] = Discribe;
                Console.Clear();
                string successrefresh = "Запись под номером №" + refresh + " успешно обновлена!\nНовая информация: " + Discribes[refresh - 1].film + ", " + Discribes[refresh - 1].produser + ", " + Discribes[refresh - 1].year + ", " + Discribes[refresh - 1].type;
                SaveToLog(successrefresh);
                Console.WriteLine(successrefresh);
            }
            else
                Console.WriteLine("Такой записи не существует, в таблице записаны фильмы в колличестве: {0}", count);
            End:
            Console.ReadLine();
        }
        static void SearchTypeFilm()
        {
            Console.WriteLine("Поиск по типу фильма, введите нужную букву для поиска\nД - драма, К – комедия, М – мелодрама, Б – боевик, А – мультфильм");
            string search = Console.ReadLine();
            Console.WriteLine(line);
            Console.WriteLine("\t\t\t\t\t\tКИНОПРОДУКЦИЯ");
            Console.WriteLine(line);
            Console.WriteLine("      Фильм\t\t-\tРежиссер\t\t-\tГод\t\t-\tТип");
            Console.WriteLine(line);
            for (count = 0; count < max; count++)
            {
                if (Discribes[count].type == search)
                {
                    Console.WriteLine("№ " + (count + 1) + ": {0}\t\t-\t{1}\t\t\t-\t{2}\t\t-\t{3}", Discribes[count].film, Discribes[count].produser, Discribes[count].year, Discribes[count].type);
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("Перечисляемый тип:\nД - драма, К – комедия, М – мелодрама, Б – боевик, А – мультфильм");
            Console.WriteLine(line);
            string strsearch = "Был произведен поиск по букве: " + search;
            SaveToLog(strsearch);
            Console.WriteLine(strsearch);
            Console.ReadLine();
        }
        static void SaveToLog(string message)
        {
            string timeinthismoment = Convert.ToString(DateTime.Now);
            datka[counttime] = DateTime.Now;
            LogTime[counttime] = (timeinthismoment + " - " + message);
            counttime++;
        }
        static void ShowLog()
        {
            Console.WriteLine("\nПоказаны последние 50 действий:\n");
            for (int i = 0; i < 51; i++)
            {
                if (counttime >= i)
                {
                    if (i != 0)
                    {
                        Console.Write("№ {0}: ", i);
                        Console.Write(LogTime[counttime - i]);
                        Console.WriteLine();
                    }
                }
            }
            int maxcounttime = counttime - 1;
            for (counttime = 0; counttime < maxcounttime; counttime++)//найти все простои
            {
                bezdeistvie[counttime] = datka[counttime].Subtract(datka[counttime + 1]);
                sravn[counttime] = bezdeistvie[counttime].TotalSeconds;
                sravn[counttime] = Math.Abs(sravn[counttime]);
                //Console.WriteLine(sravn[counttime]); ////если надо их отобразить
                //Console.ReadLine();
            }
            double max = sravn[0];
            for (int i = 0; i < counttime - 2; i++)//сравнение простоев
            {
                for (int j = 0; j < counttime - 2; j++)
                    if (sravn[i] > sravn[j])
                    {
                        max = sravn[i];
                    }
            }
            Console.Write("\n{0:0.##}", max);
            Console.Write(" , секунд - самый длинный период бездействия пользователя.\n");
            Console.WriteLine("\nДля возврата в Гл.Меню нажмите любую клавишу . . . ");
            Console.ReadLine();
        }
        static void LoadLabDat()
        {
            FileStream file = new FileStream(@"lab.dat", FileMode.Open);
            byte[] readarray = new byte[file.Length];
            file.Read(readarray, 0, readarray.Length);// считываем все байты от 0 до конца
            string textFromFile = Encoding.Default.GetString(readarray);// декодируем байты в строку
            string[] stroki = textFromFile.Split(';');
            for (int i = 0; i < stroki.Length; i++)
            {
                if (stroki.Length - 1 != i)
                {
                    string[] fraza = stroki[i].Split(',');
                    for (int j = 0; j < fraza.Length; j++)
                    {
                        if (j == 0)
                        {
                            Discribe.film = fraza[j];
                        }
                        if (j == 1)
                        {
                            Discribe.produser = fraza[j];
                        }
                        if (j == 2)
                        {
                            Discribe.year = fraza[j];
                        }
                        if (j == 3)
                        {
                            Discribe.type = fraza[j];
                        }
                    }
                    Discribes[count] = Discribe;
                    count++;
                }
            }
            max = count;
            file.Close();
        }
        static void SaveLabDat()
        {
            //File.Delete("lab.dat");
            FileStream file = new FileStream("lab.dat", FileMode.Create);
            for (int i = 0; i < count; i++)
            {
                string successaddfilminfile = Discribes[i].film + "," + Discribes[i].produser + "," + Discribes[i].year + "," + Discribes[i].type + "," + ";";
                byte[] writearray = Encoding.Default.GetBytes(successaddfilminfile);// строку в массив байтов
                file.Seek(file.Length, SeekOrigin.Begin);// указатель в конец, что бы не стереть начало
                file.Write(writearray, 0, writearray.Length);// массив байт в файл
            }
            file.Close();
        }
        static void Sort()
        {
            DiscribeFilm Discribe2;
            DiscribeFilm Discribe3;
            for (int i = 0 + 1; i < count; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    Discribe2 = Discribes[j - 1];
                    Discribe3 = Discribes[j];
                    if (Convert.ToInt32(Discribe2.year) > Convert.ToInt32(Discribe3.year))
                    {
                        Discribes[Discribes.Length - 1] = Discribe2;
                        Discribe2 = Discribe3;
                        Discribe3 = Discribes[Discribes.Length - 1];
                    }
                    Discribes[j - 1] = Discribe2;
                    Discribes[j] = Discribe3;
                }
            }
            Console.WriteLine("Таблица успешно отсортирована, для выхода в меню нажмите любую клавишу...");
            Console.ReadLine();
        }
        static int BinarySearch(int elem, int[] a, int N)
        {
            int l = 0, r = N - 1;
            while (r >= l)
            {
                countcheck++;
                int mid = (l + r) / 2;

                if (a[mid] == elem)
                    return mid;

                if (a[mid] > elem)
                    r = mid - 1;
                else
                    l = mid + 1;
            }
            return -1;
        }
        static int InterpolashionSearch(int elem, int[] a, int N)
        {
            int l = 0, r = N - 1;

            while (r >= l)
            {
                int mid = l + (r - l) * (elem - a[l]) / (a[r] - a[l]);
                countcheck++;
                if (mid < a.Length)//не работало если эл-т не сущ-ет
                {//не работало если эл-т не сущ-ет
                    if (elem < a[mid])
                        r = mid - 1;
                    else if (elem > a[mid])
                        l = mid + 1;
                    else return mid;
                }//не работало если эл-т не сущ-ет
                else return -1;//не работало если эл-т не сущ-ет
            }
            return -1;
        }
        static int LinearySearch(int elem, int[] a)
        {
            countcheck = 0;
            for (int i = 0; i < a.Length; i++)
            {
                countcheck++;
                if (a[i] == elem)
                {
                    return i;
                }
            }
            return -1;
        }
        static int[] computePrefixFunction(string s)
        {
            int m = s.Length;

            int[] pi = new int[m];
            int j = 0;
            pi[0] = 0;

            for (int i = 1; i < m; i++)
            {
                while (j > 0 && s[j] != s[i])
                {
                    j = pi[j];
                }
                if (s[j] == s[i])
                {
                    j++;
                }
                pi[i] = j;
            }
            return pi;
        }
        static int SearchKMP2(string pattern, string text)
        {
            int n = text.Length;
            int m = pattern.Length;

            int[] prefix = computePrefixFunction(pattern);

            int j = 0;

            for (int i = 1; i <= n; i++)
            {
                while (j > 0 && pattern[j] != text[i - 1])
                {
                    j = prefix[j - 1];
                }
                if (pattern[j] == text[i - 1])
                {
                    j++;
                }
                if (j == m)
                {
                    return i - m;	 // Найдено в позиции i - m 
                }
            }
            return -1;		 // Не найдено
        }
        static void SearchKMP1(string pat)
        {
            for (int i = 0; i < count; i++)
            {
                int index2 = -1;
                index2 = SearchKMP2(pat, Discribes[i].film);
                if (index2 == -1)
                    Console.WriteLine($"В {i + 1} строке: " + "По вашему запросу ничего не найдено.");
                else
                    Console.WriteLine($"В {i + 1} строке: " + "По вашему запросу было найдено:\t" + "Фильм: " + Discribes[i].film + "\tРежиссер: " + Discribes[i].produser + "\tГод: " + Discribes[i].year + "\tТип: " + Discribes[i].type);
            }
        }
        static void SearchBM1(string pat)
        {
            for (int i = 0; i < count; i++)
            {
                int index2 = -1;
                index2 = BM(Discribes[i].film, pat);
                if (index2 == -1)
                    Console.WriteLine($"В {i + 1} строке: " + "По вашему запросу ничего не найдено.");
                else
                    Console.WriteLine($"В {i + 1} строке: " + "По вашему запросу было найдено:\t" + "Фильм: " + Discribes[i].film + "\tРежиссер: " + Discribes[i].produser + "\tГод: " + Discribes[i].year + "\tТип: " + Discribes[i].type);
            }
        }
        public static int[] BadChararctersTable(string pattern)
        {
            int m = pattern.Length;
            int[] badShift = new int[256];
            for (int i = 0; i < 256; i++)
            {
                badShift[i] = -1;
            }//for 
            for (int i = 1; i < m - 1; i++)
            {
                badShift[(int)pattern[i]] = i;
            }//for 
            return badShift;
        }//BadChararctersTable 
        static int[] Suffixes(string pattern)
        {
            int m = pattern.Length;
            int[] suffixes = new int[m];
            suffixes[m - 1] = m;

            int g = m - 1, f = 0;

            for (int i = m - 2; i >= 0; --i)
            {
                if (i > g && suffixes[i + m - 1 - f] < i - g)
                {
                    suffixes[i] = suffixes[i + m - 1 - f];
                }
                else if (i < g)
                {
                    g = i;
                }
                f = i;

                while (g >= 0 && pattern[g] == pattern[g + m - 1 - f]) g--;

                suffixes[i] = f - g;
            }
            return suffixes;
        }
        public static int[] GoodSuffixTable(string pattern)
        {
            int m = pattern.Length;
            int[] suffixes = Suffixes(pattern);
            int[] goodSuffixes = new int[m];

            for (int i = 0; i < m; i++)
            {
                goodSuffixes[i] = m;
            }//for 
            for (int i = m - 1; i >= 0; i--)
            {
                if (suffixes[i] == i + 1)
                {
                    for (int j = 0; j < m - i - 1; j++)
                    {
                        if (goodSuffixes[j] == m)
                        {
                            goodSuffixes[j] = m - i - 1;
                        }//if 
                    }//for 
                }//if 
            }//for 
            for (int i = 0; i < m - 1; i++)
            {
                goodSuffixes[m - 1 - suffixes[i]] = m - i - 1;
            }//for 
            return goodSuffixes;
        }//GoodSuffixTable 
        public static int BM(string text, string key)
        {
            int n = text.Length;
            int m = key.Length;

            if (m > n)
            {
                return -1;
            }//if 

            int[] badShift = BadChararctersTable(key);
            int[] goodSuffix = GoodSuffixTable(key);

            int offset = 0;
            while (offset <= n - m)
            {
                int i;
                for (i = m - 1; i >= 0 && key[i] == text[i + offset]; i--) ;
                if (i < 0)
                {
                    return offset;
                }
                offset += Math.Max(i - badShift[text[offset + i]], goodSuffix[i]);
            }//for 
            return -1;
        }//BM 

        static void DoSearch()
        {
            int[] digitsyears = new int[count];
            for (int i = 0; i < count; i++)
            {
                digitsyears[i] = Convert.ToInt32(Discribes[i].year);
            }
            Choose:
            //линейный, бинарный, интерполяционный
            Console.WriteLine("Поиск\n\n1 – Линейный поиск\n2 – Бинарный поиск\n3 – Интерполяционный поиск\n4 - Поиск подстроки 'Алгоритм Кнута-Мориса-Пратта'\n5 - Поиск подстроки 'Алгоритм Бойера-Мура'\n6 - Вернуться назад");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    string dobinarysearch0 = "Вы выбрали Линейный поиск";
                    SaveToLog(dobinarysearch0);
                    Console.WriteLine(dobinarysearch0);
                    Console.WriteLine("\nВведите год который следует найти?");
                    int elem0 = Convert.ToInt32(Console.ReadLine());
                    DateTime start0 = new DateTime();
                    DateTime finish0 = new DateTime();
                    start0 = DateTime.Now;
                    int index0 = LinearySearch(elem0, digitsyears);
                    finish0 = DateTime.Now;
                    Console.WriteLine("Время работы алгоритма: " + Convert.ToInt32(Math.Abs(start0.Minute - finish0.Minute)) + ":" + Convert.ToInt32(Math.Abs(start0.Second - finish0.Second)) + ":" + Convert.ToInt32(Math.Abs(start0.Millisecond - finish0.Millisecond)) + "\t( минут ):( секунд ):( миллисекунд )");
                    Console.WriteLine("Колличество сравнений: " + countcheck);
                    if (index0 == -1)
                        Console.WriteLine("По вашему запросу ничего не найдено.");
                    else
                        Console.WriteLine("По вашему запросу был найден:\n" + "Фильм: " + Discribes[index0].film + "\tРежиссер: " + Discribes[index0].produser + "\tГод: " + Discribes[index0].year + "\tТип: " + Discribes[index0].type);

                    Console.ReadLine();
                    break;
                case "2":
                    Console.Clear();
                    string dobinarysearch = "Вы выбрали Бинарный поиск";
                    SaveToLog(dobinarysearch);
                    Console.WriteLine(dobinarysearch);
                    Console.WriteLine("\nВведите год который следует найти?");
                    int elem = Convert.ToInt32(Console.ReadLine());
                    DateTime start = new DateTime();
                    DateTime finish = new DateTime();
                    start = DateTime.Now;
                    int index = BinarySearch(elem, digitsyears, digitsyears.Length);
                    finish = DateTime.Now;
                    Console.WriteLine("Время работы алгоритма: " + Convert.ToInt32(Math.Abs(start.Minute - finish.Minute)) + ":" + Convert.ToInt32(Math.Abs(start.Second - finish.Second)) + ":" + Convert.ToInt32(Math.Abs(start.Millisecond - finish.Millisecond)) + "\t( минут ):( секунд ):( миллисекунд )");
                    Console.WriteLine("Колличество сравнений: " + countcheck);
                    if (index == -1)
                        Console.WriteLine("По вашему запросу ничего не найдено.");
                    else
                        Console.WriteLine("По вашему запросу был найден:\n" + "Фильм: " + Discribes[index].film + "\tРежиссер: " + Discribes[index].produser + "\tГод: " + Discribes[index].year + "\tТип: " + Discribes[index].type);
                    Console.ReadLine();
                    break;
                case "3":
                    Console.Clear();
                    string dobinarysearch2 = "Вы выбрали Интерполяционный поиск";
                    SaveToLog(dobinarysearch2);
                    Console.WriteLine(dobinarysearch2);
                    Console.WriteLine("\nВведите год который следует найти?");
                    int elem2 = Convert.ToInt32(Console.ReadLine());
                    DateTime start2 = new DateTime();
                    DateTime finish2 = new DateTime();
                    start2 = DateTime.Now;
                    int index2 = InterpolashionSearch(elem2, digitsyears, digitsyears.Length);
                    finish2 = DateTime.Now;
                    Console.WriteLine("Время работы алгоритма: " + Convert.ToInt32(Math.Abs(start2.Minute - finish2.Minute)) + ":" + Convert.ToInt32(Math.Abs(start2.Second - finish2.Second)) + ":" + Convert.ToInt32(Math.Abs(start2.Millisecond - finish2.Millisecond)) + "\t( минут ):( секунд ):( миллисекунд )");
                    Console.WriteLine("Колличество сравнений: " + countcheck);
                    if (index2 == -1)
                        Console.WriteLine("По вашему запросу ничего не найдено.");
                    else
                        Console.WriteLine("По вашему запросу был найден:\n" + "Фильм: " + Discribes[index2].film + "\tРежиссер: " + Discribes[index2].produser + "\tГод: " + Discribes[index2].year + "\tТип: " + Discribes[index2].type);
                    Console.ReadLine();
                    break;
                case "4":
                    Console.Clear();
                    string searchstring1 = "Вы выбрали Поиск подстроки 'Алгоритм Кнута-Мориса-Пратта'";
                    SaveToLog(searchstring1);
                    Console.WriteLine(searchstring1);
                    Console.WriteLine("\nВведите фильм который следует найти?");
                    string sString1 = Console.ReadLine();
                    DateTime sStart1 = new DateTime();
                    DateTime sFinish1 = new DateTime();
                    sStart1 = DateTime.Now;
                    SearchKMP1(sString1);
                    sFinish1 = DateTime.Now;
                    Console.WriteLine("Время работы алгоритма: " + Convert.ToInt32(Math.Abs(sStart1.Minute - sFinish1.Minute)) + ":" + Convert.ToInt32(Math.Abs(sStart1.Second - sFinish1.Second)) + ":" + Convert.ToInt32(Math.Abs(sStart1.Millisecond - sFinish1.Millisecond)) + "\t( минут ):( секунд ):( миллисекунд )");
                    Console.ReadLine();
                    break;
                case "5":
                    Console.Clear();
                    string searchstring2 = "Вы выбрали Поиск подстроки 'Алгоритм Бойера-Мура'";
                    SaveToLog(searchstring2);
                    Console.WriteLine(searchstring2);
                    Console.WriteLine("\nВведите фильм который следует найти?");
                    string sString2 = Console.ReadLine();
                    DateTime sStart2 = new DateTime();
                    DateTime sFinish2 = new DateTime();
                    sStart2 = DateTime.Now;
                    SearchBM1(sString2);
                    sFinish2 = DateTime.Now;
                    Console.WriteLine("Время работы алгоритма: " + Convert.ToInt32(Math.Abs(sStart2.Minute - sFinish2.Minute)) + ":" + Convert.ToInt32(Math.Abs(sStart2.Second - sFinish2.Second)) + ":" + Convert.ToInt32(Math.Abs(sStart2.Millisecond - sFinish2.Millisecond)) + "\t( минут ):( секунд ):( миллисекунд )");
                    Console.ReadLine();
                    break;
                case "6":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ошибка!\nВводить нужно число от 1 до 3 !");
                    Console.ReadLine();
                    goto Choose;
            }
        }
        class ListIntNode
        {
            public int data;
            public ListIntNode next;
        }
        public class SingleLinkedListInt
        {
            ListIntNode head = null;
            ListIntNode tail = null;

            public void Purge()
            {
                head = tail = null;
            }
            public void Append(int elem)        // то же самое (короче)
            {
                ListIntNode node = new ListIntNode();
                node.data = elem;
                node.next = null;

                if (head == null)
                    head = node;
                else
                    tail.next = node;

                tail = node;
            }
            public void Prepend(int elem)
            {
                ListIntNode node = new ListIntNode();
                node.data = elem;
                node.next = null;

                if (head == null)
                    head = node;
                else
                    node.next = head;

                head = node;
            }
            public void InsertAfter(int elem, int after)
            {
                ListIntNode node = head;

                while (node != null && node.data != after)
                    node = node.next;

                if (node == null)
                    return;

                ListIntNode insNode = new ListIntNode();
                insNode.data = elem;
                insNode.next = node.next;

                node.next = insNode;

                if (node == tail)
                    tail = insNode;
            }
            public void Remove(int elem)
            {
                ListIntNode node = head;
                ListIntNode prevnode = null;

                while (node != null && node.data != elem)
                {
                    prevnode = node;
                    node = node.next;
                }

                if (node == null)
                    return;

                if (node == head)
                    head = node.next;
                else
                    prevnode.next = node.next;
                if (node == tail)
                    tail = prevnode;
            }
            public int Count()
            {
                int n = 0;
                for (ListIntNode node = head; node != null; node = node.next, n++) ;

                return n;
            }

            public void Print()
            {
                ListIntNode node = head;

                while (node != null)
                {
                    Console.Write(node.data + " ");
                    node = node.next;
                }
            }
        }
        
        public static void DoubleList()
        {
            SingleLinkedListInt list = new SingleLinkedListInt();
            for (int i = 0; i < count; i++)
            {
                list.Append(Convert.ToInt32(Discribes[i].year));
            }
            list.Print();
            Console.ReadLine();
        }
        public static void ListCollection()
        {
            List<DiscribeFilm> lDiscribes = new List<DiscribeFilm>();
            lDiscribes.AddRange(Discribes);
            for (int i =0;i<count;i++)
            {
                Console.WriteLine($"№{i+1}: {lDiscribes[i].film}, {lDiscribes[i].produser}, {lDiscribes[i].year}, {lDiscribes[i].type}, ");
            }
            Console.ReadLine();
        }
        static void Main(string[] args)///////////////////////////////////////////////////////////// М Е Н Ю
        {
            LoadLabDat();
            Console.SetWindowSize(140, 30);
            string start = "Программа запущена";
            string start2 = "Для продолжения нажмите любую клавищу . . . ";
            Console.WriteLine(start);
            SaveToLog(start);
            Console.WriteLine(start2);
            Console.ReadLine();
            Choose:
            for (int i = 0; i < 1;)
            {
                Console.Clear();
                Console.WriteLine("Чугуй Александр Лабораторная работа №9\nЗадание 1\n\n1 – Просмотр таблицы\n2 – Добавить фильм\n3 – Удалить фильм\n4 – Обновить фильм\n5 – Поиск фильма по типу\n6 – Просмотреть лог\n7 - Отсортировать таблицу по годам\n8 - Выполнить поиск\n9 - Выход\n10 - Показать данные, используя двусвязный список\n11 - Показать данные, используя коллекция .NET List");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        string showtable = "1 – Просмотр таблицы";
                        SaveToLog(showtable);
                        Console.WriteLine(showtable);
                        ShowTable();
                        goto Choose;
                    case "2":
                        Console.Clear();
                        string addfilm = "2 – Добавить фильм";
                        SaveToLog(addfilm);
                        Console.WriteLine(addfilm);
                        AddFilmInTable();
                        goto Choose;
                    case "3":
                        Console.Clear();
                        string delfilm = "3 – Удалить фильм";
                        SaveToLog(delfilm);
                        Console.WriteLine(delfilm);
                        DeleteFilmFromTable();
                        goto Choose;
                    case "4":
                        Console.Clear();
                        string refreshfilm = "4 – Обновить фильм";
                        SaveToLog(refreshfilm);
                        Console.WriteLine(refreshfilm);
                        RefreshFilmInTable();
                        goto Choose;
                    case "5":
                        Console.Clear();
                        string searchfilm = "5 – Поиск фильма по типу";
                        SaveToLog(searchfilm);
                        Console.WriteLine(searchfilm);
                        SearchTypeFilm();
                        goto Choose;
                    case "6":
                        Console.Clear();
                        string watchlog = "6 – Просмотреть лог";
                        SaveToLog(watchlog);
                        Console.WriteLine(watchlog);
                        ShowLog();
                        goto Choose;
                    case "7":
                        Console.Clear();
                        string sortirovka = "7 - Отсортировать таблицу по годам";
                        SaveToLog(sortirovka);
                        Console.WriteLine(sortirovka);
                        Sort();
                        goto Choose;
                    case "8":
                        Console.Clear();
                        string dosearch = "8 - Выполнить поиск";
                        SaveToLog(dosearch);
                        Console.WriteLine(dosearch);
                        DoSearch();
                        goto Choose;
                    case "9":
                        Console.Clear();
                        Console.WriteLine("9 - Выход");
                        goto Exit;
                    case "10":
                        Console.Clear();
                        string doshowinformation = "10 - Показать данные, используя двусвязный список";
                        SaveToLog(doshowinformation);
                        Console.WriteLine(doshowinformation);
                        DoubleList();
                        goto Choose;
                    case "11":
                        Console.Clear();
                        string doshowinformationlist = "11 - Показать данные, используя коллекция .NET List";
                        SaveToLog(doshowinformationlist);
                        Console.WriteLine(doshowinformationlist);
                        ListCollection();
                        goto Choose;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ошибка!\nВводить нужно число от 1 до 9 !");
                        Console.ReadLine();
                        goto Choose;
                }
            }
            Exit:
            Console.WriteLine("Нажмите любую клавишу . . . ");
            Console.ReadLine();
            SaveLabDat();
        }
    }
}

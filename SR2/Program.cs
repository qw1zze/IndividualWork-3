using EBookLib;

class Program
{
    private static Random rand = new Random();

    /// <summary>
    /// Event handler of print
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void PrintHandler(object sender, EventArgs e)
    {
        Console.WriteLine("PRINTED!");
        Console.WriteLine(sender.ToString());
    }

    /// <summary>
    /// Event handler of onTake
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void TakeHandler(object sender, MyLibraryEventArgs e)
    {
        Console.WriteLine($"ATTENTION! Books starts with {e.start} were taken!");

        List<PrintEdition> newLib = new List<PrintEdition>();

        if (sender is MyLibrary<PrintEdition>)
        {
            foreach (var printEd in (MyLibrary<PrintEdition>)sender)
            {
                if (printEd is Magazine || printEd.name[0] != e.start)
                {
                    newLib.Add(printEd);
                }
            }
            ((MyLibrary<PrintEdition>)sender).library = newLib;
        }
    }
    /// <summary>
    /// Get int from -10 to 101
    /// </summary>
    /// <returns></returns>
    static int GetRandomInteger()
    {
        return rand.Next(-10, 101);
    }

    /// <summary>
    /// Get random string of random length and symbols
    /// </summary>
    /// <returns></returns>
    static string GetRandomString()
    {
        int n = rand.Next(1, 11);
        string res = "";

        for (int i = 0; i < n; i++)
        {
            res += ((char)rand.Next('a', 'z'+1)).ToString();
        }

        return res[0].ToString().ToUpper() + res.Substring(1);
    }

    /// <summary>
    /// Return Magazine ot Book
    /// </summary>
    /// <returns></returns>
    static PrintEdition GetRandomObject()
    {
        int n = rand.Next(2);

        if (n == 0)
        {
            var obj = new Magazine(GetRandomString(), Convert.ToUInt32(GetRandomInteger()), Convert.ToUInt32(GetRandomInteger()));
            obj.onPrint += PrintHandler;
            return obj;
        }
        else
        {
            var obj = new Book(GetRandomString(), Convert.ToUInt32(GetRandomInteger()), GetRandomString());
            obj.onPrint += PrintHandler;
            return obj;
        }
    }
    
    /// <summary>
    /// Return value of continuing
    /// </summary>
    /// <returns></returns>
    static bool IsContinue()
    {
        Console.Write("нажмите 'q' чтобы выйти или любую другую клавишу чтобы продолжить: ");
        var key = Console.ReadKey().Key;
        Console.WriteLine();
        if (key == ConsoleKey.Q)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Save info in "myLibrary.txt"
    /// </summary>
    /// <param name="lib"></param>
    static void SaveInformation(MyLibrary<PrintEdition> lib)
    {
        List<string> info = new List<string>();

        foreach(var obj in lib)
        {
            info.Add(obj.ToString());
        }

        File.WriteAllLines("myLibrary.txt", info, System.Text.Encoding.Unicode);
    }

    /// <summary>
    /// Parse info from file
    /// </summary>
    /// <returns></returns>
    static MyLibrary<PrintEdition> ParseInformation()
    {
        string[] file = File.ReadAllLines("myLibrary.txt");

        MyLibrary<PrintEdition> newLibrary = new MyLibrary<PrintEdition>();

        for (int i = 0; i < file.Length; i++)
        {
            string[] data = file[i].Split(';', '=');

            if (data[4] == " author")
            {
                newLibrary.Add(new Book(data[1], uint.Parse(data[3]), data[5]));
            }
            else
            {
                newLibrary.Add(new Magazine(data[1], uint.Parse(data[3]), uint.Parse(data[5])));
            }
        }
        return newLibrary;
    }

    static void Main()
    {
        while (IsContinue())
        {
            Console.Write("Введите количество создаваемых объектов: ");

            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Введено некорректное значение");
                continue;
            }

            MyLibrary<PrintEdition> myLibrary = new MyLibrary<PrintEdition>();

            for (int i = 0; i < n; i++)
            {
                try
                {
                    myLibrary.Add(GetRandomObject());
                }
                catch (OverflowException)
                {
                    Console.WriteLine("при создании объекта были сгенерированы некорректные данные. Повторная попытка создать объект...");
                    i--;
                }
            }

            myLibrary.onTake += TakeHandler;

            foreach(var printEd in myLibrary)
            {
                if (printEd is Book)
                {
                    printEd.Print();
                }
            }

            Console.WriteLine(myLibrary.ToString());

            myLibrary.TakeBooks((char)rand.Next('A', 'Z' + 1));

            Console.WriteLine(myLibrary.ToString());

            try
            {
                SaveInformation(myLibrary);
            }
            catch (Exception)
            {
                Console.WriteLine("При сохранении файла произошла ошибка!");
            }


            try
            {
                MyLibrary<PrintEdition> newLib = ParseInformation();
                Console.WriteLine(newLib.ToString());
                Console.WriteLine(newLib.MeanBooks.ToString("F2"));
                Console.WriteLine(newLib.MeanMagazine.ToString("F2"));
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка чтения файла! Проверьте, что файл находится в нужной папке и корректен...");
            } 
        }
    }
}
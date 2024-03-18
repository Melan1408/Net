using HomeWork1;

Console.WriteLine("Hello! Its Homework1.");
PrintInstruction();

string input;
int numberForCalc;

while ((input = Console.ReadLine()) != "E")
{
    var parseIsSuccess = int.TryParse(input, out numberForCalc);
    if (!parseIsSuccess) Console.WriteLine("Input is not correct, please input only integral numbers");
    else
    {
        Console.WriteLine($"Result of factorial calculation - {GetFactorial(numberForCalc)}");
        Console.WriteLine($"Result of reverse - {ReverseNumber(numberForCalc)}");
        Console.WriteLine($"Result of left shift by 3 - {LeftShiftByStep(numberForCalc,3)}");
        Console.WriteLine("Have array like this:");
        Console.WriteLine($"And now look at this!!! Its sum between min and max elements of array - {SumArrElements()}");
        Console.WriteLine("Some play with academyGroup:");

        var academyGroup = new AcademyGroup();

        academyGroup.Add(new Student(55.5, 1, "Nicko", "Kovah", 18, "380992129912"));
        academyGroup.Add(new Student(45.5, 2, "Den", "Bynno", 19, "380992659912"));
        academyGroup.Add(new Student(88, 3, "Oleg", "Krav", 18, "380992129122"));
        academyGroup.Add(new Student(99, 4, "Masha", "Klipen", 22, "380992129986"));
        academyGroup.Print();
        academyGroup.Remove("Krav");
        academyGroup.Search("Krav");
        academyGroup.Edite("Bynno", new Student(66.5, 3, "Den", "Bynno", 19, "380992129925"));
        academyGroup.Save();
        academyGroup.Read();
        academyGroup.Print();

        PrintInstruction();
    }
}

static int ReverseNumber(int number)
{
    var result = 0;
    while (number > 0)
    {
        result = result * 10 + number % 10;
        number /= 10;
    }
    return result;
}

static int LeftShiftByStep(int number, int shift)
{     
    shift = shift % number.ToString().Length;
    if (shift == 0) return number;

    var numberArr = number.ToString().ToArray();
    var shiftedArr = new char [numberArr.Length];

    var result = 0;

    Array.Copy(numberArr, 0, shiftedArr, numberArr.Length - shift, shift);
    Array.Copy(numberArr, shift, shiftedArr, 0, numberArr.Length - shift);

    foreach (char digit in shiftedArr)
        result = result * 10 + int.Parse(digit.ToString());

    return result;
}

static long GetFactorial(int number)
{
    if (number == 0) return 1;

    return number * GetFactorial(number - 1);
}

static int SumArrElements()
{
    var numberArr = Make2dArray(5);
    var maxNumber = numberArr.Cast<int>().Max();
    var maxIndex = numberArr.Cast<int>().ToList().IndexOf(maxNumber);
    var minNumber = numberArr.Cast<int>().Min();
    var minIndex = numberArr.Cast<int>().ToList().IndexOf(minNumber);

    if (maxIndex > minIndex)
    {
        var range = numberArr.Cast<int>().ToList().Where((item, idex) => idex > minIndex && idex < maxIndex).ToList();
        return range.Sum();
    }
    else
    {
        var range = numberArr.Cast<int>().ToList().Where((item, idex) => idex > maxIndex && idex < minIndex).ToList();
        return range.Sum();
    }
}


static int[,] Make2dArray(int size)
{
    var numberArr = new int[size, size];
    var randNum = new Random();
    var minRange = -100;
    var maxRange = 100;

    for (int i = 0; i < numberArr.GetLength(0); i++)
    {
        for (int j = 0; j < numberArr.GetLength(1); j++)
        {
            numberArr[i, j] = randNum.Next(minRange, maxRange);
            Console.Write(numberArr[i, j] + "\t");
        }
        Console.WriteLine();
    }
    return numberArr;
}

static void PrintInstruction()
{
    Console.WriteLine("Write number to try or E for exit");
}
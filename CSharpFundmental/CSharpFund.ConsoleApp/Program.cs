using CloudinaryDotNet;
using CSharpFund.ConsoleApp;
using CSharpFund.ConsoleApp.Interface;
using CSharpFundamentals.core.Models;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace CSharpFund.ConsoleApp
{
    internal class Program
    {
        //public enum Gender
        //{
        //    Male,
        //    Female,
        //    Test,
        //    ge = 7
        //}

        //public enum Colors
        //{
        //    Black,
        //    Blue,
        //    Green,
        //    Cyan,
        //    Red,
        //    Magenta,
        //    Yellow,
        //    White
        //}

        [Flags]
        public enum WeekDays
        {
            None = 0,
            satuerday = 1 ,
            Sunday =2,
            Monday= 4,
            Tuesday = 8,
            Wednesday =16,
            Thursday =32,
            Friday =64,

        }
        static void Main(string[] args)
        {


            //bool boolean1 =true , boolean2 =false;
            //bool result1 = boolean1 | boolean2;
            //bool result2 = boolean1 || boolean2;
            //Console.WriteLine("result 1:"); Console.WriteLine(result1);
            //Console.WriteLine("result 2:"); Console.WriteLine(result2);

            // boolean (not)
            //bool boolean3 = true, boolean4 = true;
            //bool result3 = !boolean3;// false
            //bool result4 = boolean4; //false
            //Console.WriteLine($"sult 3:{result3}");
            //Console.WriteLine($"sult 4:{result4}");

            // Char data type
            //Console.WriteLine("Whrite Everything And Press Enter");
            //int x = Console.Read();
            //1-> 49 , A->65, a->97
            //Console.WriteLine("The Value OF x is:"); Console.WriteLine(x);

            //  char char1 = 'A', char2 = 'B', char3 = 'C', char4 = 'D', char5 = 'E', char6 = 'F'
            //      , char7 = 'G', char8 = 'H', char9 = 'I', char10 = 'J', char11 = 'K', char12 = 'L',
            //      char13 = 'M', char14 = 'N', char15 = 'O', char16 = 'P', char17 = 'Q', char18 = 'R',
            //      char19 = 'S', char20 = 'T', char21 = 'U', char22 = 'V', char23 = 'W', char24 = 'X',
            //      char25 = 'Y', char26 = 'Z';


            //// بطريثه تحويل ال char الى int (Type Casting)
            //  Console.WriteLine((int)char1);
            //  Console.WriteLine((int)char2);
            //  Console.WriteLine((int)char3);
            //  Console.WriteLine((int)char4);
            //  Console.WriteLine((int)char5);
            //  Console.WriteLine((int)char6);
            //  Console.WriteLine((int)char7);
            //  Console.WriteLine((int)char8);
            //  Console.WriteLine((int)char9);
            //  Console.WriteLine((int)char10);
            //  Console.WriteLine((int)char11);
            //  Console.WriteLine((int)char12);
            //  Console.WriteLine((int)char13);
            //  Console.WriteLine((int)char14);
            //  Console.WriteLine((int)char15);
            //  Console.WriteLine((int)char16);
            //  Console.WriteLine((int)char17);
            //  Console.WriteLine((int)char18);
            //  Console.WriteLine((int)char19);
            //  Console.WriteLine((int)char20);
            //  Console.WriteLine((int)char21);
            //  Console.WriteLine((int)char22);
            //  Console.WriteLine((int)char23);
            //  Console.WriteLine((int)char24);
            //  Console.WriteLine((int)char25);
            //  Console.WriteLine((int)char26);

            //string data type
            //string str1 = "Hello";
            //string str2 = "AMR!";
            //string lablabla = @"hi sierra 
            //hello amr
            //welcome to c#";
            //Console.WriteLine(lablabla);
            //Console.WriteLine(str1+ " " + str2);
            //Console.WriteLine(lablabla);
            //Console.WriteLine("Hello " + str2 + "\nWelcome to C# Fundmentals");
            //// string using tamplate
            //Console.WriteLine($"Hello {str2}\nWelcome to C# Fundmentals");
            //string name = "";
            //Console.Write("Enter Your Name:");
            //name = Console.ReadLine();
            ////Console.WriteLine("hello mr " + name);
            ////Console.WriteLine($" {name.Length}");
            ////Console.WriteLine($"Upper Case: {name.ToUpper()}");
            ////Console.WriteLine($"Lower Case  {name.ToLower()}");
            //bool StartsWithA = name.StartsWith("am");
            //bool StartsWith = name.StartsWith("AM" , StringComparison.OrdinalIgnoreCase);
            //Console.WriteLine(StartsWithA);    
            //Console.WriteLine(StartsWith);    


            /*
             (Numeric Data Type)
            int & uint
            long & ulong
            float
            double
             */
            // int int1 = 0;
            // int int2 = 50;
            // int int3 =-50;

            // // u -> unsigned (positive only) مينغهش يشيل قيمه سالبه

            // uint uint1 = 0;
            //uint uint2 = 50;
            //uint uint3= -50;
            //Console.WriteLine($"the size of int is: {sizeof(int)}");
            //Console.WriteLine($"the size of uint{sizeof(uint)}");
            //    Console.WriteLine("-----------------------------");
            //    Console.WriteLine($"the int min value:{int.MinValue}");
            //    Console.WriteLine($"the uint min value:{uint.MinValue}");
            //    Console.WriteLine("-----------------------------");
            //    Console.WriteLine($"the int Max valu{int.MaxValue}");
            //    Console.WriteLine($"the uint Max val:{uint.MaxValue}");



            /*
             * [Arithmetic Operators]
             * addition +
             * subtraction -
             * multiplication *
             * division /
             * modulus %
             */
            //double x = 25;
            //int y = 10;

            //Console.WriteLine($"{x}+{y}{x+y}");
            //Console.WriteLine($"{x}-{y}{x-y}");
            //Console.WriteLine($"{x}*{y}{x*y}");
            //Console.WriteLine($"{x}/{y}{x / y}");

            //Console.WriteLine($" 10 +3 *2 = {10 + 3 * 2}"); // 16
            //Console.WriteLine($" 10 +3 *2 = {(10+ 3)* 2}"); //26


            //{Assignment Operators}
            //   int x = 10;
            //   Console.WriteLine(x);
            //   x += 5; // x = x + 5
            //   Console.WriteLine(x);

            //   int a = 11;
            //   int s = 3;
            //   int d = 22;
            //   Console.WriteLine(a);
            //   Console.WriteLine(s);
            //   Console.WriteLine(d);
            //   Console.WriteLine("--------------");

            //a =s =d =20;   
            //   Console.WriteLine(a);
            //   Console.WriteLine(s);
            //   Console.WriteLine(d);

            /*
             * [ Increment / Decrement Operator]
             * pre/post increment
             * pre/post decrement
             * overview operator precedence
             */

            //int x = 5;
            //int y = 8;
            //Console.WriteLine(--x);
            //Console.WriteLine(x);
            //Console.WriteLine("--------------");
            //Console.WriteLine(y--);
            //Console.WriteLine(y);   








            /* [comparison operator]
             * equal ==
             * Not Equal !=
             * Greater Than >
             * Less Than <
             * Greater Than Or Equal >=
             * Less Than Or Equal <=
             */

            //int n = 9;
            //Console.WriteLine($" n = 9 ? {n == 9}"); //true
            //Console.WriteLine($" n = 4 ? {n == 4}"); //false

            //Console.WriteLine($" n !=9 ? {n != 9}"); //false
            //Console.WriteLine($" n !=4 ? {n != 4}"); //true

            //Console.WriteLine($" n >9 ? {n >9}"); //false
            //Console.WriteLine($" n >10? {n > 10}");//false

            //Console.WriteLine($" n <9 ? {n < 9}"); //false
            //Console.WriteLine($" n <4 ? {n < 4}"); //false

            //Console.WriteLine($" n >=9 ? {n >= 9}"); //true
            //Console.WriteLine($" n >=4 ? {n >= 4}"); //true

            //Console.WriteLine($" n <=9 ? {n <=9}"); //true
            //Console.WriteLine($" n <=6 ?{n <=6}"); //true



            /* 16[ PArsing String]
             */

            //   Console.WriteLine("Enter Your Birth Year");
            //string year =  Console.ReadLine();   
            //   int intYear = int.Parse(year);
            //   Console.WriteLine($"your age until 2025 {2025 - intYear}");



            /* [Control Flow /IF Statment]*/

            //Console.WriteLine("Enter Your NUMBER");
            //string number = Console.ReadLine();
            //int ParseNumber = int.Parse(number);
            //int remainder = ParseNumber % 2;

            //if (remainder == 1)
            //{
            //    Console.WriteLine($"{number}is odd");

            //}
            //else if (remainder == 0) 
            //{
            //    Console.WriteLine("zero is ");
            //}
            //else
            //{
            //    Console.WriteLine($"{number} is even");
            //}





            /* [Arrays]
             * what is array
             * how to declare array
             * how to access array elements
             * how to sort in array
             * how to copy an array*/


            //int[] numbers = {2, 5, 1, 6, 3, 7, 4,8 };

            //int[] numbers2 = new int[8];

            //Array.Sort(numbers); // sort the array مهناه رتب المصفوفة
            //Console.WriteLine(numbers[6]);//7
            //Console.WriteLine(numbers[2]); //3

            //Array.Copy(numbers, numbers2, numbers.Length);
            //Console.WriteLine($"the nubers is{numbers2[1]}");
            //foreach (var item in numbers)
            //{
            //    Console.WriteLine(item);
            //}
            //Array.Copy()


            /* [Loops]
             * what is loop
             * for loop
             * how to loop nubers of times
             * how to loop in array
             * how to change step count
             */

            //int i = 1;
            //for ( i = 0; i < 30; i++) {
            //    i += 1;
            //    Console.WriteLine($"the numbers is :{ i}");
            //}

            //int[] num = { 50, 60, 70, 80, 90 };

            //for (int i = 0; i< num.Length; i++) {
            //    num[i] += 5;

            //    Console.WriteLine(num[i]);// out of the index
            //}

            //foreach (var item in num)
            //{
            //    Console.WriteLine(item);

            //}

            //[Assignment]
            //int[] number = new int[5];
            //Console.WriteLine("Blease Enter In Five Numbers:");

            //for (int i = 0; i < number.Length; i++)
            //{
            //    Console.Write($"Enter The Number {i + 1}: ");
            //    number[i] = int.Parse(Console.ReadLine());
            //}
            //// 2- عرض الأرقام كما أدخلها اليوزر
            //Console.WriteLine("numbers enter by user");
            //for (int i = 0; i < number.Length; i++)
            //{
            //    Console.Write(number[i] + " ");
            //}
            //Array.Sort(number);
            //Console.WriteLine("the numbers after sorting");
            //for (int i = 0; i < number.Length; i++)
            //{
            //    Console.Write(number[i] + " ");

            //}


            /*[Ternary Operator]
             * Syntax -> Condition is true ? true part :false Part
             */
            //string statments;
            //for (int i = 1; i <= 10; i++) {

            //if (i % 2 == 0)

            //    Console.WriteLine($"{i} Is Even");

            //Console.WriteLine($"{i} IS Odd");

            //statments = i % 2 == 0 ? $"{i} Is Even" : $"{i} IS Odd";
            //    Console.WriteLine(statments);


            //}

            //char[] num = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //foreach (char c in num) { 

            //Console.WriteLine($"{c} = {(int)c}");
            //}





            // [Training of For & foreach loops]


            //( For)
            //Console.Write("Enter Array Numbers Of Items :");
            //int ArraySize = int.Parse(Console.ReadLine());
            //int[] numbers = new int[ArraySize];
            //for(int i =0; i < numbers.Length; i++)
            //{
            //    Console.Write($"Enter The Number {i + 1}: ");
            //    numbers[i] = int.Parse(Console.ReadLine());
            //}

            //Array.Reverse(numbers);
            //Console.Write("The Numbers When Reserved Is :");

            //for(int i = 0; i < numbers.Length; i++)
            //{
            //    Console.Write(numbers[i] + " ");
            //}

            //(foreach)

            //Console.Write("Enter Array Numbers Of Items :");
            //int ArraySize = int.Parse(Console.ReadLine());
            //int[] numbers = new int[ArraySize];

            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    Console.Write($"Enter The Number {i + 1}: ");
            //    numbers[i] = int.Parse(Console.ReadLine());
            //}
            //int sum = 0;
            //foreach (var item in numbers)
            //{
            //    sum += item;
            //    double average = sum / ArraySize;
            //    Console.WriteLine($"The Average = : {average}");
            //}


            // [DO WHILE LOOP]

            //int z =2;
            //do
            //{
            //    Console.WriteLine($"the number is : {z++}");
            //}
            //while (z <= 10);


            /* [ control flow / switch statment]
             *  -  what is switch statment
             *  syntax 
             *  when to use switch statment
            */

            //while (true)
            //{

            //    Console.Write("Enter A String : ");
            //    string input = Console.ReadLine();

            //    Console.WriteLine("Please Select the option ");
            //    Console.WriteLine("1) convert to Capital ");
            //    Console.WriteLine("2) convert to Lower ");
            //    Console.WriteLine("3) convert to Length ");

            //    string option = Console.ReadLine();

            //    switch (option)
            //    {
            //        case "1":

            //            Console.WriteLine(input.ToUpper());
            //            break;

            //        case "2":
            //            Console.WriteLine(input.ToLower());
            //            break;
            //        case "3":
            //            Console.WriteLine(input.Length);
            //            break;
            //            default:
            //            Console.WriteLine("Invalid Option");
            //            break;
            //    }

            //if (option == "1")

            //    Console.WriteLine(input.ToUpper());
            //else if (option == "2")

            //    Console.WriteLine(input.ToLower());

            //else if (option == "3")

            //    Console.WriteLine(input.Length);

            //else

            //    Console.WriteLine("Invalid Option");


            //}

            /* [Methods]
             * what is method
             * when to use it
             * syntax (void)
             * return value from method
             * optional arguments
             */


            //int[] number = {1,2,3,4,5};
            //GetAverage(number ,PrintSumToConsole:true);


            //number = new int[] { 10, 20, 30, 40, 50 };
            //GetAverage(number, true); 

            //static double GetAverage(int[] number , bool PrintAverageToConsole =false ,bool PrintSumToConsole =false) { 
            //    int sum = 0;
            //    for (int i = 0; i < number.Length; i++)
            //    {
            //        sum += number[i];
            //    }
            //    double average = sum / number.Length;
            //    if (PrintSumToConsole == true)
            //     Console.WriteLine($"The Sum = : {sum}");

            //    if (PrintAverageToConsole == true )
            //     Console.WriteLine($"The Average = : {average}");
            //    return average;
            //}


            /* [Training]
             * Ask the user to enter the number of items in an integer array
             * Ask the user to enter the items of the array
             * find the smallest number in the array
             * find the largest number in the array
             * Calculate the average of the array 
             */

            //Console.Write("Please Enter The Numbers of Items :");
            //int ArraySize = int.Parse(Console.ReadLine());
            //int [] numbers = new int[ArraySize];
            //for (int i = 0; i < ArraySize; i++) { 
            //Console.Write($"Enter The Number {i + 1}: ");
            //numbers[i] = int.Parse(Console.ReadLine());
            //}

            //int sum = 0 , smallest = int.MaxValue, largest = 0;
            //foreach (int item in numbers) 
            //{
            //    sum += item;
            //    if (item < smallest)
            //        smallest = item;
            //    if (item > largest)
            //        largest = item;
            //}
            //double average = sum / ArraySize;
            //Console.WriteLine($"The Smallest number is = : {smallest}");
            //Console.WriteLine($"The Largest = : {largest}");
            //Console.WriteLine($"The Average = : {average}");


            /* OOP (Object Oriented Programming)
             * what is class
             * what is object
             * what is constructor
             */
            //Student[] students= new Student[3];
            //Student Amr = new Student(1, " Zaaza", " Cairo",02154);
            //Student medo = new Student(2, " medo", " Cairo",012230);
            //Console.WriteLine(  Amr.Name + Amr.Address);
            //Console.WriteLine(medo.Name + medo.Address +medo.Phone);
            //Console.WriteLine("Enter Your Id:");
            //Amr.Id = int.Parse(Console.ReadLine());
            //Console.WriteLine("Enter Your Name:");
            //Amr.Name = Console.ReadLine();
            //Console.WriteLine("Enter Your Phone:");
            //Amr.Phone = int.Parse(Console.ReadLine());
            //Console.WriteLine("Enter Your Address:");
            //Amr.Address = Console.ReadLine();

            //Student sara = new Student() { 
            //    Id = 2, Name = "saraa", Phone = 0111111, Address = "cairo"
            //};
            //students[0] = Amr;
            //students[1] = sara;
            //Console.WriteLine("Enter Your Id:"+ Amr.Id);
            //Console.WriteLine("Enter name:"+ sara.Name);
            //Console.WriteLine("Enter name:"+ sara.Address);




            /* [properties]
             * what is property
             * Getters and Setters methods with backing field
             * read-only property
             * init-only property
             * Auto-Implemented property
             */

            //Student student = new Student();
            //student.Name = "amr";  // set
            //student.Address = "cairo"; //get
            //student.Phone = 123456;
            //Console.WriteLine($"The Name Is : {student.Name} \n Your Address is :{student.Address},");

            //var test = new test();
            //test.Test = "Hello Amr";
            //Console.WriteLine(test.Test);


            /*[Access Modifiers]
             * what is access modifier
             * public
             * private
             * protected
             * internal
             */

            //Student student = new Student();
            //student.Name = "amr";  // set
            //Console.WriteLine(student.Name);



            /* [ Static Class]
            
             */

            //MyStatic_Class.DoSomething();




            /* [Exception]
             * How Handle Exceptions
             * How to throw Exceptions
             */

            //    try {

            //        Console.WriteLine(Divide(10, 2));
            //        Console.WriteLine(Divide(10, 0));
            //    }

            //    catch (DivideByZeroException ex) {
            //        Console.WriteLine("custom exception hand");

            //    }

            //    catch (Exception ex)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine(ex.ToString());
            //        Console.ForegroundColor = ConsoleColor.White;
            //    }


            //}
            //static int Divide(int num1,int divisor)
            //{
            //    if (divisor == 0)
            //        throw new DivideByZeroException();
            //    return num1 / divisor;



            /* [emums]
             * what is enum
             * simple enums
             * parse string value to enum
             * print enum values
             * practice allow user to change console background & foreground color
             */

            //Gender gender = Gender.ge;
            //Console.WriteLine($"{gender.ToString()} = {(int)(gender)}");
            //Gender ge = Gender.Test;
            //Console.WriteLine($"{ge.ToString()} = {(int)(ge)}");

            //foreach (String color in Enum.GetNames(typeof(Colors)))
            //{
            //    Console.WriteLine($"{color} = {(int)Enum.Parse(typeof(Colors), color)}");
            //}
            // 3)

            //Console.BackgroundColor = ConsoleColor.Cyan;
            //Console.ForegroundColor = ConsoleColor.Green;
            //string ColorName = "red";
            //Colors color = (Colors) Enum.Parse(typeof(Colors), ColorName ,true);
            //Console.WriteLine($"{color} = {(int)color}");

            // 4) practise
            //Console.WriteLine("please select an option");
            //Console.WriteLine("[1] change Backgrounf Color \t\t [2] change Foreground color");
            //string SelectOption = Console.ReadLine();

            //foreach (var color in Enum.GetNames(typeof(ConsoleColor)))
            //{
            //    Console.WriteLine($"{color}");
            //    Console.WriteLine("Please Write The Color");
            //    string ConsoleColor = Console.ReadLine();

            //    ConsoleColor SelectColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ConsoleColor, true);

            //    if (SelectOption == "1")
            //    {
            //        Console.BackgroundColor = SelectColor;
            //    }
            //    else if (SelectOption == "2")
            //    {
            //        Console.ForegroundColor = SelectColor;
            //    }

            //    Console.Clear(); // علشان يظهر التغيير بوضوح
            //    Console.WriteLine("Color changed successfully!");
            //}





            /* [  -> flags enums   ]
             * whats flag enum 
             * how to define flag enum
             * How it Works
             * Bitwise operation
             */

            //WeekDays weekend = WeekDays.Friday | WeekDays.Sunday |  WeekDays.Monday | WeekDays.satuerday;
            //WeekDays weekDays = WeekDays.satuerday | WeekDays.Wednesday | WeekDays.Thursday;
            //Console.WriteLine(weekend);
            ////المتكررين مع بعض & (and)
            //Console.WriteLine(weekDays & weekend);
            //foreach (var day in Enum.GetValues(typeof(WeekDays))) { 
            //    Console.WriteLine($"{day} = {(int)day}");
            //}



            /**************************************************
             * [Random Values Genarator]
             * ask the user to selct an option
             * [1]genrate random number  [2] generate random string
             * if user selected to generate random number: generate random number between 1000 and 9999
             * if user selected to generate random string: generate random string with 16 char of length
             */


            //while (true)
            //{
            //    Console.WriteLine("Please Select An Option :");
            //    Console.WriteLine("[1]genrate random number  [2] generate random string");
            //    var SelectOption = Console.ReadLine();
            //    if (SelectOption == "1")
            //    {
            //        GenerateRandomNumber();
            //    }
            //    else if (SelectOption == "2")
            //    {
            //        GenerateRandomString();
            //    }
            //}
            //static void GenerateRandomNumber()
            //{
            //    Random random = new Random();
            //    int randomNumber = random.Next(1000, 9999);
            //    Console.WriteLine($"The Random Number is : {randomNumber}");
            //    Console.WriteLine("--------------------------------------------");
            //}
            //const string Buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            //static void GenerateRandomString() {
            //    var sb = new StringBuilder();
            //    var random = new Random();
            //    while (sb.Length < 16)
            //    {
            //        var index = random.Next(0,Buffer.Length -1);
            //        sb.Append(Buffer[index]);
            //    }

            //    Console.WriteLine($"Random String :{ sb}");
            //    Console.WriteLine("---------------------------------------------------");
            //}







            /* [Apstract Class]
             */

            //var rect = new Rectangle { Length =120, Width =60};
            //rect.PrintArea();

            //Rectangle rect2 = new Rectangle { Length =100, Width = 50 }; 
            //rect2.PrintArea();

            //var circle = new Circle { Radius = 30 };
            //circle.PrintArea();

            //var test = new Rectangle();
            //test.Test();


            //*****************************************************************************************

            /* [Interface]
             * How to define interface & uses
             * Implicit & Explicit implememntaion
             * interface VS Abstract class
             */

            // استدعي الانتر فيس
            // انشاء اوبجكت من نوع الموبايل فون
            //IDevice device = new MobilePhone();
            //device.TurnOn();
            //device.TurnOff();

            //Console.WriteLine("---------------");

            //device = new Computer();
            //device.TurnOn();
            //device.TurnOff();

            //Console.WriteLine("---------------");

            //device = new LightBulb();
            //device.TurnOn();
            //device.TurnOff();
            //device.Test();

            //Console.WriteLine("---------------");

            //IRestartable restartable = new MobilePhone();
            //restartable.Restart();
            //restartable = new Computer();
            //restartable.Restart();


            //*******************************************************************************************

            /* [ Encapsulation]
             */

            //var employee = new Employee();

            //employee.GetFullName("Ammmr", "Anwer");
            ////employee.LastName = "zaaza";
            //employee.SetBirthDate(new DateOnly(2000, 2, 8));
            //employee.Salary = 5000;
            //employee.TaxPercentage = 20;
            //employee.SetAddress("cairo, Egypt");
            //Console.WriteLine($"*******************************************");

            //Console.WriteLine($"The Fist Name Is :{employee.FirstName} And The Last Name :{employee.LastName}");
            //Console.WriteLine($"The Birth Date :{employee.BirthDate}");
            //Console.WriteLine($"The Salary :{employee.Salary:0.00}");
            //Console.WriteLine($"The Tax Persentage Is :{employee.TaxPercentage}");
            //Console.WriteLine($"your Address Is :{employee.Address}");

            //Console.WriteLine($"*******************************************");


            //Console.WriteLine($"*******************************************");
            //printPersonData(employee);
            //var applicant = new Applicant();
            //applicant.GetFullName("App:Saraa", "Ali");
            //applicant.SetBirthDate(new DateOnly(1995, 5, 15));
            //applicant.SetAddress("Alex, Egypt");

            ////Console.WriteLine($"*******************************************");
            ////var person = new Person();
            ////person.GetFullName("Person:Mohamed", "Hassan");
            ////person.SetBirthDate(new DateOnly(1990, 3, 20));
            ////person.SetAddress("Giza, Egypt");
            //printPersonData(applicant);
            //Console.WriteLine($"*******************************************");

            //void printPersonData(Person person)
            //{
            //    Console.WriteLine($"The Fist Name Is :{person.FirstName} And The Last Name :{person.LastName}");
            //    Console.WriteLine($"The Birth Date :{person.BirthDate}");
            //    Console.WriteLine($"your Address Is :{person.Address}");

            //}


            //*******************************************************************************************

            /* [Polymorphism]
             */
            var salariedEmployee = new SalariedEmployee();
            salariedEmployee.BasicSalary = 4000;
            salariedEmployee.Transportation = 500;
            salariedEmployee.Housing = 1000;
            Console.WriteLine($"The Salary of Salaried Employee Is : {salariedEmployee.GetSalary():0.00}");

            Console.WriteLine("======================================================================");
            var hourlyEmployee = new HourlyEmployee();
            hourlyEmployee.HourlyRate = 50;
            hourlyEmployee.TotalHoursWorked = 160;
            Console.WriteLine($"The Salary of Hourly Employee Is : {hourlyEmployee.GetSalary():0.00}");

            Console.WriteLine("======================================================================");
            var internEmployee = new InternEmployee();

            Console.WriteLine($"The Salary of Intern Employee Is : {internEmployee.GetSalary():0.00}");
            Console.ReadKey();
        }
    }
}


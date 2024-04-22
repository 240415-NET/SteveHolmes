namespace StringArray;

class Program
{
    static void Main(string[] args)
    {
        int student_one_grade = 97;
        int student_two_grade =93;
        int student_three_grade = 100;
        int student_four_grade = 85;

        Console.WriteLine(student_one_grade);
        Console.WriteLine (student_two_grade);
        Console.WriteLine(student_three_grade);
        Console.WriteLine(student_four_grade);

    int[] student_grades = new int[4];
    student_grades[0] = 97;
    student_grades[1] = 93;
    student_grades[2] = 100;
    student_grades[3] = 85;

    int[] student_grades2 = {97, 93, 100, 85};
    for (int i = 0; i < 4; i++)
    {
        Console.WriteLine(i);
        Console.WriteLine(student_grades2[i]);
    }

    foreach(int grade in student_grades2)
    {
        Console.WriteLine(grade);
    }

    char[] hello_chars = { 'H', 'e', 'l', 'l', 'o'};
    string hello_string = new string(hello_chars);

    Console.WriteLine(hello_chars);
    Console.WriteLine(hello_string);

    string world_string = "World!";
    char[] world_chars = world_string.ToCharArray();

    Console.WriteLine(world_string);
    Console.WriteLine(world_chars);

    // String methods

    string new_word = "Banana";
    Console.WriteLine(new_word);

    string new_word_upper = new_word.ToUpper();
    Console.WriteLine(new_word_upper);

    Console.WriteLine(new_word.ToLower());

    foreach(char c in new_word){
        Console.WriteLine(c);
    }

    for(int i = 0; i < new_word.Count(); i++){
        Console.WriteLine(new_word[i]);
    }

    // substring

    Console.WriteLine(new_word.Substring(0, 3));


    //contains

    string phrase = "Hello There Someone!";
    Console.WriteLine(phrase.Contains("Some"));

    // Trim()

    Console.WriteLine("Please enter your name");
    string name2 = Console.ReadLine().Trim().ToLower();

    Console.WriteLine(name2);

    // Replace

    string text = "Hello Hello, World!";
    string newText = text.Replace("Hello", "Goodbye");

    Console.WriteLine(newText);




    }
 }

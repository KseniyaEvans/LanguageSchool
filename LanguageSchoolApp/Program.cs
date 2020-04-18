using System;

namespace LanguageSchoolApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LANGUAGE nativeLan = LANGUAGE.Ukrainian;
            User user_1 = new User("Kseniya Lakhman", 1000, nativeLan);
            UserInterface terminal = new UserInterface();
            terminal.Run(user_1);

            //Console.ReadLine();
        }
    }
}

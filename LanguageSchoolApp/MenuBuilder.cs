using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public enum MENU
    {
        MainMenu,
        PersonalAreaMenu,
        CartMenu,
        CoursesMenu,
        BackMenu,

        CoursesByLanguage,
        CoursesByLevel,

        ClearCart,
        DeleteCourse,
        BuyCourses,
        AddCourse
    }

    //BUILDER
    public interface MenuBuilderTemplate
    {
        public abstract void addMenu(MENU menu);
        public abstract void removeMenu();
        public abstract void showMenu();
       
    }
    public class MenuBuilder : MenuBuilderTemplate
    {
        Stack<MENU> _menues = new Stack<MENU>();
        User _user;
        CoursesData data = new CoursesData();
        public MenuBuilder(User user)
        {
            this._user = user;
        }
        public void addMenu(MENU menu)
        {
            this._menues.Push(menu);
        }
        private void header()
        {
            Console.WriteLine("      NAVIGATION: ");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("      [P]ersonal Area Menu\n      [C]ourses Menu\n      [Ca]rt Menu");
            Console.WriteLine("      [B]ack\n      [E]xit");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void buildMenu(MENU menu)
        {
            header();
            switch (menu)
            {
                case MENU.MainMenu:
                    {
                        printGreen("Main Menu");
                        break;
                    }
                case MENU.PersonalAreaMenu:
                    {
                        printGreen("Personal Area Menu");
                        this._user.printInfo();
                        break;
                    }
                case MENU.CartMenu:
                    {
                        printGreen("Cart Menu");
                        
                        this._user.printCart();
                        Console.WriteLine("[Buy] Courses");
                        Console.WriteLine("[Cl]ear cart");
                        Console.WriteLine("[R]emove course");
                        break;
                    }
                case MENU.CoursesMenu:
                    {
                        printGreen("Courses Menu");
                        Console.WriteLine("[lan] Show courses by my native language\n[level] Show courses by level");
                        break;
                    }
                case MENU.BackMenu:
                    {
                        removeMenu();
                        break;
                    }
                case MENU.CoursesByLanguage:
                    {
                        data.printByLanguage(_user.nativeLanguage);
                        break;
                    }
                case MENU.CoursesByLevel:
                    {
                        bool isRunning = true;
                        while(isRunning)
                        {
                            Console.WriteLine("[0] A1\n[1] A2\n[2] B1\n[3] B2\n[4] C1\n[5] C2");
                            Console.WriteLine("Enter the level: ");

                            string input = Console.ReadKey(true).KeyChar.ToString().ToLower();
                            int level_int = 0;
                            bool isNumber = Int32.TryParse(input, out level_int); 
                            if (isNumber)
                            {
                                LEVEL level_en = (LEVEL)Enum.ToObject(typeof(LEVEL), level_int);
                                data.printByLevel(level_en);
                            } else
                            {
                                isRunning = false;
                            }
                        }

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unknown command. Repeat please.");
                        break;
                    }

            }
            Console.WriteLine("What menu would you like to go?");
        }
        public void removeMenu()
        {
            if (this._menues.Count > 1)
                this._menues.Pop(); 
        }
        public void showMenu()
        {
            Console.Clear();
            if (this._menues.Count != 0)
                buildMenu(this._menues.Peek());
        }
        private void printGreen(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t" + str + ":");
            Console.ResetColor();
        }
    }

}

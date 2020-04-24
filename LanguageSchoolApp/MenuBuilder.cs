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

        CoursesByLevel,

        ClearCart,
        RemoveCourse,
        BuyCourses,
        AddCourse
    }

    //BUILDER
    public interface IMenuBuilder
    {
        public abstract void addMenu(MENU menu);
        public abstract void removeMenu();
        public abstract void showMenu();
       
    }
    public class MenuBuilder : IMenuBuilder
    {
        private Stack<MENU> _menues = new Stack<MENU>();
        private User _user;
        private CourseBase data = CourseBase.Instance();
        private Printer _printer = new Printer();
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
            Console.WriteLine("      [M]ain menu\n      [P]ersonal Area Menu\n      [C]ourses Menu\n      [Ca]rt Menu");
            Console.WriteLine("      [B]ack\n      [E]xit");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        private void printUserInfo()
        {
            this._user.printInfo();
            printCourses();
            printCart();
        }
        private void printCourses()
        {
            List<ICourse> courses = this._user.GetCourses();
            if (courses.Count != 0)
            {
                Console.WriteLine("\tYour courses: ");
                foreach (Course cr in courses)
                {
                    _printer.PrintColour(new PrintYellow(), "\t" + cr.name + ", ");
                    Console.Write(cr.cost + ", " );
                    _printer.PrintColour(new PrintGreen(), cr.level + "\n\n");
                }
            }
            else
            {
                _printer.PrintColour(new PrintRed(), "Your courses list is empty.\n");
            }
        }
        private void printCart()
        {
            List<ICourse> cart = this._user.GetCart();
            if (cart.Count != 0)
            {
                Console.WriteLine("\tYour courses cart list: ");
                foreach (Course cr in cart)
                {
                    _printer.PrintColour(new PrintYellow(), "\t" + cr.name + ", ");
                    Console.WriteLine(cr.cost + "\n");
                    //_printer.PrintColour(new PrintGreen(), cr.level + "\n");
                }
            }
            else
            {
                _printer.PrintColour(new PrintRed(), "Your cart list is empty.\n");
            }
        }

        private void buildMenu(MENU menu)
        {
            header();
            switch (menu)
            {
                case MENU.MainMenu:
                    {
                        this._printer.PrintColour(new PrintGreen(), "\t\t\tMain Menu\n");
                        break;
                    }
                case MENU.PersonalAreaMenu:
                    {
                        this._printer.PrintColour(new PrintGreen(), "\t\t\tPersonal Area Menu\n");
                        printUserInfo();
                        break;
                    }
                case MENU.AddCourse:
                    {
                        this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

                        data.printCourses();
                        Console.WriteLine("Enter the name od course: ");
                        string input = "";
                        input = Console.ReadLine().ToLower().ToString();
                        ICourse course = this.data.GetCourse(input);
                        if (course != null)
                        {
                            if (this._user.ContainsInCart(course))
                            {
                                Console.WriteLine("You have already added this course to your cart.");
                            } else if (this._user.ContainsInCourses(course))
                            {
                                Console.WriteLine("You have already bought this course.");
                            }
                            else
                            {
                                this._user.AddCourseToCart(course);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, this course hasn`t exist, or your input has a typo");
                        }

                        break;
                    }
                case MENU.CartMenu:
                    {
                        this._printer.PrintColour(new PrintGreen(), "\t\t\tCart Menu\n");

                        printCart();
                        Console.WriteLine("[Buy] Courses");
                        Console.WriteLine("[Cl]ear cart");
                        Console.WriteLine("[R]emove course");
                        break;
                    }
                case MENU.BuyCourses:
                    {
                        if (this._user.BuyCourses())
                        {
                            Console.WriteLine("Successfully bought!");
                        } else
                        {
                            Console.WriteLine("Sorry, you don't have enoght money.");
                        }
                        break;
                    }
                case MENU.ClearCart:
                    {
                        if (this._user.ClearCart())
                        {
                            Console.WriteLine("Successfully done!");
                        } else
                        {
                            Console.WriteLine("Something went wrong. But we are working!");
                        }
                        break;
                    }
                case MENU.RemoveCourse:
                    {
                        printCart();
                        Console.WriteLine("Enter the name of course you want to delete: ");
                        string input = Console.ReadLine().ToLower().ToString();
                        if (this._user.RemoveCourseFromCart(input))
                        {
                            Console.WriteLine("Successfully deleted!");
                        }
                        else
                        {
                            Console.WriteLine("Sorry, this course hasn`t exist, or your input has a typo");
                        }
                        break;
                    }
                case MENU.CoursesMenu:
                    {
                        this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

                        data.printCourses();
                        Console.WriteLine("\n[add] Add course to cart\n[level] Show courses by level");
                        break;
                    }
                case MENU.BackMenu:
                    {
                        removeMenu();
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
                            
                            if (isNumber && level_int < 6 && level_int >= 0 )
                            {
                                LEVEL level_en = (LEVEL)Enum.ToObject(typeof(LEVEL), level_int);                                
                                data.PrintByLevel(level_en);
                                Console.WriteLine("Input: " + level_int);
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
            {
                buildMenu(this._menues.Peek());
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public interface IMenuStrategy // The 'Strategy' abstract class, receiver for Command and interface for Proxy
    {
        public void runMenu();
    }
    public class InterfaceMenu
    {
        private IMenuStrategy _strategy; //Strategy
        public void Show(IMenuStrategy strategy)
        {
            this._strategy = strategy;
            _strategy.runMenu();
        }
    }
    public class UserMenuBuilder : IMenuStrategy
    {
        IMenu menubuilder;
        Invoker invoker;
        User _user;
        MENU _menu = MENU.MainMenu;
        public UserMenuBuilder(User user)
        {
            this._user = user;
        }
        void ExecuteMenu(MENU _menu)
        {
            menubuilder.addMenu(_menu);

            DefaultCommand command = new DefaultCommand(menubuilder, _menu);

            invoker.AddCommand(command);
            invoker.Execute();
        }
        void UnExecuteMenu()
        {
            menubuilder.removeMenu();

            invoker.Unexecute();
        }
        public void runMenu()
        {
            menubuilder = new MenuBuilder(this._user);
            invoker = new Invoker(menubuilder);

            string input = "";
            bool isRunning = true;

            menubuilder.addMenu(MENU.MainMenu);

            while (isRunning)
            {
                menubuilder.showMenu();
                input = Console.ReadLine().ToLower().ToString();

                switch (input)
                {
                    case "e":
                        {
                            isRunning = false;
                            Console.WriteLine("\nSee you later in out LanguageSchool!\n");
                            break;
                        }
                    case "m":
                        {
                            this._menu = MENU.MainMenu;
                            this.ExecuteMenu(this._menu);
                            menubuilder.addMenu(_menu);

                            DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                            invoker.AddCommand(command);
                            invoker.Execute();

                            break;
                        }
                    case "p":
                        {
                            this._menu = MENU.PersonalAreaMenu;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "c":
                        {
                            this._menu = MENU.CoursesMenu;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "ca":
                        {
                            this._menu = MENU.CartMenu;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "buy":
                        {
                            if (this._menu == MENU.CartMenu)
                            {
                                this._menu = MENU.BuyCourses;
                                this.ExecuteMenu(this._menu);
                            }
                            break;
                        }
                    case "cl":
                        {
                            if (this._menu == MENU.CartMenu)
                            {
                                this._menu = MENU.ClearCart;
                                this.ExecuteMenu(this._menu);
                            }
                            break;
                        }
                    case "r":
                        {
                            if (this._menu == MENU.CartMenu)
                            {
                                this._menu = MENU.RemoveCourse;
                                this.ExecuteMenu(this._menu);
                            }
                            break;
                        }
                    case "b":
                        {
                            this._menu = MENU.BackMenu;

                            this.UnExecuteMenu();
                            break;
                        }
                    case "level":
                        {
                            if (this._menu == MENU.CoursesMenu)
                            {
                                this._menu = MENU.CoursesByLevel;
                                this.ExecuteMenu(this._menu);
                            }
                            break;
                        }
                    case "add":
                        {
                            if (this._menu == MENU.CoursesMenu)
                            {
                                this._menu = MENU.AddCourse;
                                this.ExecuteMenu(this._menu);
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
    public class AdminMenuBuilder : IMenuStrategy //real subject
    {
        IMenu menubuilder;
        Invoker invoker;
        MENU _menu = MENU.MainMenu;
        Admin _admin;
        public AdminMenuBuilder(Admin admin)
        {
            this._admin = admin;
        }
        void ExecuteMenu(MENU _menu)
        {
            menubuilder.addMenu(_menu);

            DefaultCommand command = new DefaultCommand(menubuilder, _menu);

            invoker.AddCommand(command);
            invoker.Execute();
        }
        void UnExecuteMenu()
        {
            menubuilder.removeMenu();

            invoker.Unexecute();
        }
        public void runMenu()
        {
            menubuilder = new MenuBuilder(this._admin);
            invoker = new Invoker(menubuilder);

            string input = "";
            bool isRunning = true;

            menubuilder.addMenu(MENU.MainMenu);

            while (isRunning)
            {
                menubuilder.showMenu();
                input = Console.ReadLine().ToLower().ToString();

                switch (input)
                {
                    case "e":
                        {
                            isRunning = false;
                            Console.WriteLine("\nSee you later in out LanguageSchool!\n");
                            break;
                        }
                    case "b":
                        {
                            this._menu = MENU.BackMenu;

                            this.UnExecuteMenu();
                            break;
                        }
                    case "m":
                        {
                            this._menu = MENU.MainMenu;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "c":
                        {
                            this._menu = MENU.CoursesMenu;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "add":
                        {

                            this._menu = MENU.AddCourse;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "rem":
                        {

                            this._menu = MENU.RemoveCourse;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    case "cl":
                        {

                            this._menu = MENU.CloneCourse;
                            this.ExecuteMenu(this._menu);

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

        }
    }
    //proxy     
    public class AdminAuthentication : IMenuStrategy
    {
        Admin _admin = null;
        InterfaceMenu menuStrategy = new InterfaceMenu();
        public AdminAuthentication(Admin admin)
        {
            this._admin = admin;
        }
        private string getPass()
        {
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }
        public void runMenu()
        {
            Console.Clear();
            Console.WriteLine("Login: ");
            string login = Console.ReadLine().ToString();
            Console.WriteLine("Password: ");

            string pass = getPass();
            if (_admin != null)
            {
                if (_admin.name.Equals(login) && _admin.isPasswordRight(pass))
                {
                    menuStrategy.Show(new AdminMenuBuilder(_admin));
                }
                else
                {
                    Console.WriteLine("\nLogin or password is wrong!");
                    System.Threading.Thread.Sleep(1200);
                }
            }
        }
    }

    public class UserAuthentication : IMenuStrategy
    {        
        User _user = null;
        InterfaceMenu menuStrategy = new InterfaceMenu();
        public UserAuthentication(User user)
        {
            this._user = user;
        }
        private string getPass()
        {
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }
        public void runMenu()
        {
            Console.Clear();
            Console.WriteLine("Login: ");
            string login = Console.ReadLine().ToString();
            Console.WriteLine("Password: ");

            string pass = getPass();
            if (_user != null)
            {
                if (_user.name.Equals(login) && _user.isPasswordRight(pass))
                {
                    menuStrategy.Show(new UserMenuBuilder(_user));
                }
                else
                {
                    Console.WriteLine("\nLogin or password is wrong!");
                    System.Threading.Thread.Sleep(1200);
                }
            }
        }
    }
}
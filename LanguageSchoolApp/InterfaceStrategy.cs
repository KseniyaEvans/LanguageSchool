using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public interface IMenuStrategy // The 'Strategy' abstract class, receiver for Command
    {
        public void runMenu(User _user);
    }
    public class InterfaceMenu
    {
        private IMenuStrategy _strategy; //Strategy
        public void Show(IMenuStrategy strategy, User _user)
        {
            this._strategy = strategy;
            _strategy.runMenu(_user);
        }
    }
    public class UserMenuBuilder : IMenuStrategy
    {
        //IMenuStrategy menubuilder;
        IMenu menubuilder;
        Invoker invoker;
        MENU _menu = MENU.MainMenu;

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
        public void runMenu(User _user)
        {
            menubuilder = new MenuBuilder(_user);
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
    public class AdminMenuBuilder : IMenuStrategy
    {
        IMenu menubuilder;
        Invoker invoker;
        MENU _menu = MENU.MainMenu;

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
        public void runMenu(User _user)
        {
            menubuilder = new MenuBuilder(_user);
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
                    case "b":
                        {
                            this._menu = MENU.BackMenu;

                            this.UnExecuteMenu();
                            break;
                        }
                    case "add":
                        {
                            this._menu = MENU.AddCourseToDatabase;
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
}
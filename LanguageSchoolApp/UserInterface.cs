using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    class UserInterface
    {

        MENU _menu = MENU.MainMenu;

        public void Run(User user)
        {
            string input = "";
            bool isRunning = true;

            MenuBuilderTemplate menubuilder = new MenuBuilder(user);
            Invoker invoker = new Invoker(menubuilder);

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
                    case "p":
                        {
                            this._menu = MENU.PersonalAreaMenu;
                            menubuilder.addMenu(_menu);

                            DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                            invoker.AddCommand(command);
                            invoker.Execute();

                            break;
                        }
                    case "c":
                        {
                            this._menu = MENU.CoursesMenu;
                            menubuilder.addMenu(_menu);

                            DefaultCommand command = new DefaultCommand(menubuilder, _menu);
                            
                            invoker.AddCommand(command);
                            invoker.Execute();
                            
                            break;
                        }
                    case "ca":
                        {
                            this._menu = MENU.CartMenu;
                            menubuilder.addMenu(_menu);

                            DefaultCommand command = new DefaultCommand(menubuilder, _menu);
                            
                            invoker.AddCommand(command);
                            invoker.Execute();
                            
                            break;
                        }
                    case "buy":
                        {
                            if (this._menu == MENU.CartMenu)
                            {
                                this._menu = MENU.BuyCourses;
                                menubuilder.addMenu(_menu);

                                DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                                invoker.AddCommand(command);
                                invoker.Execute();
                            }
                            break;
                        }
                    case "cl":
                        {
                            if (this._menu == MENU.CartMenu)
                            {
                                this._menu = MENU.ClearCart;
                                menubuilder.addMenu(_menu);

                                DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                                invoker.AddCommand(command);
                                invoker.Execute();
                            }
                            break;
                        }
                    case "rem":
                        {
                            if (this._menu == MENU.CartMenu)
                            {
                                this._menu = MENU.RemoveCourse;
                                menubuilder.addMenu(_menu);

                                DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                                invoker.AddCommand(command);
                                invoker.Execute();
                            }
                            break;
                        }
                    case "b":
                        {
                            this._menu = MENU.BackMenu;
                            menubuilder.removeMenu();

                            invoker.Unexecute();
                            
                            break;
                        }
                    case "level":
                        {
                            if (this._menu == MENU.CoursesMenu)
                            {
                                this._menu = MENU.CoursesByLevel;
                                menubuilder.addMenu(_menu);

                                DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                                invoker.AddCommand(command);
                                invoker.Execute();
                            }
                            break;
                        }
                    case "add":
                        {
                            if (this._menu == MENU.CoursesMenu)
                            {
                                this._menu = MENU.AddCourse;
                                menubuilder.addMenu(_menu);

                                DefaultCommand command = new DefaultCommand(menubuilder, _menu);

                                invoker.AddCommand(command);
                                invoker.Execute();
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
}

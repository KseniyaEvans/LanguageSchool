using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    //public abstract class UserInterfaceTemplate // Template Method
    //{
    //    protected IMenuBuilder menubuilder;
    //    protected Invoker invoker;
    //    protected UserInterfaceTemplate(User _user)
    //    {
    //        this.menubuilder = new MenuBuilder(_user);
    //        this.invoker = new Invoker(this.menubuilder); 
    //    }
    //    protected virtual void ExecuteMenu(MENU _menu)
    //    {
    //        menubuilder.addMenu(_menu);

    //        DefaultCommand command = new DefaultCommand(menubuilder, _menu);

    //        invoker.AddCommand(command);
    //        invoker.Execute();
    //    }
    //    protected void UnExecuteMenu()
    //    {
    //        menubuilder.removeMenu();

    //        invoker.Unexecute();
    //    }
    //}
    //public class UserInterface //ONLY FOR USER
    //{
    //    //IMenuStrategy menubuilder;
    //    InterfaceMenu menubuilder = new InterfaceMenu();
    //    Invoker invoker;
    //    MENU _menu = MENU.MainMenu;

    //    void ExecuteMenu(MENU _menu)
    //    {
    //        menubuilder.addMenu(_menu);

    //        DefaultCommand command = new DefaultCommand(menubuilder, _menu);

    //        invoker.AddCommand(command);
    //        invoker.Execute();
    //    }
    //    void UnExecuteMenu()
    //    {
    //        menubuilder.removeMenu();

    //        invoker.Unexecute();
    //    }
    //    public void Run(User _user)
    //    {
    //        menubuilder = new UserMenuBuilder(_user);
    //        invoker = new Invoker(menubuilder);

    //        string input = "";
    //        bool isRunning = true;

    //        menubuilder.addMenu(MENU.MainMenu);

    //        while (isRunning)
    //        {
    //            menubuilder.RunMenu();
    //            input = Console.ReadLine().ToLower().ToString();

    //            switch (input)
    //            {
    //                case "e":
    //                    {
    //                        isRunning = false;
    //                        Console.WriteLine("\nSee you later in out LanguageSchool!\n");
    //                        break;
    //                    }
    //                case "m":
    //                    {
    //                        this._menu = MENU.MainMenu;
    //                        this.ExecuteMenu(this._menu);
    //                        menubuilder.addMenu(_menu);

    //                        DefaultCommand command = new DefaultCommand(menubuilder, _menu);

    //                        invoker.AddCommand(command);
    //                        invoker.Execute();

    //                        break;
    //                    }
    //                case "p":
    //                    {
    //                        this._menu = MENU.PersonalAreaMenu;
    //                        this.ExecuteMenu(this._menu);

    //                        break;
    //                    }
    //                case "c":
    //                    {
    //                        this._menu = MENU.CoursesMenu;
    //                        this.ExecuteMenu(this._menu);
                            
    //                        break;
    //                    }
    //                case "ca":
    //                    {
    //                        this._menu = MENU.CartMenu;
    //                        this.ExecuteMenu(this._menu);

    //                        break;
    //                    }
    //                case "buy":
    //                    {
    //                        if (this._menu == MENU.CartMenu)
    //                        {
    //                            this._menu = MENU.BuyCourses;
    //                            this.ExecuteMenu(this._menu);
    //                        }
    //                        break;
    //                    }
    //                case "cl":
    //                    {
    //                        if (this._menu == MENU.CartMenu)
    //                        {
    //                            this._menu = MENU.ClearCart;
    //                            this.ExecuteMenu(this._menu);
    //                        }
    //                        break;
    //                    }
    //                case "r":
    //                    {
    //                        if (this._menu == MENU.CartMenu)
    //                        {
    //                            this._menu = MENU.RemoveCourse;
    //                            this.ExecuteMenu(this._menu);
    //                        }
    //                        break;
    //                    }
    //                case "b":
    //                    {
    //                        this._menu = MENU.BackMenu;

    //                        this.UnExecuteMenu();
    //                        break;
    //                    }
    //                case "level":
    //                    {
    //                        if (this._menu == MENU.CoursesMenu)
    //                        {
    //                            this._menu = MENU.CoursesByLevel;
    //                            this.ExecuteMenu(this._menu);
    //                        }
    //                        break;
    //                    }
    //                case "add":
    //                    {
    //                        if (this._menu == MENU.CoursesMenu)
    //                        {
    //                            this._menu = MENU.AddCourse;
    //                            this.ExecuteMenu(this._menu);
    //                        }
    //                        break;
    //                    }
    //                default:
    //                    {
    //                        break;
    //                    }
    //            }
    //        }

    //    }
    //}
}

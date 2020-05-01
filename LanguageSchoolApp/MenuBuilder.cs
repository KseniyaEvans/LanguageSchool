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
        AddCourse,
        CloneCourse

    }

    public interface IMenu
    {
        public abstract void addMenu(MENU menu);
        public abstract void removeMenu();
        public void showMenu();
    }
    public abstract class IMenuBuilder
    {
        protected User _user = null;
        protected Admin _admin = null;
        protected CourseBase data = CourseBase.Instance();
        protected Printer _printer = new Printer();
        public IMenuBuilder(User user)
        {
            this._user = user;
        }
        public IMenuBuilder(Admin admin)
        {
            this._admin = admin;
        }
        public IMenuBuilder() { }
        //BUILDER and template method
        public virtual void SetHeader()
        {
            Console.WriteLine("      NAVIGATION: ");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\t[M]ain menu");
            Console.WriteLine("\t[C]ourses Menu");
            SetNavigation();
            Console.WriteLine("\t[B]ack\n\t[E]xit");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public virtual void SetNavigation()
        { 
            Console.WriteLine("\t[P]ersonal Area Menu");
            Console.WriteLine("\t[Ca]rt Menu\n");
        }
        public abstract void SetContent();
        public virtual void SetFooter()
        {
            Console.WriteLine("What menu would you like to go?");
        }
    }
    public class buildAddCourseMenu : IMenuBuilder
    {
        public buildAddCourseMenu(User user) : base(user) { }
        public override void SetContent()
        {
            this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

            data.printCourses();
            Console.WriteLine("Enter the number of the course's name: ");
            string input = "";
            input = Console.ReadLine().ToLower().ToString();
            int level_int = 0;
            bool isNumber = Int32.TryParse(input, out level_int);
            ICourse course = this.data.GetCourse(level_int);
            if (course != null)
            {
                if (this._user.ContainsInCart(course))
                {
                    Console.WriteLine("You have already added this course to your cart.");
                }
                else if (this._user.ContainsInCourses(course))
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
        }
    }
    public class buildCartMenu : IMenuBuilder
    {
        public buildCartMenu(User user) : base(user) { }
        private void printCart()
        {
            List<ICourse> cart = this._user.GetCart();
            if (cart.Count != 0)
            {
                Console.WriteLine("\tYour courses cart list: ");
                foreach (Course cr in cart)
                {
                    _printer.PrintColour(new PrintYellow(), "\t" + cr.name + ", ");
                    Console.WriteLine(cr.cost);
                }
                Console.WriteLine(" ");
            }
            else
            {
                _printer.PrintColour(new PrintRed(), "Your cart list is empty.\n");
            }
        }
        public override void SetContent()
        {
            this._printer.PrintColour(new PrintGreen(), "\t\t\tCart Menu\n");

            printCart();
            Console.WriteLine("[Buy] Courses");
            Console.WriteLine("[Cl]ear cart");
            Console.WriteLine("[R]emove course");
        }
    }
    public class buildPersonalAreaMenu : IMenuBuilder
    {
        public buildPersonalAreaMenu(User user) : base(user) { }
        private void printCourses()
        {
            List<ICourse> courses = this._user.GetCourses();
            if (courses.Count != 0)
            {
                Console.WriteLine("\tYour courses: ");
                foreach (Course cr in courses)
                {
                    _printer.PrintColour(new PrintYellow(), "\t" + cr.name + ", ");
                    Console.Write(cr.cost + ", ");
                    _printer.PrintColour(new PrintGreen(), cr.level + "\n");
                }
                Console.WriteLine("\n");
            }
            else
            {
                _printer.PrintColour(new PrintRed(), "Your courses list is empty.\n\n");
            }
        }
        public override void SetContent()
        {
            this._printer.PrintColour(new PrintGreen(), "\t\t\tPersonal Area Menu\n");
            this._user.printInfo();
            printCourses();
        }
    }
    public class buildMainMenu : IMenuBuilder
    {
        public buildMainMenu() { }
        public buildMainMenu(Admin admin) : base(admin) { }
        public override void SetNavigation()
        {
            if (this._admin != null) { }
            else
            {
                Console.WriteLine("\t[P]ersonal Area Menu");
                Console.WriteLine("\t[Ca]rt Menu\n");
            }
        }
        public override void SetContent()
        {
            this._printer.PrintColour(new PrintGreen(), "\t\t\tMain Menu\n");
        }
    }
    public class buildCoursesByLevel : IMenuBuilder
    {
        public override void SetContent()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("[0] A1\n[1] A2\n[2] B1\n[3] B2\n[4] C1\n[5] C2");
                Console.WriteLine("Enter the level: ");

                string input = Console.ReadKey(true).KeyChar.ToString().ToLower();
                int level_int = 0;
                bool isNumber = Int32.TryParse(input, out level_int);

                if (isNumber && level_int < 6 && level_int >= 0)
                {
                    LEVEL level_en = (LEVEL)Enum.ToObject(typeof(LEVEL), level_int);
                    data.PrintByLevel(level_en);
                    Console.WriteLine("Input: " + level_int);
                }
                else
                {
                    isRunning = false;
                }
            }
        }
    }
    public class buildCoursesMenu : IMenuBuilder
    {
        public buildCoursesMenu(User user) : base(user) { }
        public buildCoursesMenu(Admin admin) : base(admin) { }
        public override void SetContent()
        {
            this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

            data.printCourses();
        }
        public override void SetNavigation()
        {
            if (this._admin != null) { }
        }
        public override void SetFooter()
        {
            if (this._admin != null)
            {
                Console.WriteLine("\n[add] Add course to database\n[rem] Remove course from database\n[cl] Clone course\n");
            } else if (this._user != null)
            {
                Console.WriteLine("\n[add] Add course to cart\n[level] Show courses by level\n");
            }

            Console.WriteLine("What menu would you like to go?");
        }
    }
    public class buildClearCart : IMenuBuilder
    {
        public buildClearCart(User user) : base(user) { }
        public override void SetContent()
        {
            if (this._user.ClearCart())
            {
                Console.WriteLine("Successfully done!");
            }
            else
            {
                Console.WriteLine("Something went wrong. But we are working!");
            }
        }
    }
    public class buildRemoveCourse : IMenuBuilder
    {
        public buildRemoveCourse(User user) : base(user) { }
        private void printCart()
        {
            List<ICourse> cart = this._user.GetCart();
            if (cart.Count != 0)
            {
                Console.WriteLine("\tYour courses cart list: ");
                foreach (Course cr in cart)
                {
                    _printer.PrintColour(new PrintYellow(), "\t" + cr.name + ", ");
                    Console.WriteLine(cr.cost);
                }
                Console.WriteLine(" ");
            }
            else
            {
                _printer.PrintColour(new PrintRed(), "Your cart list is empty.\n");
            }
        }
        public override void SetContent()
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
        }
    }
    public class buildBuyCourses : IMenuBuilder
    {
        public buildBuyCourses(User user) : base(user) { }
        public override void SetContent()
        {
            if (this._user.BuyCourses())
            {
                Console.WriteLine("Successfully bought!");
            }
            else
            {
                Console.WriteLine("Sorry, you don't have enoght money.");
            }
        }
    }
    public class buildAddCourseToDatabase : IMenuBuilder
    {
        public buildAddCourseToDatabase(Admin admin) : base(admin)
        {
        }
        CourseCreator courseCreator = new CourseCreator();
        public override void SetContent()
        {
            data.printCourses();

            this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

            Console.WriteLine("Enter the name of course: ");
            string name = Console.ReadLine().ToLower().ToString();

            Console.WriteLine("Enter the level of course: ");
            Console.WriteLine("[0] A1\t[1] A2\t[2] B1\t[3] B2\t[4] C1\t[5] C2");
            string level = Console.ReadLine().ToLower().ToString();
            LEVEL levelVal = LEVEL.A1;
            try
            {
                levelVal = (LEVEL)Enum.Parse(typeof(LEVEL), level, true);
            } catch(ArgumentException) {
                Console.WriteLine("Setted level as A1: ");
            }

            Console.WriteLine("Enter the cost of course: ");
            int cost = Convert.ToInt32(Console.ReadLine());

            this._admin.prepareCourse(new DynamicState(name, levelVal, cost));
            courseCreator.accept(_admin);
        }
        public override void SetNavigation()
        {
            if (this._admin != null) { }
        }
    }
    public class buildCloneCourseMenu : IMenuBuilder
    {
        public buildCloneCourseMenu(Admin admin) : base(admin)
        {
        }
        CourseCreator courseCreator = new CourseCreator();
        public override void SetContent()
        {
            data.printCourses();

            this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

            Console.WriteLine("Enter the number of the course's name: ");
            string name = Console.ReadLine().ToLower().ToString();
            int level_int = 0;
            bool isNumber = Int32.TryParse(name, out level_int);
            this.data.CloneCourse(level_int);
        }
        public override void SetNavigation()
        {
            if (this._admin != null) { }
        }
    }
    
    public class buildRemoveCourseFromDatabase : IMenuBuilder
    {
        public buildRemoveCourseFromDatabase(Admin admin) : base(admin)
        {
        }
        CourseCreator courseCreator = new CourseCreator();
        public override void SetContent()
        {
            data.printCourses();

            this._printer.PrintColour(new PrintGreen(), "\t\t\tCourses Menu\n");

            Console.WriteLine("Enter the number of the course's name: ");
            string name = Console.ReadLine().ToLower().ToString();
            int level_int = 0;
            bool isNumber = Int32.TryParse(name, out level_int);
            if (this.data.RemoveCourse(level_int))
            {
                Console.WriteLine("Successfully removed!");
            }
            else {
                Console.WriteLine("Sorry, this course does not exist!");
            }
        }
        public override void SetNavigation()
        {
            if (this._admin != null) { }
        }
    }
    public class Director //Director
    {
        private IMenuBuilder _builder;

        public IMenuBuilder Builder
        {
            set { _builder = value; }
        }
        public void buildFullMenu()
        {
            this._builder.SetHeader();
            this._builder.SetContent();
            this._builder.SetFooter();
        }
    }
    public class MenuBuilder : IMenu
    {
        private Stack<MENU> _menues = new Stack<MENU>();
        private User _user = null;
        private Admin _admin = null;
        Director director = new Director();
        IMenuBuilder builder = new buildMainMenu();
        public MenuBuilder(User user)
        {
            this._user = user;
        }
        public MenuBuilder(Admin admin)
        {
            this._admin = admin;
        }
        public void addMenu(MENU menu)
        {
            this._menues.Push(menu);
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
                if (this._admin != null)
                {
                    buildAdminMenu(this._menues.Peek());
                }
                else
                {
                    buildUserMenu(this._menues.Peek());
                }
            }
        }
        private void buildUserMenu(MENU menu)
        {
            switch (menu)
            {
                case MENU.MainMenu:
                    {
                        this.builder = new buildMainMenu();
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.PersonalAreaMenu:
                    {
                        this.builder = new buildPersonalAreaMenu(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.AddCourse:
                    {
                        this.builder = new buildAddCourseMenu(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.CartMenu:
                    {
                        this.builder = new buildCartMenu(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.BuyCourses:
                    {
                        this.builder = new buildBuyCourses(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.ClearCart:
                    {
                        this.builder = new buildClearCart(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.RemoveCourse:
                    {
                        this.builder = new buildRemoveCourse(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.CoursesMenu:
                    {
                        this.builder = new buildCoursesMenu(this._user);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.BackMenu:
                    {
                        removeMenu();
                        break;
                    }
                case MENU.CoursesByLevel:
                    {
                        this.builder = new buildCoursesByLevel();
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unknown command. Repeat please.");
                        break;
                    }
            }
            
        }

        private void buildAdminMenu(MENU menu)
        {
            switch (menu)
            {
                case MENU.MainMenu:
                    {
                        this.builder = new buildMainMenu(this._admin);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.RemoveCourse:
                    {
                        this.builder = new buildRemoveCourseFromDatabase(this._admin);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.AddCourse:
                    {
                        this.builder = new buildAddCourseToDatabase(this._admin);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.CloneCourse:
                    {
                        this.builder = new buildCloneCourseMenu(this._admin);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.CoursesMenu:
                    {
                        this.builder = new buildCoursesMenu(this._admin);
                        director.Builder = this.builder;
                        director.buildFullMenu();
                        break;
                    }
                case MENU.BackMenu:
                    {
                        removeMenu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unknown command. Repeat please.");
                        break;
                    }
            }

        }

    }
}

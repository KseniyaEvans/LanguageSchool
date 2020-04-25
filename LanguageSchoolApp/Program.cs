using System;

namespace LanguageSchoolApp
{
    class Program
    {
        //Main Patterns: command, builder, prototype, state, visitor, flyweight, strategy, template method
        //Additional Patterns: delegate, singleton
        static void Main(string[] args)
        {
            User sophia = new User("Sophia Lakhman", 1000);
            Admin admin = new Admin("Kseniya");
            Database database = new Database(admin);

            InterfaceMenu menuStrategy = new InterfaceMenu();
            Console.WriteLine("[A]dmin or [U]ser? Type [a] or [u]");
            string role = Console.ReadKey(true).KeyChar.ToString().ToLower();

            if (role.Equals("a"))
            {
                menuStrategy.Show(new AdminMenuBuilder(), admin);
            } else if (role.Equals("u"))
            {
                menuStrategy.Show(new UserMenuBuilder(), sophia);
            }

            

        }
        public class Database
        {
            CourseCreator courseCreator = new CourseCreator();
            public Database(Admin admin)
            {
                admin.prepareCourse(new DynamicState("german", LEVEL.A1, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("chinese", LEVEL.A1, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("french", LEVEL.A1, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("ukrainian", LEVEL.A2, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("russian", LEVEL.A2, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("english", LEVEL.B1, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("norwegian", LEVEL.B2, 150));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("spanish", LEVEL.C1, 150));
                courseCreator.accept(admin);
            }
            
        }
    }
}

using System;

namespace LanguageSchoolApp
{
    class Program
    {
        //Main Patterns: command, builder, prototype, state, visitor, flyweight
        //Additional Patterns: delegate, pattern methon, singleton

        static void Main(string[] args)
        {
            User vasya = new User("Vasya");
            Admin admin = new Admin("Kseniya");

            Database database = new Database(admin);

            LANGUAGE nativeLan = LANGUAGE.Ukrainian;
            User user_1 = new User("Sophia Lakhman", 1000, nativeLan);
            UserInterface terminal = new UserInterface();
            terminal.Run(user_1);          
        }
        public class Database
        {
            CourseCreator courseCreator = new CourseCreator();
            public Database(Admin admin)
            {
                admin.prepareCourse(new DynamicState("german", LEVEL.A1, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("chinese", LEVEL.A1, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("french", LEVEL.A1, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("ukrainian", LEVEL.A2, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("russian", LEVEL.A2, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("english", LEVEL.B1, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("norwegian", LEVEL.B2, 150, 1));
                courseCreator.accept(admin);

                admin.prepareCourse(new DynamicState("spanish", LEVEL.C1, 150, 1));
                courseCreator.accept(admin);
            }
            
        }
    }
}

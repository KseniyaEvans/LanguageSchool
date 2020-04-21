using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public enum LANGUAGE
    {
        NON,
        Ukrainian,
        Russian,
        English,
        Norwegian, 
        Spanish, 
        French,
        Chinese,
        German
    }
    public abstract class IUser : ICourseVisitor // visitor
    {
        public string name;
        
        protected int money;
        public LANGUAGE nativeLanguage;
        protected List<CourseTemplate> courses = new List<CourseTemplate>();
        protected List<CourseTemplate> cart = new List<CourseTemplate>();
        public abstract void visit(CourseCreator courseCreator);
    }
    public class User : IUser
    {
        public User(string _name)
        {
            this.name = _name;
        }
        public override void visit(CourseCreator courseCreator)
        {
            Console.WriteLine(this.name + ", you are not allowed to create a course");
        }
        public User()
        {
            this.money = 0;
            this.name = "guest";
        }

        public User(string name, int money)
        {
            this.name = name;
            this.money = money;
        }
        public User(string name, int money, LANGUAGE language)
        {
            this.name = name;
            this.money = money;
            this.nativeLanguage = language;
        }
        public void printInfo()
        {
            Console.WriteLine("Hi, " + this.name + "!");
            Console.WriteLine("Your balance: " + this.money + " UAN");
            Console.WriteLine("Your native language: " + this.nativeLanguage);
            printCourses();
            printCart();


        }
        public void printCourses()
        {
            if (this.courses.Count != 0)
            {
                Console.WriteLine("Your courses list: ");
                foreach (Course cr in this.courses)
                {
                    Console.WriteLine(cr.name);
                }
            }
            else
            {
                Console.WriteLine("Your courses list is empty. Would you like to buy something? :) ");
            }
        }
        public bool AddCourseToCart(CourseTemplate course)
        {
            cart.Add(course);
            return true;
        }
        public bool RemoveCourseFromCart(CourseTemplate course)
        {
            this.cart.Remove(course);
            return true;
        }
        public void printCart()
        {
            if (this.cart.Count != 0)
            {
                Console.WriteLine("Your courses list: ");
                foreach (Course cr in this.cart)
                {
                    Console.WriteLine(cr.name);
                }
            } else
            {
                Console.WriteLine("Your cart list is empty. Would you like to buy something? :) ");
            }
        }
        public bool ClearCart()
        {
            this.cart.Clear();
            return true;
        }
        public bool BuyCourses()
        {
            if (this.money == 0 || this.cart.Count == 0)
            {
                return false;
            }
            else
            {
                int moneyNeeded = 0;
                foreach (Course item in this.cart)
                {
                    moneyNeeded += item.cost;
                }
                if (this.money < moneyNeeded)
                {
                    return false;
                }
                else
                {
                    this.money -= moneyNeeded;
                    this.courses.AddRange(cart);
                    this.cart.Clear();
                    return true;
                }
            }
        }
        public List<CourseTemplate> GetMyCourses()
        {
            return this.courses;
        }
    }

    public class Admin : User
    {
        CourseState preparedCourse = new DefaultState();
        public Admin(string _name) : base(_name) { }
        public void prepareCourse(CourseState state)
        {
            this.preparedCourse = state;
        }

        //Prototype, State and Visitor together (and Flyweight additional)
        public override void visit(CourseCreator courseCreator)
        {
            CourseTemplate ct = courseCreator.prototype.create(preparedCourse);
            courseCreator.database.AddCourse(ct.name, ct);
        }
    }
}

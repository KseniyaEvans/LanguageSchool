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
    public class User
    {
        private string name;
        private int money;
        public LANGUAGE nativeLanguage;
        private List<Course> courses = new List<Course>();
        private List<Course> cart = new List<Course>();

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
        public bool AddCourseToCart(Course course)
        {
            cart.Add(course);
            return true;
        }
        public bool RemoveCourseFromCart(Course course)
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
        public List<Course> GetMyCourses()
        {
            return this.courses;
        }
    }
}

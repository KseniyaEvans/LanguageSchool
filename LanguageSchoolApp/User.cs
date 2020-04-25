using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public abstract class IUser : ICourseVisitor // visitor
    {
        public string name;
        protected int money;
        protected List<ICourse> _courses = new List<ICourse>();
        protected List<ICourse> _cart = new List<ICourse>();
        public abstract void visit(CourseCreator courseCreator);
    }
    public class User : IUser
    {
        public User(string _name)
        {
            this.name = _name;
            this.money = 0;
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
        public List<ICourse> GetCourses()
        {
            return this._courses;
        }
        public List<ICourse> GetCart()
        {
            return this._cart;
        }
        public bool ClearCart()
        {
            this._cart.Clear();
            return true;
        }
        public bool BuyCourses()
        {
            if (this.money == 0 || this._cart.Count == 0)
            {
                return false;
            }
            else
            {
                int moneyNeeded = 0;
                foreach (Course item in this._cart)
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
                    this._courses.AddRange(this._cart);
                    ClearCart();
                    return true;
                }
            }
        }
        public bool ContainsInCart(ICourse course)
        {
            if (this._cart.Contains(course))
            {
                return true;
            }    
            return false;
        }
        public bool ContainsInCourses(ICourse course)
        {
            if (this._courses.Contains(course))
            {
                return true;
            }
            return false;
        }
        private ICourse GetCourseFromCart(string name)
        {
            foreach(ICourse cr in this._cart)
            {
                if (cr.name.Equals(name))
                {
                    return cr;
                }                
            }
            return null;
        }
        public bool AddCourseToCart(ICourse course)
        {
            this._cart.Add(course);
            return true;
        }
        public bool RemoveCourseFromCart(string name)
        {
            ICourse cr = GetCourseFromCart(name);
            if (cr == null)
            {
                return false;
            }
            if (this._cart.Remove(cr))
            {
                return true;
            }
            return false;
        }
        public void printInfo()
        {
            Console.WriteLine("Hi, " + this.name + "!");
            Console.WriteLine("Balance: " + this.money + " UAN");
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
            ICourse ct = courseCreator.prototype.create(preparedCourse);
            courseCreator.database.AddCourse(ct.name, ct);
        }
    }
}

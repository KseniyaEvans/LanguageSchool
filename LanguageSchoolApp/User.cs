using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public abstract class IUser : ICourseVisitor // concrete visitor
    {
        public string name;
        protected int money;
        protected string password = "";
        protected List<ICourse> _courses = new List<ICourse>();
        protected List<ICourse> _cart = new List<ICourse>();
        public abstract void visit(CourseCreator courseCreator);
        public abstract void visit(CourseRemover courseRemover);
        public virtual bool isPasswordRight(string pass)
        {
            return this.password.Equals(pass);
        }
    }
    public class User : IUser
    {
        public User(string _name, string pass)
        {
            this.name = _name;
            this.money = 0;
            this.password = pass;
        }
        public override void visit(CourseCreator courseCreator)
        {
            Console.WriteLine(this.name + ", you are not allowed to create a course");
        }
        public override void visit(CourseRemover courseRemover)
        {
            Console.WriteLine(this.name + ", you are not allowed to delete a course");
        }
        public User(string name, string pass, int money)
        {
            this.name = name;
            this.money = money;
            this.password = pass;
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

    public class Admin : IUser
    {
        CourseState preparedCourse = new DefaultState();
        public Admin(string login, string password)
        {
            this.name = login;
            this.money = 0;
            this.password = password;
        }
        public void prepareCourse(CourseState state)
        {
            this.preparedCourse = state;
        }
        //Prototype, State and Visitor together
        public override void visit(CourseCreator courseCreator)
        {
            ICourse ct = courseCreator.prototype.create(preparedCourse);
            courseCreator.database.AddCourse(ct);
        }
        public override void visit(CourseRemover courseRemover)
        {
            courseRemover.database.RemoveCourse(courseRemover.key);
        }
    }

}


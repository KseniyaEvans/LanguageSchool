using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace LanguageSchoolApp
{
    interface ICourseBase
    {
        public ICourse GetCourse(int key);
        public bool AddCourse(ICourse course);
        public bool CloneCourse(int key);
        public bool RemoveCourse(int key);
        public void printCourses();
        public void PrintByLevel(LEVEL level);
    }
    public class CourseBase : ICourseBase//Flyweight
    {
        private Dictionary<int, ICourse> _courses = new Dictionary<int, ICourse>();
        private static CourseBase _instance;
        private static int count = 1;
        private Printer _printer = new Printer();
        private CourseBase() { }
        public static CourseBase Instance() //Singleton
        {
            if (_instance == null)
            {
                _instance = new CourseBase();
            }
            return _instance;
        }
        public ICourse GetCourse(int key)
        {
            if (this._courses.ContainsKey(key))
            {
                return this._courses[key];
            }
            return null;
        }
        public bool CloneCourse(int key)
        {
            ICourse cr = GetCourse(key);
            AddCourse((ICourse)cr.Clone());
            return true;
        }
        public bool AddCourse(ICourse course)
        {
            if (course == null)
            {
                Console.WriteLine("Sorry, your course is null");
                return false;
            }
            
            this._courses.Add(count, course);
            count += 1;
            return true;
        }
        public bool RemoveCourse(int key)
        {
            if (GetCourse(key)!= null)
            {
                this._courses.Remove(key);
                return true;
            }
            return false;
        }
        public void printCourses()
        {
            foreach (KeyValuePair<int, ICourse> cs in this._courses)
            {
                Console.Write("\t\t");
                Console.BackgroundColor = ConsoleColor.Red;
                this._printer.PrintColour(new PrintYellow(), "\t\t[" + cs.Key.ToString() + "] " + cs.Value.name);
                Console.Write(", " + cs.Value.cost + " UAN");
                Console.WriteLine(", " + cs.Value.level);
            }
            Console.WriteLine("");
        }
        public void PrintByLevel(LEVEL level)
        {
            Console.Write("\n\t\tCourses by level ");
            this._printer.PrintColour(new PrintGreen(), level.ToString() + "\n");

            foreach (KeyValuePair<int, ICourse> cs in this._courses)
            {
                if (cs.Value.level == level)
                {
                    this._printer.PrintColour(new PrintYellow(), "\t\t[" + cs.Key.ToString() + "] " + cs.Value.name);
                    Console.WriteLine(", " + cs.Value.cost + " UAN");
                }
            }
            Console.WriteLine("");
        }
    }
}

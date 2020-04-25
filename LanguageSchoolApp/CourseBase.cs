using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LanguageSchoolApp
{
    interface ICourseBase
    {
        public ICourse GetCourse(string key);
        public bool AddCourse(string key, ICourse course);
        public bool RemoveCourse(string key);
        public void printCourses();
        public void PrintByLevel(LEVEL level);
    }
    public class CourseBase : ICourseBase//Flyweight
    {
        private Dictionary<string, ICourse> _courses = new Dictionary<string, ICourse>();
        private static CourseBase _instance;
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
        public ICourse GetCourse(string key)
        {
            if (this._courses.ContainsKey(key))
            {
                return this._courses[key];
            }
            return null;
        }
        public bool AddCourse(string key, ICourse course)
        {
            if (this._courses.ContainsKey(key))
            {
                Console.WriteLine("Sorry, such course has already exist");
                return false;
            }
            if (course == null)
            {
                Console.WriteLine("Sorry, your course is null");
                return false;
            }
            
            this._courses.Add(key, course);
            return true;
        }
        public bool RemoveCourse(string key)
        {
            if (this._courses.ContainsKey(key))
            {
                this._courses.Remove(key);
                return true;
            }
            return false;
        }
        public void printCourses()
        {
            foreach (KeyValuePair<string, ICourse> cs in this._courses)
            {
                Console.Write("\t\t");
                Console.BackgroundColor = ConsoleColor.Red;
                this._printer.PrintColour(new PrintYellow(), cs.Key);
                Console.Write(", " + cs.Value.cost + " UAN");
                Console.WriteLine(", " + cs.Value.level);
            }
            Console.WriteLine("");
        }
        public void PrintByLevel(LEVEL level)
        {
            Console.Write("\n\t\tCourses by level ");
            this._printer.PrintColour(new PrintGreen(), level.ToString() + "\n");

            foreach (KeyValuePair<string, ICourse> cs in this._courses)
            {
                if (cs.Value.level == level)
                {
                    this._printer.PrintColour(new PrintYellow(), "\t\t" + cs.Key);
                    Console.WriteLine(", " + cs.Value.cost + " UAN");
                }
            }
            Console.WriteLine("");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LanguageSchoolApp
{
    interface ICourseBase
    {
        public CourseTemplate GetCourse(string key);
        public bool AddCourse(string key, CourseTemplate course);
        public void printCourses();
        public void PrintByLevel(LEVEL level);
        public void PrintColour(PrintColourStrategy printstrategy, string message);


    }
    public class CourseBase : ICourseBase//Flyweight
    {
        private static CourseBase _instance;
        private PrintColourStrategy _printstrategy; //Strategy
        protected CourseBase() { }
        public static CourseBase Instance() //Singleton
        {
            if (_instance == null)
            {
                _instance = new CourseBase();
            }
            return _instance;
        }
        private Dictionary<string, CourseTemplate> _courses= new Dictionary<string, CourseTemplate>();
        public CourseTemplate GetCourse(string key)
        {
            if (this._courses.ContainsKey(key))
            {
                return this._courses[key];
            }
            return null;
        }
        public bool AddCourse(string key, CourseTemplate course)
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

        public void printCourses()
        {
            Console.WriteLine("\nHi! All courses we have: ");
            foreach (KeyValuePair<string, CourseTemplate> cs in this._courses)
            {
                Console.WriteLine("Course name: " + cs.Key);
            }
            Console.WriteLine("");
        }
        public void PrintColour(PrintColourStrategy printstrategy, string message)
        {
            this._printstrategy = printstrategy;
            _printstrategy.Print(message);
        }
        public void PrintByLevel(LEVEL level)
        {
            Console.Write("\nCourses by level ");
            this.PrintColour(new PrintGreen(), level.ToString());

            foreach (KeyValuePair<string, CourseTemplate> cs in this._courses)
            {
                if (cs.Value.level == level)
                {
                    Console.Write("Course name: " + cs.Key);
                    Console.WriteLine("cost: " + cs.Value.cost);
                }
            }
            Console.WriteLine("");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public enum LEVEL
    {
        A1,
        A2,
        B1,
        B2,
        C1,
        C2
    }
    public class CoursesData
    {
        Composite french_l = new Composite(LANGUAGE.French);
        Composite german_l = new Composite(LANGUAGE.German);
        Composite russian_l = new Composite(LANGUAGE.German);

        Composite A1_l = new Composite(LEVEL.A1);
        Composite A2_l = new Composite(LEVEL.A2);
        Composite C2_l = new Composite(LEVEL.C2);

        public CoursesData()
        {
            buildDataBase();
        }
        private void buildDataBase()
        {
            Course name_ch = new Course(LANGUAGE.Chinese, LEVEL.A1, 350);
            Course name_en = new Course(LANGUAGE.English, LEVEL.A1, 200);
            Course name_uk = new Course(LANGUAGE.Ukrainian, LEVEL.C2, 350);
            Course name_no = new Course(LANGUAGE.Norwegian, LEVEL.A1, 150);
            Course name_sp = new Course(LANGUAGE.Spanish, LEVEL.C2, 400);
            Course name_ru = new Course(LANGUAGE.Russian, LEVEL.C2, 100);
            Course name_fr = new Course(LANGUAGE.French, LEVEL.A2, 125);
            Course name_ge = new Course(LANGUAGE.German, LEVEL.C2, 100);

            french_l.Add(name_ch);
            french_l.Add(name_en);
            french_l.Add(name_uk);
            french_l.Add(name_no);
            french_l.Add(name_sp);
            french_l.Add(name_ge);

            german_l.Add(name_uk);
            german_l.Add(name_sp);
            german_l.Add(name_ru);
            german_l.Add(name_ch);
            german_l.Add(name_no);

            russian_l.Add(name_ch);
            russian_l.Add(name_en);
            russian_l.Add(name_sp);

            A1_l.Add(name_ch);
            A1_l.Add(name_no);
            A1_l.Add(name_en);

            A2_l.Add(name_fr);

            C2_l.Add(name_uk);
            C2_l.Add(name_sp);
            C2_l.Add(name_ge);
            C2_l.Add(name_ru);
        }
        public void printAllByLanguage()
        {
            french_l.Display(4);
            german_l.Display(4);
            russian_l.Display(4);
        }
        public void printAllByLevel()
        {
            A1_l.Display(4);
            A2_l.Display(4);
            C2_l.Display(4);
        }
        public void printByLevel(LEVEL level)
        {
            switch(level)
            {
                case LEVEL.A1:
                    {
                        A1_l.Display(4);
                        break;
                    }
                case LEVEL.A2:
                    {
                        A2_l.Display(4);
                        break;
                    }
                case LEVEL.C1:
                    {
                        C2_l.Display(4);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Sorry, we don't have course with such level. All actual courses: ");
                        printAllByLevel();
                        break;
                    }
            }
        }
        public void printByLanguage(LANGUAGE lan)
        {
            switch(lan)
            {
                case LANGUAGE.German:
                    {
                        german_l.Display(4);
                        break;
                    }
                case LANGUAGE.French:
                    {
                        french_l.Display(4);
                        break;
                    }
                case LANGUAGE.Russian:
                    {
                        russian_l.Display(4);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Sorry, we don't have course teached in such language. All actual courses: ");
                        printAllByLanguage();
                        break;
                    }
            }
        }
    }

        //COMPOSITE
        public abstract class CourseComponent
        {
            public string name;

            public LEVEL level;
            public int cost;
            public int count;
            public string state;
            public CourseComponent(LANGUAGE name)
            {
                this.name = name.ToString();
                this.cost = 0;
            }
            public CourseComponent(LEVEL level)
            {
                this.name = level.ToString();
                this.cost = 0;
            }
            public CourseComponent(LANGUAGE name, LEVEL level, int cost)
            {
                this.name = name.ToString();
                this.level = level;
                this.cost = cost;
            }
            public abstract void Add(CourseComponent c);
            public abstract int GetCourseCount();
            public abstract void Remove(CourseComponent c);
            public abstract void Display(int depth);
        }
        class Composite : CourseComponent
        {
            private List<CourseComponent> _children = new List<CourseComponent>();
            public Composite(LANGUAGE name) : base(name)
            {
                this.count = 0;
                this.state = "composite";
            }
            public Composite(LEVEL level) : base(level)
            {
                this.count = 0;
                this.state = "composite";
            }

            public override void Add(CourseComponent component)
            {
                _children.Add(component);
                this.count += 1;
            }

            public override void Remove(CourseComponent component)
            {
                _children.Remove(component);
                this.count -= 1;
            }

            public override void Display(int depth)
            {
                Console.WriteLine(new String('-', depth) + name + " " + this.count.ToString());
                foreach (CourseComponent component in _children)
                {
                    component.Display(depth + 6);
                }
            }
            public override int GetCourseCount()
            {
                int num = 0;
                foreach (CourseComponent component in _children)
                {
                    num += component.GetCourseCount();

                }
                return num;
            }
        }

        public class Course : CourseComponent
        {
            public Course(LANGUAGE name, LEVEL level, int cost) : base(name, level, cost)
            {
                this.state = "course";
                this.count = 1;
            }
            public override void Add(CourseComponent c)
            {
                Console.WriteLine("Impossible operation");
            }
            public override void Remove(CourseComponent c)
            {
                Console.WriteLine("Impossible operation");
            }
            public override void Display(int depth)
            {
                Console.Write(new String('-', depth));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(name);
                Console.ResetColor();
            }
            public override int GetCourseCount()
            {
                return 1;
            }
        }
    
}

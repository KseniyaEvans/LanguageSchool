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

    //Prototype
    public abstract class CourseTemplate : ICloneable
    {
        protected string _name;
        protected LEVEL _level;
        protected int _cost;
        protected int _count;

        public string name { get { return _name; } }
        public LEVEL level { get { return _level; } }
        public int cost { get { return _cost; } }
        public int count { get { return _count; } }

        //State
        abstract public CourseState saveState();
        abstract public void loadState(CourseState state);
        abstract public CourseTemplate create(CourseState state);
        abstract public object Clone();
    }
    public abstract class CourseState
    {
        public string name;
        public LEVEL level;
        public int cost;
        public int count;
    }
    public class DefaultState : CourseState
    {
        public DefaultState()
        {
            this.name = "underfined name";
            this.level = LEVEL.A1;
            this.cost = 0;
            this.count = 0;
        }
    }
    public class DynamicState : CourseState
    {
        public DynamicState(string name, LEVEL level, int cost, int count)
        {
            this.name = name;
            this.level = level;
            this.cost = cost;
            this.count = count;
        }
    }
    public class Course : CourseTemplate //element
    {
        CourseState defState = new DefaultState();
        public Course()
        {
            loadState(defState);
        }
        public override CourseState saveState()
        {
            return new DynamicState(this._name, this._level, this._cost, this._count);
        }
        public override void loadState(CourseState state)
        {
            this._name = state.name;
            this._level = state.level;
            this._cost = state.cost;
            this._count = state.count;
        }
        public override CourseTemplate create(CourseState state)
        {
            loadState(state);
            return this.Clone() as CourseTemplate;
        }
        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //VISITOR
    interface ICourseElement //element
    {
        public void accept(ICourseVisitor visitor);
    }
    public interface ICourseVisitor // visitor
    {
        public void visit(CourseCreator courseCreator);
    }
    public class CourseCreator: ICourseElement //element
    {
        public Course prototype = new Course();
        public CourseBase database = CourseBase.Instance();
        public void accept(ICourseVisitor visitor)
        { 
            visitor.visit(this);
        }
    }
}

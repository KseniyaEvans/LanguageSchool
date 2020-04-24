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
    public abstract class ICourse : ICloneable
    {
        protected string _name;
        protected LEVEL _level;
        protected int _cost;

        public string name { get { return _name; } }
        public LEVEL level { get { return _level; } }
        public int cost { get { return _cost; } }

        //State
        abstract public CourseState saveState();
        abstract public void loadState(CourseState state);
        abstract public ICourse create(CourseState state);
        abstract public object Clone();
    }
    public abstract class CourseState
    {
        public string name;
        public LEVEL level;
        public int cost;
    }
    public class DefaultState : CourseState
    {
        public DefaultState()
        {
            this.name = "underfined name";
            this.level = LEVEL.A1;
            this.cost = 0;
        }
    }
    public class DynamicState : CourseState
    {
        public DynamicState(string name, LEVEL level, int cost)
        {
            this.name = name;
            this.level = level;
            this.cost = cost;
        }
    }
    public class Course : ICourse //element
    {
        CourseState _defaultState = new DefaultState();
        public Course()
        {
            loadState(_defaultState);
        }
        public override CourseState saveState()
        {
            return new DynamicState(this._name, this._level, this._cost);
        }
        public override void loadState(CourseState state)
        {
            this._name = state.name;
            this._level = state.level;
            this._cost = state.cost;
        }
        public override ICourse create(CourseState state)
        {
            loadState(state);
            return this.Clone() as ICourse;
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

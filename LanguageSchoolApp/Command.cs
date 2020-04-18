using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Unexecute();
    }

    public class DefaultCommand : Command
    {
        MenuBuilderTemplate _receiver;
        MENU _menu;
        public DefaultCommand(MenuBuilderTemplate receiver, MENU menu)
        {
            this._menu = menu;
            this._receiver = receiver;
        }
        public override void Execute()
        {
            _receiver.addMenu(this._menu);
        }
        public override void Unexecute()
        {
            _receiver.removeMenu();
        }
    }

    public class Invoker: Command
    {
        private List<Command> _commandsToExecute = new List<Command>();
        private MenuBuilderTemplate _receiver;
        
        public Invoker(MenuBuilderTemplate builder)
        {
            this._receiver = builder;
        }

        public void AddCommand(Command command)
        {
            this._commandsToExecute.Add(command);
        }

        public override void Execute()
        {
            _commandsToExecute[0].Execute();
            _commandsToExecute.RemoveAt(0);

        }
        public override void Unexecute()
        {
            _receiver.removeMenu();
        }
    }
}


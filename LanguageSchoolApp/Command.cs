using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public interface ICommand
    {
        public void Execute();
        public void Unexecute();
    }

    public class DefaultCommand : ICommand
    {
        IMenu _receiver;
        MENU _menu;
        public DefaultCommand(IMenu receiver, MENU menu)
        {
            this._menu = menu;
            this._receiver = receiver;
        }
        public void Execute()
        {
            _receiver.addMenu(this._menu);
        }
        public void Unexecute()
        {
            _receiver.removeMenu();
        }
    }

    public class Invoker: ICommand
    {
        private List<ICommand> _commandsToExecute = new List<ICommand>();
        private IMenu _receiver;
        public Invoker(IMenu builder)
        {
            this._receiver = builder;
        }
        public void AddCommand(ICommand command)
        {
            this._commandsToExecute.Add(command);
        }

        public void Execute()
        {
            _commandsToExecute[0].Execute();
            _commandsToExecute.RemoveAt(0);

        }
        public void Unexecute()
        {
            _receiver.removeMenu();
        }
    }
}


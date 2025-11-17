// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System.Collections.Generic;

namespace CommandPattern
{
    /// <summary>
    /// A holder to keep track of commands (presumably) for use in a future assignment
    /// </summary>
    public class CommandInvoker
    {
        private readonly Stack<Command> _undoStack = new Stack<Command>();
        private readonly Stack<Command> _redoStack = new Stack<Command>();

        public void ExecuteCommand(Command command)
        {
            command.Execute();
            _undoStack.Push(command);

            // clear out the redo stack if we make a new move
            _redoStack.Clear();
        }

        public void UndoCommand()
        {
            if (_undoStack.Count > 0)
            {
                Command activeCommand = _undoStack.Pop();
                _redoStack.Push(activeCommand);
                activeCommand.Undo();
            }
        }

        public void RedoCommand()
        {
            if (_redoStack.Count > 0)
            {
                Command activeCommand = _redoStack.Pop();
                _undoStack.Push(activeCommand);
                activeCommand.Execute();
            }
        }
    }
}
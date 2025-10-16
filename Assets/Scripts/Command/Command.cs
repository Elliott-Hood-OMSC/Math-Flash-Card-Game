// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

namespace CommandPattern
{
    //Base class for the commands
    //This class should always look like this to make it more general, so no constructors, parameters, etc!!!
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();
    }
}

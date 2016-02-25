using Lisa.Commands;
using System.Collections.Generic;

namespace Lisa
{
    public static class Repository
    {
        public static List<Command> GetAllCommands()
        {
            return new List<Command>
            {
                new HelloCommand(),
                new MathCommand(),
                new ChangeCultureCommand(),
                new ShutUpCommand(),
                new GoogleSearchCommand()
            };
        }
    }
}

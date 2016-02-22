using Lisa.Commands;
using System.Collections.Generic;

namespace Lisa
{
    public static class Repository
    {
        private static List<Command> Commands { get; set; }

        static Repository()
        {
            Commands = new List<Command>();

            AddDefaultCommands();
        }

        public static List<Command> GetAllCommands()
        {
            return Commands;
        }

        private static void AddDefaultCommands()
        {
            Commands.Add(new HelloCommand());
            Commands.Add(new MathCommand());
        }
    }
}

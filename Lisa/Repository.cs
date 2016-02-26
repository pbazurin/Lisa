using Lisa.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lisa
{
    public static class Repository
    {
        public static Lazy<List<Command>> _commands;

        static Repository()
        {
            Reload();
        }

        public static void Reload()
        {
            _commands = new Lazy<List<Command>>(() =>
            {
                var result = new List<Command>();

                foreach (Type type in Assembly.GetAssembly(typeof(Command)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Command))))
                {
                    result.Add((Command)Activator.CreateInstance(type));
                }

                return result;
            });
        }

        public static List<Command> GetAllCommands()
        {
            return _commands.Value;
        }
    }
}

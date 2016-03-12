using Lisa.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lisa
{
    public static class Repository
    {
        public static Lazy<List<AbstractModule>> _Modules;

        static Repository()
        {
            Reload();
        }

        public static void Reload()
        {
            _Modules = new Lazy<List<AbstractModule>>(() =>
            {
                var result = new List<AbstractModule>();

                foreach (Type type in Assembly.GetAssembly(typeof(AbstractModule)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractModule))))
                {
                    result.Add((AbstractModule)Activator.CreateInstance(type));
                }

                return result;
            });
        }

        public static List<AbstractModule> GetAllModules()
        {
            return _Modules.Value;
        }
    }
}

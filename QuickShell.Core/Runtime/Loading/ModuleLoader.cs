using System.Reflection;
using System.Runtime.Loader;
using Zion;

namespace QuickShell.Runtime
{
    public sealed class ModuleLoader
    {
        private readonly string DllPath
            = Path.Combine(AppContext.BaseDirectory, "Modules", "MyModule.dll");//TODO


        public SessionHub Load()
        {
            if (!File.Exists(DllPath))
            {
                return new InvalidSessionHub($"Couldn't load module dll from \"{DllPath}\"");
            }

            try
            {
                Assembly Assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(DllPath);

                return FindHub(Assembly, out Type HubType)
                ? (SessionHub)Activator.CreateInstance(HubType).NotNull()
                : new InvalidSessionHub("The entry point was not found. The module must contain a non-abstract class that inherits from SessionHub.");
            }
            catch (Exception Exception)
            {
                return new InvalidSessionHub($"Couldn't load module dll from \"{DllPath}\":\n{Exception.GetType().FullName}\n{Exception.Message}");
            }
        }

        private static bool FindHub(Assembly Assembly, out Type Hub)
        {
            Type Target = typeof(SessionHub);

            foreach (Type Type in Assembly.GetExportedTypes())
            {
                if (Type.IsSubclassOf(Target))
                {
                    Hub = Type;
                    return true;
                }
            }

            Hub = default!;
            return false;
        }
    }
}
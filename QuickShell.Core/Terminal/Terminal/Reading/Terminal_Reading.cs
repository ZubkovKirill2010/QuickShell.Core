using Zion;

namespace QuickShell
{
    //_Reading
    public sealed partial class Terminal
    {
        public string ReadLine() => FromAsync(ReadLineAsync);
        public async Task<string> ReadLineAsync()
        {
            throw new NotImplementedException();
            //TODO
        }

        public T Read<T>() where T : IParsable<T> => FromAsync(ReadAsync<T>);
        public async Task<T> ReadAsync<T>() where T : IParsable<T>
        {
            throw new NotImplementedException();
            //TODO
        }


        private static T FromAsync<T>(AsyncFunc<T> Async)
        {
            return Async().GetAwaiter().GetResult();
        }
    }
}
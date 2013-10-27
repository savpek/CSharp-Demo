using System;

namespace WpfDemo.Utilities
{
    // This context can be used to disable asynchronous operations. This main purpose is 
    // to enable testability. But this also can be used to make AsyncCommand as non asynchronous
    // in production code.
    // 
    // VERY IMPORTANT TO NOTICE! Current implementation is not THREAD SAFE.
    public class AsynchronousIsDisabledContext : IDisposable
    {
        static AsynchronousIsDisabledContext()
        {
            IsAsyncCommandsEnabled = true;
        }

        public AsynchronousIsDisabledContext()
        {
            IsAsyncCommandsEnabled = false;
        }

        public static bool IsAsyncCommandsEnabled { get; private set; }

        public void Dispose()
        {
            IsAsyncCommandsEnabled = true;
        }
    }
}

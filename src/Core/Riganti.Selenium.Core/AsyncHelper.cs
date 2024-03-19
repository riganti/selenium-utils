﻿using System.Threading;
using System.Threading.Tasks;

namespace Riganti.Selenium.Core
{
    namespace Riganti.Utils.Infrastructure.Core
    {
        internal static class AsyncHelper
        {
            private static readonly TaskFactory taskFactory = new
                TaskFactory(CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.Default);

            public static TResult RunSync<TResult>(this Task<TResult> func)
                => taskFactory
                    .StartNew(() => func)
                    .Unwrap()
                    .GetAwaiter()
                    .GetResult();

            public static void RunSync(this Task func)
                => taskFactory
                    .StartNew(() => func)
                    .Unwrap()
                    .GetAwaiter()
                    .GetResult();
        }
    }
}
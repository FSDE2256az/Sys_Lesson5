Thread[] threads = new Thread[5];

var sync = new object();


for (int i = 0; i < 5; i++)
{
    threads[i] = new Thread(() =>
    {
        for (int i = 0; i < 10000000; i++)
        {
            // #region example 0
            //
            // Counter.Count++;
            //
            // #endregion example 0

            #region example 1

            // Interlocked.Increment(ref Counter.Count);

            // After
            // if (Counter.Count % 2 == 0)
            //     Interlocked.Increment(ref Counter.Count2);

            #endregion example 1

            #region example 2

            // // T3, T4, T5
            // Monitor.Enter(sync); // Sync => SynBlockIndex = -1
            // 
            // try
            // {
            //     Counter.Count++;
            // 
            //     if (Counter.Count % 2 == 0)
            //         Counter.Count2++;
            // }
            // finally
            // {
            //     Monitor.Exit(sync); // Sync => SynBlockIndex = -1
            // }

            #endregion example 2




            #region example 3

            lock (sync)
            {
                Counter.Count++;

                if (Counter.Count % 2 == 0)
                    Counter.Count2++;
            }

            #endregion example 3
        }
    });
}

for (int i = 0; i < 5; i++) threads[i].Start();
for (int i = 0; i < 5; i++) threads[i].Join();


Console.WriteLine($"Count 1: {Counter.Count}");
Console.WriteLine($"Count 2: {Counter.Count2}");



class Counter
{
    public static int Count = 0;
    public static int Count2 = 0;
}
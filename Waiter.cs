using System.Diagnostics;

namespace AutomationExercise.Tests;

public class Waiter
{
    public static async Task<bool> WaitForCondition(Func<Task<bool>> condition, int timeoutSec = 5, int timeIntervalMilliseconds = 300)
    {
        var timer = new Stopwatch();
        timer.Start();

        do
        {
            if (await condition())
            {
                return true;
            }

            await Task.Delay(timeIntervalMilliseconds);

        } while (timer.Elapsed.TotalSeconds < timeoutSec);

        return false;
    }
}
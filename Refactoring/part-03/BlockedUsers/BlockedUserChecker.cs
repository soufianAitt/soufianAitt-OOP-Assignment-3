using System.Diagnostics;

namespace RefactoringLab.Part03.BlockedUsers;

public static class BlockedUserChecker
{
    // HashSet<int>.Contains is O(1) on average, instead of O(n) for List<int>.Contains.
    public static int CountBlocked(HashSet<int> blockedIds, int[] requestIds)
    {
        var blocked = 0;
        foreach (var id in requestIds)
        {
            if (blockedIds.Contains(id))
                blocked++;
        }
        return blocked;
    }

    public static HashSet<int> BuildBlockedIds(int count)
    {
        var set = new HashSet<int>(count);
        for (var i = 0; i < count; i++)
            set.Add(i);
        return set;
    }

    public static int[] BuildRequestIds(int count, int maxId, int seed = 42)
    {
        var rnd = new Random(seed);
        var ids = new int[count];
        for (var i = 0; i < count; i++)
            ids[i] = rnd.Next(maxId);
        return ids;
    }

    // Returns fractional milliseconds: after the fix the work takes well under 1 ms,
    // which a whole-millisecond value would just show as 0.
    public static double MeasureMs(HashSet<int> blockedIds, int[] requestIds, out int found)
    {
        var sw = Stopwatch.StartNew();
        found = CountBlocked(blockedIds, requestIds);
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }
}

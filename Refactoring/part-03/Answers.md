# Part 03 — answers

---

## BlockedUsers

> **Measure the "before" numbers yourself, before changing the code.**
> Run the ORIGINAL project (the first commit of this branch / the downloaded source) with `dotnet run`
> and write the printed `time=… ms` below. The value depends on your machine, so it is left as a
> placeholder here on purpose — do not leave the placeholders in your submission.

- Time complexity before: **O(n × m)** — `blockedIds` is a `List<int>`, and `List.Contains` scans the list
  from the start (O(n), n = 50,000 blocked ids) for each of the m = 5,000 request ids. That is up to
  250,000,000 comparisons in the worst case.
- Time (ms) before: **`<PASTE YOUR MEASURED VALUE> ms`** (from the original code, `dotnet run`)
- What did you change?
  Changed the blocked ids from a `List<int>` to a `HashSet<int>`. `HashSet.Contains` uses a hash lookup
  and is O(1) on average, so each request costs one hash lookup instead of a scan. The set is built once
  (O(n)), outside `CountBlocked`. The `found` count is unchanged, because a set holds the same ids and
  the blocked ids are unique. `MeasureMs` now returns fractional milliseconds (`double`), because the
  new time is below 1 ms and would otherwise print as `0`.
- Time complexity after: **O(m)** for `CountBlocked` — m hash lookups at O(1) average each (plus a
  one-time O(n) to build the set).
- Time (ms) after: **`<PASTE YOUR MEASURED VALUE> ms`** (from the refactored code, `dotnet run`)

---

## Students

- **What was the problem?**
  `GetAllStudents()` was eager: it created 1,000,000 `Student` objects (and 1,000,000 name strings)
  and put them all into a `List<Student>` before returning, even though `Program.cs` only reads the
  first 3 and then `break`s. That wasted time and a lot of memory (hundreds of MB of allocations) for
  work nobody used.
- **What did you change?**
  Changed the method to return `IEnumerable<Student>` and to use `yield return`. It is now lazy: a
  student is created only when the `foreach` asks for the next one. After 3 students and the `break`,
  only 3 `Student` objects have ever been created. The program still prints the first 3 students
  (`printed=3`).

# Collections — answers

---

## Task 2.1 — Research: IReadOnlyDictionary & SortedDictionary

### What is `IReadOnlyDictionary<TKey, TValue>`?

it is an interface that that decsribse a view of clolleciont pairs key/value.


### How is it different from `Dictionary`, and why return one from a public method?

Dictionary is a class concrete that can be modifed..I readyonlydictionary expose
only the reading
ps: dictionary implements it 

### What is `SortedDictionary<TKey, TValue>`?

A dictionary that keeps its entries **sorted by key** at all times (using the key's default comparer, or
a comparer you pass in). It is implemented as a balanced binary search tree (a red-black tree).

### How is it different from `Dictionary` in ordering and in lookup speed?

| | `Dictionary` | `SortedDictionary` |
|---|---|---|
| Order when enumerating | No guaranteed order (not sorted) | Always ascending by key |
| Lookup / add / remove | O(1) on average (hash table) | O(log n) (tree) |
| Memory | Lower | Higher (a tree node per entry) |

So `SortedDictionary` is slower for pure lookups, but it gives sorted output for free.

### When would I choose it?
When I nedd data always sequencial or in key order.. and the collection changes
constantly


---

## Task 2.2 — Pick the Collection

| # | Collection | Why (one sentence) |
|---|---|---|
| S1 | `Dictionary<string, Student>`|   it is a hash table that finds studendts so quickly on o(1) time complexity. which it matters when it is done many time a 
| S2 | `HashSet<string>` | A set rejects duplicates by design, so the same tag can never be stored twice (and checking a tag is O(1)). |
| S3 | `List<decimal>` | A list keeps insertion order and allows the same value many times. |
| S4 | `IReadOnlyDictionary<string, decimal>` | Returning the read-only interface lets callers look up prices but gives them no way to add or change any. |
| S5 | `SortedDictionary<TimeOnly, Session>` | It stays sorted by start time after every insert, so printing is always in time order, and inserts are only O(log n). |
| S6 | `IEnumerable<T>` | The caller just loops once, so returning a lazy sequence (with `yield return`) avoids building a whole list and allows stopping early. |


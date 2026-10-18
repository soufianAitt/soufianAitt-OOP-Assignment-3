# Part 02 — answers

---

## Reports

- **What was the problem?**
  `CsvReportExporter`, `JsonReportExporter` and `TextReportExporter` were three copies of the same
  class. `Export`, `Load`, `Validate` and `Save` were identical in all of them (copy-paste duplication);
  only `Format` was different. A change to the algorithm — for example adding a logging step, or changing
  validation — had to be repeated three times, and one copy could easily be forgotten or drift apart.
- **What did you change?**
  Created an abstract class `ReportExporter` that implements the **Template Method** pattern:
  `Export(path)` is written exactly once and fixes the order *Load → Validate → Format → Save*.
  `Load`, `Validate` and `Save` are shared as protected virtual defaults, and `Format` is `abstract`.
  Each exporter now contains only its `Format` method (comma-joined for CSV, JSON array, pipe-separated
  for text). The generated files have exactly the same content as before.
- **Why did you choose that approach? (abstract class vs interface)**
  An abstract class is the right fit because the pattern needs to share **code and fix the algorithm**,
  not just promise a method signature:
  - The abstract class can hold the real implementation of the shared steps (`Load`, `Validate`,
    `Save`) and the template method `Export` itself. An interface describes only a contract; before
    default interface methods it could not contain any implementation, and even with them an interface
    cannot hold protected members or state, so it is a poor home for a reusable skeleton.
  - `Export` is non-virtual, so subclasses cannot reorder or skip steps. An interface cannot enforce
    that — every implementer could write its own order, which brings the duplication back.
  - Protected hooks (`Format`) are visible only to subclasses and not exposed to callers; interface
    members are public.
  - The exporters are a true "is-a" family that share behavior, and a class has only one base class
    — which is fine here. (An interface such as `IReportExporter` could still be added on top if callers
    need to depend on an abstraction.)

---

## Enrollment

- **What was the problem?**
  To enroll one student, `Program.cs` had to create four services (`PaymentGateway`, `SeatInventory`,
  `InvoiceGenerator`, `EmailService`) and call them one by one in the right order, passing the invoice
  id from one to the next. The caller was tightly coupled to four classes and to the internal workflow.
  Every other place that enrolls a student would have to repeat the same steps, and a wrong order
  (e.g. e-mailing before charging) or a forgotten step would be an easy mistake.
- **What did you change?**
  Added `EnrollmentFacade` with one method, `Enroll(studentId, courseId, amount)`. It owns the four
  services and runs the steps in the right order. `Program.cs` now does a single call and no longer
  creates or calls any of the four services. `Services.cs` is unchanged, and the output is the same.
  The facade also has a constructor taking the four services, so they can be replaced in tests.
- **Why did you choose that approach?**
  The **Facade** pattern is meant exactly for hiding a complex subsystem behind a simple interface:
  the caller gets one easy entry point, and the subsystem stays untouched. What the caller no longer
  needs to know: which services exist, how to construct them, which order the steps run in, and how data
  flows between them (the invoice id produced by `InvoiceGenerator` that goes into the e-mail). If the
  workflow changes later (a new step, a different order), only the facade changes and the callers are
  unaffected.

# Part 01 — answers

---

## ShippingCostCalculator

- **What was the problem?**
    a hard coded switch style on carrier name. it is bad for our code because
    every time the carrier meant copening and editing shippingcostcalculator

- **What did you change?**
    Adding ishippingcarrier interface and one small class per carrier that
    impelent the interface 

---

## OrderProcessor

- **What was the problem?**
    orderprocessor created its own dependecnies, it was hard wired to sql and
    that break of depedecnty inversion principle.
- **What did you change?**
    adding as abstractions  of  orderrepositoryand emailsender, and let the
    concrete classes implement them.
---

## Notifications

- **What was the problem?**
    a very big hirachy of inheretance...it makes the code harder to read and
    there is no one single of truth... you should read a
    understand just one functionality.

- **What did you change?**
    Replace inheritance with compositions..  any channel now can be normal,
    urgent, scheduled or both with no extract classes

---

## Proof

- New carrier file(s): `src/BostaCarrier.cs`
- New notification channel file(s): `src/PushChannel.cs`
- Existing classes left unchanged? **yes** — adding `BostaCarrier` and `PushChannel` required no edit to
  `ShippingCostCalculator`, the other carriers, `Notifier`, or any channel. The only existing file edited
  to wire them in is `Program.cs`, as the assignment allows.

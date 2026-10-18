namespace RefactoringLab.Part02.Reports;

/// <summary>
/// Template Method: the export steps and their ORDER are written here, once.
/// Subclasses only supply what is different (<see cref="Format"/>).
/// </summary>
public abstract class ReportExporter
{
    // The template method — not virtual, so no subclass can change the order of the steps.
    public void Export(string path)
    {
        var rows = Load();
        if (!Validate(rows))
            throw new InvalidOperationException("Invalid data");
        var content = Format(rows);
        Save(path, content);
    }

    // Steps that were identical in all three exporters: shared defaults (can still be overridden).
    protected virtual List<string[]> Load() =>
    [
        ["Id", "Name"],
        ["1", "Keyboard"],
        ["2", "Mouse"]
    ];

    protected virtual bool Validate(List<string[]> rows) =>
        rows.Count > 1 && rows[0].Length > 0;

    protected virtual void Save(string path, string content) =>
        File.WriteAllText(path, content);

    // The only step that differs per report type.
    protected abstract string Format(List<string[]> rows);
}

// ===== CONFIG: EDIT THESE =====
string targetTableName = "Calendar";                   // <-- CHANGE to your measures table EXACT name
string displayFolder   = "MonthLags";             // <-- Optional; "" to skip folder
string calendarTable   = "'Calendar'";                  // <-- CHANGE to your Calendar table name, keep quotes if needed
string monthNameCol    = "[FISCAL_YEAR_YYYYMMM]";       // <-- CHANGE to your month label column
string monthLagCol     = "[MONTHLAG]";                  // <-- CHANGE if your lag column differs
int startLag = 0;                                       // Start at 0 as requested
int endLag   = 23;                                      // End at 23
// ==============================

// Resolve target table (TE 2.25: use LINQ)
var table = Model.Tables.FirstOrDefault(t => t.Name == targetTableName);
if (table == null)
{
    Error("Table '" + targetTableName + "' was not found. Please set 'targetTableName' to an existing table.");
    return;
}

// Helper: create/update a single lag month name measure
Action<int> upsertLagMonthName = (lag) =>
{
    var measName = "Lag" + lag.ToString() + "_Month_Name";
    // DAX uses fully-qualified column refs built from config:
    var dax = "CALCULATE(MAX(" + calendarTable + monthNameCol + "), " + calendarTable + monthLagCol + " = " + lag.ToString() + ")";

    var m = table.Measures.FirstOrDefault(x => x.Name == measName);
    if (m == null)
    {
        m = table.AddMeasure(measName, dax);
    }

    m.Expression = dax;
    m.Description = "Max of " + calendarTable + monthNameCol + " where " + calendarTable + monthLagCol + " = " + lag.ToString();

    if (!string.IsNullOrEmpty(displayFolder))
        m.DisplayFolder = displayFolder;

    // Format as text explicitly (optional). Comment out if you want to inherit something else.
    m.FormatString = "";
};

// Create Lag_0 .. Lag_23
for (int lag = startLag; lag <= endLag; lag++)
{
    upsertLagMonthName(lag);
}

Output("Created/updated Lag" + startLag.ToString() + "_Month_Name .. Lag" + endLag.ToString() + "_Month_Name in table '" + table.Name + "'.");

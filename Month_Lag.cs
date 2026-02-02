// ===== CONFIG: EDIT THESE =====
string targetTableName = "Direct Spend Data";            // <-- CHANGE to your measures table EXACT name
string baseMeasureName = "Total Spend";          // <-- CHANGE to your base measure name
string lagColumnRef    = "'Calendar'[MONTHLAG]"; // <-- CHANGE to your model's column reference
int startLag = 1;                                // <-- Start lag
int endLag   = 23;                               // <-- End lag
bool createLag0 = false;                         // <-- true if you also want Spend_Lag_0
string displayFolder = "Lags";                   // <-- "" to skip folder
// ==============================

// --- Resolve target table (TE 2.25: use LINQ instead of ContainsName)
var table = Model.Tables.FirstOrDefault(t => t.Name == targetTableName);
if (table == null)
{
    Error("Table '" + targetTableName + "' was not found. Please set 'targetTableName' to an existing table.");
    return;
}

// --- Find base measure (for format inheritance)
var baseMeasure = Model.AllMeasures.FirstOrDefault(m => m.Name == baseMeasureName);
if (baseMeasure == null)
{
    Error("Base measure '" + baseMeasureName + "' was not found. Please check 'baseMeasureName'.");
    return;
}

string inheritedFormat = null;
if (!string.IsNullOrWhiteSpace(baseMeasure.FormatString))
{
    inheritedFormat = baseMeasure.FormatString;
}

// --- Helper to create/update a lag measure (TE 2.25: use Any/FirstOrDefault)
Action<int> upsertLag = (lag) =>
{
    var measName = "Spend_Lag_" + lag.ToString();
    var dax = "CALCULATE([" + baseMeasureName + "], " + lagColumnRef + " = " + lag.ToString() + ")";

    var m = table.Measures.FirstOrDefault(x => x.Name == measName);
    if (m == null)
    {
        m = table.AddMeasure(measName, dax);
    }

    m.Expression = dax;

    if (!string.IsNullOrEmpty(displayFolder))
        m.DisplayFolder = displayFolder;

    if (!string.IsNullOrWhiteSpace(inheritedFormat))
        m.FormatString = inheritedFormat;

    m.Description = "[" + baseMeasureName + "] filtered by " + lagColumnRef + " = " + lag.ToString();
};

// --- Optional Lag_0
if (createLag0)
{
    upsertLag(0);
}

// --- Lag_1..Lag_23
for (int lag = startLag; lag <= endLag; lag++)
{
    upsertLag(lag);
}

Output("Created/updated Spend_Lag_" + (createLag0 ? "0" : startLag.ToString()) + ".." + endLag.ToString() + " in table '" + table.Name + "'.");

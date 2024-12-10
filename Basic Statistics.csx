// Iterate over each selected column to create various statistical measures and hide the column
foreach(var c in Selected.Columns)
{
    var TableName = c.Table.Name;  // Dynamically fetch the table name of the column being processed
    var ColumnName = c.Name;      // Get the name of the column to use as a folder name

    // Dynamically create a folder for each column using the column name only (without prefix)
    var folderName = ColumnName;

    // Create Minimum measure
    var minMeasure = c.Table.AddMeasure(
        "Min of " + ColumnName,                    // Measure name
        "Min(" + c.DaxObjectFullName + ")",        // DAX formula to compute the minimum value
        folderName                                 // Dynamic folder name based on column name
    );
    minMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    minMeasure.Description = "This measure is the Min of column " + ColumnName;

    // Create Maximum measure
    var MaxMeasure = c.Table.AddMeasure(
        "Max of " + ColumnName,                    // Measure name
        "Max(" + c.DaxObjectFullName + ")",        // DAX formula to compute the maximum value
        folderName                                 // Dynamic folder name based on column name
    );
    MaxMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    MaxMeasure.Description = "This measure is the Max of column " + ColumnName;

    // Create Mean measure (average)
    var MeanMeasure = c.Table.AddMeasure(
        "Mean of " + ColumnName,                    // Measure name
        "Average(" + c.DaxObjectFullName + ")",    // DAX formula for mean (average)
        folderName                                 // Dynamic folder name based on column name
    );
    MeanMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    MeanMeasure.Description = "This measure is the Mean of column " + ColumnName;

    // Create Median measure
    var MedainMeasure = c.Table.AddMeasure(
        "Median of " + ColumnName,                    // Measure name
        "Median(" + c.DaxObjectFullName + ")",        // DAX formula for median value
        folderName                                 // Dynamic folder name based on column name
    );
    MedainMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    MedainMeasure.Description = "This measure is the Median of column " + ColumnName;

    // Create Quartile 25% measure
    var Q25Measure = c.Table.AddMeasure(
        "Q25 of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",0.25)",    // DAX formula for 25th percentile (Q25)
        folderName                                 // Dynamic folder name based on column name
    );
    Q25Measure.FormatString = "0.00";  // Set the format for displaying the measure
    Q25Measure.Description = "This measure is the Q25 of column " + ColumnName;

    // Create Quartile 50% measure (which is the Median)
    var Q50Measure = c.Table.AddMeasure(
        "Q50 of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",0.5)",    // DAX formula for 50th percentile (Q50)
        folderName                                 // Dynamic folder name based on column name
    );
    Q50Measure.FormatString = "0.00";  // Set the format for displaying the measure
    Q50Measure.Description = "This measure is the Q50 of column " + ColumnName;

    // Create Quartile 75% measure
    var Q75Measure = c.Table.AddMeasure(
        "Q75 of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",0.75)",    // DAX formula for 75th percentile (Q75)
        folderName                                 // Dynamic folder name based on column name
    );
    Q75Measure.FormatString = "0.00";  // Set the format for displaying the measure
    Q75Measure.Description = "This measure is the Q75 of column " + ColumnName;

    // Create Quartile 100% measure (maximum value)
    var Q100Measure = c.Table.AddMeasure(
        "Q100 of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",1)",    // DAX formula for 100th percentile (Q100)
        folderName                                 // Dynamic folder name based on column name
    );
    Q100Measure.FormatString = "0.00";  // Set the format for displaying the measure
    Q100Measure.Description = "This measure is the Q100 of column " + ColumnName;

    // Create Standard Deviation measure
    var sdMeasure = c.Table.AddMeasure(
        "Std of " + ColumnName,                    // Measure name
        "STDEV.S(" + c.DaxObjectFullName + ")",    // DAX formula for standard deviation
        folderName                                 // Dynamic folder name based on column name
    );
    sdMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    sdMeasure.Description = "This measure is the Standard Deviation of column " + ColumnName;

    // Create Variance measure
    var VarMeasure = c.Table.AddMeasure(
        "Var of " + ColumnName,                    // Measure name
        "VAR.S(" + c.DaxObjectFullName + ")",    // DAX formula for variance
        folderName                                 // Dynamic folder name based on column name
    );
    VarMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    VarMeasure.Description = "This measure is the Variance of column " + ColumnName;

    // Create Interquartile Range (IQR) measure
    var IQRMeasure = c.Table.AddMeasure(
        "IQR of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",0.75)-PERCENTILE.INC(" + c.DaxObjectFullName + ",0.25)",    // DAX formula for IQR
        folderName                                 // Dynamic folder name based on column name
    );
    IQRMeasure.FormatString = "0.00";  // Set the format for displaying the measure
    IQRMeasure.Description = "This measure is the IQR of column " + ColumnName;

    // Create Lower Fence measure
    var LowerFence = c.Table.AddMeasure(
        "LowerFence of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",0.25)-(1.5 *(PERCENTILE.INC(" + c.DaxObjectFullName + ",0.75)-PERCENTILE.INC(" + c.DaxObjectFullName + ",0.25)))",    // DAX formula for lower fence
        folderName                                 // Dynamic folder name based on column name
    );
    LowerFence.FormatString = "0.00";  // Set the format for displaying the measure
    LowerFence.Description = "This measure is the Lower Fence of column " + ColumnName;

    // Create Upper Fence measure
    var UpperFence = c.Table.AddMeasure(
        "UpperFence of " + ColumnName,                    // Measure name
        "PERCENTILE.INC(" + c.DaxObjectFullName + ",0.75)+(1.5 *(PERCENTILE.INC(" + c.DaxObjectFullName + ",0.75)-PERCENTILE.INC(" + c.DaxObjectFullName + ",0.25)))",    // DAX formula for upper fence
        folderName                                 // Dynamic folder name based on column name
    );
    UpperFence.FormatString = "0.00";  // Set the format for displaying the measure
    UpperFence.Description = "This measure is the Upper Fence of column " + ColumnName;

    // Create Upper Outliers measure
    var UpperOutliers = c.Table.AddMeasure(
        "UpperOutliers of " + ColumnName,                    // Measure name
        "CALCULATE(COUNT(" + c.DaxObjectFullName + "),Filter("+TableName+","+c.DaxObjectFullName+">PERCENTILE.INC(" +c.DaxObjectFullName+",0.75)+(1.5 *(PERCENTILE.INC("+c.DaxObjectFullName+",0.75)-PERCENTILE.INC(" +c.DaxObjectFullName+",0.25)))))", 
        folderName                                 // Dynamic folder name based on column name
    );
    UpperOutliers.FormatString = "0.00";  // Set the format for displaying the measure
    UpperOutliers.Description = "This measure is count of upper outliers of column " + ColumnName;

    // Create Lower Outliers measure
    var LowerOutliers = c.Table.AddMeasure(
        "LowerOutliers of " + ColumnName,                    // Measure name
        "CALCULATE(COUNT(" + c.DaxObjectFullName + "),Filter("+TableName+","+c.DaxObjectFullName+">PERCENTILE.INC(" +c.DaxObjectFullName+",0.25)-(1.5 *(PERCENTILE.INC("+c.DaxObjectFullName+",0.75)-PERCENTILE.INC(" +c.DaxObjectFullName+",0.25)))))", 
        folderName                                 // Dynamic folder name based on column name
    );
    LowerOutliers.FormatString = "0.00";  // Set the format for displaying the measure
    LowerOutliers.Description = "This measure is count of lower outliers of column " + ColumnName;

    // Hide the base column after creating all the measures
    c.IsHidden = true;  // Set to 'true' to hide the original column
}

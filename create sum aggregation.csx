foreach (var column in Selected.Columns) 

{
    string measureFormat = column.FormatString; 
    column.DisplayFolder = column.DaxObjectName.Replace("[", "").Replace("]", "");
    var newMeasure = column.Table.AddMeasure("Total " + column.Name, "SUM(" + column.DaxObjectFullName + ")", column.DisplayFolder); 
    newMeasure.FormatString = measureFormat; 
    newMeasure.Description = "Sum of " + column.Name;
    
}

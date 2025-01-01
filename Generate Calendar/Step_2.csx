
//Before running this script make sure Generate_Calendar_Step_1.csx executed and saved model
// Make sure Date dimension is available in power bi desktop

///Rename Date Dimension
string tablename = "Calendar";

(Model.Tables["Date"].Columns["Weekday Name"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Weekday Number EU"] as CalculatedTableColumn);


(Model.Tables["Date"].Columns["Weekday Short"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Weekday Number EU"] as CalculatedTableColumn);
//Week
(Model.Tables["Date"].Columns["Calendar Week EU"] as CalculatedTableColumn).SortByColumn =(Model.Tables["Date"].Columns["Calendar Week Number EU"] as CalculatedTableColumn);
(Model.Tables["Date"].Columns["Calendar Week ISO"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Week Number ISO"] as CalculatedTableColumn);
(Model.Tables["Date"].Columns["Calendar Week US"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Week Number US"] as CalculatedTableColumn);
//

 // Sort Months
(Model.Tables["Date"].Columns["Calendar Month"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Month #"] as CalculatedTableColumn);
(Model.Tables["Date"].Columns["Calendar Month Day (MMM DD)"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Month Day (MDD)"] as CalculatedTableColumn);
(Model.Tables["Date"].Columns["Calendar Month Year"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Year Month"] as CalculatedTableColumn);
        
        // Sort Quarters
(Model.Tables["Date"].Columns["Calendar Quarter Year"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Year Quarter"] as CalculatedTableColumn);
        
        // Sort Years
(Model.Tables["Date"].Columns["Calendar Year"] as CalculatedTableColumn).SortByColumn = (Model.Tables["Date"].Columns["Calendar Year Number"] as CalculatedTableColumn);

// For all the columns in the date table:
        foreach (var c in Model.Tables["Date"].Columns )
        {
        c.DisplayFolder = "7. Boolean Fields";
        c.IsHidden = false;
        
        // Organize the date table into folders
            if ( ( c.DataType == DataType.DateTime & c.Name.Contains("Date") ) )
                {
                c.DisplayFolder = "6. Calendar Date";
                c.IsHidden = false;
                c.IsKey = false;
                }
        
            if ( c.Name == "BQTD" )
                {
                    c.DisplayFolder = "8.TillDate";
                    c.IsHidden = false;
                }
             if ( c.Name == "BYTD" )
                {
                    c.DisplayFolder = "8.TillDate";
                     c.IsHidden = false;
                }
                     if ( c.Name == "BMTD" )
                {
                c.DisplayFolder = "8.TillDate";
                c.IsHidden = false;
                }   
                 
            
                         if ( c.Name == "WTD" )
                {
                    c.DisplayFolder = "8.TillDate";
                c.IsHidden = false;
                }
            

            if ( c.Name.Contains("Year") & c.DataType != DataType.Boolean )
                {
                c.DisplayFolder = "1. Year";
                c.IsHidden = false;
                }
        
            if ( c.Name.Contains("Week") & c.DataType != DataType.Boolean )
                {
                c.DisplayFolder = "4. Week";
                c.IsHidden = false;
                }
        
            if ( c.Name.Contains("day") & c.DataType != DataType.Boolean )
                {
                c.DisplayFolder = "5. Weekday / Workday\\Weekday";
                c.IsHidden = false;
                }
        
            if ( c.Name.Contains("Month") & c.DataType != DataType.Boolean )
                {
                 c.DisplayFolder = "3. Month";
                 c.IsHidden = false;
                }
        
            if ( c.Name.Contains("Quarter") & c.DataType != DataType.Boolean )
                {
                 c.DisplayFolder = "2. Quarter";
                 c.IsHidden = false;
                }
        
        }
 

Model.Tables["Date"].DataCategory = "Time";
Model.Tables["Date"].Columns["Date"].IsKey = true;


(Model.Tables["Date"] as CalculatedTable).Name = tablename; 

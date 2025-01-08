// Creates Time based  measures for every selected measure.
//Make sure format of selected measures were implemented early 


foreach(var m in Selected.Measures) {
    
   m.DisplayFolder = m.DaxObjectName.Replace("[", "").Replace("]", "");
    
    //m.DisplayFolder = m.Name; // Comment or uncomment to create folder name same as base measure or hard code it
   string measureFormat = m.FormatString;// Get format of measure to apply to new measures
   var CQTDMeasure = m.Table.AddMeasure(
      m.Name + " CQTD", // Name
      "CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"CQTD\")", // DAX expression
      m.DisplayFolder // Display Folder
  );
   CQTDMeasure.Description = "This measure is sum of Current quarter to till date of " + m.Name;
   CQTDMeasure.FormatString = measureFormat; // Format same as selected measure
   
   var PQTDMeasure = m.Table.AddMeasure(
      m.Name + " PQTD", // Name
      "CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"PQTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   PQTDMeasure.Description = "This measure is sum of Previous quarter to till date of " + m.Name;
   PQTDMeasure.FormatString = measureFormat; // Format same as selected measure
   
   var LYQTDMeasure = m.Table.AddMeasure(
      m.Name + " LYQTD", // Name
      "CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"LYQTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   LYQTDMeasure.Description = "This measure is sum of Previous quarter to till date of " + m.Name;
   LYQTDMeasure.FormatString = measureFormat; // Format same as selected measure
   
   var CYTDMeasure = m.Table.AddMeasure(
      m.Name + " CYTD", // Name
      "CALCULATE(" + m.DaxObjectName + ", Calendar[BYTD] = \"CYTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   CYTDMeasure.Description = "This measure is sum of Current Year to till date of " + m.Name;
   CYTDMeasure.FormatString = measureFormat; // Format same as selected measure
   
   var LYYTDMeasure = m.Table.AddMeasure(
      m.Name + " LYYTD", // Name
      "CALCULATE(" + m.DaxObjectName + ", Calendar[BYTD] = \"LYYTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   LYYTDMeasure.Description = "This measure is sum of Last Year to till date of " + m.Name;
   LYYTDMeasure.FormatString = measureFormat; // Format same as selected measure

   var CMTDMeasure = m.Table.AddMeasure(
   m.Name + " CMTD", // Name
   "CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"CMTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   CMTDMeasure.Description = "This measure is sum of Current month to till date of " + m.Name;
   CMTDMeasure.FormatString = measureFormat; // Format same as selected measure
   
    var PMTDMeasure = m.Table.AddMeasure(
    m.Name + " PMTD", // Name
   "CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"PMTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   PMTDMeasure.Description = "This measure is sum of Prevous month to till date of prevoius month " + m.Name;
   PMTDMeasure.FormatString = measureFormat; // Format same as selected measure
   
       var LYMTDMeasure = m.Table.AddMeasure(
    m.Name + " LYMTD", // Name
    "CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"LYMTD\")", // DAX expression
      m.DisplayFolder // Display Folder
   );
   LYMTDMeasure.Description = "This measure is sum of Prevous year same month to till date of prevoius year same month " + m.Name;
   LYMTDMeasure.FormatString = measureFormat; // Format same as selected measure

    var QoQDelta  = m.Table.AddMeasure(
    m.Name + " Delta Quarter QoQ" , 
       
      "\n"+"Var CQTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"CQTD\")"
       + "\n" +"Var PQTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"PQTD\")"+"\n"
     +  "return CQTD-PQTD", // DAX expression
     m.DisplayFolder );
     QoQDelta.Description = "Quarter on Quarter Delta Quarter growth  " + m.Name;
     QoQDelta.FormatString = measureFormat;
     
    
    var YoYDelta  = m.Table.AddMeasure(
    m.Name + " Delta Quarter YoY" , 
       
      "\n"+"Var CQTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"CQTD\")"
      + "\n" +"Var LYQTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"LYQTD\")"+"\n"
     +  "return CQTD-LYQTD", // DAX expression
     m.DisplayFolder );
     YoYDelta.Description = "Year on Year Delta Quarter growth  " + m.Name;
     YoYDelta.FormatString = measureFormat; 
     
     
         var YTDDelta  = m.Table.AddMeasure(
         m.Name + " Delta YTD YoY" , 
       
    "\n"+"Var CYTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BYTD] = \"CYTD\")"
    + "\n" +"Var LYYTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BYTD] = \"LYYTD\")"+"\n"
     +  "return CYTD-LYYTD", // DAX expression
     m.DisplayFolder );
     YTDDelta.Description = "Year on Year Delta YTD growth  " + m.Name;
     YTDDelta.FormatString = measureFormat; 
   
     
              var MoMDelta  = m.Table.AddMeasure(
              m.Name + " Delta Month MoM" , 
       
              "\n"+"Var CMTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"CMTD\")"
              + "\n" +"Var PMTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"PMTD\")"+"\n"
     +  "return CMTD-PMTD", // DAX expression
     m.DisplayFolder );
     MoMDelta.Description = "Month on Month Delta Monthly growth  " + m.Name;
     MoMDelta.FormatString = measureFormat; 
     
              var YoYMonthDelta  = m.Table.AddMeasure(
               m.Name + " Delta Month YoY" , 
       
              "\n"+"Var CMTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"CMTD\")"
              + "\n" +"Var LYMTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"LYMTD\")"+"\n"
     +  "return CMTD-LYMTD", // DAX expression
     m.DisplayFolder );
     YoYMonthDelta.Description = "Month on Month Delta Yearly growth  " + m.Name;
     YoYMonthDelta.FormatString = measureFormat; 
     
     
      var QoQpg  = m.Table.AddMeasure(
      m.Name + " Quarter QoQ%" , 
       
      "\n"+"Var CQTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"CQTD\")"
       + "\n" +"Var PQTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"PQTD\")"+"\n"
       +  "return Divide(CQTD-PQTD,PQTD)", // DAX expression
     m.DisplayFolder );
      QoQpg.Description = "QoQ Quarter growth%  " + m.Name;
      QoQpg.FormatString = "Percent";
     
     
      var YoYpg  = m.Table.AddMeasure(
      m.Name + " Quarter YoY%" , 
       
      "\n"+"Var CQTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"CQTD\")"
      + "\n" +"Var LYQTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BQTD] = \"LYQTD\")"+"\n"
       +  "return Divide(CQTD-LYQTD,LYQTD)", // DAX expression
     m.DisplayFolder );
     YoYpg.Description = "YoY Quarter growth%  " + m.Name;
     YoYpg.FormatString = "Percent";
     
       var YTDYoYpg  = m.Table.AddMeasure(
       m.Name + " YTD YoY%" , 
       
       "\n"+"Var CYTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BYTD] = \"CYTD\")"
       + "\n" +"Var LYYTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BYTD] = \"LYYTD\")"+"\n"
       +  "return Divide(CYTD-LYYTD,LYYTD)", // DAX expression
     m.DisplayFolder );
     YTDYoYpg.Description = "YoY Yearly growth%  " + m.Name;
     YTDYoYpg.FormatString = "Percent";
     
     
     var  MoMPg  = m.Table.AddMeasure(
     m.Name + " MoM%" , 
       
     "\n"+"Var CMTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"CMTD\")"
     + "\n" +"Var PMTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"PMTD\")"+"\n"
       +  "return Divide(CMTD-PMTD,PMTD)", // DAX expression
     m.DisplayFolder );
     MoMPg.Description = "MoM growth%  " + m.Name;
     MoMPg.FormatString = "Percent";
     
          var  YearMoMpg  = m.Table.AddMeasure(
          m.Name + " Yearly MoM%" , 
       
     "\n"+"Var CMTD = " +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"CMTD\")"
     + "\n" +"Var LYMTD = "  +"CALCULATE(" + m.DaxObjectName + ", Calendar[BMTD] = \"LYMTD\")"+"\n"
       +  "return Divide(CMTD-LYMTD,LYMTD)", // DAX expression
     m.DisplayFolder );
     YearMoMpg.Description = "Yearly MoM growth%  " + m.Name;
     YearMoMpg.FormatString = "Percent";
   
}
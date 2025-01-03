// 1. Set the start date of calendar  ,minus of 2 years of Facts Minimum date 
var _DateDaxExpression = @"-- Reference date for the latest date in the report
        -- Until when the business wants to see data in reports
        VAR _Refdate_Measure =  DATE(2009,7,8)
        VAR _Today = TODAY ( )-1
        VAR _Lastdate  = DATE ( YEAR ( _Today ) + 2, 12, 1 )
        
        -- Replace with ""Today"" if [RefDate] evaluates blank
        VAR _Refdate =  _Today //IF ( ISBLANK ( _Refdate_Measure ), _Today, _Refdate_Measure )
            VAR _RefYear        = YEAR ( _Refdate )
            VAR _RefQuarter     = _RefYear * 100 + QUARTER(_Refdate)
            VAR _RefMonth       = _RefYear * 100 + MONTH(_Refdate)
            VAR _RefWeek_EU     = _RefYear * 100 + WEEKNUM(_Refdate, 2)
        
        -- Earliest date in the model scope
    
        
        ------------------------------------------
        -- Base calendar table
        VAR _Base_Calendar      = CALENDAR ( _Refdate_Measure, _Lastdate )
        ------------------------------------------
        
        
        
        ------------------------------------------
        VAR _IntermediateResult = 
            ADDCOLUMNS ( _Base_Calendar,
        
                    ------------------------------------------
                ""Calendar Year Number"",           --|
                    YEAR ([Date]),                          --|-- Year
                                                            --|
                ""Calendar Year"",                  --|
                    FORMAT ([Date], ""YYYY""),                --|
                    ------------------------------------------
        
                    ------------------------------------------
                ""Calendar Quarter Year"",       --|
                    ""Q"" &                                   --|-- Quarter
                    CONVERT(QUARTER([Date]), STRING) &      --|
                    "" "" &                                   --|
                    CONVERT(YEAR([Date]), STRING),          --|
                                                            --|
                ""Calendar Year Quarter"",        --|
                    YEAR([Date]) * 100 + QUARTER([Date]),   --|
                    ------------------------------------------
        
                    ------------------------------------------
                ""Calendar Month Year"",          --|
                    FORMAT ( [Date], ""MMM YY"" ),            --|-- Month
                                                            --|
                ""Calendar Year Month"",          --|
                    YEAR([Date]) * 100 + MONTH([Date]),     --|
                                                            --|
                ""Calendar Month"",                  --|
                    FORMAT ( [Date], ""MMM"" ),               --|
                                                            --|
                ""Calendar Month #"",                  --|
                    MONTH ( [Date] ),                       --|
                    ------------------------------------------
                    
                    ------------------------------------------
                ""Calendar Week EU"",               --|
                    ""WK"" & WEEKNUM( [Date], 2 ),            --|-- Week
                                                            --|
                ""Calendar Week Number EU"",          --|
                    WEEKNUM( [Date], 2 ),                   --|
                                                            --|
                ""Calendar Year Week Number EU"", --|
                    YEAR ( [Date] ) * 100                   --|
                    +                                       --|
                    WEEKNUM( [Date], 2 ),                   --|
                                                            --|
                ""Calendar Week US"",               --|
                    ""WK"" & WEEKNUM( [Date], 1 ),            --|
                                                            --|
                ""Calendar Week Number US"",          --|
                    WEEKNUM( [Date], 1 ),                   --|
                                                            --|
                ""Calendar Year Week Number US"", --|
                    YEAR ( [Date] ) * 100                   --|
                    +                                       --|
                    WEEKNUM( [Date], 1 ),                   --|
                                                            --|
                ""Calendar Week ISO"",              --|
                    ""WK"" & WEEKNUM( [Date], 21 ),           --|
                                                            --|
                ""Calendar Week Number ISO"",         --|
                    WEEKNUM( [Date], 21 ),                  --|
                                                            --|
                ""Calendar Year Week Number ISO"",--|
                    YEAR ( [Date] ) * 100                   --|
                    +                                       --|
                    WEEKNUM( [Date], 21 ),                  --|
                    ------------------------------------------
        
                    ------------------------------------------
                ""Weekday Short"",                 --|
                    FORMAT ( [Date], ""DDD"" ),               --|-- Weekday
                                                            --|
                ""Weekday Name"",               --|
                    FORMAT ( [Date], ""DDDD"" ),              --|
                                                            --|
                ""Weekday Number EU"",               --|
                    WEEKDAY ( [Date], 2 ),                  --|
                    ------------------------------------------
                    
                    ------------------------------------------
                ""Calendar Month Day (MMM DD)"",         --|
                    FORMAT ( [Date], ""MMM DD"" ),            --|-- Day
                                                            --|
                ""Calendar Month Day (MDD)"",           --|
                    MONTH([Date]) * 100                     --|
                    +                                       --|
                    DAY([Date]),                            --|
                                                            --|
                ""YYYYMMDD"",                                 --|
                    YEAR ( [Date] ) * 10000                 --|
                    +                                       --|
                    MONTH ( [Date] ) * 100                  --|
                    +                                       --|
                    DAY ( [Date] ),                         --|
                    ------------------------------------------
        
        
                    ------------------------------------------
                ""IsDateInScope"",                            --|
                    [Date] <= _Refdate                      --|-- Boolean
                    &&                                      --|
                    YEAR([Date]) > YEAR(_Refdate_Measure),     --|
                                                            --|
                ""IsBeforeThisMonth"",                        --|
                    [Date] <= EOMONTH ( _Refdate, -1 ),     --|
                                                            --|
                ""IsLastMonth"",                              --|
                    [Date] <= EOMONTH ( _Refdate, 0 )       --|
                    &&                                      --|
                    [Date] > EOMONTH ( _Refdate, -1 ),      --|
                                                            --|
                ""IsYTD"",                                    --|
                    MONTH([Date])                           --|
                    <=                                      --|
                    MONTH(EOMONTH ( _Refdate, 0 )),         --|
                                                            --|
                ""IsActualToday"",                            --|
                    [Date] = _Today,                        --|
                                                            --|
                ""IsRefDate"",                                --|
                    [Date] = _Refdate,                      --|
                                                            --|
                ""IsHoliday"",                                --|
                    MONTH([Date]) * 100                     --|
                    +                                       --|
                    DAY([Date])                             --|
                        IN {0101, 0501, 1111, 1225},        --|
                                                            --|
                ""IsWeekday"",                                --|
                    WEEKDAY([Date], 2)                      --|
                        IN {1, 2, 3, 4, 5})                 --|
                    ------------------------------------------
        
        VAR _Result = 
            
                    --------------------------------------------
            ADDCOLUMNS (                                      --|
                _IntermediateResult,                          --|-- Boolean #2
                ""IsThisYear"",                                 --|
                    [Calendar Year Number]          --|
                        = _RefYear,                           --|
                                                            --|
                ""IsThisMonth"",                                --|
                    [Calendar Year Month]         --|
                        = _RefMonth,                          --|
                                                            --|
                ""IsThisQuarter"",                              --|
                    [Calendar Year Quarter]       --|
                        = _RefQuarter,                        --|
                                                            --|
                ""IsThisWeek"",                                 --|
                    [Calendar Year Week Number EU]--|
                        = _RefWeek_EU                         --|
            )                                                 --|
                    --------------------------------------------
                    
        VAR _ResultWithAdditionalColumns = 
                   ----------------------------------------------
           ADDCOLUMNS ( _Result, 
                          ""BQTD"" , 
                           SWITCH(
        TRUE(),
        -- Current Quarter Till Date
        [Calendar Year Quarter] = _RefQuarter &&
        [Date] <= _Refdate,""CQTD"",

        -- Same Quarter Previous Year Till Date
        [Calendar Year Quarter] = (_RefQuarter - 100) &&
        [Date] <= DATE(YEAR(_Refdate) - 1, MONTH(_Refdate), DAY(_Refdate)),""LYQTD"",

        -- Previous Quarter Till Date
        [Calendar Year Quarter] =  SWITCH(QUARTER(_Refdate),1,year(_Refdate)-1,YEAR(_Refdate))    * 100 + (SWITCH(QUARTER(_Refdate),1,4,_RefQuarter - 1))          &&
        [Date] <= DATE(SWITCH(QUARTER(_Refdate),1,year(_Refdate)-1,YEAR(_Refdate))  , SWITCH(MONTH(_Refdate),1,10,2,11,3,12,MONTH(_Refdate)-3), DAY(_Refdate)),""PQTD"",

        
        -- Default case
        BLANK()
        ) 
        ,
        
         ""BMTD"" , 
                           SWITCH(
        TRUE(),
        -- Current Month Till Date
        [Calendar Year Month] = _RefMonth &&
        [Date] <= _Refdate,""CMTD"",

        -- Same Month Previous Year Till Date
        [Calendar Year Month] = (_RefMonth - 100) &&
        [Date] <= DATE(YEAR(_Refdate) - 1, MONTH(_Refdate), DAY(_Refdate)),""LYMTD"",

        -- Previous Month Till Date
        [Calendar Year Month] =  SWITCH(MONTH(_Refdate) , 1 , (year(_Refdate)-1 )*100+ 12 ,  (_RefMonth - 1)) &&
        [Date] <= DATE(SWITCH(MONTH(_Refdate) , 1 , (year(_Refdate)-1 ) ,year(_Refdate) ), SWITCH(MONTH(_Refdate) , 1 , 12 ,  (_RefMonth - 1)), DAY(_Refdate)),""PMTD"",

        -- Default case
        BLANK()
        ) 
        ,
               ""BYTD"",
    SWITCH(
        TRUE(),
        -- Current Year Till Date
        [Calendar Year Number] = _RefYear &&
        [Date] <= _Refdate,
        ""CYTD"",

        -- Previous Year Till Date
        [Calendar Year Number] = (_RefYear - 1) &&
        [Date] <= DATE(YEAR(_Refdate) - 1, MONTH(_Refdate), DAY(_Refdate)),
        ""LYYTD"",

        -- Default case
        BLANK()
    )

  
,      ""WTD"",
    SWITCH(
        TRUE(),
        -- Current Week Till Date
        WEEKNUM([Date], 2) = WEEKNUM(_Refdate, 2) && YEAR([Date]) = YEAR(_Refdate) && 
        [Date] <= _Refdate,
        ""CWTD"",

        -- Previous Week Till Date
        WEEKNUM([Date], 2) = WEEKNUM(_Refdate - 7, 2) && YEAR([Date]) = YEAR(_Refdate)
        && [Date] <= DATE(YEAR(_Refdate), MONTH(_Refdate), DAY(_Refdate) - 7),
        ""PWTD"",

        -- Previous Year Same Week Till Date
        WEEKNUM([Date], 2) = WEEKNUM( _Refdate-365 , 2) && YEAR([Date])=   YEAR(_Refdate-365)
        && [Date] <= DATE(YEAR(_Refdate) , MONTH(_Refdate), DAY(_Refdate) - 364),
        ""LYWTD"",

        -- Default case
        BLANK()
    )
   
        )       
                    
        RETURN 
            _ResultWithAdditionalColumns";
        
Model.AddCalculatedTable("Date",_DateDaxExpression);
        
        



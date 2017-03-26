

EXECUTE sp_execute_external_script    
      @language = N'R'         
    , @input_data_1 = N' select top 5 TodayClosed as Closed, Industry from stockquotes SQ
         inner join StockSymbols SS on SS.ID = SQ.StockId where TodayClosed is not null;'    
    , @script = N'
         library(data.table)
         dtf <- data.frame(InputDataSet)
         dt <- data.table(dtf)         
         OutputDataSet <- dt[,.(mean=mean(Closed),sd=sd(Closed)),by=Industry]'
    WITH RESULT SETS ((Industry char(75) NULL, [mean] float NULL, [sd] float NULL));

--EXECUTE sp_execute_external_script    
--      @language = N'R'         
--    , @input_data_1 = N' select top 5 Bid, TodayClosed as Closed from stockquotes ;'    
--    , @script = N'
--         dtf <- data.frame(InputDataSet)
--         View(dtf)
--         OutputDataSet <- dtf'
--    WITH RESULT SETS (([Closed] float NULL));

--    ,by=Industry
--    , Industry char(75) NULL
--         ---select top 2 TodayClosed as Closed, Industry from stockquotes SQ
--         ---inner join StockSymbols SS on SS.ID = SQ.StockId where TodayClosed is not null;)
--   -- install.packages("data.table")


--EXECUTE sp_execute_external_script    
--      @language = N'R'    
--    , @script = N' OutputDataSet <- InputDataSet;'    
--    , @input_data_1 = N' select top 5 TodayClosed as Closed, Industry from stockquotes SQ
--         inner join StockSymbols SS on SS.ID = SQ.StockId where TodayClosed is not null;'    
--      WITH RESULT SETS ((Closed float NULL, Industry char(75) NULL));
----      WITH RESULT SETS (([mean] float NULL, [sd] float NULL, Industry char(75) NULL));

--EXECUTE sp_execute_external_script  
--@language=N'R'  
--,@script = N'str(OutputDataSet);  
--packagematrix <- installed.packages();  
--OutputDataSet <- as.data.frame(packagematrix);'  
--,@input_data_1 = N'SELECT 1 as col'  
--WITH RESULT SETS ((PackageName nvarchar(250) )) 

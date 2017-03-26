-- Place SQL query retrieving data for the R stored procedure here
select top 1000 TodayClosed, Industry from stockquotes SQ
   inner join StockSymbols SS
      on SS.ID = SQ.StockId
where TodayClosed is not null
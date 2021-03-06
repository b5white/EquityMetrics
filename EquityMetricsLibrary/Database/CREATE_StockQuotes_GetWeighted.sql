USE [StockPicker]
GO

/****** Object:  StoredProcedure [dbo].[StockSymbols_GetAll]    Script Date: 2/7/2017 11:39:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop procedure StockQuotes_GetWeighted
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[StockQuotes_GetWeighted] 
           @Weight int
AS
BEGIN

With BaseCTE ( Symbol, Min, Max, MaxDate) as 
(select Symbol, min(TodayClosed), max(TodayClosed), max(DateTime)
from StockQuotes
where symbol like 'A%'
and datetime > DateAdd(year, -1, GetDate())
group by Symbol)

, ChildCTE ( Symbol, Min, Max, Mult, Closed, Base) as 
(select S.Symbol, S.Min, S.Max, 1 / (Max - Min + 0.01) as Mult, Q.TodayClosed, (TodayClosed - Min + 0.01) as Base
  from StockQuotes Q
  inner join BaseCTE S 
    on S.Symbol = Q.Symbol
	and S.MaxDate = Q.DateTime)

Select Symbol, Min, Max, Mult, Closed, Base, 100 - Round(Base * Mult * @Weight, 1) as Score from ChildCTE
order by Score desc

END
GO
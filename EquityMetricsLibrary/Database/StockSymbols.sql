/*
   Saturday, October 22, 201612:03:09 AM
   User: sa
   Server: DESKTOP-NHSGI12\SQLEXPRESS
   Database: StockPicker
   Application: 
*/

-- http://www.kibot.com/Files/2/Russell_3000_Intraday.txt

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
--DROP TABLE dbo.StockSymbols;

CREATE TABLE dbo.StockSymbols
	(
	ID int NOT NULL,
	Symbol char(6) NOT NULL,
	StartDate date NOT NULL,
	Size float NOT NULL,
	Description char(75) NULL,
	Exchange char(15) NULL,
	Industry char(75) NULL,
	Sector char(25) NULL
	)  ON [PRIMARY]
GO

COMMIT

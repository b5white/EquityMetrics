BEGIN {
	FS = "\t"
	OFS = " "
   Heading = 1;
	
	print "-- Retrieved from http://www.kibot.com/Files/2/Russell_3000_Intraday.txt"
}

# #	Symbol	StartDate	Size(MB)	Description	Exchange	Industry	Sector

# Before
# 1	A	11/18/1999	75.53	Agilent Technologies, Inc.	NYSE	Biotechnology: Laboratory Analytical Instruments	Capital Goods
# 2	AA	1/2/1998	90.87	Alcoa Inc.	NYSE	Metal Fabrications	Capital Goods

# After
# INSERT INTO StockSymbols VALUES (1,'A', '11/18/1999', 76.5, 'Agilent Technologies, Inc.', 'NYSE', 'Biotechnology: Laboratory Analytical Instruments', 'Capital Goods')
# INSERT INTO StockSymbols VALUES (2,'AA', '1/2/1998', 91.93, 'Alcoa Corporation', 'NYSE', 'Metal Fabrications', 'Capital Goods')

Heading == 1 && ($1 == "Listed:") {   
   print "--select * from StockSymbols";
   print "--delete  from StockSymbols";
   print "";
   print "	--ID int NOT NULL,";
   print "	--Symbol char(6) NOT NULL,";
   print "	--StartDate date NOT NULL,";
   print "	--Size float NOT NULL,";
   print "	--Description char(75) NULL,";
   print "	--Exchange char(15) NULL,";
   print "	--Industry char(75) NULL,";
   print "	--Sector char(25) NULL";
   print "";
}
Heading == 1 && NF == 0
Heading == 1 && NF > 0 { print "--" $0;}
Heading == 1 && $1 == "#" { Heading = 0; next;}

Heading == 0 { 
   gsub("'", "''", $5);
   printf "INSERT INTO StockSymbols VALUES (%d,'%s', '%s', %f, '%s', '%s', '%s', '%s')\n", $1, $2, $3, $4, $5, $6, $7, $8; 
}

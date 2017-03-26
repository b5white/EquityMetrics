# @InputDataSet: input data frame, result of SQL query execution
# @OutputDataSet: data frame to pass back to SQL

# Test code
library(rsqlserver)
 channel <- odbcDriverConnect(dbConnection)
InputDataSet <- sqlQuery(channel, iconv(paste(readLines('~/visual studio 2015/projects/equitymetrics/rproject/sqlsproc.query.sql', encoding = 'UTF-8', warn = FALSE), collapse = '\n'), from = 'UTF-8', to = 'ASCII', sub = ''))
 odbcClose(channel)

OutputDataSet <- InputDataSet

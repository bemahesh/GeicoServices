
/*
	This script adds 99 tasks with high priority status. This will help with testing adding of new high priority task that hits
	the threshold and returning the error to the user.
*/

--due date is two day from today
declare @dueDate as Datetime = GetDate()
set @dueDate = DATEADD(day, 2, @dueDate)

--startDate is one less than due date
declare @startDate as Datetime
set @startDate = DATEADD(day, -1, @dueDate)

--endDate is on the due date
declare @endDate as Datetime = @dueDate

DECLARE @Counter INT = 1
WHILE ( @Counter <= 99)
BEGIN
	declare @name as varchar(200) = 'Name' + CAST(@Counter AS VARCHAR(100))
	declare @descr as varchar(200) = 'Descr' + CAST(@Counter AS VARCHAR(100))

	EXEC CreateTask @dueDate, 3, 1, @name, @descr, @startDate, @endDate
    SET @Counter  = @Counter  + 1
END


--exec CreateTask '5/4/2022', 1, 1, 'First', 'First', '5/3/2022', '5/4/2022'
--exec CreateTask '5/4/2022', 2, 2, 'Second', 'Second', '5/3/2022', '5/4/2022'
--exec UpdateTask 1, '5/4/2022', 1, 1, 'First2', 'First2', '5/3/2022', '5/4/2022'
--select *
--from task
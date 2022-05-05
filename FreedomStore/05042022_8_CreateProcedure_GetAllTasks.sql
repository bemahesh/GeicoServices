CREATE OR ALTER PROCEDURE GetAllTasks
AS
BEGIN
	--TODO: paging?
	SELECT 
		[TaskId],
		[Name],
		[Description],
		[DueDate],
		[StartDate],
		[EndDate],
		[Priority],
		[Status]
	FROM DBO.TASK
END
GO
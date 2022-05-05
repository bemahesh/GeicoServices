CREATE OR ALTER  PROCEDURE [dbo].[UpdateTask]
	@TaskId as Int,
	@DueDate as DateTime,
	@Priority as SmallInt,
	@Status as SmallInt,
	@Name as NVarchar(50) = null,
	@Description as Nvarchar(100) = null,
	@StartDate as DateTime = null,
	@EndDate as DateTime = null
AS
BEGIN
	
	DECLARE @currentDate AS DATETIME = GETDATE() 
	DECLARE @HighPriorityCount AS INT = 0

	IF(@DueDate < @currentDate)
	BEGIN
		RAISERROR('Due date can''t be in the past', 16, 1)
		RETURN
	END

	IF(@Priority = 3)
	BEGIN
	
		SELECT @HighPriorityCount = Count(TaskId)
		FROM Task
		WHERE Priority = 3
		AND Status <> 3
		AND CAST(DueDate AS DATE) = cast(@DueDate AS DATE)

		--100 is threshold set by requirement, should we parametarize this?
		IF(@HighPriorityCount + 1 > 100)
		BEGIN
			RAISERROR('Too many high priority tasks for the same due date!', 16, 1)
			RETURN
		END
	END

	UPDATE TASK
	SET [Name] = @Name,
		[Description] = @Description,
		DueDate = @DueDate,
		StartDate = @StartDate,
		EndDate = @EndDate,
		[Priority] = @Priority,
		[Status] = @Status,
		UpdatedDate = GetDate(),
		IsActive = 1
	WHERE TaskId = @TaskId

	SELECT *
	FROM Task
	WHERE Taskid = @TaskId

	/*
	TODO:
	 Priority
		 Low = 1,
		 Medium = 2,
		 High = 3

	Status
		New = 1,
        InProgress = 2,
        Finished = 3

	*/

END
GO

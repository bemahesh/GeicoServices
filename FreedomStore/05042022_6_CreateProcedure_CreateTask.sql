CREATE OR ALTER  PROCEDURE [dbo].[CreateTask]
	@DueDate as DateTime,
	@Priority as SmallInt,
	@Status as SmallInt,
	@Name as NVarchar(50) = null,
	@Description as Nvarchar(100) = null,
	@StartDate as DateTime = null,
	@EndDate as DateTime = null
AS
BEGIN

	DECLARE @HighPriorityCount AS INT = 0
	DECLARE @CurrentDate AS DATETIME = GETDATE()

	IF(@DueDate < @currentDate)
	BEGIN
		RAISERROR('Due date can''t be in the past', 16, 1)
		RETURN
	END

	IF(@Priority = 3)
	BEGIN
	
		SELECT @HighPriorityCount = Count(TaskId)
		FROM Task
		WHERE Status <> 3
		AND Priority = 3
		AND CAST(DueDate AS DATE) = cast(@DueDate AS DATE)

		--100 is threshold set by requirement, should we parametarize this?
		IF(@HighPriorityCount + 1 > 100)
		BEGIN
			RAISERROR('Too many high priority tasks for the same due date!', 16, 1)
			RETURN
		END
	END

	DECLARE @insertedTaskId int

	INSERT INTO Task
	([Name], [Description], DueDate, StartDate, EndDate, [Priority], [Status], CreatedDate, IsActive)
	Values
	(@Name, @Description, @DueDate, @StartDate, @EndDate, @Priority, @Status, GETDATE(), 1)

	 
	SELECT @insertedTaskId = SCOPE_IDENTITY()

	SELECT *
	FROM Task
	WHERE Taskid = @insertedTaskId

END
GO
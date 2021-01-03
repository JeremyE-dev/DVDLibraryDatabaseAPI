use DvdLibrary
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectAll')
		drop procedure DvdSelectAll
go

create procedure DvdSelectAll
as
	select DvdId, Title, ReleaseYear, DirectorName, RatingName, Notes
	from Dvd d
	-- inner join not null (inner returns matcing record for both tables)
	-- left join null (if data does not exist on related table fro a record, gap represented by null
	-- use left for fields that allow null values
	left join ReleaseYear y on d.ReleaseYearId = y.ReleaseYearId
	left join Director n on d.DirectorId = n.DirectorId
	left join Rating r on d.RatingId = r.RatingId

	order by Title
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectById')
		drop procedure DvdSelectById
go

create procedure DvdSelectById(
	@DvdId int
)
as 
	select DvdId, Title, ReleaseYear, DirectorName, RatingName, Notes
	from Dvd d
	left join ReleaseYear y on d.ReleaseYearId = y.ReleaseYearId
	left join Director n on d.DirectorId = n.DirectorId
	left join Rating r on d.RatingId = r.RatingId
	
	where DvdId = @DvdId
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectByTitle')
		drop procedure DvdSelectByTitle
go

create procedure DvdSelectByTitle(
	@Title varchar(50)
)

-- should this include join statements to get the ReleaseYear, DirectorName, and rating Name 
-- instead of the id's, adapt if needed
as 
	select DvdId, Title, ReleaseYear, DirectorName, RatingName, Notes
	from Dvd d
	left join ReleaseYear y on d.ReleaseYearId = y.ReleaseYearId
	left join Director n on d.DirectorId = n.DirectorId
	left join Rating r on d.RatingId = r.RatingId
	
	where Title like @Title
go
		
if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectByReleaseYear')
		drop procedure DvdSelectByReleaseYear
go

create procedure DvdSelectByReleaseYear(
	@ReleaseYear int
)

-- should this include join statements to get the ReleaseYear, DirectorName, and rating Name 
-- instead of the id's, adapt if needed
as 
	select DvdId, Title, ReleaseYear, DirectorName, RatingName, Notes
	from Dvd d
	left join ReleaseYear y on d.ReleaseYearId = y.ReleaseYearId
	left join Director n on d.DirectorId = n.DirectorId
	left join Rating r on d.RatingId = r.RatingId
	
	where ReleaseYear =  @ReleaseYear
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectByDirectorName')
		drop procedure DvdSelectByDirectorName
go

create procedure DvdSelectByDirectorName(
	@DirectorName varchar(250)
)

as 
	select DvdId, Title, ReleaseYear, DirectorName, RatingName, Notes
	from Dvd d
	left join ReleaseYear y on d.ReleaseYearId = y.ReleaseYearId
	left join Director n on d.DirectorId = n.DirectorId
	left join Rating r on d.RatingId = r.RatingId
	
	where DirectorName like @DirectorName
go


if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectByRating')
		drop procedure DvdSelectByRating
go

create procedure DvdSelectByRating(
	@RatingName varchar(50)
)

as 
	select DvdId, Title, ReleaseYear, DirectorName, RatingName, Notes
	from Dvd d
	left join ReleaseYear y on d.ReleaseYearId = y.ReleaseYearId
	left join Director n on d.DirectorId = n.DirectorId
	left join Rating r on d.RatingId = r.RatingId
	
	where RatingName like @RatingName
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdInsert')
		drop procedure DvdInsert
go

create procedure DvdInsert (
	@DvdId int output,
	@ReleaseYearId int,
	@DirectorId int,
	@RatingId int,
	@Title varchar(50),
	@Notes varchar(300)
)

as 
	insert into Dvd (ReleaseYearId, DirectorId, RatingId, Title, Notes)
	values(@ReleaseYearId, @DirectorId, @RatingId, @Title, @Notes)

	set @DvdId = SCOPE_IDENTITY()
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdUpdate')
		drop procedure DvdUpdate
go

create procedure DvdUpdate (
	@DvdId int output,
	@ReleaseYearId int,
	@DirectorId int,
	@RatingId int,
	@Title varchar(50),
	@Notes varchar(300)
)

as
	update Dvd
		set ReleaseYearId = @ReleaseYearId,
		DirectorId = @DirectorId,
		RatingId = @RatingId,
		Title = @Title,
		Notes = @Notes
	where DvdId = @DvdId
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdDelete')
		drop procedure DvdDelete
go

create procedure DvdDelete(
	@DvdId int
)

as
	delete from Dvd
	where DvdId = @DvdId
go


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
	@Title varchar (100)
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
	@DirectorName varchar(100)
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
	@RatingName varchar(100)
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
	where ROUTINE_NAME = 'DvdInsertText')
		drop procedure DvdInsertText
go

create procedure DvdInsertText (
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

if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'GetReleaseYearId')
drop procedure GetReleaseYearId

go 

create procedure GetReleaseYearId(
@ReleaseYear int
)
as

select y.ReleaseYearId
from ReleaseYear y
where y.ReleaseYear = @ReleaseYear

go


if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'GetDirectorId')
drop procedure GetDirectorId

go 

create procedure GetDirectorId(
@DirectorName varchar(250)
)
as

select d.DirectorId
from Director d
where d.DirectorName = @DirectorName

go


if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'GetRatingId')
drop procedure GetRatingId

go 

create procedure GetRatingId(
@RatingName varchar(250)
)
as

select r.RatingId
from Rating r
where r.RatingName = @RatingName

go



if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'InsertReleaseYearIdAndYear')
drop procedure InsertReleaseYearIdAndYear

go 

create procedure InsertReleaseYearIdAndYear(
@ReleaseYear int,
@ReleaseYearId int
)
as

set Identity_insert ReleaseYear on

insert into ReleaseYear (ReleaseYearId, ReleaseYear)
values(@ReleaseYearId, @ReleaseYear)

set Identity_insert ReleaseYear off

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'NumberOfRecordsInReleaseYear')
drop procedure NumberOfRecordsInReleaseYear

go 

create procedure NumberOfRecordsInReleaseYear
as

select count(*)
from ReleaseYear

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'InsertRatingIdAndName')
drop procedure InsertRatingIdAndName

go 

create procedure InsertRatingIdAndName(
@RatingName varchar(50),
@RatingId int
)
as

set Identity_insert Rating on

insert into Director (DirectorId, DirectorName)
values(@RatingId, @RatingName)

set Identity_insert Rating off

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'InsertDirectorIdAndName')
drop procedure InsertDirectorIdAndName

go 

create procedure InsertDirectorIdAndName(
@DirectorName varchar(50),
@DirectorId int
)
as

set Identity_insert Director on

insert into Director (DirectorId, DirectorName)
values(@DirectorId, @DirectorName)

set Identity_insert Director off

go







if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'NumberOfRecordsInDirector')
drop procedure NumberOfRecordsInDirector

go 
create procedure NumberOfRecordsInDirector
as

select count(*)
from Director

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
where ROUTINE_NAME = 'NumberOfRecordsInRating')
drop procedure NumberOfRecordsInRating

go 

create procedure NumberOfRecordsInRating
as

select count(*)
from Rating

go



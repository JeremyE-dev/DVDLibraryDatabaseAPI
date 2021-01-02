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
	select DvdId, Title, Notes, ReleaseYearId, DirectorId, RatingId
	from Dvd
	where DvdId = @DvdId
go

		
	
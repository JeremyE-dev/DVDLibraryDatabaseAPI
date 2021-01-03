use DvdLibrary
go

set identity_insert Director on
insert into Director (DirectorId, DirectorName)
values (1, 'Smith'),
	(2, 'Baker'),
	(3, 'Pertwee'),
	(4, 'Hartnell'),
	(5, 'Tennant'),
	(6, 'Eudaly')

set identity_insert Director off

set identity_insert Rating on
insert into Rating (RatingId, RatingName)
values (1, 'G'),
	(2, 'PG'),
	(3, 'PG-13'),
	(4, 'R')
	
set identity_insert Rating off

set identity_insert ReleaseYear on
insert into ReleaseYear(ReleaseYearId, ReleaseYear )
values(1, 1975),
	(2, 1985),
	(3,1995),
	(4, 2000),
	(5, 2010),
	(6, 2021)

set identity_insert ReleaseYear off

set identity_insert Dvd on
insert into Dvd(DvdId, Title, RatingId, DirectorId, ReleaseYearId)
values (1, 'The Lion King', 1, 1, 3),
(2, 'Frontios', 2, 1, 2),
(3, 'Ressurection of the Daleks', 2, 4, 1),
(4, 'Dalek Invasion of Earth', 2, 2, 4)

set identity_insert Dvd off


use DvdLibrary

SET IDENTITY_INSERT ON

INSERT INTO Director(DirectorId, DirectorName)
VALUES(1,'Hartnell'),
(2,'Troughton'),
(3, 'Pertwee'),
(4, 'Baker')

SET IDENTITY_INSERT OFF

SET IDENTITY_INSERT ON

INSERT INTO Rating (RatingId, RatingName)
VALUES (1, 'G'),
(2, 'PG'),
(3,'PG-13'),
(4, 'R')

SET IDENTITY_INSERT OFF


SET IDENTITY_INSERT ON

INSERT INTO ReleaseYear(ReleaseYearId, ReleaseYear)
VALUES (1, '1960'),
(2, '1970'),
(3,'1980'),
(4, '1990')

SET IDENTITY_INSERT OFF


SET IDENTITY_INSERT ON

INSERT INTO Dvd(DvdId, ReleaseYearId, DirectorId, RatingId, Title, Notes)
VALUES(1, 1, 1, 1, 'Movie1', 'Notes1'),
VALUES(2, 2, 2, 2, 'Movie2', 'Notes2'),
VALUES(3, 3, 3, 3, 'Movie3', 'Notes3'),
VALUES(3, 3, 3, 3, 'Movie4', 'Notes4')

SET IDENTITY_INSERT OFF

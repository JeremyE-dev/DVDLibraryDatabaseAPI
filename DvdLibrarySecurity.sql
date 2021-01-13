USE master
GO

CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

USE DvdLibrary
GO

CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO

GRANT EXECUTE ON DvdDelete TO DvdLibraryApp
GRANT EXECUTE ON DvdInsert TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectAll TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByDirectorName TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectById TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByRating TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByReleaseYear TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectByTitle TO DvdLibraryApp
GRANT EXECUTE ON DvdUpdate TO DvdLibraryApp
GRANT EXECUTE ON GetDirectorId TO DvdLibraryApp
GRANT EXECUTE ON GetRatingId TO DvdLibraryApp
GRANT EXECUTE ON GetReleaseYearId TO DvdLibraryApp
GRANT EXECUTE ON InsertDirectorIdAndName TO DvdLibraryApp
GRANT EXECUTE ON InsertRatingIdAndName TO DvdLibraryApp
GRANT EXECUTE ON InsertReleaseYearIdAndYear TO DvdLibraryApp
GRANT EXECUTE ON NumberOfRecordsInDirector TO DvdLibraryApp
GRANT EXECUTE ON NumberOfRecordsInRating TO DvdLibraryApp
GRANT EXECUTE ON NumberOfRecordsInReleaseYear TO DvdLibraryApp
GO

GRANT SELECT ON Dvd TO DvdLibraryApp
GRANT INSERT ON Dvd TO DvdLibraryApp
GRANT UPDATE On Dvd TO DvdLibraryApp
GRANT DELETE ON Dvd TO DVDLibraryApp

GRANT SELECT ON Director TO DvdLibraryApp
GRANT INSERT ON Director TO DvdLibraryApp
GRANT UPDATE On Director TO DvdLibraryApp
GRANT DELETE ON Director TO DVDLibraryApp

GRANT SELECT ON Rating TO DVDbraryApp
GRANT INSERT ON Rating TO DvdLibraryApp
GRANT UPDATE On Rating TO DvdLibraryApp
GRANT DELETE ON Rating TO DVDLibraryApp

GRANT SELECT ON ReleaseYear TO DVDbraryApp
GRANT INSERT ON ReleaseYear TO DvdLibraryApp
GRANT UPDATE On ReleaseYear TO DvdLibraryApp
GRANT DELETE ON ReleaseYear TO DVDLibraryApp
GO
use master;
go

if exists(select * from sys.databases where name='DvdLibrary')
drop database DvdLibrary
go

create database DvdLibrary;
go

use DvdLibrary
go

-- tables (Year, Director, Rating, Dvd)
-- Drop tables
if exists(select * from sys.tables where name='Dvd')
		drop table Dvd

if exists(select * from sys.tables where name='Rating')
		drop table Rating

if exists(select * from sys.tables where name='Director')
		drop table Director

if exists(select * from sys.tables where name='ReleaseYear')
		drop table ReleaseYear

-- Create Tables
create table ReleaseYear (
		ReleaseYearId int identity (1,1) primary key not null,
		ReleaseYear int not null
)

create table Director (
		DirectorId int identity (1,1) primary key not null,
		DirectorName varchar(250) not null
)

create table Rating (
		RatingId int identity (1,1) primary key not null,
		RatingName varchar(50) not null
)

create table Dvd(
		DvdId int identity (1,1) primary key not null,
		ReleaseYearId int foreign key references ReleaseYear(ReleaseYearId) null,
		DirectorId int foreign key references Director(DirectorId) null,
		RatingId int foreign key references Rating(RatingId) null,
		Title varchar(50) not null,
		Notes varchar(300) null

)


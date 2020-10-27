delete from dbo.Genres
DBCC CHECKIDENT('Genres', RESEED, 0)
go
use [Albums]
GO

-- Drop Constraints

ALTER TABLE Album DROP CONSTRAINT FK_Album_Artist_ArtistId
ALTER TABLE Album DROP CONSTRAINT FK_Album_RecordLabel_RecordLabelId
ALTER TABLE Artist DROP CONSTRAINT FK_Artist_RecordLabel_RecordLabelId
GO

TRUNCATE TABLE Album
TRUNCATE TABLE Artist
TRUNCATE TABLE RecordLabel
TRUNCATE TABLE Certifications
GO

-- Bulk insert from CSV files

BULK INSERT Album
FROM 'D:/Labs Sem4/Lab SDI/lab-5x-913-Hornea-Dorian-Alexandru/DB Inserts/albums.csv'
WITH(FORMAT = 'CSV', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n');
GO

BULK INSERT Artist
FROM 'D:/Labs Sem4/Lab SDI/lab-5x-913-Hornea-Dorian-Alexandru/DB Inserts/artists.csv'
WITH(FORMAT = 'CSV', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n');
GO

BULK INSERT RecordLabel
FROM 'D:/Labs Sem4/Lab SDI/lab-5x-913-Hornea-Dorian-Alexandru/DB Inserts/record_labels.csv'
WITH(FORMAT = 'CSV', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n');
GO

BULK INSERT Certifications
FROM 'D:/Labs Sem4/Lab SDI/lab-5x-913-Hornea-Dorian-Alexandru/DB Inserts/certifications.csv'
WITH(FORMAT = 'CSV', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n');
GO

-- Re-Create Constraints
ALTER TABLE Album ADD CONSTRAINT FK_Album_Artist FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID)
ALTER TABLE Album ADD CONSTRAINT FK_Album_RecordLabel FOREIGN KEY (RecordLabelID) REFERENCES RecordLabel(RecordLabelID)
ALTER TABLE Artist ADD CONSTRAINT FK_Artist_RecordLabel FOREIGN KEY (RecordLabelID) REFERENCES RecordLabel(RecordLabelID)
GO

SELECT COUNT(*) AS 'Albums' from Album
SELECT COUNT(*) AS 'Artists' from Artist
SELECT COUNT(*) AS 'Record Labels' from RecordLabel
SELECT COUNT(*) AS 'Certifications' from Certifications
SELECT * FROM Certifications
GO

CREATE TYPE PersonalTableType AS TABLE
(
    Name NVARCHAR(100),
    GenderName NVARCHAR(10),
    HobbyName NVARCHAR(50),
    Age INT
)
GO

CREATE PROCEDURE InsertPersonalData
    @PersonalData PersonalTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert atau update Gender
        MERGE INTO tblM_gender AS target
        USING (SELECT DISTINCT GenderName FROM @PersonalData) AS source
        ON target.Name = source.GenderName
        WHEN NOT MATCHED THEN
            INSERT (Name) VALUES (source.GenderName);

        -- Insert atau update Hobby
        MERGE INTO tblM_hobby AS target
        USING (SELECT DISTINCT HobbyName FROM @PersonalData) AS source
        ON target.Name = source.HobbyName
        WHEN NOT MATCHED THEN
            INSERT (Name) VALUES (source.HobbyName);

        -- Insert Personal data
        INSERT INTO tblT_personal (Name, GenderId, HobbyId, Age)
        SELECT 
            p.Name,
            g.Id AS GenderId,
            h.Id AS HobbyId,
            p.Age
        FROM @PersonalData p
        JOIN tblM_gender g ON p.GenderName = g.Name
        JOIN tblM_hobby h ON p.HobbyName = h.Name;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE GetGenderTotals
AS
BEGIN
    SELECT 
        g.Name AS GenderName,
        COUNT(p.Id) AS Total
    FROM 
        tblM_gender g
        LEFT JOIN tblT_personal p ON g.Id = p.GenderId
    GROUP BY 
        g.Name
    ORDER BY 
        g.Name
END
GO
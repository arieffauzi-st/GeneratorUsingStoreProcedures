   -- Bagian 1: Data personal dengan detail gender dan hobi
   SELECT TOP 3 p.id, p.name, p.genderId, g.Name AS GenderName, p.hobbyId, h.Name AS hobbyName, p.Age
   FROM tblT_personal p
   JOIN tblM_gender g ON p.genderId = g.Id
   JOIN tblM_hobby h ON p.hobbyId = h.Id
   ORDER BY p.id;

   -- Bagian 2: Total per gender (menggunakan stored procedure)
   EXEC GetGenderTotals
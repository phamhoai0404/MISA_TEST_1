CREATE DEFINER = 'dev'@'%'
PROCEDURE `MISA.WEB12.HOAIPT`.Proc_ExportEmployee(OUT m_totalRecord INT)
BEGIN
   CREATE TEMPORARY TABLE IF NOT EXISTS tbEmployee AS 
  (
    SELECT @row_num := @row_num +1 AS SortIndex, vee.*
    FROM View_ExportEmployee vee,  (SELECT @row_num:= 0 AS num) AS c 
  );
  SELECT * FROM tbEmployee;
  SET m_totalRecord = (SELECT COUNT(*) FROM tbEmployee);
  DROP  TABLE tbEmployee;
END
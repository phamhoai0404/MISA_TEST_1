CREATE DEFINER = 'dev'@'%'
PROCEDURE `MISA.WEB12.HOAIPT`.Proc_GetEmployeePaging(IN m_SearchText VARCHAR(255), IN m_PageIndex INT, IN m_PageSize INT, OUT m_TotalRecord INT, OUT m_TotalPage INT)
  COMMENT 'Dùng để thực hiện phân trang đồng thời tìm kiếm'
BEGIN
-- Lấy ra tổng số bản ghi ban đầu khi chưa thực hiện gì
SET m_TotalRecord = (SELECT COUNT(*) FROM View_AllEmployee);

-- Thực hiện gián giá trị bằng rỗng nếu từ khóa tìm kiếm bằng null
IF(m_SearchText IS NULL) THEN
  SET m_SearchText = '';
END IF;

-- Nếu mà thứ tự trang không có giá trị hoặc nhỏ hơn 1, Số trang cũng null hoặc nhỏ hơn 1
IF (m_PageIndex IS NULL OR m_PageIndex < 1 OR m_PageSize IS NULL OR m_PageSize < 1) THEN
  SET m_TotalPage = 1; /*Tổng số trang lúc này chỉ là duy nhất*/
  SET m_PageIndex = 1;/*Thứ tự trang cũng là đầu tiên*/
  set m_PageSize = m_TotalRecord;/*Kích thước trang lúc này bằng đúng tổng số bản ghi ban đầu*/
END IF;

CREATE TEMPORARY TABLE IF NOT EXISTS tbEmployee AS(/*Tạo bảng tạm có tên tbEmployee để thực hiện tìm kiếm */
  SELECT @row_num := @row_num + 1 AS SortIndex, vee.* /*Chèn thêm số thứ tự để thực hiện phân trang*/
  FROM View_AllEmployee vee,  (SELECT @row_num:= 0 AS num) AS c
  WHERE vee.FullName LIKE CONCAT('%',m_SearchText,'%') OR vee.EmployeeCode LIKE CONCAT('%',m_SearchText,'%')
);

SET @EndIndex = m_PageIndex *m_PageSize; /*Số thứ tự Kết thúc của trang*/
SET @StartIndex = @EndIndex - m_PageSize + 1;/*Số thứ tự Bắt đầu của trang*/
SELECT * FROM tbEmployee WHERE SortIndex BETWEEN @StartIndex AND @EndIndex;/*Thực hiện lấy ra số bản ghi phân trang*/

-- Thiết lập Lại giá trị cho tổng số bản ghi
SET m_TotalRecord = (SELECT COUNT(*) FROM tbEmployee);

IF(m_PageSize > 0 AND m_TotalRecord > 0 ) THEN /*Nếu tồn tại bản ghi thì*/
  SET @Surplus = m_TotalRecord % m_PageSize;/*Thực hiện tìm số dư*/
  IF(@Surplus > 0 )THEN/*Nếu tồn tại số dư*/
    SET m_TotalPage = m_TotalRecord DIV m_PageSize + 1;/*Số trang + 1 */
  ELSE
    SET m_TotalPage = m_TotalRecord DIV m_PageSize; END IF;
ELSE
  SET m_TotalPage = 1;/*Nếu không tồn tại bản ghi nào thì số trang = 1*/
END IF;
  
DROP TABLE tbEmployee;
END
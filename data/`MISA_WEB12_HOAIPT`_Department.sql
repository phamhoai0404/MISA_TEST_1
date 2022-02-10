CREATE TABLE `MISA.WEB12.HOAIPT`.Department (
  DepartmentId char(36) NOT NULL DEFAULT '' COMMENT 'Khóa chính',
  DepartmentName varchar(255) NOT NULL DEFAULT '' COMMENT 'Tên phòng ban',
  Description varchar(255) DEFAULT NULL COMMENT 'Mô tả phòng ban',
  CreatedDate datetime DEFAULT NULL COMMENT 'Ngày tạo phòng ban',
  CreatedBy varchar(255) DEFAULT NULL COMMENT 'Tạo bởi người',
  ModifiedDate datetime DEFAULT NULL COMMENT 'Thời gian cập nhật',
  ModifiedBy varchar(255) DEFAULT NULL COMMENT 'Người cập nhật',
  PRIMARY KEY (DepartmentId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8,
COLLATE utf8_general_ci,
COMMENT = 'Phòng ban';
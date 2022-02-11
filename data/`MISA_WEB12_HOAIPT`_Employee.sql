CREATE TABLE `MISA.WEB12.HOAIPT`.Employee (
  EmployeeId char(36) NOT NULL DEFAULT '' COMMENT 'Khóa chính nhân viên',
  EmployeeCode char(20) NOT NULL DEFAULT '' COMMENT 'Mã nhân viên',
  FullName varchar(100) NOT NULL DEFAULT '' COMMENT 'Họ tên nhân viên',
  DateOfBirth date DEFAULT NULL COMMENT 'Ngày sinh',
  Gender int(11) DEFAULT NULL COMMENT 'Giới tính { 0: Nữ, 1: Nam, 2: Khác, null: Không xác định}',
  IdentityNumber varchar(20) DEFAULT NULL COMMENT 'Số CMND',
  IdentityDate date DEFAULT NULL COMMENT 'Ngày cấp CMND',
  IdentityPlace varchar(255) DEFAULT NULL COMMENT 'Nơi cấp CMND',
  PositionName varchar(255) DEFAULT NULL COMMENT 'Tên vị trí chức vụ',
  MobilephoneNumber varchar(10) DEFAULT NULL COMMENT 'Số ĐT di động',
  Address varchar(255) DEFAULT NULL COMMENT 'Địa chỉ',
  TelephoneNumber varchar(15) DEFAULT NULL COMMENT 'Số ĐT cố định',
  Email varchar(100) DEFAULT NULL COMMENT 'Địa chỉ email',
  BankAccountNumber varchar(255) DEFAULT NULL COMMENT 'Số tài khoản ngân hàng',
  BankBranchName varchar(255) DEFAULT NULL COMMENT 'Chi nhánh ngân hàng',
  BankName varchar(255) DEFAULT NULL COMMENT 'Tên ngân hàng',
  IsSupplier tinyint(1) DEFAULT NULL COMMENT 'true: là khách hàng; false,null: không phải là khách hàng',
  IsCustomer tinyint(1) DEFAULT NULL COMMENT 'true: là nhà cung cấp; false,null: không phải là nhà cung cấp',
  DepartmentId char(36) NOT NULL DEFAULT '' COMMENT 'Mã phòng ban',
  CreatedDate datetime DEFAULT NULL COMMENT 'Thời gian tạo nhân viên',
  CreatedBy varchar(255) DEFAULT NULL COMMENT 'Người tạo nhân viên',
  ModifiedDate datetime DEFAULT NULL COMMENT 'Thời  gian sửa nhân viên',
  ModifiedBy varchar(255) DEFAULT NULL COMMENT 'Người cập nhật nhân viên',
  PRIMARY KEY (EmployeeId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 546,
CHARACTER SET utf8,
COLLATE utf8_general_ci,
COMMENT = 'Nhân viên';

ALTER TABLE `MISA.WEB12.HOAIPT`.Employee
ADD UNIQUE INDEX UK_Employee_EmployeeCode (EmployeeCode);

ALTER TABLE `MISA.WEB12.HOAIPT`.Employee
ADD CONSTRAINT FK_Employee_DeparmentId FOREIGN KEY (DepartmentId)
REFERENCES `MISA.WEB12.HOAIPT`.Department (DepartmentId) ON DELETE NO ACTION;
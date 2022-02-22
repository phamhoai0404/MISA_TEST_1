<template>
<div class="m-dialog">
    <div class="m-dialog-content">
        <div class="m-dialog-header">
            <div class="m-dialog-title">
                <div class="m-title" data-title="xiinh gái">Thông tin nhân viên</div>
                <div class="m-checkbox">
                    <input type="checkbox" id="khachhang" />
                    <label class="m-label" for="khachhang" style="font-family: Notosans;">
                        <span></span> &nbsp; &nbsp;&nbsp;Là khách hàng</label>
                </div>
                <div class="m-checkbox">
                    <input type="checkbox" id="cungcap" />
                    <label class="m-label" for="cungcap" style="font-family: Notosans;"> <span></span> &nbsp; &nbsp;&nbsp;Là nhà cung cấp</label>
                </div>
            </div>
            <div class="m-dialog-close">
                <div class="m-icon m-icon-help"></div>
                <div class="m-icon m-icon-close" @click='btnCloseDialog()'></div>

            </div>
        </div>

        <div class="m-dialog-body">
            <div class="m-dialog-ct">
                <div class="m-dialog-one" style="padding-bottom: 24px">
                    <div class="m-dialog-left">
                        <div class="m-row-one">
                            <div class="m-content-input" style="width: 40%;">
                                <div class="m-label">Mã <span>*</span></div>
                                <input type="text" ref='focusMe' class="m-input" v-model="employee.EmployeeCode" @input="inputOnChange(employee.EmployeeCode, $event, 1)" id="employeeCode">
                            </div>
                            <div class="m-content-input" style="width: 58%;">
                                <div class="m-label">Tên <span>*</span></div>
                                <input type="text" class="m-input" v-model="employee.FullName" @input="inputOnChange(employee.FullName, $event,2)" id="employeeName" @mouseleave="standardizeFullName()">
                            </div>
                        </div>
                        <div class="m-content-combobox" style="padding: 0 0 12px 0">
                            <div class="m-label">Đơn vị <span>*</span> </div>
                            <div class="m-dialog-combobox" id="departmentName" @mouseleave="isShowDepartment = false">
                                <input type="text" v-model="employee.DepartmentName" readonly>
                                <button class="" @click='btnSelectDepartment()'>
                                    <div class="m-icon-button m-icon-down"></div>
                                </button>
                                <div class="m-dialog-combobox-data" v-if="isShowDepartment">
                                    <div v-for="(department,index) in listDepartment" :key="index" class="m-dialog-combobox-item" :class="{'selected': department.DepartmentId == employee.DepartmentId}" @click='selectedDepartment(index)'>{{ department.DepartmentName }}</div>
                                </div>
                            </div>
                        </div>
                        <div class="m-content-input ">
                            <div class="m-label ">Chức danh</div>
                            <input type="text " class="m-input" v-model="employee.PositionName">
                        </div>
                    </div>
                    <div class="m-dialog-right ">
                        <div class="m-row-one">
                            <div class="m-content-input" style="width: 40%">
                                <div class="m-label">Ngày sinh</div>
                                <v-date-picker v-model="employee.DateOfBirth" color="green" :max-date='new Date()' :masks="masks">
                                    <template v-slot="{ inputValue, inputEvents ,togglePopover }">
                                        <div class="form-date">
                                            <input type="text" class="m-title-date" placeholder="DD/MM/YYYY" :value="inputValue" v-on="inputEvents">
                                            <div class="m-icon-" @click="togglePopover()">
                                                <div class="m-icon-date"></div>
                                            </div>
                                        </div>
                                    </template>
                                </v-date-picker>
                            </div>
                            <div class="m-content-radio " style="width: 58%">
                                <div class="m-label">Giới tính</div>
                                <div class="m-groud-content-radio">
                                    <div class="m-item">
                                        <input type="radio" class="m-item-radio" value="1" v-model="employee.Gender" id="nam" name="employeeGender">
                                        <label class="m-item-content" for="nam">
                                            <span></span>
                                            Nam</label>
                                    </div>
                                    <div class="m-item">
                                        <input type="radio" class="m-item-radio" value="0" v-model="employee.Gender" id="nu" name="employeeGender">
                                        <label class="m-item-content" for="nu">
                                            <span></span>
                                            Nữ</label>
                                    </div>
                                    <div class="m-item">
                                        <input type="radio" class="m-item-radio" value="2" v-model="employee.Gender" id="khac" name="employeeGender">
                                        <label class="m-item-content" for="khac">
                                            <span></span>
                                            Khác
                                        </label>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="m-row-one">
                            <div class="m-content-input" style="width: 58%;">
                                <div class="m-label" title="Số Chứng minh nhân dân">Số CMND</div>
                                <input type="text" class="m-input" v-model="employee.IdentityNumber">
                            </div>
                            <div class="m-content-input" style="width: 40%;">
                                <div class="m-label">Ngày cấp</div>
                                <v-date-picker v-model="employee.IdentityDate" color="green" :max-date='new Date()' :masks="masks">
                                    <template v-slot="{ inputValue, inputEvents ,togglePopover }">
                                        <div class="form-date">
                                            <input type="text" class="m-title-date" placeholder="DD/MM/YYYY" :value="inputValue" v-on="inputEvents">
                                            <div class="m-icon-" @click="togglePopover()">
                                                <div class="m-icon-date"></div>
                                            </div>
                                        </div>
                                    </template>
                                </v-date-picker>
                            </div>
                        </div>
                        <div class="m-content-input ">
                            <div class="m-label ">Nơi cấp</div>
                            <input type="text " class="m-input " v-model="employee.IdentityPlace" />
                        </div>
                    </div>

                </div>
                <div class="m-dialog-two">
                    <div class="m-content-input" style="width: 100%">
                        <div class="m-label ">Địa chỉ</div>
                        <input type="text " class="m-input " v-model="employee.Address">
                    </div>
                    <div class="m-row-two">
                        <div class="m-content-input">
                            <div class="m-label" title="Điện thoại di động">ĐT di động</div>
                            <input type="text" class="m-input" v-model="employee.MobilephoneNumber">
                        </div>
                        <div class="m-content-input">
                            <div class="m-label" title="Điện thoại cố định">ĐT cố định</div>
                            <input type="text" class="m-input" v-model="employee.TelephoneNumber">
                        </div>
                        <div class="m-content-input">
                            <div class="m-label">Email</div>
                            <input type="text" class="m-input" v-model="employee.Email">
                        </div>
                    </div>
                    <div class="m-row-two">
                        <div class="m-content-input">
                            <div class="m-label">Tài khoản ngân hàng</div>
                            <input type="text" class="m-input" v-model="employee.BankAccountNumber">
                        </div>
                        <div class="m-content-input">
                            <div class="m-label">Tên ngân hàng</div>
                            <input type="text" class="m-input" v-model="employee.BankName">
                        </div>
                        <div class="m-content-input">
                            <div class="m-label">Chi nhánh</div>
                            <input type="text" class="m-input" v-model="employee.BankBranchName">
                        </div>
                    </div>
                </div>
            </div>
            <div class="m-dialog-line "></div>
            <div class="m-dialog-footer ">
                <div class="m-footer-left ">
                    <button class="m-button m-button-not-color " @click="btnCancelDialogDetail()">Hủy</button>
                </div>
                <div class="m-footer-right ">
                    <button class="m-button m-button-not-color" @click="btnSaveOnClick(1)" v-shortkey="['ctrl', 's']" @shortkey="btnSaveOnClick(1)" title="Thêm (Ctrl+S)">Cất</button>
                    <button class="m-button" @click="btnSaveOnClick(2)" v-shortkey="['ctrl', 'shift', 's']" @shortkey="btnSaveOnClick(2)" title="Cất và Thêm (Ctrl+Shift+S)">Cất và Thêm</button>
                </div>
            </div>
        </div>
    </div>
    <MessageInfoEmployee v-if="isShowInfoMessage" @openInfoEmployee="isShowInfoMessage =!isShowInfoMessage" :errorData='errorInfo' />
    <MessageWarningEmployee v-if="isShowWarningMessage" @openWarningMessage="isShowWarningMessage =!isShowWarningMessage" :employeeCode='employeeCode' />
    <MessageConfirmEdit v-if="isShowEditMessage" @openEditEmployee="isShowEditMessage=!isShowEditMessage" @closeDialogDetail="btnCancelDialogDetail()" @closeDialogDetailAndSave="btnSaveOnClick(1)" />
</div>
</template>

<script>
import MessageInfoEmployee from '@/view/employee/EmployeeInfor.vue'
import MessageWarningEmployee from '@/view/employee/EmployeeWarning.vue'
import MessageConfirmEdit from '@/view/employee/EmployeeConfirmEdit.vue'
import axios from 'axios'

import * as mylib from '../../js/resourcs'

export default {
    components: {
        MessageInfoEmployee,
        MessageWarningEmployee,
        MessageConfirmEdit
    },
    props: ['editMode', 'employee', 'listDepartment'],
    data() {
        return {
            masks: {
                input: 'DD/MM/YYYY', //Dạng format của kiểu date
            },

            isShowInfoMessage: false, //Trạng thái đầu tiên của form Thông tin lỗi
            isShowWarningMessage: false, //Trạng thái đầu tiên của form cảnh báo lỗi
            isShowEditMessage: false, //Trạng thái đầu tiên của form edit

            isShowDepartment: false, //Trạng thái đầu tiên của combobox department
            employeeCode: "",

            errorInfo: "", //Thông tin lỗi

            departmentName: null, //Phải tạo cái trung gian này vì nếu theo dõi employee thì employee thay đổi thằng khác thì nó sẽ ảnh hưởng đến departmentName
        }
    },
    mounted() {
        this.$refs.focusMe.focus(); //Tập trung vào ô mã đầu tiên
    },

    watch: {
        /**
         * Theo dõi sự thay đổi của tên phòng ban
         * CreatedBy: HoaiPT(08/02/2022)
         */
        departmentName: function (value) {
            if (!value) {
                document.getElementById("departmentName").classList.add('m-border-red');
                document.getElementById("departmentName").setAttribute("title",mylib.resourcs["VI"].notNullDepartmentName);
            } else {
                document.getElementById("departmentName").classList.remove('m-border-red');
                document.getElementById("departmentName").setAttribute("title","");
            }
        },

        /**
         * Theo dõi sự thay đổi của isShowDepartment phục vụ cho css thôi
         * CreatedBy: HoaiPT(08/02/2022)
         */
        isShowDepartment: function (value) {
            if (value) {
                document.getElementById("departmentName").classList.add('m-border-green');
            } else {
                document.getElementById("departmentName").classList.remove('m-border-green');
            }
        }
    },

    methods: {
        /**
         * Thực hiện thay đổi trạng thái đóng mở của Department
         *CreatedBy: HoaiPT(07/02/2022)
         */
        btnSelectDepartment() {
            this.isShowDepartment = !this.isShowDepartment;
        },
        /**
         * Thực hiện thay đổi giá trị của DepartmentId, DepartmentName khi mà có sự lựa chọn thay đổi
         *CreatedBy: HoaiPT(07/02/2022)
         */
        selectedDepartment(index) {
            var me = this;
            me.isShowDepartment = !me.isShowDepartment;
            me.employee.DepartmentId = me.listDepartment[index].DepartmentId;
            me.employee.DepartmentName = me.listDepartment[index].DepartmentName;

            me.departmentName = me.employee.DepartmentName; //Trung gian để css thôi

        },

        /**
         * Hành động khi click vào nút dấu (x) ở Detail
         *CreatedBy: HoaiPT(07/02/2022)
         */
        btnCloseDialog() {
            var me = this;
            me.isShowEditMessage = true; //Thực hiện mở form Edit của Detail    
        },
        /**
         * Hành động khi ấn vào nút (Cất) hoặc (Cất và Thêm)
         *CreatedBy: HoaiPT(7/02/2022)
         */
        async btnSaveOnClick(value) {
            try {
                var me = this;

                //Validate dữ liệu nếu mà nó không thỏa mãn thì kết thúc luôn
                if (!await me.validateData()) {
                    return;
                }

                //Phải format về dạng này thì nó mới nhận đúng
                me.employee.DateOfBirth = me.formatDate(me.employee.DateOfBirth);
                me.employee.IdentityDate = me.formatDate(me.employee.IdentityDate);

                //Lưu lại thời gian tạo nếu là tạo nhân viên mới
                if (me.editMode == mylib.misaEnum.editMode.Add) {
                    me.employee.CreatedDate = me.formatDateAndTimeNow();
                }

                //Lưu lại thời gian cập nhật  dữ liệu
                me.employee.ModifiedDate = me.formatDateAndTimeNow();

                switch (me.editMode) {
                    case mylib.misaEnum.editMode.Add: //Thực hiện thêm mới
                        await axios.post('https://localhost:44338/api/v1/Employees', me.employee)
                            .then(function () {
                                me.checkAction(value);
                            })
                            .catch(function () {
                                me.openWarning(me);
                            })
                        break;
                    case mylib.misaEnum.editMode.Edit: //Thực hiện sửa
                        await axios.put(`https://localhost:44338/api/v1/Employees/${me.employee.EmployeeId}`, me.employee)
                            .then(function () {
                                me.checkAction(value);

                            })
                            .catch(function () {
                                me.openWarning(me);
                            })
                        break;

                    default:
                        alert('có lỗi xảy ra');
                        break;
                }

            } catch (error) {
                console.error('Lỗi gì đó ' + error);
            }
        },
        /**
         * Thực hiện hủy hành động
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnCancelDialogDetail() {
            var me = this;
            me.$parent.isShowEmployeeDetail = false;
        },
        /**
         * Thực hiện css cho ô input có giá trị bằng rỗng
         *CreatedBy: HoaiPT(07/02/2022)
         */
        inputOnChange(valueField, ee, value) {
            if (!valueField) {
                ee.target.classList.add('m-border-red');
                if (value == 1) {
                    value = mylib.resourcs["VI"].notNullEmployeeCode;
                } else {
                    value = mylib.resourcs["VI"].notNullFullName;
                }
                ee.target.setAttribute("title", value);
            } else {
                ee.target.classList.remove('m-border-red');
                ee.target.setAttribute("title", "");
            }
        },
        /**
         * Thực hiện validate cho dữ liệu cho Form detail
         *CreatedBy: HoaiPT(07/02/2022)
         *UpdatedBy: HoaiPT(21/02/2022)
         */
        async validateData() {
            var me = this;
            //Không được để trống mã, họ tên và đơn vị
            if (!me.employee.EmployeeCode || !me.employee.FullName || !me.employee.DepartmentName) {
                me.errorInfo = "";
                //Phải style cho border nếu để trống
                if (!me.employee.EmployeeCode) {
                    let employeeCode = document.getElementById("employeeCode");
                    employeeCode.classList.add('m-border-red');
                    employeeCode.setAttribute("title", mylib.resourcs["VI"].notNullEmployeeCode);
                    me.errorInfo += "Mã";
                }
                if (!me.employee.FullName) {
                    let employeeName = document.getElementById("employeeName");
                    employeeName.classList.add('m-border-red');
                    employeeName.setAttribute("title", mylib.resourcs["VI"].notNullFullName);

                    me.errorInfo += me.errorInfo != "" ? ", Tên" : "Tên";
                }
                if (!me.employee.DepartmentName) {
                    let departmentName = document.getElementById("departmentName");
                    departmentName.classList.add('m-border-red');
                    departmentName.setAttribute("title", mylib.resourcs["VI"].notNullDepartmentName);

                    me.errorInfo += me.errorInfo != "" ? ", Đơn vị" : "Đơn vị";
                }
                me.errorInfo += " không được để trống.";

                //Hiển thị thông báo
                me.isShowInfoMessage = true;
                return false;
            }

            //Validate mã nhân viên
            return await me.validateEmployeeCode();

        },
        /**
         * Thực hiện validate dữ liệu cho mã nhân viên
         * CreatedBy: HoaiPT(17/07/2022)
         */
        validateEmployeeCode() {
            var me = this;
            let temp = /^NV-[0-9]+$/.test(me.employee.EmployeeCode);
            console.log(temp);
            if (temp == false) {
                let employeeCode = document.getElementById("employeeCode");

                //Thiết lập css cho border
                employeeCode.classList.add('m-border-red');
                employeeCode.setAttribute("title", mylib.resourcs["VI"].errorEmployeeCode );

                //Thiết lập dữ liệu lỗi cho form Infor
                me.errorInfo = mylib.resourcs["VI"].errorEmployeeCode;

                // Hiển thị thông báo cho form
                me.isShowInfoMessage = true;
                return false;
            }
            return true;

        },
        /**
         * Thực hiện mở form cảnh báo lỗi trong form deatail
         *CreatedBy: HoaiPT(07/02/2022)
         */
        async openWarning(me) {
            await me.setEmployeeCode(me.employee.EmployeeCode);
            me.isShowWarningMessage = true;
        },
        /**
         * Thực hiện set mã code 
         *CreatedBy: HoaiPT(07/02/2022)
         */
        setEmployeeCode(value) {
            var me = this;
            me.employeeCode = value;
        },
        /**
         * Định dạng dữ liệu kiểu datetime kiểu: YYYY-MM-DD
         *CreatedBy: HoaiPT(07/02/2022)
         */
        formatDate: function (value) {
            if (value) {
                value = new Date(value);
                let date = value.getDate();
                date = date < 10 ? `0${date}` : date;
                let month = value.getMonth() + 1;
                month = month < 10 ? `0${month}` : month;
                let year = value.getFullYear();
                value = `${year}-${month}-${date}`;
            } else value = "";
            return value;
        },
        /**
         * Định dạng dữ liệu kiểu datetime  thời gian hiện tại kiểu: YYYY-MM-DDThh:dd:ss
         *CreatedBy: HoaiPT(07/02/2022)
         */
        formatDateAndTimeNow() {
            var curDate = new Date();
            let hours = curDate.getHours(); //Lấy giờ hiện tại
            hours = hours < 10 ? `0${hours}` : hours;
            let minutes = curDate.getMinutes() + 1; //Lấy phút hiện tại
            minutes = minutes < 10 ? `0${minutes}` : minutes;
            let seconds = curDate.getSeconds(); //Lấy giây hiện tại
            seconds = seconds < 10 ? `0${seconds}` : seconds;

            return `${this.formatDate(curDate)}T${hours}:${minutes}:${seconds}`;
        },
        /**
         * Thực hiện kiểm tra xem là hành động (Cất) | (Cất và Thêm) để thực hiện phù hợp
         * 1: Cất
         * 2: Cất và Thêm
         *CreatedBy: HoaiPT(7/02/2022)
         */
        async checkAction(value) {
            var me = this;
            switch (value) {
                case 1: //Nếu là cất
                    me.$emit('openDialog', null); //Đóng dialog detail
                    me.$emit('reloadData', null); //Load lại dữ liệu table
                    break;
                case 2: //Nếu là cất và thêm 
                    me.$parent.editMode = mylib.misaEnum.editMode.Add; //Đây là hành động thêm mới
                    me.$parent.isShowLoading = true; //Load lại dữ liệu
                    await me.$parent.showData();

                    await me.$parent.getCodeNew(); //Lấy ở data mã code mới
                    me.resetForm(); //reset form 

                    me.$refs.focusMe.focus(); //Tập trung forcus ở ô Mã
                    break;
                default:
                    break;

            }
        },
        /**
         * Làm mới form detail
         *CreatedBy: HoaiPT(7/02/2022)
         */
        resetForm() {
            var me = this;

            //Làm mới toàn bộ để lại mã mới tự tăng
            for (var propName in me.employee) {
                if (propName != "EmployeeCode") {
                    me.employee[propName] = null;
                }
            }
            me.employee.Gender = mylib.misaEnum.gender.Male; //Form mặc định Giới tính: Nam
        },
        /**
         * Thực hiện chuẩn hóa lại tên
         *CreatedBy: HoaiPT(7/02/2022)
         */
        async standardizeFullName() {
            var me = this;
            me.employee.FullName = await me.standardizeString(me.employee.FullName);
        },
        /**
         * Chuẩn hóa chuỗi String viết hoa những chữ cái đầu và bỏ khoảng trắng đầu và cuối
         */
        standardizeString(value) {
            if (value) {
                value = value.trim(); // chuẩn hóa đầu và cuối
                value = value.split(" "); // cắt chuỗi

                //Thực hiện xóa bỏ những phần tử có có trị bằng ""
                for (let i = 1; i < value.length; i++) {
                    if ((value[i] == "")) //Nếu không có gí trị thì thực hiện xóa bỏ
                    {
                        value.splice(i, 1); //xóa 
                        i--; //Phải lùi về 1 vì sau vòng for nó tăng lên 1
                    }
                }

                //Thực hiện chuẩn hóa từng từ
                for (let i = 0; i < value.length; i++) {
                    value[i] = value[i].toLowerCase(); //Thực hiện từng từ một đều là chữ thường
                    value[i] = value[i].charAt(0).toUpperCase() + value[i].substring(1); // In hoa chữ cái đầu tiên
                }

                return value.join(' '); //Nối các phần tử trở thành chuỗi đã chuẩn hóa rồi trả về
            }

            return null;

        }
    }
}
</script>

<style scoped>
@import url(../../style/view/employee/employee-detail.css);
</style>

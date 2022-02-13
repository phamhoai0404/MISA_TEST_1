<template>
<div class="m-dialog">
    <div class="m-dialog-content">
        <div class="m-dialog-header">
            <div class="m-dialog-title">
                <div class="m-title">Thông tin nhân viên</div>
                <div class="m-checkbox">
                    <input type="checkbox" id="khachhang" />
                    <label class="m-label" for="khachhang">
                        <span></span> &nbsp; &nbsp;&nbsp;Là khách hàng</label>
                </div>
                <div class="m-checkbox">
                    <input type="checkbox" id="cungcap" />
                    <label class="m-label" for="cungcap"> <span></span> &nbsp; &nbsp;&nbsp;Là nhà cung cấp</label>
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
                                <input type="text" ref='focusMe' class="m-input" v-model="employee.EmployeeCode" @input="inputOnChange(employee.EmployeeCode, $event)" id="employeeCode" title="Mã không được để trống!">
                            </div>
                            <div class="m-content-input" style="width: 58%;">
                                <div class="m-label">Tên <span>*</span></div>
                                <input type="text" class="m-input" v-model="employee.FullName" @input="inputOnChange(employee.FullName, $event)" id="employeeName" title="Tên không được để trống!">
                            </div>
                        </div>
                        <div class="m-content-combobox" style="padding: 0 0 12px 0">
                            <div class="m-label">Đơn vị <span>*</span> </div>
                            <div class="m-dialog-combobox" id="departmentName" @mouseleave="isShowDepartment = false" @mouseover="isShowDepartment = true">
                                <input type="text" v-model="employee.DepartmentName" readonly title="Đơn vị không được để trống!">
                                <button class="" @click='btnSelectDepartment()'>
                                    <div class="m-icon-button m-icon-down"></div>
                                </button>
                                <div class="m-dialog-combobox-data" v-if="isShowDepartment">
                                    <div v-for="(department,index) in listDepartment" :key="index" class="m-dialog-combobox-item " @click='selectedDepartment(index)'>{{ department.DepartmentName }}</div>
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
                                <!-- <input type="date" class="m-input" v-model="employee.DateOfBirth"> -->
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
                                <div class="m-label">Số CMND</div>
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
                            <div class="m-label">ĐT di động</div>
                            <input type="text" class="m-input" v-model="employee.MobilephoneNumber">
                        </div>
                        <div class="m-content-input">
                            <div class="m-label">ĐT cố định</div>
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
                    <button class="m-button m-button-not-color" @click="btnSaveOnClick(1)">Cất</button>
                    <button class="m-button" @click="btnSaveOnClick(2)">Cất và Thêm</button>
                </div>
            </div>
        </div>
    </div>
    <MessageInfoEmployee v-if="isShowInfoMessage" @openInfoEmployee="isShowInfoMessage =!isShowInfoMessage" :errorData='errorInfo' />
    <MessageWarningEmployee v-if="isShowWarningMessage" @openWarningMessage="isShowWarningMessage =!isShowWarningMessage" :employeeCode='employeeCode' />
    <MessageConfirmEdit v-if="isShowEditMessage" @openEditEmployee="isShowEditMessage=!isShowEditMessage" @closeDialogDetail="closeDialogDetail()" @closeDialogDetailAndSave="btnSaveOnClick(1)" />
</div>
</template>

<script>
import MessageInfoEmployee from '@/view/employee/EmployeeInfor.vue'
import MessageWarningEmployee from '@/view/employee/EmployeeWarning.vue'
import MessageConfirmEdit from '@/view/employee/EmployeeConfirmEdit.vue'
import axios from 'axios'

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

            isShowInfoMessage: false,
            isShowWarningMessage: false,
            isShowEditMessage: false,

            isShowDepartment: false,
            employeeCode: "",

            errorInfo: "",

            departmentName: null, //Phải tạo cái trung gian này vì nếu theo dõi thằng employee thì employee thay đổi thằng khác thì nó sẽ ảnh hưởng đến departmentName
        }
    },
    mounted() {
        this.$refs.focusMe.focus();
        console.log(this.employee);
    },

    watch: {
        departmentName: function (value) {
            if (!value) {
                document.getElementById("departmentName").classList.add('m-border-red');
            } else {
                document.getElementById("departmentName").classList.remove('m-border-red');
            }
        },

        isShowDepartment: function (value) {
            if (value) {
                document.getElementById("departmentName").classList.add('m-border-green');
            } else {
                document.getElementById("departmentName").classList.remove('m-border-green');
            }
        }
    },

    methods: {
        btnSelectDepartment() {
            this.isShowDepartment = !this.isShowDepartment;

        },
        selectedDepartment(index) {
            this.isShowDepartment = !this.isShowDepartment;
            this.employee.DepartmentId = this.listDepartment[index].DepartmentId;
            this.employee.DepartmentName = this.listDepartment[index].DepartmentName;

            this.departmentName = this.employee.DepartmentName; //Trung gian để css thôi

        },
        closeDialogDetail() {
            var me = this;
            me.$emit('openDialog', null);
            //Phải load lại data vì đây mình để model khi mà nó sửa thì bảng nó cũng ăn nên cần phải load lại
            me.$emit('reloadData', null);
        },
        btnCloseDialog() {
            var me = this;
            me.isShowEditMessage = true;
        },
        async btnSaveOnClick(value) {
            try {
                //Validate dữ liệu nếu mà nó không thỏa mãn thì kết thúc luôn
                if (!this.validateData()) {
                    return;
                }

                //Gọi API thực hiện add hoặc là update
                var me = this;

                //Phải format về dạng này thì nó mới nhận đúng
                me.employee.DateOfBirth = me.formatDate(me.employee.DateOfBirth);
                me.employee.IdentityDate = me.formatDate(me.employee.IdentityDate);

                //Lưu lại thời gian tạo nếu là tạo nhân viên mới
                if (me.editMode == 1) {
                    me.employee.CreatedDate = me.formatDateAndTimeNow();
                }

                //Lưu lại thời gian cập nhật  dữ liệu
                me.employee.ModifiedDate = me.formatDateAndTimeNow();

                switch (me.editMode) {
                    case 1:
                        await axios.post('https://localhost:44338/api/v1/Employees', me.employee)
                            .then(function () {
                                me.checkAction(value);

                            })
                            .catch(function () {
                                me.openWarning(me);
                            })
                        break;
                    case 2:
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
        btnCancelDialogDetail() {
            var me = this;
            me.$emit('openDialog', null);
            me.$emit('reloadData', null);
        },
        inputOnChange(valueField, ee) {
            if (!valueField) {
                ee.target.classList.add('m-border-red');
            } else {
                ee.target.classList.remove('m-border-red');
            }
        },
        validateData() {
            //Không được để trống mã, họ tên và đơn vị
            if (!this.employee.EmployeeCode || !this.employee.FullName || !this.employee.DepartmentName) {

                //Phải style cho border nếu để trống
                if (!this.employee.DepartmentName) {
                    let departmentName = document.getElementById("departmentName");
                    departmentName.classList.add('m-border-red');
                    this.errorInfo = "Đơn vị";
                }

                if (!this.employee.FullName) {
                    let employeeName = document.getElementById("employeeName");
                    employeeName.classList.add('m-border-red');
                    this.errorInfo = "Tên";
                }
                if (!this.employee.EmployeeCode) {
                    let employeeCode = document.getElementById("employeeCode");
                    employeeCode.classList.add('m-border-red');
                    this.errorInfo = "Mã";
                }

                //Hiển thị thông báo
                this.isShowInfoMessage = true;
                return false;
            }
            return true;

        },
        async openWarning(me) {
            await me.setEmployeeCode(me.employee.EmployeeCode);
            me.isShowWarningMessage = true;
        },
        setEmployeeCode(value) {
            this.employeeCode = value;
        },
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
        async checkAction(value) {
            var me = this;
            switch (value) {
                case 1: //Nếu là cất
                    me.$emit('openDialog', null); //Đóng dialog detail
                    me.$emit('reloadData', null); //Load lại dữ liệu table
                    break;
                case 2:
                    me.editMode = 1;
                    await me.$parent.getCodeNew();
                    me.resetForm();

                    me.$refs.focusMe.focus();
                    break;
                default:
                    break;

            }
        },
        resetForm() {
            var me = this;
            
            for (var propName in me.employee) {
                if(propName !="EmployeeCode"){
                    me.employee[propName] = null;
                }     
            }

            me.employee.Gender = "1";
            console.log(me.employee);

        }

    }
}
</script>

<style scoped>
@import url(../../style/view/employee/employee-detail.css);
</style>

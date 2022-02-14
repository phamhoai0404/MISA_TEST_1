<template>
<div class="employee-list">
    <div class="row-one">
        <div class="row-title">Nhân viên</div>
        <ButtonAdd @showDialog='showAddEmployeeDetail()' />
    </div>

    <div class="row-two">
        <div class="row-two-top">
            <div class="row-two-left">
                <div class="m-button-group">
                    <div class="m-button-title">Thực hiện hàng loạt</div>
                    <div class="m-button-icon"></div>
                    <div class="m-button-function">
                        <div @click="btnRemoveMany()"> Xóa</div>
                    </div>
                </div>
            </div>
            <div class="row-two-right">
                <div style="width:220px;">
                    <input type="text" class="m-input m-input-icon search" placeholder="Tìm theo mã, tên nhân viên" v-model="keywordSearch">
                </div>
                <div class="button- btn-refresh" title="Lấy lại dữ liệu" @click="btnRefresh()">
                </div>
                <div class="button- btn-excel " title="Xuất re Excel" @click="btnExportExcel()">
                </div>
            </div>

        </div>
        <div class="m-grid" id="m-grid">
            <table border="1" class="m-table">
                <thead>
                    <tr>
                        <th class="m-checkbox sticky-checkbox">
                            <input type="checkbox" id="hangloat" />
                            <label for="hangloat" @click="batchExecution()"><span></span></label>
                        </th>
                        <th style="min-width:100px; border-left:unset;">MÃ NHÂN VIÊN</th>
                        <th style="min-width:155px">TÊN NHÂN VIÊN</th>
                        <th style="min-width:70px">GIỚI TÍNH</th>
                        <th style="min-width:88px" class="text-align-center">NGÀY SINH</th>
                        <th style="min-width:120px">SỐ CMTND</th>
                        <th style="min-width:100px;">CHỨC DANH</th>
                        <th style="min-width:150px">TÊN ĐƠN VỊ</th>
                        <th style="min-width:100px">SỐ TÀI KHOẢN</th>
                        <th style="min-width:170px">TÊN NGÂN HÀNG</th>
                        <th style="min-width:150px">CHI NHÁNH NGÂN HÀNG</th>
                        <th class="sticky-function text-align-center">CHỨC NĂNG</th>

                    </tr>
                </thead>

                <tbody>
                    <tr v-for="(employee,index) in listEmployee" :key="index">
                        <td class="m-checkbox sticky-checkbox">
                            <input type="checkbox" :id="employee.EmployeeId" />
                            <label :for="employee.EmployeeId" @click="removeOrBatchExecution(employee.EmployeeId)"><span></span></label>
                        </td>
                        <td style="border-left:unset;">{{employee.EmployeeCode}}</td>
                        <td>{{employee.FullName}} </td>
                        <td>{{employee.Gender | formatGender }} </td>
                        <td class="text-align-center">{{employee.DateOfBirth | formatDate}}</td>
                        <td class="text-align-left">{{employee.IdentityNumber}}</td>
                        <td>{{employee.PositionName}}</td>
                        <td>{{ employee.DepartmentName }}</td>
                        <td>{{employee.BankAccountNumber}}</td>
                        <td>{{employee.BankName}}</td>
                        <td>{{employee.BankBranchName}}</td>

                        <td class="m-funtionsss sticky-function">
                            <div class="m-function">
                                <div class="m-title" @click="btnEditEmployeeDetail(employee)">Sửa</div>
                                <button class="m-button-function">
                                    <div class="m-button-icon" @click="showFunction($event, employee, index)" id="clickFunction">
                                    </div>
                                </button>

                            </div>

                        </td>
                    </tr>

                </tbody>

            </table>
        </div>
        <div class="m-table-paging">
            <div class="m-paging-left">
                Tổng số: <b v-if=" listEmployee!=null ">{{listEmployee.length}}</b>
                <b v-if=" listEmployee==null ">0</b>
                bản ghi
            </div>
            <div class="m-paging-right">
                <div class="m-left">
                    <div class="m-combobox" style="width: 200px;">
                        <input type="text">
                        <button><i class="fas fa-angle-down"></i></button>
                        <div class="m-combobox-data" style="display: none;">
                            <div class="m-combobox-item">Trạng thái 1</div>
                            <div class="m-combobox-item">Trạng thái 2</div>
                            <div class="m-combobox-item m-combobox-item-selected">Trạng thái 3</div>
                        </div>
                    </div>
                </div>
                <div class="m-right">
                    <button class="m-paging-title ">Trước</button>
                    <div class="m-page-number-group">
                        <button class="m-page-number">1</button>
                        <button class="m-page-number">2</button>
                        <button class="m-page-number">3</button>
                        <button class="m-page-number m-page-active">4</button>
                    </div>
                    <button class="m-paging-title ">Sau</button>

                </div>

            </div>

        </div>
    </div>
    <EmployeeFunction @openRemoveEmployee="isShowRemoveEmployee=!isShowRemoveEmployee" @openDuplication='btnDuplicate' />
    <EmployeeDetail v-if='isShowEmployeeDetail' @openDialog='isShowEmployeeDetail =!isShowEmployeeDetail' :editMode='editMode' :employee='employeeIdSelected' @reloadData='getData' :listDepartment='listDepartmentTable' />

    <MessageRemoveEmployee v-if='isShowRemoveEmployee' :employee='employeeIdSelected' @openMessageRemove='openMessage' @reloadData='getData' :action="actions" />

    <Loading v-show="isShowLoading" />

</div>
</template>

<script>
import ButtonAdd from '@/components/base/BaseButton.vue'
import Loading from '@/components/base/BaseLoading.vue'
import EmployeeDetail from '@/view/employee/EmployeeDetail.vue'
import MessageRemoveEmployee from '@/view/employee/EmployeeConfirmRemove.vue'
import EmployeeFunction from '@/view/employee/EmployeeFunction.vue'
import axios from 'axios'
export default {
    components: {
        ButtonAdd,
        Loading,
        EmployeeDetail,
        MessageRemoveEmployee,
        EmployeeFunction,
    },
    data() { //Các biến có trong component
        return {
            listEmployee: null,
            listDepartmentTable: null,
            isShowLoading: false,
            isShowEmployeeDetail: false,
            isShowRemoveEmployee: false,
            editMode: 1,
            employeeIdSelected: {},

            keywordSearch: null,

            myTimeout: "",
            testExport: "Danh_sach_nhan_vien",
            tessst: null,

            actions: 0,//0 là không thực hiện gì, 1: thực hiện xóa một bản ghi, 2: thực hiện xóa nhiều
        }
    },
    created() {
        //Gọi API lấy dữ liệu
        this.getDataListDepartment();
        this.getData();

    },

    watch: {
        keywordSearch: function (value) {
            var me = this;
            clearTimeout(me.myTimeout);
            me.myTimeout = setTimeout(function () {
                if (value) {
                    me.getDataByKeywordSearch(value);
                } else {
                    me.getData();
                }
            }, 1000);
        }
    },

    methods: {
        openMessage(){
            var me = this;
            me.isShowRemoveEmployee = !me.isShowRemoveEmployee;//Thực hiện đóng nếu mở hoặc mở nếu đóng
        },
        btnRemoveMany() {
            var me = this;
            me.isShowLoading = true; //Hiển thị đang load
            var arr = new Array();
            arr.push("6a763a51-4322-5c5a-b5d2-89b22d10517e");
            arr.push("197fda97-5a35-781d-0e77-d9ebc53aac3d");
            console.log(arr);
            if (arr.length >= 1) {
                axios.post('https://localhost:44338/api/v1/Employees/DeleteMany',arr)
                    .then(function () {
                        me.getData();
                    })
                    .catch(function (res) {
                        console.error(res);
                    })
            }


        },
        async getCodeNew() {
            var me = this;
            me.isShowLoading = true; //Hiển thị đang load
            await axios.get('https://localhost:44338/api/v1/Employees/CodeNew')
                .then(function (res) {
                    me.employeeIdSelected.EmployeeCode = res.data;
                    me.isShowLoading = false; //đóng
                })
                .catch(function (res) {
                    console.error(res);
                })
        },

        getData() {
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;

            me.isShowLoading = true; //Hiển thị đang load
            axios.get('https://localhost:44338/api/v1/Employees')
                .then(function (res) {
                    me.listEmployee = res.data;
                    console.log(res.data);

                    me.isShowLoading = false;
                })
                .catch(function (res) {
                    console.log(res);

                })
        },

        getDataListDepartment() {
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;
            axios.get('https://localhost:44338/api/v1/Departments')
                .then(function (res) {
                    console.log(res.data);
                    me.listDepartmentTable = res.data;
                })
                .catch(function (res) {
                    console.err(res);
                })
        },
        getDataByKeywordSearch(keyword) {
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;
            me.isShowLoading = true;
            axios.get(`http://amis.manhnv.net/api/v1/Employees/filter?employeeFilter=${keyword}`)
                .then(function (res) {
                    me.listEmployee = res.data.Data;
                    me.isShowLoading = false;
                })
                .catch(function (res) {
                    console.log(res);
                })
        },

        //Thực hiện hàng loạt
        batchExecution() {
            var me = this;
            if (!document.getElementById('hangloat').checked) {
                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    document.getElementById(id).checked = true;
                }
            } else {
                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    document.getElementById(id).checked = false;
                }
            }
        },
        removeOrBatchExecution(employeeId) {
            if (document.getElementById(employeeId).checked) {
                document.getElementById('hangloat').checked = false;
            } else {
                var me = this;
                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    if (id != employeeId && !document.getElementById(id).checked) {
                        return;
                    }
                }
                document.getElementById('hangloat').checked = true;
            }
        },
        showFunction(event, employee, index) {

            //Hiển thị function  
            document.getElementById('function-ground').style.display = 'block';

            //thiết lập vị trí
            this.positionFunction(event.clientY, index);

            //Truyền vào employee để bắt đầu thực hiện hành động
            this.employeeIdSelected = employee;
        },
        positionFunction(y, index) {
            y = index != (this.listEmployee.length - 1) ? (y + 14) : (y - 105);
            document.getElementById('function-ground').style.position = 'fixed';
            document.getElementById('function-ground').style.right = '70px';
            document.getElementById('function-ground').style.top = `${y}px`;

        },
        async showAddEmployeeDetail() {
            var me = this;

            //Khởi tạo đối tượng rỗng với giá trị của giới tính mặc định là Nam và gán giá trị editMode = 1 là thuộc kiểu thêm
            me.editMode = 1;
            me.employeeIdSelected = {};
            me.employeeIdSelected.Gender = "1";
            await me.getCodeNewAndShowDialog();

        },
        btnEditEmployeeDetail(employee) {
            // //Đóng hộp thoại function
            // document.getElementById('function-ground').style.display = 'none';

            this.editMode = 2; //Thể hiện là đang sửa
            this.employeeIdSelected = employee;

            this.isShowEmployeeDetail = true;
        },
        btnDuplicate() {
            var me = this;
            me.editMode = 1; //Thực hiện thêm mới
            me.getCodeNewAndShowDialog();
        },
        async getCodeNewAndShowDialog() {
            var me = this;
            me.isShowLoading = true; //Hiển thị đang load
            await axios.get('https://localhost:44338/api/v1/Employees/CodeNew')
                .then(function (res) {
                    me.employeeIdSelected.EmployeeCode = res.data;
                    me.isShowLoading = false; //đóng
                    me.isShowEmployeeDetail = true;
                })
                .catch(function (res) {
                    console.error(res);
                })

        },
        showRemoveEmployee(employee) {
            //Thực hiện gán dữ liệu và mở message lên
            this.employeeIdSelected = employee;

            this.isShowRemoveEmployee = true;

        },
        btnRefresh() {
            var me = this;
            if (me.keywordSearch == null) {
                me.getData(); //nếu mà ô tìm kiếm không có dữ liệu thì load lại dữ liệu
            } else {
                me.keywordSearch = null; //ô tìm kiếm có dữ liệu thì sẽ set về null rồi tự khắc ở phần watch nó sẽ theo dõi cái keyworkSearch để thực hiện load lại dữ liệu
            }

        },
        btnExportExcel() {
            // alert("hoa");
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;
            me.isShowLoading = true; //Hiển thị đang load
            axios.get('https://localhost:44338/api/v1/Employees/excel', {
                responseType: 'blob',
            }).then((response) => {
                const url = URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute(
                    'download',
                    `${this.testExport}-${new Date().toLocaleDateString()}.xlsx`
                )
                document.body.appendChild(link)
                link.click();
                me.isShowLoading = false;
            })
        }
    },
    filters: {
        formatDate: function (value) {
            if (value) {
                value = new Date(value);

                let date = value.getDate();
                date = date < 10 ? `0${date}` : date;

                let month = value.getMonth() + 1;
                month = month < 10 ? `0${month}` : month;

                let year = value.getFullYear();

                value = `${date}/${month}/${year}`;
            } else value = "";

            return value;
        },
        formatGender(value) {
            switch (value) {
                case 0:
                    value = "Nữ";
                    break;
                case 1:
                    value = "Nam";
                    break;
                case 2:
                    value = "Khác";
                    break;
                default:
                    value = "";
                    break;
            }
            return value;

        },

    }

}
</script>

<style scoped>
@import url(../../style/view/employee/employee-list.css);
@import url(../../style/view/employee/employee-page.css);
@import url(../../style/view/employee/employee-list-add.css);

/* end */
</style>

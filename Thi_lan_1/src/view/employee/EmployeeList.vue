<template>
<div class="employee-list">
    <div class="row-one">
        <div class="row-title" >Nhân viên</div>
        <ButtonAdd @showDialog='showAddEmployeeDetail()' />
    </div>

    <div class="row-two">
        <div class="row-two-top">
            <div class="row-two-left">
                <div class="m-button-group">
                    <div class="m-button-title">Thực hiện hàng loạt</div>
                    <div class="m-button-icon" @click="isShowDeleteMany = !isShowDeleteMany"></div>
                    <div class="m-button-function" v-if="actions=== 2 && isShowDeleteMany===true " @mouseleave="isShowDeleteMany = false">
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
                <div class="button- btn-excel " title="Xuất ra Excel" @click="btnExportExcel()">
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
                        <th style="min-width:120px" title="Số Chứng minh nhân dân">SỐ CMND</th>
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
                Tổng số: <b>{{totalRecord}}</b>
                bản ghi
            </div>
            <div class="m-paging-right">
                <div class="m-left">
                    <div class="m-combobox" style="width: 200px;" :class="{'selected': isShowPage}">
                        <input type="text" :value="pageTextInInput" readOnly>
                        <button class="m-button-padding" @click="btnSelectPage()">
                            <div class="m-icon-drown"></div>
                        </button>
                        <div class="m-combobox-data" v-if="isShowPage">
                            <div v-for="(textPage,index) in listPageText" :key="index" class="m-combobox-item-page" @click='selectedPageText(index)' :class="{'selected': selectTextPage == textPage }">{{ textPage }} bản ghi trên 1 trang</div>
                        </div>
                    </div>
                </div>
                <div class="m-right">
                    <paginate v-model="pageAction" :page-count="totalPage" :page-range="3" :margin-pages="1" :click-handler="clickCallback" :prev-text="'Trước'" :next-text="'Sau'" :container-class="'m-page-number'" :page-class="'m-page-item'">
                    </paginate>
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

import * as mylib from '../../js/resourcs'

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
            listEmployee: null, //Danh sách employee
            listDepartmentTable: null, //Danh sách Department
            isShowLoading: false, //Trạng thái đầu tiên của Loading
            isShowEmployeeDetail: false, //Trạng thái đầu tiên của form detail
            isShowRemoveEmployee: false, //Trạng thái đầu tiên của form remove
            editMode: mylib.misaEnum.editMode.Add, //form thêm hoặc sửa: 1: thêm mới; 2: sửa
            employeeIdSelected: {}, //employee đang được chọn

            keywordSearch: "", //Từ khóa tìm kiếm mặc định ban đầu bằng empty
            myTimeout: "", //Thực hiện cho setTimeout ở filer

            testExport: mylib.resourcs["VI"].fileNameExport, //Tên của file export

            actions: mylib.misaEnum.actionDelete.NoAction, //0 là không thực hiện gì, 1: thực hiện xóa một bản ghi, 2: thực hiện xóa nhiều
            isShowDeleteMany: false, //Trạng thái xóa nhiều
            arrayEmployeeId: new Array(), //Nơi lưu trữ EmployeeId chuẩn bị xóa

            totalRecord: 0, //Tổng số bản ghi ban đầu là 0
            totalPage: 1, //Tổng số trang ban đầu là 1
            pageAction: 1, //Trang đang thực hiện
            listPageText: mylib.data.listPageSize,
            selectTextPage: 10, //Kích thước trang
            isShowPage: false, //Trạng thái ban đầu của form Page là đóng
        }
    },
    created() {
        //Gọi API lấy dữ liệu
        this.getDataListDepartment(); //Thực hiện lấy danh sách phòng ban
        this.getData(); //Thực hiện load dữ liệu
    },
    computed: {
        pageTextInInput: function () {
            return this.selectTextPage + mylib.resourcs["VI"].titlePage;
        }
    },

    watch: {
        //Phần này nên để lại để thực hiện phân trang đang làm 
        keywordSearch: function () {
            var me = this;
            clearTimeout(me.myTimeout);
            me.myTimeout = setTimeout(function () {
                me.pageAction = 1;
                me.isShowLoading = true;
                me.showData();
            }, 500);
        }
    },

    methods: {
        /**
         * Thay đổi trạng thái tắt mở của form select paging
         * CreatedBy: HoaiPT(07/02/2022)
         */
        btnSelectPage() {
            var me = this;
            me.isShowPage = !me.isShowPage;
        },
        /**
         * Thực hiện hành động khi chọn kích thước trang 
         * CreatedBy: HoaiPT(17/02/2022)
         */
        async selectedPageText(index) {
            var me = this;

            me.selectTextPage = me.listPageText[index]; //Thực hiện gán số kích thước trang đã lựa chọn 
            me.isShowLoading = true; //Show loading
            me.pageAction = 1; //Hiển thị ở trang đầu tiên

            await me.showData(); //Thực hiện load lại dữ liệu trên table với đúng kích thước trang đã chọn

            me.isShowPage = false; //Đóng form page
        },
        /**
         * Hành động khi chọn vào trang bất kì ở paging
         * CreatedBy: HoaiPT(17/02/2022)
         */
        clickCallback(pageIndex) {
            var me = this;
            me.pageAction = pageIndex; //Thực hiện gián giá trị mới cho trang muốn đứng
            me.isShowLoading = true; //Show loading

            me.showData(); //Load lại dữ liệu table

        },
        /**
         * Thay đổi trạng thái đóng mở của form MessageRemoveEmployee
         * CreatedBy: HoaiPT(17/02/2022)
         */
        openMessage() {
            var me = this;
            me.isShowRemoveEmployee = !me.isShowRemoveEmployee; //Thực hiện đóng nếu mở hoặc mở nếu đóng
        },
        /**
         * Thực hiện khi click vào nút xóa nhiều
         * CreatedBy: HoaiPT(14/02/2022)
         */
        btnRemoveMany() {
            var me = this;
            me.isShowDeleteMany = false; //Đóng xóa nhiều
            me.isShowRemoveEmployee = true; //Mở MessageRemoveEmployee
        },
        /**
         * Thực hiện lấy mã mới ở backend
         * CreatedBy: HoaiPT(15/02/2022)
         */
        async getCodeNew() {
            try {
                var me = this;
                me.isShowLoading = true; //Hiển thị đang load
                await axios.get('https://localhost:44338/api/v1/Employees/CodeNew')
                    .then(function (res) {
                        me.employeeIdSelected.EmployeeCode = res.data; //Gán mã mới vào MessageRemoveEmployee 
                        me.isShowLoading = false; //đóng load
                    })
                    .catch(function (res) {
                        console.error(res);
                    })
            } catch {
                console.log(mylib.resourcs["VI"].errorMsg);
            }

        },
        /**
         * Lấy toàn bộ dữ liệu về trạng thái ban đầu
         * CreatedBy: HoaiPT(17/02/2022)
         */
        getData() {
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;

            me.selectTextPage = 10; //Reset lại số lượng bản ghi trên 1 trang 
            me.pageAction = 1; //Reset lại số trang  đang hiển thị
            me.isShowLoading = true; //Hiển thị đang load
            me.keywordSearch = "";

            me.showData(); //Load dữ liệu

        },
        /**
         * Lấy và hiển thị dữ liệu
         * CreatedBy: HoaiPT(17/02/2022)
         */
        async showData() {
            try {
                var me = this;
                await axios.get(`https://localhost:44338/api/v1/Employees/getPaging?searchText=${me.keywordSearch}&pageIndex=${me.pageAction}&pageSize=${me.selectTextPage}`)
                    .then(function (res) {
                        me.listEmployee = res.data.Data; //Thực hiện gián listEmployee vào với kích thước trang, từ khóa tìm kiếm và trang đang đứng
                        me.totalPage = Number(res.data.TotalPage); //Gán vào tổng số trang
                        me.totalRecord = res.data.TotalRecord; //Gán vào tổng số bản ghi
                        me.isShowLoading = false; //Đóng loading
                    })
                    .catch(function (res) {
                        console.log(res);

                    })
            } catch {
                console.log(mylib.resourcs["VI"].errorMsg);
            }

        },
        /**
         * Lấy danh sách phòng ban
         * CreatedBy: HoaiPT(08/02/2022)
         */
        getDataListDepartment() {
            try {
                //Chắc chắn là con trỏ this đang ở đây;
                var me = this;
                axios.get('https://localhost:44338/api/v1/Departments')
                    .then(function (res) {
                        me.listDepartmentTable = res.data; //Thực hiện gán vào danh sách Department
                    })
                    .catch(function (res) {
                        console.err(res);
                    })
            } catch {
                console.log(mylib.resourcs["VI"].errorMsg);
            }

        },
        /**
         * Hành động khi click vào nút refresh
         * CreatedBy: HoaiPT(17/02/2022)
         */
        btnRefresh() {
            var me = this;
            me.isShowLoading = true;
            me.showData();

        },

        /**
         * Hành động khi thực hiện vào hàng loạt
         * CreatedBy: HoaiPT(14/02/2022)
         */
        batchExecution() {
            var me = this;
            me.isShowDeleteMany = false; //Để nó bằng false tránh trường hợp cái action = 2, rồi isShowDeleteMany thì nó TỰ ĐỘNG mở ra cái xóa nhiều ra
            me.arrayEmployeeId = []; //Làm mới để chuẩn bị thêm toàn bộ hoặc không có gì
            if (!document.getElementById('hangloat').checked) {
                me.actions = mylib.misaEnum.actionDelete.Multi; //Có thể ấn hành động là xóa nhiều

                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    document.getElementById(id).checked = true;
                    me.arrayEmployeeId.push(id) //Add vào để chuẩn bị xóa
                }
            } else {
                me.actions = mylib.misaEnum.actionDelete.NoAction; //Ấn không ra không hành động gì hết
                me.removeAllChecked(me.listEmployee); //Xóa bỏ hết checked
            }
        },
        /**
         * Thực hiện làm mới xóa bỏ tất cả checked
         * CreatedBy: HoaiPT(08/02/2022)
         * UpdatedBy: HoaiPT(14/02/2022)
         */
        removeAllChecked(listEmployee) {
            for (let i = 0; i < listEmployee.length; i++) {
                let id = listEmployee[i].EmployeeId;
                if (document.getElementById(id).checked)
                    document.getElementById(id).checked = false;
            }
        },
        /**
         * Thực hiện khi click vào từng ô checked một
         * CreatedBy: HoaiPT(08/02/2022)
         * UpdatedBy: HoaiPT(14/02/2022)
         * 
         */
        removeOrBatchExecution(employeeId) {
            var me = this;
            me.isShowDeleteMany = false;

            if (document.getElementById(employeeId).checked) {
                document.getElementById('hangloat').checked = false;
                var index = me.arrayEmployeeId.indexOf(employeeId);
                me.arrayEmployeeId.splice(index, 1);
                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    if (id != employeeId && document.getElementById(id).checked) {
                        return;
                    }
                }
                me.actions = mylib.misaEnum.actionDelete.NoAction; //Nếu mà tất cả đều không tích chọn thì sẽ không click vào thực hiện hàng loạt được
            } else {
                me.actions = mylib.misaEnum.actionDelete.Multi; //Có thể ấn hành động là xóa nhiều
                me.arrayEmployeeId.push(employeeId);
                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    if (id != employeeId && !document.getElementById(id).checked) {
                        return;
                    }
                }
                document.getElementById('hangloat').checked = true; //Nếu tất cả đều click chọn thì hàng loạt sẽ tích chọn
            }
        },
        /**
         * Thực hiện hiển thị funtion
         * CreatedBy: HoaiPT(08/02/2022)
         */
        showFunction(event, employee, index) {

            //Hiển thị function  
            document.getElementById('function-ground').style.display = 'block';

            //thiết lập vị trí
            this.positionFunction(event.clientY, index);

            //Truyền vào employee để bắt đầu thực hiện hành động
            this.employeeIdSelected = employee;
        },
        /**
         * Thực hiện truyền vào vị trị cho function
         * CreatedBy: HoaiPT(08/02/2022)
         */
        positionFunction(y, index) {
            y = index != (this.listEmployee.length - 1) ? (y + 14) : (y - 105);
            document.getElementById('function-ground').style.position = 'fixed';
            document.getElementById('function-ground').style.right = '70px';
            document.getElementById('function-ground').style.top = `${y}px`;

        },
        /**
         * Thực hiện khi click vào ô thêm mới
         * CreatedBy: HoaiPT(08/02/2022)
         */
        async showAddEmployeeDetail() {
            var me = this;

            //Khởi tạo đối tượng rỗng với giá trị của giới tính mặc định là Nam và gán giá trị editMode = 1 là thuộc kiểu thêm
            me.editMode = mylib.misaEnum.editMode.Add;
            me.employeeIdSelected = {}; //Khởi tạo giá trị rỗng
            me.employeeIdSelected.Gender = "1";
            await me.getCodeNewAndShowDialog();

        },
        /**
         * Thực hiện click vào nút sửa
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnEditEmployeeDetail(employee) {
            var me = this;
            me.editMode = mylib.misaEnum.editMode.Edit; //Thể hiện là đang sửa
            me.employeeIdSelected = employee; //Gán employee bằng employee đang chọn

            me.isShowEmployeeDetail = true; //Thực hiện mở form detail
        },
        /**
         * Thực hiện khi click vào nhân bản
         * CreatedBy: HoaiPT(14/02/2022)
         */
        btnDuplicate() {
            var me = this;
            me.editMode = mylib.misaEnum.editMode.Add; //Thực hiện thêm mới
            me.getCodeNewAndShowDialog();
        },
        /**
         * Thực hiện Lấy mã employeeCode mới và Thực hiện mở dialog phục vụ cho thêm mới và nhân bản
         * CreatedBy: HoaiPT(08/02/2022)
         * CreatedBy: HoaiPT(14/02/2022)
         */
        async getCodeNewAndShowDialog() {
            try {
                var me = this;
                me.isShowLoading = true; //Hiển thị đang load
                await axios.get('https://localhost:44338/api/v1/Employees/CodeNew')
                    .then(function (res) {
                        me.employeeIdSelected.EmployeeCode = res.data; //Gán mã mới vào 
                        me.isShowLoading = false; //đóng
                        me.isShowEmployeeDetail = true; //mở form detail
                    })
                    .catch(function (res) {
                        console.error(res);
                    });
            } catch {
                console.log(mylib.resourcs["VI"].errorMsg);
            }

        },
        /**
         * Thực hiện mở Form xác nhận xóa
         * CreatedBy: HoaiPT(08/02/2022)
         */
        showRemoveEmployee(employee) {
            //Thực hiện gán dữ liệu và mở message lên
            this.employeeIdSelected = employee;

            this.isShowRemoveEmployee = true; //Mở fil

        },
        /**
         * Thực hiện export Excel
         * CreatedBy: HoaiPT(14/02/2022)
         */
        btnExportExcel() {
            try {
                //Chắc chắn là con trỏ this đang ở đây;
                var me = this;
                me.isShowLoading = true; //Hiển thị đang load
                axios.get('https://localhost:44338/api/v1/Employees/excel', {
                    responseType: 'blob',
                }).then((response) => {
                    const url = URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a'); //Tạo ra một element mới là thẻ a
                    link.href = url; //Thẻ a này có đường dẫn là file excel trả về từ database
                    link.setAttribute(
                        'download',
                        `${this.testExport}-${new Date().toLocaleDateString()}.xlsx` //File excel tải về có dạng như vậy
                    )
                    document.body.appendChild(link); //Thêm đường link mới này vào trong file html
                    link.click();
                    me.isShowLoading = false; //Đóng Loading
                })
            } catch {
                console.log(mylib.resourcs["VI"].errorMsg);
            }

        }
    },
    filters: {
        /**
         * Thực hiện định dạng lại ngày
         * CreatedBy: HoaiPT(08/02/2022)
         */
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
        /**
         * Thực hiện format giới tính
         * CreatedBy: HoaiPT(08/02/2022)
         */
        formatGender(value) {
            switch (value) {
                case mylib.misaEnum.genderNumber.Female.Value:
                    value = mylib.misaEnum.genderNumber.Female.Text;
                    break;
                case mylib.misaEnum.genderNumber.Male.Value:
                    value = mylib.misaEnum.genderNumber.Male.Text;
                    break;
                case mylib.misaEnum.genderNumber.Other.Value:
                    value = mylib.misaEnum.genderNumber.Other.Text;
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

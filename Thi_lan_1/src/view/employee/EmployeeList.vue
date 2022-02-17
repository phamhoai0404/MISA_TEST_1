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
                    <div class="m-button-icon" @click="isShowDeleteMany = !isShowDeleteMany"></div>
                    <div class="m-button-function" v-if="actions===2 && isShowDeleteMany===true " @mouseleave="isShowDeleteMany = false">
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
            editMode: 1, //form thêm hoặc sửa: 1: thêm mới; 2: sửa
            employeeIdSelected: {}, //employee đang được chọn

            keywordSearch: "",

            myTimeout: "", //Thực hiện cho setTimeout ở filer
            testExport: "Danh_sach_nhan_vien", //Tên của file export

            actions: 0, //0 là không thực hiện gì, 1: thực hiện xóa một bản ghi, 2: thực hiện xóa nhiều
            isShowDeleteMany: false, //Trạng thái xóa nhiều
            arrayEmployeeId: new Array(), //Nơi lưu trữ EmployeeId chuẩn bị xóa

            totalRecord: 0,
            totalPage: 1,
            pageAction: 1,
            listPageText: [
                "10", "20", "30", "50", "100"
            ],
            selectTextPage: 10,
            isShowPage: false,
        }
    },
    created() {
        //Gọi API lấy dữ liệu
        this.getDataListDepartment(); //Thực hiện lấy danh sách phòng ban
        this.getData(); //Thực hiện load dữ liệu

    },
    computed: {
        pageTextInInput: function () {
            return this.selectTextPage + " bản ghi trên 1 trang";
        }
    },

    watch: {
        //Phần này nên để lại để thực hiện phân trang đang làm 
        keywordSearch: function () {
            var me = this;
            clearTimeout(me.myTimeout);
            me.myTimeout = setTimeout(function () {
                  me.isShowLoading = true;
                  me.showData();
            }, 1000);
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
        async selectedPageText(index) {
            var me = this;

            me.selectTextPage = me.listPageText[index];//Thực hiện gán số kích thước trang đã lựa chọn 
            me.isShowLoading = true;//Show loading
            me.pageAction = 1;//Hiển thị ở trang đầu tiên

            await me.showData();//Thực hiện load lại dữ liệu trên table với đúng kích thước trang đã chọn

            me.isShowPage = false;//Đóng form page
        },
        clickCallback(pageIndex) {
            var me = this;
            me.pageAction = pageIndex;//Thực hiện gián giá trị mới cho trang muốn đứng
            me.isShowLoading = true;//Show loading

            me.showData();//Load lại dữ liệu table

        },
        /**
         * Thay đổi trạng thái đóng mở của form MessageRemoveEmployee
         */
        openMessage() {
            var me = this;
            me.isShowRemoveEmployee = !me.isShowRemoveEmployee; //Thực hiện đóng nếu mở hoặc mở nếu đóng
        },
        /**
         * Thực hiện khi click vào nút xóa nhiều
         */
        btnRemoveMany() {
            var me = this;
            console.log(me.arrayEmployeeId);
            me.isShowDeleteMany = false; //Đóng xóa nhiều
            me.isShowRemoveEmployee = true; //Mở MessageRemoveEmployee

        },
        /**
         * Thực hiện lấy mã mới ở backend
         */
        async getCodeNew() {
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
        },
        /**
         * Lấy toàn bộ dữ liệu về trạng thái ban đầu
         */
        getData() {
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;

            me.selectTextPage = 10; //Reset lại số lượng bản ghi trên 1 trang 
            me.pageAction = 1; //Reset lại số trang  đang hiển thị
            me.isShowLoading = true; //Hiển thị đang load
            me.keywordSearch = "";

            me.showData();//Load dữ liệu

           
        },
        /**
         * Lấy và hiển thị dữ liệu
         */
        async showData(){
            var me = this;
             await axios.get(`https://localhost:44338/api/v1/Employees/getPaging?searchText=${me.keywordSearch}&pageIndex=${me.pageAction}&pageSize=${me.selectTextPage}`)
                .then(function (res) {
                    console.log(res);
                    me.listEmployee = res.data.Data;
                    me.totalPage = Number(res.data.TotalPage);
                    me.totalRecord = res.data.TotalRecord;
                    me.isShowLoading = false;
                })
                .catch(function (res) {
                    console.log(res);

                })
        },
        /**
         * Lấy danh sách phòng ban
         */
        getDataListDepartment() {
            //Chắc chắn là con trỏ this đang ở đây;
            var me = this;
            axios.get('https://localhost:44338/api/v1/Departments')
                .then(function (res) {
                    console.log(res.data);
                    me.listDepartmentTable = res.data; //Thực hiện gán vào danh sách Department
                })
                .catch(function (res) {
                    console.err(res);
                })
        },
        btnRefresh(){
            var me = this;
            if(me.keywordSearch !=""){//Nếu nó khác rỗng 
                me.keywordSearch ="";//thì gián bằng rỗng sau thời gian thì nó sẽ xem sự thay đổi của biến keywordSearch in watch
            }else{
                me.getData();//Nếu không thì load lại dữ liệu
            }
        },

        /**
         * Hành động khi thực hiện vào hàng loạt
         */
        batchExecution() {
            var me = this;
            me.isShowDeleteMany = false; //Để nó bằng false tránh trường hợp cái action = 2, rồi isShowDeleteMany thì nó TỰ ĐỘNG mở ra cái xóa nhiều ra
            me.arrayEmployeeId = []; //Làm mới để chuẩn bị thêm toàn bộ hoặc không có gì
            if (!document.getElementById('hangloat').checked) {
                me.actions = 2; //Có thể ấn hành động là xóa nhiều

                for (let i = 0; i < me.listEmployee.length; i++) {
                    let id = me.listEmployee[i].EmployeeId;
                    document.getElementById(id).checked = true;
                    me.arrayEmployeeId.push(id) //Add vào để chuẩn bị xóa
                }
            } else {
                me.actions = 0; //Ấn không ra không hành động gì hết
                me.removeAllChecked(me.listEmployee); //Xóa bỏ hết checked
            }
        },
        /**
         * Thực hiện làm mới xóa bỏ tất cả checked
         */
        removeAllChecked(listEmployee) {
            for (let i = 0; i < listEmployee.length; i++) {
                let id = listEmployee[i].EmployeeId;
                if (document.getElementById(id).checked)
                    document.getElementById(id).checked = false;
            }
            document.getElementById('hangloat').checked = false;
        },
        /**
         * Thực hiện khi click vào từng ô checked một
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
                me.actions = 0; //Nếu mà tất cả đều không tích chọn thì sẽ không click vào thực hiện hàng loạt được
            } else {
                me.actions = 2; //Có thể ấn hành động là xóa nhiều
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
         */
        positionFunction(y, index) {
            y = index != (this.listEmployee.length - 1) ? (y + 14) : (y - 105);
            document.getElementById('function-ground').style.position = 'fixed';
            document.getElementById('function-ground').style.right = '70px';
            document.getElementById('function-ground').style.top = `${y}px`;

        },
        /**
         * Thực hiện khi click vào ô thêm mới
         */
        async showAddEmployeeDetail() {
            var me = this;

            //Khởi tạo đối tượng rỗng với giá trị của giới tính mặc định là Nam và gán giá trị editMode = 1 là thuộc kiểu thêm
            me.editMode = 1;
            me.employeeIdSelected = {};
            me.employeeIdSelected.Gender = "1";
            await me.getCodeNewAndShowDialog();

        },
        /**
         * Thực hiện click vào nút sửa
         */
        btnEditEmployeeDetail(employee) {
            // //Đóng hộp thoại function
            // document.getElementById('function-ground').style.display = 'none';

            this.editMode = 2; //Thể hiện là đang sửa
            this.employeeIdSelected = employee;

            this.isShowEmployeeDetail = true;
        },
        /**
         * Thực hiện khi click vào nhân bản
         */
        btnDuplicate() {
            var me = this;
            me.editMode = 1; //Thực hiện thêm mới
            me.getCodeNewAndShowDialog();
        },
        /**
         * Thực hiện Lấy mã employeeCode mới và Thực hiện mở dialog phục vụ cho thêm mới và nhân bản
         */
        async getCodeNewAndShowDialog() {
            var me = this;
            me.isShowLoading = true; //Hiển thị đang load
            await axios.get('https://localhost:44338/api/v1/Employees/CodeNew')
                .then(function (res) {
                    me.employeeIdSelected.EmployeeCode = res.data; //Gán mã mới vào 
                    me.isShowLoading = false; //đóng
                    me.isShowEmployeeDetail = true;
                })
                .catch(function (res) {
                    console.error(res);
                })

        },
        /**
         * Thực hiện mở Form xác nhận xóa
         */
        showRemoveEmployee(employee) {
            //Thực hiện gán dữ liệu và mở message lên
            this.employeeIdSelected = employee;

            this.isShowRemoveEmployee = true;

        },
        /**
         * Thực hiện export Excel
         */
        btnExportExcel() {
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
                me.isShowLoading = false; //Đóng Loading
            })
        }
    },
    filters: {
        /**
         * Thực hiện định dạng lại ngày
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
         */
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

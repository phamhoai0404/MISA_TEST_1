<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-icon-warning"></div>
            <div class="m-mess-title">{{writeTextRemove()}}
            </div>
        </div>
        <div class="m-message-line"></div>
        <div class="m-messages-footer-question">
            <div class="m-footer-left">
                <button class="m-button m-button-not-color" @click="btnCancel()">Không</button>
            </div>
            <div class="m-footer-right">
                <button class="m-button" @click="btnConfirmRemove()">Có</button>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import axios from 'axios'
import * as mylib from '../../js/resourcs'

export default {
    props: [
        'employee', 'action'
    ],
    methods: {
        /**
         * Thực hiện khi click vào cancel
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnCancel() {
            var me = this;
            me.$emit('openMessageRemove');
        },
        /**
         * Thực hiện khi click vào xác nhận xóa
         * CreatedBy: HoaiPT(08/02/2022)
         * UpdatedBy: HoaiPT(14/02/2022)
         */
        async btnConfirmRemove() {
            try {
                var me = this;
                await me.$parent.removeAllChecked(me.$parent.listEmployee); //Xóa hết checked
                document.getElementById('hangloat').checked = false;

                switch (me.action) {
                    case mylib.misaEnum.actionDelete.One: //Thực hiện xóa 1
                        await axios.delete(`https://localhost:44338/api/v1/Employees/${me.employee.EmployeeId}`)
                            .then(function () {
                                me.resetTable();//Làm mới lại table
                            })
                        break;
                    case mylib.misaEnum.actionDelete.Multi: //Thực hiện xóa nhiều
                        if (me.$parent.arrayEmployeeId.length >= 1) {

                            await axios.post('https://localhost:44338/api/v1/Employees/DeleteMany', me.$parent.arrayEmployeeId)
                                .then(function () {
                                    me.resetTable();//Làm mới lại table
                                })
                                .catch(function (res) {
                                    console.error(res);
                                })
                        }
                        break;
                }

            } catch {
                console.log(mylib.resourcs["VI"].errorMsg);
            }
        },
        /**
         * Thực hiện thiết lập lai Table
         * CreatedBy: HoaiPT(14/02/2022)
         */
        resetTable() {
            var me = this;
            me.$parent.actions = mylib.misaEnum.actionDelete.NoAction;//Thể hiện không phải là xóa cũng không phải là xóa nhiều
            me.$parent.arrayEmployeeId = []; //Làm mới lại
            me.$emit('openMessageRemove'); //Đóng message 
            me.$emit('reloadData', null); //Load lại table
        },
        /**
         * Thực hiện hiển thị text phù hợp
         * CreatedBy: HoaiPT(14/02/2022)
         */
        writeTextRemove() {
            var me = this;
            switch (me.action) {
                case mylib.misaEnum.actionDelete.One://thực hiện xóa 1
                    return `Bạn có thực sự muốn xóa nhân viên <${me.employee.EmployeeCode}> không?`;
                case mylib.misaEnum.actionDelete.Multi://Thực hiện xóa nhiều
                    return mylib.resourcs["VI"].deleteMulti;
                default:
                    return "";
            }
        }

    }
}
</script>

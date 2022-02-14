<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-icon-warning"></div>
            <div class="m-mess-title" >{{writeTextRemove()}}
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
export default {
    props: [
        'employee', 'action'
    ],
    created() {
        var me = this;
        console.log("created");
        console.log(me.action);

    },
    methods: {
        btnCancel() {
            var me = this;
            me.$emit('openMessageRemove');
        },
        btnConfirmRemove() {
            try {
                var me = this;
                axios.delete(`https://localhost:44338/api/v1/Employees/${me.employee.EmployeeId}`)
                    .then(function () {
                        me.$emit('openMessageRemove'); //Đóng message 
                        me.$emit('reloadData', null);  //Load lại table
                    })
                    .catch(function (res) {
                        console.log(res);
                    })

            } catch (error) {
                console.error('Có lỗi xảy ra' + error);
            }
        },
        writeTextRemove() {
            var me = this;
            console.log("đó là ");
            console.log(me.action);
            switch (me.action) {
                case 1:
                    return  `Bạn có thực sự muốn xóa nhân viên <${me.employee.EmployeeCode}> không?`;
                case 2:
                    return "Bạn thực sự có muốn xóa những nhân viên đã chọn không?";
                default:
                    return "";
            }
        }

    }
}
</script>

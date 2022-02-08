<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-icon-warning"></div>
            <div class="m-mess-title">Bạn có thực sự muốn xóa nhân viên &lt;{{employee.EmployeeCode}}&gt; không?
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
    props:[
        'employee',
    ],
    methods:{
        btnCancel(){
            var me = this;
            me.$emit('openMessageRemove');
        },
        btnConfirmRemove(){
            try {
                var me = this;
                axios.delete(`http://amis.manhnv.net/api/v1/Employees/${this.employee.EmployeeId}`)
                   .then(function () {          
                    //Đóng message 
                    me.$emit('openMessageRemove');

                    //Load lại table
                    me.$emit('reloadData', null);
                })
                .catch(function (res) {
                    console.log(res);
                })

            } catch (error) {
                console.error('Có lỗi xảy ra' + error);
            }
        }
    }
}
</script>

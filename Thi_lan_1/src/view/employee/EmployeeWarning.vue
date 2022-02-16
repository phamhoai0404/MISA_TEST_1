<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-icon-warning"></div>
            <div class="m-mess-title">
                Mã nhân viên &lt;{{employeeCode}}&gt; đã tồn tại trong hệ thống, vui lòng kiểm tra lại?
            </div>
        </div>
        <div class="m-message-line"></div>
        <div class="m-messages-footer-question">
            <div class="m-footer-left">

            </div>
            <div class="m-footer-right">
                <button class="m-button" @click="btnCloseWarningMessage()">Đồng ý</button>
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    props: [
        'employeeCode',
    ],
    methods: {
        /**
         * Thực hiện khi click vào đồng ý
         */
        btnCloseWarningMessage() {
            var me = this;
            me.$emit('openWarningMessage');
        }
    }
}
</script>

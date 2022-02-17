<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-icon-info"></div>
            <div class="m-mess-title"> {{ errorData }} 
            </div>
        </div>
        <div class="m-message-line"></div>
        <div class="m-messages-footer-info">
            <button class="m-button" @click="btnCloseInfoEmployee()">Đóng</button>
        </div>
    </div>
</div>
</template>

<script>
export default {
    props:[
        'errorData',
    ],
    methods:{
        /**
         * Thực hiện khi click vào đóng
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnCloseInfoEmployee(){
            var me = this;
            me.$emit('openInfoEmployee');
        }
    }
}
</script>

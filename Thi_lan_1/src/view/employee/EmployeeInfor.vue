<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-icon-info"></div>
            <div class="m-mess-title"> {{ errorData }} không được để trống.
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
        btnCloseInfoEmployee(){
            var me = this;
            me.$emit('openInfoEmployee');
        }
    }
}
</script>

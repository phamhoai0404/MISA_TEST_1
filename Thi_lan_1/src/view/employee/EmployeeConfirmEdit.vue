<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-exclamation-question"></div>
            <div class="m-mess-title">Dữ liệu đã bị thay đổi bạn có muốn cất không?
            </div>
        </div>
        <div class="m-message-line"></div>
        <div class="m-messages-footer-question">
            <div class="m-footer-left">
                <button class="m-button m-button-not-color" @click="btnCancelCloseDialog()">Hủy</button>
            </div>
            <div class="m-footer-right">
                <button class="m-button m-button-not-color"  @click="btnCloseDialog()" >Không</button>
                <button class="m-button" @click="btnCloseDialogAndSave()">Có</button>

            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    methods: {
        btnCancelCloseDialog() {
            var me = this;
            me.$emit('openEditEmployee');
        },
        btnCloseDialog(){
            var me = this;
            me.$emit('closeDialogDetail');
        },
        btnCloseDialogAndSave(){
            var me = this;
            me.$emit('openEditEmployee');
            me.$emit('closeDialogDetailAndSave');
        }
    }
}
</script>

<template>
<div class="m-message ">
    <div class="m-message-content">
        <div class="m-messages-body">
            <div class="m-mess-icon m-exclamation-question"></div>
            <div class="m-mess-title">{{titleForm}}
            </div>
        </div>
        <div class="m-message-line"></div>
        <div class="m-messages-footer-question">
            <div class="m-footer-left">
                <button class="m-button m-button-not-color" @click="btnCancelCloseDialog()">Hủy</button>
            </div>
            <div class="m-footer-right">
                <button class="m-button m-button-not-color" @click="btnCloseDialog()">Không</button>
                <button class="m-button" @click="btnCloseDialogAndSave()">Có</button>

            </div>
        </div>
    </div>
</div>
</template>

<script>
import * as mylib from '../../js/resourcs'
export default {
    data(){
        return {
            titleForm: mylib.resourcs["VI"].confirmEdit,
        }
    },
    methods: {
        /**
         * Thực hiện click vào nút hủy
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnCancelCloseDialog() {
            var me = this;
            me.$emit('openEditEmployee');
        },
        /**
         * Thực hiện click vào nút không
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnCloseDialog() {
            var me = this;
            me.$emit('closeDialogDetail');
        },
        /**
         * Thực hiện click vào nút cất
         * CreatedBy: HoaiPT(08/02/2022)
         */
        btnCloseDialogAndSave() {
            var me = this;
            me.$emit('openEditEmployee');
            me.$emit('closeDialogDetailAndSave');
        }
    }
}
</script>

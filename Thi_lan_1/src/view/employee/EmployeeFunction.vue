<template>
<div class="m-function-group" id="function-ground" v-click-outside="hideFunction">
    <div class="m-function-item" @click ="btnDuplication()">Nhân bản</div>
    <div class="m-function-item" @click="btnRemove">Xóa</div>
    <div class="m-function-item">Ngừng sử dụng</div>
</div>
</template>

<script>
import * as mylib from '../../js/resourcs'
export default {
    methods: {
        /**
         * Thực hiện khi click vào nút xóa
         * CreatedBy: HoaiPT(08/02/2022)
         * UpdatedBy: HoaiPT(14/02/2022)
         */
        btnRemove() {
            var me = this;
            me.$parent.actions = mylib.misaEnum.actionDelete.One;//Thể hiện là đang xóa 1
            me.$emit('openRemoveEmployee');
        },
         /**
         * Thực hiện khi click vào nút nhân bản
         * CreatedBy: HoaiPT(14/02/2022)
         */
        btnDuplication() {
            var me = this;
            me.$emit('openDuplication');
        },
        /**
         * Thực hiện ẩn khi click vào bất kì cái nào khác function-ground với khác event.target.id == "clickFunction"
         * CreatedBy: HoaiPT(08/02/2022)
         */
        hideFunction() { 
            if (window.document.getElementById('function-ground')) {
                var functionGroup = window.document.getElementById('function-ground').style.display;

                if (functionGroup + "" != "none") {
                    window.document.getElementById('function-ground').style.display = 'none';
                }
            }
        }
    }
}
</script>

<style scoped>
@import url(../../style/view/employee/employee-function.css);
</style>

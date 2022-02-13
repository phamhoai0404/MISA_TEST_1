<template>
<div class="m-function-group" id="function-ground" v-click-outside="hideFunction">
    <div class="m-function-item" @click ="btnDuplication()">Nhân bản</div>
    <div class="m-function-item" @click="btnRemove">Xóa</div>
    <div class="m-function-item">Ngừng sử dụng</div>
</div>
</template>

<script>
export default {
    methods: {
        btnRemove() {
            //Đóng hộp thoại function
            document.getElementById('function-ground').style.display = 'none';

            var me = this;
            me.$emit('openRemoveEmployee');
        },

        btnDuplication() {
            //Đóng hộp thoại function
            document.getElementById('function-ground').style.display = 'none';

            var me = this;
            me.$emit('openDuplication');
        },
        hideFunction() { //Thực hiện ẩn khi click vào bất kì cái nào khác function-ground với khác event.target.id == "clickFunction"
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

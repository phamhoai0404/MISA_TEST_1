<template>
<div class="m-menu">
    <div class="m-menu-top">
        <div class="m-icon"></div>
        <div class="m-logo"></div>
    </div>
    <div class="m-menu-body" id="menu-body">
        <router-link to="/employee" class="menu-item" id="overview"  >
            <div class="m-item-icon overview" ></div>
            <div class="m-item-name" data-title="Tổng quan">Tổng quan</div>
        </router-link>
        <router-link to="/customer" class="menu-item" @click.native="removeActiveFirst()">
            <div class="m-item-icon cash"></div>
            <div class="m-item-name">Tiền mặt</div>
        </router-link>
        <div class="menu-item">
            <div class="m-item-icon deposits"></div>
            <div class="m-item-name">Tiền gửi</div>
        </div>

        <div class="menu-item">
            <div class="m-item-icon purchase"></div>
            <div class="m-item-name">Mua hàng</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon sell"></div>
            <div class="m-item-name">Bán hàng</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon order-management"></div>
            <div class="m-item-name">Quản lý đơn</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon warehouse"></div>
            <div class="m-item-name">Kho</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon tools "></div>
            <div class="m-item-name">Công cụ dụng cụ</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon fixed-assets"></div>
            <div class="m-item-name">Tài sản cố định</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon tax"></div>
            <div class="m-item-name">Thuế</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon aggregate-cost"></div>
            <div class="m-item-name">Giá thành tổng hợp</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon budget"></div>
            <div class="m-item-name">Ngân sách </div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon report"></div>
            <div class="m-item-name">Báo cáo</div>
        </div>
        <div class="menu-item">
            <div class="m-item-icon financial-analysis"></div>
            <div class="m-item-name">Phân tích tài chính</div>
        </div>
    </div>

    <!-- <div class="m-menu-bottom">

            </div> -->
</div>
</template>

<script>
export default {
    mounted() {
        this.currentPath = this.$router.currentRoute.path;
    },
    data() {
        return {
            currentPath: '',
        }
    },
    watch: {//để xem sự thay đổi thì lần đầu tiên đa
        currentPath: function (value) {
            value = this.$router.currentRoute.path;
            if (value == '/') {
                document.getElementById('overview').classList.add('router-link-active');
            } 
        }
    },
    methods: {
        removeActiveFirst() {
            this.currentPath = this.$router.currentRoute.path;
            if(this.currentPath != '/'){
                document.getElementById('overview').classList.remove('router-link-active');
            }else{
                document.getElementById('overview').classList.add('router-link-active');
            }
        }
    }

}
</script>

<style scoped>
@import url(../../style/layout/menu.css);
</style>

import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import EmployeeList from './view/employee/EmployeeList.vue'
import CustomerList from './view/customer/CustomerList.vue'
import VCalendar from 'v-calendar';
import Paginate from 'vuejs-paginate'



Vue.use(VueRouter) //Thực hiện sử dụng vue router
Vue.use(VCalendar) //Thực hiện thư viện của datepiker
Vue.component('paginate', Paginate) //Thực hiện thư viện sử dụng phân trang

Vue.config.productionTip = false

//Thiết lập router
const routes = [
    { path: '/', component: EmployeeList },
    { path: '/employee', component: EmployeeList },
    { path: '/customer', component: CustomerList }
]

const router = new VueRouter({
    mode: 'history', //Xóa dấu # ở thanh URL
    routes // short for `routes: routes`
})

//Thực hiện để thực hiện hành động bên ngoài click của thành phần nào đó
Vue.directive('click-outside', {
    bind: function(el, binding, vnode) {
        window.event = function(event) {
            if (!(el == event.target || event.target.id == "clickFunction")) {
                vnode.context[binding.expression](event);
            }
        };
        document.body.addEventListener('click', window.event)
    },
});

new Vue({
    router,
    render: h => h(App),
}).$mount('#app')
import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import EmployeeList from './view/employee/EmployeeList.vue'
import CustomerList from './view/customer/CustomerList.vue'
import VCalendar from 'v-calendar';


Vue.use(VueRouter)
Vue.use(VCalendar)

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


Vue.directive('click-outside', {
    bind: function(el, binding, vnode) {
        window.event = function(event) {
            if (!(el == event.target || el.contains(event.target) || event.target.id == "clickFunction")) {
                vnode.context[binding.expression](event);
            }
        };
        document.body.addEventListener('click', window.event)
    },
    // unbind: function() {
    //     document.body.removeEventListener('click', this.event)
    // },
});

new Vue({
    router,
    render: h => h(App),
}).$mount('#app')
<template>
     <button class="m-button" @click="btnAddOnClick()" style="border-radius: 89px;">Thêm mới nhân viên</button>
</template>
<script>
export default {
     methods:{
          btnAddOnClick(){
               var me =  this;
               me.$emit('showDialog');
          }
     }
}
</script>

<template>
     <button class="m-button" @click="btnAddOnClick()" style="border-radius: 89px;">Thêm mới nhân viên</button>
</template>
<script>
export default {
     methods:{
          /**
           * Thực hiện khi click vào thêm mới nhân viên
           * CreatedBy: HoaiPT(07/02/2022)
           */
          btnAddOnClick(){
               var me =  this;
               me.$emit('showDialog');
          }
     }
}
</script>

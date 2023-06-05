<template>
    <div class="row">
        <div class="col-2">登入使用者</div>
        <div class="col-4">
        <span>目前user Name：</span><span id="spanUserName" >{{vUserInfoStr}}</span>
        </div>
    </div>
    <div>
        <p>人員列表</p>
		<select name="selectedFriend" @change="SelectedFriend_onChange($event)" >
            名稱:
            <option value="">請選擇</option>
            <option value="john">john</option>
            <option value="amy">amy</option>
            <option value="may">may</option>
        </select>
    </div>
    
     <div>
        <SignalRTest2Child  :SelUser="SelUser"  />
     </div>

</template>

<script lang="ts" >
import { defineComponent,ref,reactive,watch,onMounted  } from 'vue';
import axios from 'axios';
import SignalRTest2Child from '../components/SignalRTest2Child.vue'

export default defineComponent ({
    name:"SignalRTest2Parent",
    components: {
        SignalRTest2Child
  },
  setup(){

    let vUserInfoStr:any="ken";
    let SelUser:any=reactive({
    id:"",
    name:"",
    age:"",
    });

    function SelectedFriend_onChange(event:any)
    {
        SelUser.id = "";
        SelUser.name = event.target.value;
        console.log(SelUser);
    }

    watch(SelUser,()=>{
         console.log("parent watch:"+SelUser.name);
    });
    onMounted(() => {
        
    });

    return{
        SelUser,
        vUserInfoStr,
        SelectedFriend_onChange,
    }

  },

})
</script>

<style>

</style>
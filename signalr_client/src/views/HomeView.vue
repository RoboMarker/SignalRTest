<template>
  <div class="home">
    帳號:<input type="text"  v-model="vUser.UserId" placeholder="請輸入使用者名稱"  /><br/>
    密碼:<input type="password"  v-model="vUser.Password" placeholder="請輸入密碼" /><br/>
    <button @click="funSubmit">登入</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import {ref,reactive} from 'vue';
import { default as axios } from 'axios';


export default defineComponent({
  name: 'HomeView',
    setup() {
    var vUser:any=reactive({
      UserId:"",
      UserName:"",
      Password:""
    });


      function funSubmit()
      {
        var  vUrl = "https://localhost:44371/api/Login/login";
        axios({
            method: "post",
            url: vUrl,
            data: vUser,
            headers: {
                'Content-Type': 'application/json',
            }
        }
        ).then(res => {
            console.log(res);
            if(res.status==200)
            {
              localStorage.setItem('loginToken', res.data.token);
              console.log(res.data.token);
              window.open("/about");
            }
        }).catch(function (error) {
            console.log(error)
        });
      }

      return{
        vUser,
        funSubmit,
      }
    }
});
</script>

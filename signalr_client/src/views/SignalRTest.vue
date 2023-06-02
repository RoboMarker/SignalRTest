<template>
  <div class="about">
    <h1>This is an about page</h1>
    <p>連線id:<span>{{ConnectId}}</span></p>
    <p>訊息狀態:<span>{{ConnecStatusMessage}}</span></p>
    <p>顯示訊息</p>
    <div id="divMessages" >
             <li v-for="(input, index) in vMessagesList">
            <span>{{input}}</span>
            </li>
    </div>
    <input id="tbMessage" type="text" v-model="InputMessage" @keyup.enter="send" />
    <button id="btnSend" @click="send">Send</button>
  </div>
</template>

<script lang="ts">
import { ref, defineComponent, reactive, onMounted  } from "vue";
import {HubConnectionBuilder} from "@microsoft/signalr";


export default defineComponent({
  name: "SignalRTest",
  setup() {
    //const token:any= localStorage.getItem("loginToken");
    let hub = reactive({
      connection: {},
      HubConnId: "",
      resultInfo: {},
    });
    

    let vMessagesList=reactive([]);
    const InputMessage = ref('');
    const username = new Date().getTime();
    const ConnectId=ref('');
    const ConnecStatusMessage=ref('');
    const connectionServer = 
      new HubConnectionBuilder()
        .withUrl("https://localhost:44371/progressHub",{
            accessTokenFactory:async () =>
                { 
                   const token:any= localStorage.getItem("loginToken");
                   return token;
                } // 获取JWT访问令牌
        })
        .build();

    connectionServer.on('messageReceived', (username:never, message:never) => {
      console.log("messageReceived");
      //"username:"+username+",message:"+message
      vMessagesList.push(message);
      });

    const send = () => {
      connectionServer.send('newMessage', username, InputMessage.value).then(() => (InputMessage.value = ''));
    };
     connectionServer.on("SetHubConnId", (id) => {
      hub.HubConnId = id;
      ConnectId.value=id;
      ConnecStatusMessage.value="connection sunccess";
    });
 

    onMounted(() => {
      connectionServer.start().catch((err) => document.write(err));
    connectionServer.onclose((error) => {
      ConnecStatusMessage.value="connection error";
    });
    });

    return {
      vMessagesList,
      InputMessage,
      send,
      ConnectId,
      ConnecStatusMessage
    }
  },
});

 
</script>

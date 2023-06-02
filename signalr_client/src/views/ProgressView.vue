<template>
  
</template>


<script lang="ts">
import { ref, defineComponent, reactive, getCurrentInstance } from "vue";
import {HubConnectionBuilder} from "@microsoft/signalr";
 
export default defineComponent({
  name: "HomeView",
  setup() {
    const connectionServer = 
      new HubConnectionBuilder()
        .withUrl("https://localhost:44371/progressHub")
        .build();
    connectionServer.start();
    connectionServer.on("SetHubConnId",function(jsonList:never){
        console.log(jsonList);
        //vUserList.push(jsonList);
    });
    return {
   
    }
  },
});
</script>

<template>
  <div class="about">
    <h1>This is an about page</h1>
    <p>{{Msg}}</p>
  </div>
</template>

<script lang="ts">
import {  defineComponent } from "vue";

export default defineComponent({
  name: "AboutView",
  setup() {
    const token:any= localStorage.getItem("loginToken");
    let Msg="未登入";
    if(token!=null)
    {
      Msg="登入成功";
    }

    return {
     Msg
    }
  },
});
</script>

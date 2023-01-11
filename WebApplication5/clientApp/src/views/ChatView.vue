<template>
  <div>
    <input type="text" v-model="state.userMessage" v-on:keypress="txtMsgOnkeypress"/>
    <div>
      <ul>
        <li v-for="(msg,index) in state.messages" :key="index">{{ msg }}</li>
      </ul>
    </div>
  </div>
</template>
<script>
import {reactive, onMounted} from 'vue';
import * as signalR from '@microsoft/signalr';

let connection;
export default {
  name: 'Login',
  setup() {
    const state = reactive({userMessage: "", messages: []});
    const txtMsgOnkeypress = async function (e) {
      if (e.keyCode != 13) return;
      await connection.invoke("SendPublicMessage", state.userMessage);
      state.userMessage = "";
    };
    onMounted(async function () {
      // Create a connection to the server  
      // 禁用协商 直接使用websockets
      const options = {skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets};
      connection = new signalR.HubConnectionBuilder()
          .withUrl('https://localhost:7164/Hubs/chatroom', options)
          .withAutomaticReconnect().build();
      await connection.start();
      connection.on('ReceivePublicMessage', msg => {
        state.messages.push(msg);
      });
    });
    return {state, txtMsgOnkeypress};
  },
}
</script>
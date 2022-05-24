<template>
  <div>
    <div class="messanger-display">
      <div v-for="message in messages" :key="message.text" class="mb-4">
        <div v-if="message.fromId == id" class="d-flex justify-content-end">
      
          <div class="message_composition">
            <div class="msg_cotainer_send">
              {{ message.text }}
            </div>
            <!-- <span class="msg_time">8:40 AM, Today</span> -->
          </div>
        </div>
        <div v-else class="d-flex justify-content-start">
          <div class="message_composition">
            <div class="msg_cotainer">
              {{ message.text }}
            </div>
          </div>
    
        </div>
      </div>
    </div>
    <div class="card-footer">
      <div class="input-group">
        <textarea
          name=""
          class="form-control type_msg"
          placeholder="Type your message..."
          v-model="textarea"
        ></textarea>
        <div class="input-group-append">
          <span class="input-group-text send_btn" @click="buttonClickHandler">
            <img src="../../assets/forward.png" alt="" />
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import messageService from "../../services/MessageService";

export default {
  name: "Main",
  data() {
    return {
      textarea: "",
    };
  },

  methods: {
    buttonClickHandler() {
      const text = this.textarea;
      const id = this.$store.getters["dialog/get"].id;

      this.textarea = "";

      messageService.send(id, text);
    },
  },

  computed: {
    messages() {
      return this.$store.getters["dialog/getMessages"];
    },
    id() {
      return this.$store.getters["account/get"].id;
    },
  },
};
</script>

<style scoped>
.messanger-bottom {
  display: flex;
  padding: 20px 20px 0 20px;
}
.textarea {
  min-width: 94%;
  resize: none;
}
.button {
  align-self: flex-end;
}

.messanger-display {
  flex-grow: 1;
  padding: 40px;
}

.left {
  text-align: left;
}

.right {
  text-align: right;
}

.message-line {
  margin-bottom: 10px;
}
.msg_cotainer {
  margin-top: auto;
  margin-bottom: auto;
  margin-left: 10px;
  border-radius: 25px;
  background-color: #82ccdd;
  padding: 10px;
  position: relative;
}
.msg_cotainer_send {
  margin-top: auto;
  margin-bottom: auto;
  margin-right: 10px;
  border-radius: 25px;
  background-color: #78e08f;
  padding: 10px;
  position: relative;
}
.msg_time {
  position: absolute;
  left: 0;
  bottom: -15px;
  color: rgba(255, 255, 255, 0.5);
  font-size: 10px;
}
.msg_time_send {
  position: absolute;
  right: 0;
  bottom: -15px;
  color: rgba(255, 255, 255, 0.5);
  font-size: 10px;
}
.msg_head {
  position: relative;
}
.card-footer {
  border-radius: 0 0 15px 15px !important;
  border-top: 0 !important;
}

.input-group {
  position: relative;
  display: -ms-flexbox;
  display: flex;
  -ms-flex-wrap: wrap;
  flex-wrap: wrap;
  -ms-flex-align: stretch;
  align-items: stretch;
  width: 100%;
}
.type_msg {
  background-color: rgba(0, 0, 0, 0.3) !important;
  border: 0 !important;
  color: white !important;
  height: 60px !important;
  overflow-y: auto;
}
.type_msg:focus {
  box-shadow: none !important;
  outline: 0px !important;
}
.send_btn {
  border-radius: 0 15px 15px 0 !important;
  background-color: rgba(0, 0, 0, 0.3) !important;
  border: 0 !important;
  color: white !important;
  cursor: pointer;
}
.user_img_msg {
  height: 40px;
  width: 40px;
  border: 1.5px solid #f5f6fa;
}

.message_composition {
  position: relative;
}

.messanger-display {
      height: 304px;
    overflow: auto;
}
</style>
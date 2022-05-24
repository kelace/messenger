<template>
  <div class="Dialog">
    <div class="card-header msg_head">
      <div class="d-flex bd-highlight">

        <div class="user_info">
          <span>Chat with {{ user.name }}</span>
        </div>
        <div class="video_cam">
          <span><i class="fas fa-video"></i></span>
          <span><i class="fas fa-phone"></i></span>
        </div>
      </div>

    </div>
    <div class="card-body msg_card_body">
      <Empty
        v-if="userType == 'Offer'"
        :belong="user.isThisUserInitiatorOfRelation"
        :status="user.status"
        :id="user.id"
      />
      <Main v-else />
    </div>
  </div>
</template>

<script>
import Empty from "./Empty.vue";
import Main from "./Main.vue";
import userService from "../../services/UserService";

export default {
  name: "dialog",
  data() {
    return {
      messageWindow: "message",
      userBelong: "",
      userStatus: "",
      userType: "Offer",
      userId: "",
      user: "",
    };
  },
  computed: {},
  components: {
    Empty,
    Main,
  },

  created() {
    const id = this.$route.params.id;
    const user = userService.get(id);

    this.user = user;
    userService.changeDialogUser(id);
  },

  mounted() {
    const id = this.$route.params.id;
    const user = userService.get(id);

    this.user = user;
  },

  updated() {
    const id = this.$route.params.id;
    const user = userService.get(id);

    console.log(user);

    this.user = user;
    userService.changeDialogUser(id);
  },
};
</script>

<style scoped>
.Dialog {
  flex-grow: 1;
}

.img_cont {
  position: relative;
  height: 70px;
  width: 70px;
}

.img_cont_msg {
  height: 40px;
  width: 40px;
}
.online_icon {
  position: absolute;
  height: 15px;
  width: 15px;
  background-color: #4cd137;
  border-radius: 50%;
  bottom: 0.2em;
  right: 0.4em;
  border: 1.5px solid white;
}
.offline {
  background-color: #c23616 !important;
}
.user_info {
  margin-top: auto;
  margin-bottom: auto;
  margin-left: 15px;
}
.user_info {
  margin-top: auto;
  margin-bottom: auto;
  margin-left: 15px;
}
.user_info span {
  font-size: 20px;
  color: white;
}
.user_info p {
  font-size: 10px;
  color: rgba(255, 255, 255, 0.6);
}
</style>

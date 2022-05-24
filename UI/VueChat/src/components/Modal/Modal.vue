<template>
  <div class="modal">
    <div class="modal-core">
      <div class="modal-exit" @click="$emit('close')">
        <img src="../../assets/reject.png" alt="" />
      </div>
    <div class="user-name">
      {{ currenUserName }}
    </div>
      <div class="button is-info" @click="logOutHandler">Log out</div>
      
    </div>
    <div class="modal-overllay" @click="$emit('close')"></div>
  </div>
</template>

<style scoped>
.user-name {
  margin-bottom: 30px;
}
.photo {
  position: relative;
  width: 100px;
  height: 100px;
  background: gray;
  margin: 0 auto;
  border-radius: 50%;
  margin-bottom: 40px;
}
.photo-btn {
  text-align: center;
}
.modal {
  position: fixed;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-core {
  position: relative;
  background: #ffff;
  border-radius: 5px;
  width: 700px;
  padding: 30px;
  z-index: 1;
}

.modal-overllay {
  position: absolute;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
}

.modal-exit {
  position: absolute;
  right: 20px;
  top: 20px;
  cursor: pointer;
}

.photo img {
  border-radius: 50%;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.file {
  justify-content: center;
  margin-bottom: 30px;
}
</style>

<script>
import axios from "axios";
import serviceAuth from "../../services/AuthService";

export default {
  name: "modal",

  data() {
    return {
      files: null,
    };
  },

  computed: {
    currenUserName() {
      return this.$store.state.account.currentUser.name;
    },
    photo() {
      return this.$store.state.account.currentUser.photo;
    },
  },
  methods: {
    logOutHandler() {
      serviceAuth.logOut();
    },
    saveClickHandler() {
      const files = this.files;
      const formData = new FormData();
      formData.append("photo", files[0]);
      axios({
        method: "post",
        url: "/Account/ChangeAccountSettings",
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: serviceAuth.prepareToken(),
        },
      });
    },

    closeModal() {},

    photoChangedHandler(e) {
      this.files = e.target.files;
      const files = e.target.files;

      if (files.length > 1) {
        alert("больше одного нельзя");
        return;
      }

      const fr = new FileReader();

      fr.addEventListener("load", () => {
        this.$store.dispatch("account/changePhoto", fr.result);
      });

      fr.readAsDataURL(files[0]);
    },
  },
};
</script>

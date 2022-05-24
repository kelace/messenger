<template>
  <Modal v-show="modalOpen" @close="closeModalHandler" />
  <div class="messanger-holder">
    <div class="container-fluid h-100">
      <div class="row justify-content-center h-100">
        <div class="col-md-4 col-xl-3 chat">
          <div class="card mb-sm-3 mb-md-0 contacts_card">
            <div class="card-header">
              <div class="settings-icon" @click="settingsClickHanlder">
                <img
                  src="../assets/settings.png"
                  alt=""
                  @click="openModalHandler"
                />
              </div>
              <div class="input-group">
                <input
                  type="text"
                  placeholder="Search..."
                  name=""
                  class="form-control search"
                  v-model="searchInput"
                />
                <div class="input-group-prepend">
                  <span class="input-group-text search_btn"
                    ><i class="fas fa-search"></i
                  ></span>
                </div>
              </div>
            </div>
            <div class="card-body contacts_body">
              <progress
                v-if="loading"
                class="progress is-small is-primary"
                max="100"
              >
                15%
              </progress>
              <div v-else>
                <div v-if="searchBlock == 'Search'" class="users-list">
                  <ul class="contacts">
                    <router-link
                      v-for="card in searchingUsersList"
                      :key="card.name"
                      :to="'/chat/profile/' + card.id"
                    >
                      <li :class="{ active: this.$route.params.id == card.id }">
                        <div class="d-flex bd-highlight">
                          <div class="user_info">
                            <span>{{ card.name }}</span>

                            <p>
                              {{ getMessagePreview(card) }}
                            </p>
                          </div>
                        </div>
                      </li>
                    </router-link>
                  </ul>
                </div>
                <div v-else class="users-list">
                  <ul class="contacs">
                    <router-link
                      v-for="user in users"
                      :key="user.name"
                      :to="'/chat/profile/' + user.id"
                    >
                      <li :class="{ active: this.$route.params.id == user.id }">
                        <div class="d-flex bd-highlight">
                          <div class="user_info">
                            <span>{{ user.name }}</span>
                            <p>
                              {{ getMessagePreview(user) }}
                            </p>
                          </div>
                        </div>
                      </li>
                    </router-link>
                  </ul>
                </div>
              </div>
            </div>
            <div class="card-footer"></div>
          </div>
        </div>
        <div class="col-md-8 col-xl-6 chat">
          <div class="card">
            <router-view></router-view>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import authService from "../services/AuthService";
import searchService from "../services/SearchService";
import userService from "../services/UserService";
import * as signalR from "@microsoft/signalr";

import relationService from "../services/RelationService";
import Modal from "../components/Modal/Modal.vue";

export default {
  name: "Home",
  data() {
    return {
      searchInput: "",
      searchBlock: "Friends",
      modalOpen: false,
    };
  },

  components: {
    Modal,
  },

  computed: {
    loading() {
      return searchService.isLoading();
    },
    search() {
      return searchService.getSearchStatus();
    },
    searchingUsersList() {
      return searchService.getMixedUser(this.searchInput);
    },
    users() {
      return userService.getAll();
    },
    currenUserName() {
      return this.$store.state.account.name;
    },
  },

  methods: {
    getMessagePreview(card) {
      const lastMessage = card.messages[card.messages.length - 1];
      if (!lastMessage) return "";

      return lastMessage.text;
    },
    closeModalHandler() {
      this.modalOpen = false;
    },
    openModalHandler() {
      this.modalOpen = true;
    },
    logOutHandler() {
      authService.logOut();
    },

    settingsClickHanlder() {},
  },

  created() {
    const con = new signalR.HubConnectionBuilder()
      .withUrl("/realTime", {
        accessTokenFactory: () => authService.getToken(),
      })
      .build();

    con.on("ReceiveOffer", (data) => {
      const user = data;
      relationService.recieveOffer(user);
    });

    con.on("recieveMessage", (message) => {
      this.$store.dispatch("user/recieveMessage", message);
    });

    con.on("AcceptOffer", (user) => {
      relationService.reactToAccepting(user, this.$route.params.id);
    });

    con.start();
  },

  watch: {
    searchInput() {
      const searchInput = this.searchInput;
      this.searchBlock = "Search";

      this.$store.dispatch("search/startSearching");

      if (searchInput == "") {
        this.searchBlock = "Friends";
        this.$store.dispatch("search/searchUsers", []);
        this.$store.dispatch("search/stopSearching");
        return;
      }

      searchService.searchUsers(searchInput);
    },
  },
};
</script>

<style scoped>
.messanger-bottom {
  padding: 30px;
}

.user-search {
  margin-bottom: 20px;
}

.messanger-holder {
  min-height: 100vh;
}

.messanger-box {
  border: 1px solid #dbdbdb;
  padding: 10px 0;
  border-radius: 20px;
  height: 80vh;
  max-width: 1300px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
}

.messanger-bar {
  text-align: right;
  padding-right: 20px;
}

.logout {
  color: #fff;
}

.messanger-row {
  display: flex;
  flex-grow: 1;
}

.messanger-nav {
  width: 300px;
  flex-shrink: 0;
  padding: 20px;
  border-right: 1px solid #dbdbdb;
}

.messanger-core {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  flex-grow: 1;
}

.media-content .title {
  margin-bottom: 8px;
}

.messanger-holder {
  background: linear-gradient(to right, #91eae4, #86a8e7, #7f7fd5);
}

/*  */

body,
html {
  height: 100%;
  margin: 0;
  background: #7f7fd5;
  background: -webkit-linear-gradient(to right, #91eae4, #86a8e7, #7f7fd5);
  background: linear-gradient(to right, #91eae4, #86a8e7, #7f7fd5);
}

.chat {
  margin-top: auto;
  margin-bottom: auto;
}
.card {
  height: 500px;
  border-radius: 15px !important;
  background-color: rgba(0, 0, 0, 0.4) !important;
}
.contacts_body {
  padding: 0.75rem 0 !important;
  overflow-y: auto;
  white-space: nowrap;
}
.msg_card_body {
  overflow-y: auto;
  display: flex;
}
.card-header {
  border-radius: 15px 15px 0 0 !important;
  border-bottom: 0 !important;
}

.container {
  align-content: center;
}
.search {
  border-radius: 15px 0 0 15px !important;
  background-color: rgba(0, 0, 0, 0.3) !important;
  border: 0 !important;
  color: white !important;
}
.search:focus {
  box-shadow: none !important;
  outline: 0px !important;
}

.attach_btn {
  border-radius: 15px 0 0 15px !important;
  background-color: rgba(0, 0, 0, 0.3) !important;
  border: 0 !important;
  color: white !important;
  cursor: pointer;
}

.search_btn {
  border-radius: 0 15px 15px 0 !important;
  background-color: rgba(0, 0, 0, 0.3) !important;
  border: 0 !important;
  color: white !important;
  cursor: pointer;
}
.contacts {
  list-style: none;
  padding: 0;
}
.contacts li {
  width: 100% !important;
  padding: 5px 10px;
  margin-bottom: 15px !important;
}
.active {
  background-color: rgba(0, 0, 0, 0.3);
}
.user_img {
  height: 70px;
  width: 70px;
  border: 1.5px solid #f5f6fa;
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
.user_info span {
  font-size: 20px;
  color: white;
}
.user_info p {
  font-size: 10px;
  color: rgba(255, 255, 255, 0.6);
  text-align: left;
}
.video_cam {
  margin-left: 50px;
  margin-top: 5px;
}
.video_cam span {
  color: white;
  font-size: 20px;
  cursor: pointer;
  margin-right: 20px;
}

#action_menu_btn {
  position: absolute;
  right: 10px;
  top: 10px;
  color: white;
  cursor: pointer;
  font-size: 20px;
}
.action_menu {
  z-index: 1;
  position: absolute;
  padding: 15px 0;
  background-color: rgba(0, 0, 0, 0.5);
  color: white;
  border-radius: 15px;
  top: 30px;
  right: 15px;
  display: none;
}
.action_menu ul {
  list-style: none;
  padding: 0;
  margin: 0;
}
.action_menu ul li {
  width: 100%;
  padding: 10px 15px;
  margin-bottom: 5px;
}
.action_menu ul li i {
  padding-right: 10px;
}
.action_menu ul li:hover {
  cursor: pointer;
  background-color: rgba(0, 0, 0, 0.2);
}
.settings-icon {
  display: flex;
  align-items: center;
  margin-right: 30px;
  cursor: pointer;
}
.contacs a:hover {
  text-decoration: none;
}
@media (max-width: 576px) {
  .contacts_card {
    margin-bottom: 15px !important;
  }
}
</style>
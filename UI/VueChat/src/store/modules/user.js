const userModule = {
  namespaced: true,
  state() {
    return {
      users: [],
    };
  },

  mutations: {

    addMessageToUser(state, message) {
      
      let user = state.users.find((user)=> {

        if(user.id == message.fromId) return user;

      });

      user.messages.push(message);
      
    },
    add(state, user) {
      state.users.push(user);
    },

    update(state, updatedUser) {

      let index = state.users.forEach((element, i) => {
        if (element.id == updatedUser.id) {
          return i;
        }
      });

      if (index != undefined) {
        state.users[index] = updatedUser;
      }

    },

    remove(state, id) {
      for (var i = 0; i < state.users.length; i++) {
        if (state.users[i].id === id) {
          state.users.splice(i, 1);
          break;
        }
      }
    },

    addMessage(state, data) {
      const user = state.users.find(function (element) {
        if (element.id == data.id) return true;
      });

      user.messages.push(data.message);
      user.messagePreview = data.message.text;
    },

    replace(state, users) {
      state.users = users;
    },

    replaceAll(state, users) {
      state.users = users;
    },
  },

  getters: {
    get: (state) => (id) => {
      const user = state.users.find(function (element) {
        if (element.id == id) return true;
      });

      return user;
    },

    getIndex: (state) => (id) => {
      const user = state.users.find(function (element) {
        if (element.id == id) return true;
      });

      return user;
    },

    getAll(state) {
      return state.users;
    },

    getAllByLetter: (state) => (letter) => {
      return state.users.filter(function (element) {
        if (element.name.includes(letter)) return true;
      });
    },
  },

  actions: {
    recieveOffer(context, data) {
      context.commit("add", data);
    },

    removeOffer(context, id) {
      let user = this.getters.getUser(id);
      user.status = "Nothing";
      user.relationInitiator = "Stranger";

      context.commit("update", user);
    },

    declineOffer(context, id) {
      let user = this.getters.getUser(id);
      user.status = "Nothing";
      user.relationInitiator = "Stranger";

      context.commit("update", user);
    },

    addMessageToUser(context, data) {
      context.commit("addMessage", data);
    },

    acceptOffer(context, id) {
      const user = this.getters["user/get"](id);

      user.status = "Accepted";

      context.commit("update", user);
    },

    reactToAccepting(context, user) {
      user.status = "Accepted";
      context.commit("add", user);
    },

    remove(context, id) {
      context.commit("remove", id);
    },

    add(context, data) {
      context.commit("add", data);
    },

    update(context, data) {
      context.commit("update", data);
    },
    replace(context, data) {
      context.commit("replace", data);
    },

    initiateAllInterlocutorsData(context, users){
      context.commit("replaceAll", users);
    },

    recieveMessage(context, message){
      context.commit("addMessageToUser", message);
    }
  },
};

export default userModule;

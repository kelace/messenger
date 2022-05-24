const dialogModule = {
  namespaced: true,
  state() {
    return {
      dialogUser: {},
    };
  },

  mutations: {
    change(state, user) {
      state.dialogUser = user;
    },
  },

  getters: {
    getMessages: (state) => {
      return state.dialogUser.messages;
    },
    get: (state) => {
      const user = state.dialogUser;

      if (user.name == undefined) return undefined;

      return user;
    },
  },

  actions: {
    acceptOffer(context) {
      const user = this.getters["dialog/get"];

      user.status = "Accepted";

      context.commit("change", user);
    },

    changeDialogUser(context, user) {
      context.commit("change", user);
    },

    declineOffer(context) {
      const user = this.getters["dialog/get"];
      user.status = "Nothing";
      user.isThisUserInitiatorOfRelation = false;
      context.commit("change", user);
    },

    removeOffer(context) {
      const user = this.getters["dialog/get"];
      user.status = "Nothing";
      user.isThisUserInitiatorOfRelation = false;
      context.commit("change", user);
    },

    sendOffer(context) {
      const user = this.getters["dialog/get"];
      user.status = "Sended";
      user.isThisUserInitiatorOfRelation = false;
      context.commit("change", user);
    },
  },
};

export default dialogModule;

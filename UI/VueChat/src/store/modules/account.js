import httpClient from "../../HttpClient/HttpClient";

const accountModule = {
  namespaced: true,
  state() {
    return {
      currentUser: {
        name: "",
        id: "",
        photo: "",
      },
    };
  },

  mutations: {
    change(state, user) {
      state.currentUser = user;
    },
  },

  getters: {
    get: (state) => {
      const user = state.currentUser;
      return user;
    },
  },

  actions: {
    async queryAppData(context, dialogId) {
      const result = await httpClient.send({
        url: "/Account/StartPoint",
        method: "post",
        data: {
          dialogId: dialogId,
        },
      });

      if (result.status == 200) {
        const user = {
          name: result.data.name,
          id: result.data.id,
          photo: result.data.photo,
        };

        context.commit("change", user);
        this.dispatch("user/initiateAllInterlocutorsData", result.data.users);
      }
    },

    initiateAppData(context, data) {
      const user = {
        name: data.name,
        id: data.id,
        photo: data.photo,
      };

      context.commit("change", user);
      this.dispatch("user/initiateAllInterlocutorsData", data.users);
    },

    changePhoto(context, path) {
      const user = this.getters["account/get"];
      user.photo = path;

      context.commit("change", user);
    },
  },
};

export default accountModule;

const searchModule = {
  namespaced: true,
  state() {
    return {
      isSearching: false,
      isLoading: false,
      searchingUsersList: [],
    };
  },

  mutations: {
    add(state, user) {
      state.searchingUsersList.push(user);
    },

    update(state, updatedUser) {
      let user = state.searchingUsersList4.forEach((element) => {
        if (element.id == updatedUser.id) {
          return element;
        }
      });

      if (user != undefined) {
        user = updatedUser;
      }
    },

    replace(state, users) {
      state.searchingUsersList = users;
    },

    changeLoadingStatus(state, status) {
      state.isLoading = status;
    },
  },

  getters: {
    get: (state) => (id) => {
      const user = state.searchingUsersList.find(function (element) {
        if (element.id == id) return true;
      });

      return user;
    },

    getAll(state) {
      return state.users;
    },
  },

  actions: {
    startSearching(context) {
      context.commit("changeLoadingStatus", true);
    },

    stopSearching(context) {
      context.commit("changeLoadingStatus", false);
    },

    add(context, data) {
      context.commit("add", data);
    },

    update(context, data) {
      context.commit("update", data);
    },

    searchUsers(context, data) {
      context.commit("replace", data);
    },
  },
};

export default searchModule;

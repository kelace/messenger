import Service from "./Base/Service";
import axios from "axios";
import serviceAuth from "./AuthService";
import userService from "./UserService";

class SearchService extends Service {
  constructor() {
    super();
  }

  searchUsers(searchInput) {
    axios({
      method: "get",
      url: "/User/SearchUser",
      params: { letter: searchInput },
      headers: {
        Authorization: serviceAuth.prepareToken(),
      },
    }).then((response) => {

      this.store.dispatch("search/searchUsers", response.data);
      this.store.dispatch("search/stopSearching");
    });
  }

  getSearchStatus() {
    return this.store.state.search.getSearchStatus();
  }

  getSearchedUserd() {
    return this.store.state.search.searchingUsersList;
  }

  getMixedUser(letter) {
    const searchedUsers = this.getSearchedUserd();
    const users = userService.getAllByLetter(letter);

    let accetedUsers = [];
    let searchedUser = [];

    searchedUsers.forEach(function (user) {
      if (user.status == "Accepted") {
        let id = user.id;
        let acceptedUser = users.find(function (el) {
          if (el.id == id) return true;
        });

        accetedUsers.push(acceptedUser);
        return;
      }

      searchedUser.push(user);
    });

    let concatedUsers = accetedUsers.concat(searchedUser);

    return concatedUsers;
  }
  isLoading() {
    return this.store.state.search.isLoading;
  }
}

export default new SearchService();

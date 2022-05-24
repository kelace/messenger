import Service from "./Base/Service";
import axios from "axios";
import serviceAuth from "./AuthService";
import userService from "./UserService";

class RelationService extends Service {
  constructor() {
    super();
  }

  acceptOffer(id) {
    axios({
      method: "post",
      url: "/Relation/Accept",
      params: { Id: id },
      headers: {
        Authorization: serviceAuth.prepareToken(),
      },
    }).then(() => {
      this.store.dispatch("user/acceptOffer", id);
      this.store.dispatch("dialog/acceptOffer");
    });
  }

  reactToAccepting(user) {
    this.store.dispatch("dialog/acceptOffer");
    this.store.dispatch("user/reactToAccepting", user);
  }

  sendOffer(id) {
    axios({
      method: "post",
      url: "/Relation/CreateOffer",
      params: { Id: id },
      headers: {
        Authorization: serviceAuth.prepareToken(),
      },
    }).then(() => {
      this.store.dispatch("dialog/sendOffer", id);
    });
  }

  declineOffer(id) {
    axios({
      method: "post",
      url: "/Relation/Decline",
      params: { Id: id },
      headers: {
        Authorization: serviceAuth.prepareToken(),
      },
    }).then(() => {
        this.store.dispatch("user/remove", id);
        this.store.dispatch("dialog/declineOffer");
    });
  }

  removeOffer(id) {
    axios({
      method: "post",
      url: "/Relation/RemoveOffer",
      params: { Id: id },
      headers: {
        Authorization: serviceAuth.prepareToken(),
      },
    }).then(() => {
        this.store.dispatch("dialog/removeOffer", id);
    });
  }

  recieveOffer(user) {
    userService.add(user);
  }
}

export default new RelationService();

import axios from "axios";
import serviceAuth from "../services/AuthService";
import Message from "../Entities/Message";
import Service from "./Base/Service";

class MessageService extends Service {
  constructor() {
    super();
  }

  send(id, text) {
    const currentUser = this.store.getters["account/get"];
    axios({
      method: "post",
      url: "/Message/CreateMessage",
      data: {
        text: text,
        toId: id,
      },
      headers: {
        Authorization: serviceAuth.prepareToken(),
      },
    }).then(() => {

      const message = new Message(currentUser.id, id, text);

      this.store.dispatch("user/addMessageToUser", {
        message: message,
        id: id,
      });
    });
  }
}

export default new MessageService();

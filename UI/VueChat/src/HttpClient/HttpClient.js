import axios from "axios";
import authService from "../services/AuthService";
import router from "../router/index";

class HttpClient {
  constructor() {
    this.axios = axios;
  }

  async send(params) {
    const token = authService.prepareToken();

    params.headers = {
      Authorization: token,
    };

    let result;

    try {

      result = await this.axios(params);

    } catch (error) {

      router.push("/auth");

      return null;
      
    }

    return result;

  }
}

export default new HttpClient();

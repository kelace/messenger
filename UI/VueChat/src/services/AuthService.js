import axios from "axios";
import router from "../router";

class AuthServices {
  isAuthenticated() {
    const token = localStorage.getItem("token");

    if (token != undefined && token != null && token.length != 0) {
      return true;
    }

    return false;
  }

  logIn(name, password) {
    return new Promise((resolve, reject) => {
      axios({
        method: "post",
        url: "/Account/Login",
        data: {
          Name: name,
          Password: password,
        },
        headers: {
          "Content-Type": "application/json",
        },
      })
        .then((response) => {
          const data = response.data;
          const token = data.token;
          if (data != null) {
            this.setToken(token);
            resolve(data);
          }
        })
        .catch((err) => {
          reject(err);
        });
    });
  }

  signUp(name, password, confirmPassword) {
    return new Promise((resolve, reject) => {
      axios({
        method: "post",
        url: "/Account/Register",
        data: {
          Name: name,
          Password: password,
          PasswordConfirm: confirmPassword,
        },
        headers: {
          "Content-Type": "application/json",
        },
      })
        .then((response) => {
          const data = response.data;

          if (data != null) {
            this.setToken(data.token);
            resolve(data);
            router.push("/chat");
          }
        })
        .catch((err) => {
          reject(err);
        });
    });
  }

  logOut() {
    axios({
      method: "post",
      url: "/Account/LogOut",
      headers: {
        "Content-Type": "application/json",
        Authorization: this.prepareToken(),
      },
    })
      .then(() => {
        localStorage.setItem("token", "");
        router.push("/auth");
      })
      .catch(function () {
      });
  }

  setToken(token) {
    localStorage.setItem("token", token);
  }

  getToken() {
    return localStorage.getItem("token");
  }

  prepareToken() {
    return `Bearer ${localStorage.getItem("token")}`;
  }
}

export default new AuthServices();

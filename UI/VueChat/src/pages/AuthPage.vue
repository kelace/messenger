<template>
  <div class="wrapper fadeInDown">
    <div id="formContent">
      <!-- Tabs Titles -->
      <h2
        class="underlineHover tab"
        :class="{
          active: formActive == 'login',
          inactive: formActive != 'login',
        }"
        @click="changeTabBlock('login')"
      >
        Sign In
      </h2>
      <h2
        class="inactive underlineHover tab"
        :class="{
          active: formActive == 'signup',
          inactive: formActive != 'signup',
        }"
        @click="changeTabBlock('signup')"
      >
        Sign Up
      </h2>

      <div class="fadeIn first">
        <img src="../assets/programmer.png" id="icon" alt="User Icon" />
      </div>
      <div class="form-block-content">
        <form v-if="formActive == 'login'" @submit="loginSubmitHandler">
          <div class="form-line">
            <input
              class="input"
              :class="{ 'is-danger': loginForm.errors.name }"
              type="text"
              placeholder="Name"
              name="name"
              v-model="loginForm.inputs.name"
            />
            <p v-if="loginForm.errors.name != null" class="help is-danger">
              {{ loginForm.errors.name }}
            </p>
          </div>
          <div class="form-line">
            <input
              class="input"
              :class="{ 'is-danger': loginForm.errors.password }"
              type="Password"
              placeholder="Password"
              name="password"
              v-model="loginForm.inputs.password"
            />
            <p v-if="loginForm.errors.password != null" class="help is-danger">
              {{ loginForm.errors.password }}
            </p>
          </div>
          <div class="form-line">
            <input type="submit" class="" value="Log In" />
          </div>
        </form>
        <form v-else @submit="registerSubmitHandler">
          <div class="form-line">
            <input
              class="input"
              :class="{ 'is-danger': registerForm.errors.name }"
              type="text"
              placeholder="Name"
              v-model="registerForm.inputs.name"
            />
            <p v-if="registerForm.errors.name != null" class="help is-danger">
              {{ registerForm.errors.name }}
            </p>
          </div>
          <div class="form-line">
            <input
              class="input"
              :class="{ 'is-danger': registerForm.errors.password }"
              type="Password"
              placeholder="Password"
              v-model="registerForm.inputs.password"
            />
            <p
              v-if="registerForm.errors.password != null"
              class="help is-danger"
            >
              {{ registerForm.errors.password }}
            </p>
          </div>
          <div class="form-line">
            <input
              type="Password"
              class="input"
              :class="{ 'is-danger': registerForm.errors.confirmPassword }"
              placeholder="Confirm Password"
              v-model="registerForm.inputs.confirmPassword"
            />
            <p
              v-if="registerForm.errors.confirmPassword != null"
              class="help is-danger"
            >
              {{ registerForm.errors.confirmPassword }}
            </p>
          </div>
          <div class="form-line">
            <input type="submit" class="" value="Log In" />
          </div>
        </form>
      </div>

      <div id="formFooter">
        <!-- <a class="underlineHover" href="#">Forgot Password?</a> -->
      </div>
    </div>
  </div>
</template>

<script>
import authService from "../services/AuthService";

import router from "../router/index";

export default {
  name: "AuthPage",
  data() {
    return {
      formActive: "login",
      loginForm: {
        inputs: {
          name: "",
          password: "",
        },
        errors: {
          name: null,
          password: null,
          count: 0,
        },
      },
      registerForm: {
        inputs: {
          name: "",
          password: "",
          confirmPassword: "",
        },
        errors: {
          name: null,
          password: null,
          confirmPassword: null,
          count: 0,
        },
      },
    };
  },

  methods: {
    changeTabBlock(tab) {
      this.formActive = tab;
      this.resetErrors();
    },

    loginSubmitHandler(e) {
      e.preventDefault();
      const name = this.loginForm.inputs.name;
      const password = this.loginForm.inputs.password;

      if (name == "") {
        this.loginForm.errors.name = "Name shouldnt be empty";
        this.loginForm.errors.count += 1;
      }

      if (password == "") {
        this.loginForm.errors.password = "Password field shouldnt be empty";
        this.loginForm.errors.count += 1;
      }

      authService.logIn(name, password).then((response) => {
        this.$store.dispatch("account/initiateAppData", response);
        router.push("/chat");
      });
    },

    resetErrors() {
      this.registerForm.errors.name = null;
      this.registerForm.errors.password = null;
      this.registerForm.errors.confirmPassword = null;
      this.registerForm.errors.count = 0;
    },

    async registerSubmitHandler(e) {
      e.preventDefault();
      const name = this.registerForm.inputs.name;
      const password = this.registerForm.inputs.password;
      const confirmPassword = this.registerForm.inputs.confirmPassword;
      this.resetErrors();

      if (name == "") {
        this.registerForm.errors.name = "Name shouldnt be empty";
        this.registerForm.errors.count += 1;
      }

      if (password == "") {
        this.registerForm.errors.password = "Password field shouldnt be empty";
        this.registerForm.errors.count += 1;
      }

      if (confirmPassword == "") {
        this.registerForm.errors.confirmPassword =
          "Password confirm field shouldnt be empty";
        this.registerForm.errors.count += 1;
      }

      if (password != confirmPassword) {
        this.registerForm.errors.password =
          "Password and confirm password fields should be equal";
        this.registerForm.errors.confirmPassword =
          "Password and confirm password fields should be equal";
        this.registerForm.errors.count += 1;
      }

      if (this.registerForm.errors.count > 0) {
        return;
      }

      const result = await authService.signUp(name, password, confirmPassword);

      await this.$store.dispatch("account/initiateAppData", result);
    },
  },
};
</script>


<style scoped>
.auth-page {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.auth-form-active {
  display: block;
}

.formBlockTabtActive {
  background: gray !important;
  color: #fff !important;
}

.form-block-tab {
  display: flex;
}

.form-line {
  margin-bottom: 20px;
}

a {
  color: #92badd;
  display: inline-block;
  text-decoration: none;
  font-weight: 400;
}

h2 {
  text-align: center;
  font-size: 16px;
  font-weight: 600;
  text-transform: uppercase;
  display: inline-block;
  margin: 40px 8px 10px 8px;
  color: #cccccc;
}

/* STRUCTURE */

.wrapper {
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
  width: 100%;
  min-height: 100vh;
  padding: 20px;
}

#formContent {
  -webkit-border-radius: 10px 10px 10px 10px;
  border-radius: 10px 10px 10px 10px;
  background: #fff;
  padding: 30px;
  width: 90%;
  max-width: 450px;
  position: relative;
  padding: 0px;
  -webkit-box-shadow: 0 30px 60px 0 rgba(0, 0, 0, 0.3);
  box-shadow: 0 30px 60px 0 rgba(0, 0, 0, 0.3);
  text-align: center;
}

#formFooter {
  background-color: #f6f6f6;
  border-top: 1px solid #dce8f1;
  padding: 25px;
  text-align: center;
  -webkit-border-radius: 0 0 10px 10px;
  border-radius: 0 0 10px 10px;
}

/* TABS */

h2.inactive {
  color: #cccccc;
}

h2.active {
  color: #0d0d0d;
  border-bottom: 2px solid #5fbae9;
}

/* FORM TYPOGRAPHY*/

input[type="button"],
input[type="submit"],
input[type="reset"] {
  background-color: #56baed;
  border: none;
  color: white;
  padding: 15px 80px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  text-transform: uppercase;
  font-size: 13px;
  -webkit-box-shadow: 0 10px 30px 0 rgba(95, 186, 233, 0.4);
  box-shadow: 0 10px 30px 0 rgba(95, 186, 233, 0.4);
  -webkit-border-radius: 5px 5px 5px 5px;
  border-radius: 5px 5px 5px 5px;
  margin: 5px 20px 40px 20px;
  -webkit-transition: all 0.3s ease-in-out;
  -moz-transition: all 0.3s ease-in-out;
  -ms-transition: all 0.3s ease-in-out;
  -o-transition: all 0.3s ease-in-out;
  transition: all 0.3s ease-in-out;
}

input[type="submit"] {
  cursor: pointer;
}

input[type="button"]:hover,
input[type="submit"]:hover,
input[type="reset"]:hover {
  background-color: #39ace7;
}

input[type="button"]:active,
input[type="submit"]:active,
input[type="reset"]:active {
  -moz-transform: scale(0.95);
  -webkit-transform: scale(0.95);
  -o-transform: scale(0.95);
  -ms-transform: scale(0.95);
  transform: scale(0.95);
}

input {
  background-color: #f6f6f6;
  border: none;
  color: #0d0d0d;
  padding: 15px 32px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 5px;
  width: 85%;
  border: 2px solid #f6f6f6;
  -webkit-transition: all 0.5s ease-in-out;
  -moz-transition: all 0.5s ease-in-out;
  -ms-transition: all 0.5s ease-in-out;
  -o-transition: all 0.5s ease-in-out;
  transition: all 0.5s ease-in-out;
  -webkit-border-radius: 5px 5px 5px 5px;
  border-radius: 5px 5px 5px 5px;
}

/* ANIMATIONS */

/* Simple CSS3 Fade-in-down Animation */
.fadeInDown {
  -webkit-animation-name: fadeInDown;
  animation-name: fadeInDown;
  -webkit-animation-duration: 1s;
  animation-duration: 1s;
  -webkit-animation-fill-mode: both;
  animation-fill-mode: both;
}

@-webkit-keyframes fadeInDown {
  0% {
    opacity: 0;
    -webkit-transform: translate3d(0, -100%, 0);
    transform: translate3d(0, -100%, 0);
  }
  100% {
    opacity: 1;
    -webkit-transform: none;
    transform: none;
  }
}

@keyframes fadeInDown {
  0% {
    opacity: 0;
    -webkit-transform: translate3d(0, -100%, 0);
    transform: translate3d(0, -100%, 0);
  }
  100% {
    opacity: 1;
    -webkit-transform: none;
    transform: none;
  }
}

/* Simple CSS3 Fade-in Animation */
@-webkit-keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@-moz-keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

.fadeIn {
  opacity: 0;
  -webkit-animation: fadeIn ease-in 1;
  -moz-animation: fadeIn ease-in 1;
  animation: fadeIn ease-in 1;

  -webkit-animation-fill-mode: forwards;
  -moz-animation-fill-mode: forwards;
  animation-fill-mode: forwards;

  -webkit-animation-duration: 1s;
  -moz-animation-duration: 1s;
  animation-duration: 1s;
}

.fadeIn.first {
  -webkit-animation-delay: 0.4s;
  -moz-animation-delay: 0.4s;
  animation-delay: 0.4s;
}

.fadeIn.second {
  -webkit-animation-delay: 0.6s;
  -moz-animation-delay: 0.6s;
  animation-delay: 0.6s;
}

.fadeIn.third {
  -webkit-animation-delay: 0.8s;
  -moz-animation-delay: 0.8s;
  animation-delay: 0.8s;
}

.fadeIn.fourth {
  -webkit-animation-delay: 1s;
  -moz-animation-delay: 1s;
  animation-delay: 1s;
}

/* Simple CSS3 Fade-in Animation */
.underlineHover:after {
  display: block;
  left: 0;
  bottom: -10px;
  width: 0;
  height: 2px;
  background-color: #56baed;
  content: "";
  transition: width 0.2s;
}

.underlineHover:hover {
  color: #0d0d0d;
}

.underlineHover:hover:after {
  width: 100%;
}

/* OTHERS */

*:focus {
  outline: none;
}

#icon {
  width: 30px;
}

* {
  box-sizing: border-box;
}

.tab {
  cursor: pointer;
}
</style>
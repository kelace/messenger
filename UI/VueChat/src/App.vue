<template>
  <div>
    <router-view v-if="isInitialFetchLoading" />
    <Loader v-else />
  </div>
</template>

<script>
import router from "./router/index";
import Loader from "./components/Loader/Loader.vue";
import httpClient from "./HttpClient/HttpClient";
export default {
  name: "App",
  components: {
    Loader,
  },
  data() {
    return {
      isInitialFetchLoading: false,
    };
  },

  async mounted() {
    const dialogId = this.$route.params.id;
    
    const result = await httpClient.send({
      url: "/Account/StartPoint",
      method: "post",
      data: {
        dialogId: dialogId,
      },
    });

    setTimeout(() => {
      this.isInitialFetchLoading = true;
    }, 2000);

    if (result) {
      this.$store.dispatch("account/initiateAppData", result.data);
      this.$store.dispatch("user/initiateAllInterlocutorsData", result.data.users);
      router.push("/chat");
    }
  },
};
</script>

<style>
@import "~bulma/css/bulma.css";
@import "~bootstrap/dist/css/bootstrap.css";

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>

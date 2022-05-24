<template>
  <div class="Empty">
    <div v-if="status == 'Nothing'">
      <div class="buttons">
        <button class="button is-primary" @click="offerButtonClickHandler">
          Отправить запрос
        </button>
      </div>
    </div>

    <div v-if="status == 'Sended'">
      <div v-if="belong == false">
        <div class="buttons">
          <button
            class="button is-danger"
            @click="removeOfferButtonClickHandler"
          >
            Удалить запрос
          </button>
        </div>
      </div>
      <div v-if="belong == true">
        <div class="buttons">
          <button
            class="button is-primary"
            @click="acceptOfferButtonClickHandler"
          >
            Принять запрос
          </button>
          <button
            class="button is-danger"
            @click="declineOfferButtonClickHandler"
          >
            Отказать
          </button>
        </div>
      </div>
    </div>

    <div v-if="status == 'Accepted'">
      <Main />
    </div>
  </div>
</template>

<script>
import Main from "./Main.vue";
import relationService from "../../services/RelationService";

export default {
  name: "Empty",
  props: ["belong", "status", "id"],
  data() {
    return {};
  },

  components: {
    Main,
  },

  methods: {
    offerButtonClickHandler() {
      relationService.sendOffer(this.id);
    },

    removeOfferButtonClickHandler() {
      relationService.removeOffer(this.id);
    },

    acceptOfferButtonClickHandler() {
      relationService.acceptOffer(this.id);
    },

    declineOfferButtonClickHandler() {
      relationService.declineOffer(this.id);
    },
  },
};
</script>

<style scoped>
.messanger-display {
  flex-grow: 1;
}

.Empty {
  height: 100%;
}

.Empty > div {
  height: 100%;
}

.Empty > div > div {
  display: flex;
  flex-direction: column;
  height: 100%;
}
</style>
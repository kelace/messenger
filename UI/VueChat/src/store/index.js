
import { createStore } from "vuex";
import userModule from "./modules/user";
import searchModule from "./modules/search";
import dialogModule from "./modules/dialog";
import accountModule from "./modules/account";


const store = createStore({
  modules:{
    user:userModule,
    search:searchModule,
    dialog:dialogModule,
    account:accountModule,
  },

});


export default store;

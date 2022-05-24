import { createRouter, createWebHistory } from "vue-router";
import Home from "../pages/Home.vue";
import AuthPage from "../pages/AuthPage.vue";
import Dialog from "../components/Dialog/Dialog.vue";


const routes = [
  {
    path: "/chat/",
    name: "Home",
    component: Home,
    children:[
      {
        path:"profile/:id",
        component: Dialog
      }
    ]
  },

  {
    path: "/auth",
    name: "AuthPage",
    component: AuthPage,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});


export default router;

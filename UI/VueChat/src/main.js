import { createApp } from 'vue';
import router from './router/index.js';
import store from './store/index';
import App from './App.vue';

const app = createApp(App).use(store).use(router).mount('#app');

export default app;

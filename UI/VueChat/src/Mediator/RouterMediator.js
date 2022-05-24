import router from '../router/index';

class RouterMediator {
  constructor() {
    this.router = router;
  }

  commit(event, url){
      if(event == 'redirect'){
        this.router.push(url)
      }
  }
}

export default new RouterMediator(router);

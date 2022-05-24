import Service from "./Base/Service";

 class UserService extends Service {
  constructor() {
    super();
  }

  get(id) {
    const user = this.store.getters['user/get'](id);
    const userDialog = this.store.getters['dialog/get'];
    const userSearched = this.store.getters['search/get'](id);

    if (user != undefined) return user;
    if (userDialog != undefined && userDialog.id == id) return userDialog;
    if (userSearched != undefined) return userSearched;

    throw "Пользователь не существует";
  }

  add(user){
    this.store.dispatch('user/add', user);
  }

  getDialogUser(){
    return this.store.getters['dialog/get'];
  }

  changeDialogUser(id){
    const user = this.get(id);

    this.store.dispatch('dialog/changeDialogUser', user);

    return user;
  }

  getAll() {
    return this.store.getters['user/getAll'];
  }

  getAllByLetter(letter){
    return this.store.getters['user/getAllByLetter'](letter);
  }

  changeAllUsers(users){
    this.store.dispatch('user/replace', users);
  }

  initiateAllInterlocutorsData(users){
    this.store.dispatch('user/replace', users);
  }

}


export default new UserService();
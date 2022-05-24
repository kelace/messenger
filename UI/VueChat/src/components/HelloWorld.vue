<template>
  <div class="auth-page">
    <div class="form-block">
      <div class="form-block-tab">
        <div>Log in</div>

        <div>Sign up</div>
      </div>
      <div class="form-block-content">
        <form v-if="formActive == 'login'" class="auth-form-login auth-form auth-form-active">
          <div class="form-line">
            <input type="text" />
          </div>
          <div class="form-line">
            <input type="text" />
          </div>
        </form>
        <form v-else class="auth-form-signup auth-form">
          <div class="form-line">
            <input type="text" />
          </div>
          <div class="form-line">
            <input type="text" />
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>

export default {
    name:"home",
    data(){
        return{
            formActive:"login"
        }
    }
}
</script>


<style scoped>
.auth-page {
  display: flex;
  align-items: center;
  justify-content: center;
}

.auth-form {
  display: none;
}

.auth-form-active {
  display: block;
}
</style>
<template>
  <div class="login">
    <h1>
      Sigma Booking cross-platform PowerShell supported booking module VERSION
      0.17.35.♂♂♂♂♂
    </h1>
    <label for="username">Username: </label>
    <input type="text" id="username" username="username" required size="10" />
    <br />
    <br />

    <label for="password">Password: </label>
    <input type="text" id="password" password="password" required size="10" />
    <br />

    <button class="tryLogin" type="button">Login</button>
  </div>
</template>

<script>
export default {
  name: "LoginView",
};
</script>

<style scoped></style>

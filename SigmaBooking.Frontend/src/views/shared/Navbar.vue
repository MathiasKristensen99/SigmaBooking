<template>
  <header>
    <div class="wrapper">
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
        <div class="container">
          <a class="navbar-brand" href="/home">
            <img class="logo" src="/src/assets/sigma-logo-without-bg.png" />
          </a>
          <button
            class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarToggler"
            aria-controls="navbarToggler"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarToggler">
            <ul class="navbar-nav ms-auto">
              <li class="nav-item">
                <a class="nav-link" href="/home">Home</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="/reservations">Reservations</a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<script>
export default {
  name: "Navbar",
};
</script>

<style scoped>
.wrapper {
  margin: 90px;
}
.logo {
  width: 153px;
  height: 63px;
  max-height: 63px;
  max-width: 153px;
}
</style>

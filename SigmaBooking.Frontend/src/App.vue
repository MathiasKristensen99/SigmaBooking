<script setup></script>

<template>
  <header>
    <div class="wrapper">
      <nav>
        <RouterLink to="/home">Home | </RouterLink>
        <RouterLink to="/about">About |</RouterLink>
        <RouterLink to="/login">Login</RouterLink>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style></style>

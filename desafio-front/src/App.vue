<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <div class="wrapper">
      <nav class="navbar" style="background-color: #63a553;">
        <div class="container-fluid justify-content-center">
          <a class="navbar-brand text-white" href="#">Desafio Vertical TI</a>
        </div>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>

</style>

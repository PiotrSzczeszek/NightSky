<script lang="ts">
  import AppHeader from "./components/Header.svelte";
  import LeftMenu from "./components/LeftMenu.svelte";

  import { AppContent, Scrim } from "@smui/drawer";
  import Button, { Label } from "@smui/button";
  import {
    isMobile,
    documentWidth,
    isLeftMenuOpen,
  } from "./stores/AppStateStore";

  import { _ } from "svelte-i18n";
  import { onMount } from "svelte";

  onMount(() => {
    isLeftMenuOpen.set(!$isMobile);
  });

  let active = "";
</script>

<svelte:head>
  <!-- Fonts -->
  <link
    rel="stylesheet"
    href="https://fonts.googleapis.com/icon?family=Material+Icons"
  />
  <link
    rel="stylesheet"
    href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,600,700"
  />

  <!-- Material Typography -->
  <link
    rel="stylesheet"
    href="https://unpkg.com/@material/typography@14.0.0/dist/mdc.typography.css"
  />

  <!-- SMUI -->
  <link rel="stylesheet" href="https://unpkg.com/svelte-material-ui/bare.css" />
  <link rel="stylesheet" href="../build/smui-dark.css" />
</svelte:head>

<svelte:window bind:innerWidth={$documentWidth} />

<div class="drawer-container">
  <LeftMenu />
  {#if $isMobile}
    <Scrim />
  {/if}

  <AppContent class="app-content">
    <main class="main-content">
      <AppHeader />
      <div class="flexy">
        <div class="top-app-bar-container flexor" />
      </div>
      <br />
      {$isLeftMenuOpen}
      <!-- {$isMobile} -->
    </main>
  </AppContent>
</div>

<style>
</style>

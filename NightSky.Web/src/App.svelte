<script lang="ts">
  import AppHeader from "./components/Header.svelte";
  import LeftMenu from "./components/LeftMenu.svelte";
  import { AppContent, Scrim } from "@smui/drawer";
  import Snackbar, { Actions, Label } from "@smui/snackbar";

  import { snackbars } from "./stores/AppStateStore";
  import IconButton from "@smui/icon-button";

  import {
    isMobile,
    documentWidth,
    isLeftMenuOpen,
  } from "./stores/AppStateStore";

  import { _ } from "svelte-i18n";
  import { onMount } from "svelte";
  import Kitchen from "@smui/snackbar/kitchen";

  import { currentTarget, currentTargetType } from "./stores/TargetsStore";

  onMount(() => {
    isLeftMenuOpen.set(!$isMobile);
  });

  // snackbars.subscribe(() => {
  //   if (!$snackbars.length) return;
  //   setTimeout(() => {
  //     console.log("splice");
  //     snackbars.update((x) => {
  //       if (!x.length) return;
  //       x.splice(0, 1);
  //       return x;
  //     });
  //   }, 5 * 1000);
  // });
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
  <link rel="stylesheet" href="smui-dark.css" />
</svelte:head>

<svelte:window bind:innerWidth={$documentWidth} />

<div class="drawer-container h-100">
  <LeftMenu />
  {#if $isMobile}
    <Scrim />
  {/if}

  <AppContent class="app-content h-100">
    <main class="main-content h-100">
      <AppHeader />
      <div style="flex-grow: 1">
        {#key $currentTargetType}
          <svelte:component this={$currentTarget} />
        {/key}
      </div>
    </main>
  </AppContent>
</div>

<Kitchen bind:this={$snackbars} dismiss$class="material-icons" />

<style>
  :global(.h-100) {
    height: 100% !important;
  }
  .main-content {
    display: flex;
    flex-direction: column;
  }
</style>

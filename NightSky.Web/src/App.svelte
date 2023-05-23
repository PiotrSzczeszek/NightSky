<script lang="ts">
  import AppHeader from "./components/Header.svelte";
  import LeftMenu from "./components/LeftMenu.svelte";

  import { AppContent, Scrim } from "@smui/drawer";
  import Button, { Label } from "@smui/button";

  import { _ } from "svelte-i18n";
  let open = false;
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

<div class="drawer-container">
  <LeftMenu bind:open />
  <Scrim />

  <AppContent class="app-content">
    <main class="main-content">
      <div class="flexy">
        <div class="top-app-bar-container flexor">
          <AppHeader bind:open />
        </div>
      </div>
      <Button on:click={() => (open = !open)}>
        <Label>Toggle Drawer</Label>
      </Button>
      <br />
      <p class="status">Active: {active}</p>
    </main>
  </AppContent>
</div>

<style>
  * :global(.shaped-outlined),
  * :global(.shaped-outlined .mdc-select__anchor) {
    border-radius: 28px;
  }
  * :global(.shaped-outlined .mdc-text-field__input) {
    padding-left: 32px;
    padding-right: 0;
  }
  *
    :global(
      .shaped-outlined .mdc-notched-outline .mdc-notched-outline__leading
    ) {
    border-radius: 28px 0 0 28px;
    width: 28px;
  }
  *
    :global(
      .shaped-outlined .mdc-notched-outline .mdc-notched-outline__trailing
    ) {
    border-radius: 0 28px 28px 0;
  }
  * :global(.shaped-outlined .mdc-notched-outline .mdc-notched-outline__notch) {
    max-width: calc(100% - 28px * 2);
  }
  *
    :global(
      .shaped-outlined.mdc-select--with-leading-icon
        .mdc-notched-outline:not(.mdc-notched-outline--notched)
        .mdc-floating-label
    ) {
    left: 16px;
  }
</style>

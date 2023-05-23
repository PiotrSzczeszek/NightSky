<script lang="ts">
  import TopAppBar, { Row, Section, Title } from "@smui/top-app-bar";
  import IconButton from "@smui/icon-button";
  import Drawer, {
    AppContent,
    Content,
    Header,
    Subtitle,
    Scrim,
  } from "@smui/drawer";
  import Button, { Label } from "@smui/button";
  import List, { Item, Text, Graphic, Separator, Subheader } from "@smui/list";
  import SegmentedButton, { Segment } from "@smui/segmented-button";

  import { _, locale, locales } from "svelte-i18n";

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
  <Drawer variant="modal" bind:open>
    <Header>
      <Title>Super Mail</Title>
      <Subtitle>It's the best fake mail app drawer.</Subtitle>
    </Header>
    <Content>
      <List>
        <Item href="javascript:void(0)" activated={active === "Inbox"}>
          <Graphic class="material-icons" aria-hidden="true">inbox</Graphic>
          <Text>Inbox</Text>
        </Item>
      </List>
    </Content>
  </Drawer>

  <Scrim />

  <AppContent class="app-content">
    <main class="main-content">
      <div class="flexy">
        <div class="top-app-bar-container flexor">
          <TopAppBar variant="short" dense={true} color="secondary">
            <Row>
              <Section>
                <IconButton
                  class="material-icons"
                  on:click={() => (open = !open)}>menu</IconButton
                >
                <Title>{$_("page.title")}</Title>
              </Section>
              {$locale}
              <Section align="end" toolbar>
                <SegmentedButton
                  segments={$locales}
                  color="secondary"
                  let:segment
                  singleSelect
                  bind:selected={$locale}
                >
                  <!-- Note: the `segment` property is required! -->
                  <Segment {segment}>
                    <Label>{segment}</Label>
                  </Segment>
                </SegmentedButton>
              </Section>
            </Row>
          </TopAppBar>
        </div>
      </div>
      <Button on:click={() => (open = !open)}>
        <Label>Toggle Drawer</Label></Button
      >
      <br />
      <pre class="status">Active: {active}</pre>
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

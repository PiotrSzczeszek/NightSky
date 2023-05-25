<script lang="ts">
  import { Title } from "@smui/top-app-bar";
  import Drawer, { Content, Subtitle, Header } from "@smui/drawer";
  import List, { Separator, Item, Text, Graphic } from "@smui/list";

  import { _ } from "svelte-i18n";

  import {
    targetAssociations,
    currentTargetType,
  } from "../stores/TargetsStore";

  import { isMobile, isLeftMenuOpen } from "../stores/AppStateStore";
</script>

{#key $isMobile}
  <Drawer
    variant={$isMobile ? "modal" : "dismissible"}
    bind:open={$isLeftMenuOpen}
  >
    <Header style="padding-top: 1rem">
      <Title style="padding-left: 0">
        {$_("leftMenu.title")}
      </Title>
      <Subtitle>
        {$_("leftMenu.subtitle")}
      </Subtitle>
    </Header>
    <Separator />
    <Content>
      <List>
        {#each targetAssociations as target}
          <Item
            href="javascript:void(0)"
            activated={$currentTargetType == target.type}
            style="color: white"
            on:click={() => ($currentTargetType = target.type)}
          >
            <Text>{$_(`leftMenu.option.${target.type}`)}</Text>
          </Item>
        {/each}
      </List>
    </Content>
  </Drawer>
{/key}

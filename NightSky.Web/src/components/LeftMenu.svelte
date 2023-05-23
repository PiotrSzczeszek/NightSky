<script lang="ts">
  import { Title } from "@smui/top-app-bar";
  import Drawer, { Content, Subtitle, Header } from "@smui/drawer";
  import List, { Item, Text, Graphic } from "@smui/list";

  import { isMobile, isLeftMenuOpen } from "../stores/AppStateStore";
</script>

{#key $isMobile}
  <Drawer
    variant={$isMobile ? "modal" : "dismissible"}
    bind:open={$isLeftMenuOpen}
  >
    <Header>
      <Title>Super Mail</Title>
      <Subtitle>It's the best fake mail app drawer.</Subtitle>
    </Header>
    <Content>
      <List>
        <Item href="javascript:void(0)">
          <Graphic class="material-icons" aria-hidden="true">inbox</Graphic>
          <Text>Inbox</Text>
        </Item>
      </List>
    </Content>
  </Drawer>
{/key}

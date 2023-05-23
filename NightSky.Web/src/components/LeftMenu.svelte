<script lang="ts">
    import { Title } from "@smui/top-app-bar";
    import Drawer, {
        AppContent,
        Content,
        Subtitle,
        Header,
        Scrim,
    } from "@smui/drawer";
    import List, { Item, Text, Graphic } from "@smui/list";

    export let open = false;
</script>

<Drawer variant="modal" bind:open>
    <Header>
        <Title>Super Mail</Title>
        <Subtitle>It's the best fake mail app drawer.</Subtitle>
    </Header>
    <Content>
        <List>
            <Item href="javascript:void(0)">
                <Graphic class="material-icons" aria-hidden="true"
                    >inbox</Graphic
                >
                <Text>Inbox</Text>
            </Item>
        </List>
    </Content>
</Drawer>

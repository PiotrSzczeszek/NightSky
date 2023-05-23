<script lang="ts">
    import TopAppBar, { Row, Section, Title } from "@smui/top-app-bar";
    import { Subtitle } from "@smui/drawer";
    import IconButton from "@smui/icon-button";
    import Menu from "@smui/menu";
    import List, { Item, Text, Subheader } from "@smui/list";

    import { _, locale, locales } from "svelte-i18n";
    let menu: Menu;
    let langMenuButton;
    export let open = false;
</script>

<TopAppBar variant="short" dense={true} color="primary">
    <Row>
        <Section>
            <IconButton class="material-icons" on:click={() => (open = !open)}
                >menu</IconButton
            >
            <Title>{$_("page.title")}</Title>
        </Section>
        <Section align="end">
            <div bind:this={langMenuButton}>
                <IconButton
                    class="material-icons"
                    size="button"
                    on:click={(e) => {
                        menu.setOpen(true);
                    }}
                >
                    flag
                </IconButton>
            </div>
            <Menu
                bind:this={menu}
                anchorElement={langMenuButton}
                anchorCorner="BOTTOM_LEFT"
            >
                <Subheader>
                    <Subtitle>{$_("page.selectLang")}</Subtitle>
                </Subheader>
                <List dense>
                    {#each $locales as lang}
                        <Item on:SMUI:action={() => locale.set(lang)}>
                            <Text>{lang}</Text>
                        </Item>
                    {/each}
                </List>
            </Menu>

            <!--  -->
        </Section>
    </Row>
</TopAppBar>

<script lang="ts">
  import DataTable, { Head, Body, Row, Cell } from "@smui/data-table";
  import Dialog, { Title, Content, Actions } from "@smui/dialog";
  import IconButton from "@smui/icon-button";
  import Button, { Icon, Label } from "@smui/button";
  import Tooltip, { Wrapper } from "@smui/tooltip";
  import FormField from "@smui/form-field";
  import Textfield from "@smui/textfield";

  import { _ } from "svelte-i18n";
  import { type StarDataModel } from "../classes/StarData";

  let rowData: Array<StarDataModel> = [
    {
      constellations: [],
      description: "",
      imageUrl: "",
      starId: 0,
      starName: "star",
    },
  ];

  let dialogOpen = false;

  function editElement(row) {
    console.log("edit", row);
  }

  function deleteElement(row) {
    console.log("delete", row);
  }

  function onFormClosed(event) {
    console.log(event);
  }
</script>

<DataTable table$aria-label="Stars list" style="width: 100%;">
  <Head>
    <Row>
      <Cell>{$_("star.table.headers.id")}</Cell>
      <Cell>{$_("star.table.headers.name")}</Cell>
      <Cell>{$_("star.table.headers.description")}</Cell>
      <Cell style="text-align: center">{$_("star.table.headers.image")}</Cell>
      <Cell style="text-align: center">{$_("star.table.headers.actions")}</Cell>
    </Row>
  </Head>
  <Body>
    {#each rowData as row}
      <Cell>{row.starId}</Cell>
      <Cell>{row.starName}</Cell>
      <Cell>{row.description}</Cell>
      <Cell style="text-align: center">
        {#if row.imageUrl}
          <img src={row.imageUrl} alt="Star" />
        {:else}
          <span>-</span>
        {/if}
      </Cell>
      <Cell style="text-align: center">
        <IconButton
          class="material-icons"
          size="button"
          on:click={() => editElement(row)}>edit</IconButton
        >
        <IconButton
          class="material-icons"
          size="button"
          on:click={() => deleteElement(row)}>delete</IconButton
        >
      </Cell>
    {/each}
    <Row />
  </Body>
</DataTable>
<Dialog
  bind:open={dialogOpen}
  aria-labelledby="event-title"
  aria-describedby="event-content"
  on:SMUIDialog:closed={onFormClosed}
>
  <Title id="event-title">{$_("skyWeather.title")}</Title>
  <Content id="event-content">
    <FormField
      style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
    >
      <Textfield
        style="width: 100%;"
        value={0}
        label={$_("skyWeather.date")}
        type="datetime"
        disabled
      />
    </FormField>
  </Content>

  <Actions>
    <Button action="cancel">
      <Label>{$_("modal.cancel")}</Label>
    </Button>
    <Button action="save" default>
      <Label>{$_("modal.save")}</Label>
    </Button>
  </Actions>
</Dialog>

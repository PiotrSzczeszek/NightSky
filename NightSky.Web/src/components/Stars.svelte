<script lang="ts">
  import DataTable, { Head, Body, Row, Cell } from "@smui/data-table";
  import Dialog, { Title, Content, Actions } from "@smui/dialog";
  import IconButton from "@smui/icon-button";
  import Button, { Icon, Label } from "@smui/button";
  import Tooltip, { Wrapper } from "@smui/tooltip";
  import FormField from "@smui/form-field";
  import Textfield from "@smui/textfield";
  import LinearProgress from "@smui/linear-progress";
  import CharacterCounter from "@smui/textfield/character-counter";
  import HelperText from "@smui/textfield/helper-text";

  import { _ } from "svelte-i18n";
  import { type StarDataModel } from "../classes/StarData";
  import { api } from "../api/ApiCalls";

  import { onMount } from "svelte";

  let rowData: Array<StarDataModel> = [];
  let loaded = false;
  let dialogOpen = false;
  let model: StarDataModel;
  let urlInvalid = false;
  let nameInvalid = false;

  function openAddForm() {
    model = {
      starName: "",
      description: "",
      imageUrl: "",
      constallations: [],
    };

    dialogOpen = true;
  }

  function editElement(row: StarDataModel) {
    model = row;
    dialogOpen = true;
  }

  async function deleteElement(row: StarDataModel) {
    await api.deleteRequest(`stars/${row.starId}`);

    await loadData();
  }

  async function onFormClosed({ detail: { action } }) {
    if (action != "save") return;

    await saveData();
    await loadData();
  }

  async function loadData() {
    loaded = false;
    const { data } = await api.getRequest("stars");
    rowData = data;
    loaded = true;
  }
  async function saveData() {
    if (model.starId) {
      await api.putRequest("stars", model);
    } else {
      await api.postRequest("stars", model);
    }
  }

  onMount(async () => {
    await loadData();
  });
</script>

<div style="width: 100%; text-align: right">
  <Button
    variant="raised"
    color="secondary"
    style="margin: 1rem 0.5rem"
    on:click={openAddForm}
  >
    {$_("general.add")}
  </Button>
</div>
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
      <Row>
        <Cell>{row.starId}</Cell>
        <Cell style="max-width: 100px;">{row.starName}</Cell>
        <Cell style="max-width: 250px">{row.description}</Cell>
        <Cell style="text-align: center">
          {#if row.imageUrl}
            <img
              style="max-width: 100px; max-height: 100px"
              src={row.imageUrl}
              alt="Star"
            />
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
      </Row>
    {/each}
  </Body>
  <LinearProgress
    indeterminate
    bind:closed={loaded}
    aria-label="Data is being loaded..."
    slot="progress"
  />
</DataTable>
{#if dialogOpen}
  <Dialog
    bind:open={dialogOpen}
    aria-labelledby="event-title"
    aria-describedby="event-content"
    style="min-width: 400px"
    on:SMUIDialog:closed={onFormClosed}
  >
    <Title id="event-title">{$_("star.title")}</Title>
    <Content id="event-content" style="min-width: min(500px, 90vw)">
      <FormField
        style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
      >
        {#if model.starId}
          <Textfield
            style="width: 100%;"
            value={model.starId}
            label={$_("star.id")}
            type="number"
            disabled
          />
        {/if}
      </FormField>

      <FormField
        style="width: 100%; display: flex; flex-direction: column; align-items: end; margin-top: 1rem"
      >
        <Textfield
          style="width: 100%;"
          bind:value={model.starName}
          label={$_("star.name")}
          input$maxlength={50}
          bind:invalid={nameInvalid}
          updateInvalid
          required
        >
          <svelte:fragment slot="helper">
            <HelperText validationMsg>
              {$_("general.fieldRequired")}
            </HelperText>
            <CharacterCounter>0 / 50</CharacterCounter>
          </svelte:fragment>
        </Textfield>
      </FormField>

      <FormField
        style="width: 100%; display: flex; flex-direction: column; align-items: start; margin-top: 1rem"
      >
        <Textfield
          type="url"
          style="width: 100%;"
          bind:invalid={urlInvalid}
          updateInvalid
          bind:value={model.imageUrl}
          label={$_("star.imageUrl")}
          input$maxlength={250}
        >
          <svelte:fragment slot="helper">
            <HelperText validationMsg>
              {$_("star.invalidUrl")}
            </HelperText>
          </svelte:fragment>
        </Textfield>
      </FormField>

      <FormField
        style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
      >
        <Textfield
          style="width: 100%;"
          bind:value={model.description}
          label={$_("star.description")}
          type="text"
          textarea
          input$maxlength={500}
        >
          <CharacterCounter slot="internalCounter">0 / 500</CharacterCounter>
        </Textfield>
      </FormField>
    </Content>

    <Actions>
      <Button action="cancel">
        <Label>{$_("modal.cancel")}</Label>
      </Button>
      <Button action="save" default disabled={urlInvalid || nameInvalid}>
        <Label>{$_("modal.save")}</Label>
      </Button>
    </Actions>
  </Dialog>
{/if}

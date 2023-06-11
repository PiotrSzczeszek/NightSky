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
  import Autocomplete from "@smui-extra/autocomplete";
  import Chip, { Set, TrailingAction, Text } from "@smui/chips";

  import { _ } from "svelte-i18n";
  import { type ConstallationDataModel } from "../classes/ConstallationData";
  import { api } from "../api/ApiCalls";

  import { onMount } from "svelte";
  import type { StarDataModel } from "../classes/StarData";

  let rowData: Array<ConstallationDataModel> = [];
  let loaded = false;
  let dialogOpen = false;
  let model: ConstallationDataModel;
  let urlInvalid = false;
  let nameInvalid = false;
  let selectedStar;

  let availableStars: Array<StarDataModel> = [];

  function openAddForm() {
    model = {
      name: "",
      imageUrl: "",
      stars: [],
    };

    dialogOpen = true;
  }

  function editElement(row: ConstallationDataModel) {
    model = row;
    dialogOpen = true;
  }

  async function deleteElement(row: ConstallationDataModel) {
    await api.deleteRequest(`constallations/${row.constallationId}`);

    await loadData();
  }

  async function onFormClosed({ detail: { action } }) {
    if (action != "save") return;

    await saveData();
    await loadData();
  }

  async function loadData() {
    loaded = false;
    const { data } = await api.getRequest("constallations");
    rowData = data;
    loaded = true;
  }
  async function saveData() {
    if (model.constallationId) {
      await api.putRequest("constallations", model);
    } else {
      await api.postRequest("constallations", model);
    }
  }
  async function getAvailableStars() {
    const { data } = await api.getRequest("stars");
    availableStars = data;
  }

  function starOptionLabelFormatter(option) {
    return option ? option.starName : "";
  }

  function handleSelection(event: CustomEvent<StarDataModel>) {
    // Don't actually select the item.
    event.preventDefault();

    // // You could also set value back to '' here.
    model.stars.push(event.detail);
    model = model;

    selectedStar = null;
    // // Make sure the chips get updated.
    // selected = selected;

    // selector.focus();
  }
  onMount(async () => {
    await loadData();
    await getAvailableStars();
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
<DataTable table$aria-label="Constallations list" style="width: 100%;">
  <Head>
    <Row>
      <Cell>{$_("constallation.table.headers.id")}</Cell>
      <Cell>{$_("constallation.table.headers.name")}</Cell>
      <Cell style="text-align: center"
        >{$_("constallation.table.headers.image")}</Cell
      >
      <Cell style="text-align: center"
        >{$_("constallation.table.headers.actions")}</Cell
      >
    </Row>
  </Head>
  <Body>
    {#each rowData as row}
      <Row>
        <Cell>{row.constallationId}</Cell>
        <Cell style="max-width: 100px;">{row.name}</Cell>
        <Cell style="text-align: center">
          {#if row.imageUrl}
            <img
              style="max-width: 100px; max-height: 100px"
              src={row.imageUrl}
              alt="Constallation"
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
    <Title id="event-title">{$_("constallation.title")}</Title>
    <Content id="event-content" style="min-width: min(500px, 90vw)">
      <FormField
        style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
      >
        {#if model.constallationId}
          <Textfield
            style="width: 100%;"
            value={model.constallationId}
            label={$_("constallation.id")}
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
          bind:value={model.name}
          label={$_("constallation.name")}
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
        style="width: 100%; display: flex; flex-direction: column; align-items: constallationt; margin-top: 1rem"
      >
        <Textfield
          type="url"
          style="width: 100%;"
          bind:invalid={urlInvalid}
          updateInvalid
          bind:value={model.imageUrl}
          label={$_("constallation.imageUrl")}
          input$maxlength={250}
        >
          <svelte:fragment slot="helper">
            <HelperText validationMsg>
              {$_("constallation.invalidUrl")}
            </HelperText>
          </svelte:fragment>
        </Textfield>
      </FormField>

      <FormField
        style="width: 100%; display: flex; flex-direction: column; align-items: constallationt; margin-top: 1rem"
      >
        <div class="status">
          <Set style="display: inline-block;" bind:chips={model.stars} let:chip>
            <Chip {chip}>
              <Text tabindex={0}>{chip.starName}</Text>
              <TrailingAction icon$class="material-icons"
                >{$_("general.cancel")}</TrailingAction
              >
            </Chip>
          </Set>
        </div>
        {#if availableStars && availableStars.length}
          <Autocomplete
            options={availableStars.filter(
              (e) => !model.stars?.some((x) => x.starId == e.starId)
            )}
            bind:value={selectedStar}
            label={$_("constallation.stars")}
            getOptionLabel={starOptionLabelFormatter}
            on:SMUIAutocomplete:selected={handleSelection}
          />
        {/if}
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

<script lang="ts">
  import LayoutGrid, { Cell } from "@smui/layout-grid";
  import IconButton from "@smui/icon-button";
  import Button, { Icon, Label } from "@smui/button";
  import { date, _ } from "svelte-i18n";
  import Dialog, { Title, Content, Actions } from "@smui/dialog";
  import FormField from "@smui/form-field";
  import Slider from "@smui/slider";
  import type { SkyDataModel } from "../classes/SkyData";
  import SegmentedButton, { Segment } from "@smui/segmented-button";
  import Tooltip, { Wrapper } from "@smui/tooltip";
  import Textfield from "@smui/textfield";
  import CircularProgress from "@smui/circular-progress";

  import { api } from "../api/ApiCalls";

  const compareDates = (date1: Date, date2: Date) => {
    const x = new Date(date1);
    const y = new Date(date2);
    x.setHours(0, 0, 0, 0);
    y.setHours(0, 0, 0, 0);

    return x.toDateString() == y.toDateString();
  };

  const datesData: Array<SkyDataModel> = [];

  let showedDate = new Date();
  let showDataForm = false;

  $: formattedDate = $date(showedDate, { format: "medium" });

  let formObject: SkyDataModel = {
    skyId: null,
    fogLevel: 0,
    cloudsLevel: 0,
    precipitationType: "none",
    dataDate: showedDate,
  };

  function recalculateFormObject() {
    const x = datesData.find(
      (e) => compareDates(showedDate, e.dataDate) == true
    ) ?? {
      fogLevel: 0,
      cloudsLevel: 0,
      precipitationType: "none",
      dataDate: showedDate,
    };
    if (!x) return;
    formObject = x;
  }

  function addDays(days: number) {
    const tempDate = new Date(showedDate.valueOf());

    tempDate.setDate(tempDate.getDate() + days);

    showedDate = tempDate;
    promise = getData();
  }
  function showAddModal() {
    showDataForm = true;
  }
  async function saveData() {
    if (!formObject.skyId) {
      await api.postRequest("sky-data", formObject);
    } else {
      await api.putRequest("sky-data", formObject);
    }
    promise = getData();
  }

  async function getData() {
    const tempDate = new Date(showedDate.valueOf());
    tempDate.setDate(tempDate.getDate() - 3);

    const startDate = tempDate.toISOString();
    tempDate.setDate(tempDate.getDate() + 6);
    const endDate = tempDate.toISOString();

    const response = await api.getRequest(
      `sky-data?startDate=${startDate}&endDate=${endDate}`
    );

    datesData.push(...response.data);

    recalculateFormObject();
  }

  let promise = getData();

  async function onShowDataFormClosed({ detail: { action } }) {
    if (action != "save") {
      recalculateFormObject();
      return;
    }
    await saveData();
  }

  $: {
    if (formObject.cloudsLevel == 0) {
      formObject.precipitationType = "none";
    }
  }

  $: {
    getData();
  }

  const precipitationTypes = ["none", "rain", "snow", "sleet", "hail"];
</script>

<div class="sky-weather">
  <LayoutGrid>
    <Cell spanDevices={{ desktop: 6, tablet: 0, phone: 0 }} />

    <Cell spanDevices={{ desktop: 6, tablet: 12, phone: 12 }}>
      <div class="date-cell" style="height: 80px;">
        <IconButton
          class="material-icons"
          size="button"
          on:click={() => addDays(-1)}
        >
          navigate_before
        </IconButton>
        <span
          style="margin: 0 0.3rem; margin-top: -6px; min-width: 86px; display:inline-block; text-align: center"
        >
          {formattedDate}
        </span>
        <IconButton
          class="material-icons"
          size="button"
          on:click={() => addDays(1)}
        >
          navigate_next
        </IconButton>
      </div>
    </Cell>
  </LayoutGrid>
  {#await promise}
    <div style="display: flex; justify-content: center">
      <CircularProgress style="height: 96px; width: 96px;" indeterminate />
    </div>
  {:then}
    <div class="data-container">
      {#if !formObject.skyId}
        <div class="x">
          <Icon class="material-icons" style="font-size: 5rem">
            sentiment_dissatisfied
          </Icon>
          <h3>
            {$_("skyWeather.noData")}
          </h3>
          <p>
            {$_("skyWeather.noDataDesc")}
            {formattedDate}
          </p>
          <Button
            style="margin-top: 1rem"
            variant="raised"
            color="secondary"
            on:click={showAddModal}
          >
            {$_("general.add")}
          </Button>
        </div>
      {:else}
        <div class="x">
          <FormField
            style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
          >
            <Textfield
              style="width: 100%;"
              value={formattedDate}
              label={$_("skyWeather.date")}
              type="datetime"
              disabled
            />
          </FormField>
          <FormField
            style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
          >
            <Textfield
              style="width: 100%;"
              value={formObject.fogLevel}
              label={$_("skyWeather.fogLevel")}
              type="number"
              disabled
            />
          </FormField>
          <FormField
            style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
          >
            <Textfield
              style="width: 100%;"
              value={formObject.cloudsLevel}
              label={$_("skyWeather.cloudsLevel")}
              type="number"
              disabled
            />
          </FormField>

          <FormField
            style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
          >
            <Textfield
              style="width: 100%;"
              value={$_(
                `skyWeather.precipitationTypes.${formObject.precipitationType.toLowerCase()}`
              )}
              label={$_("skyWeather.precipitationType")}
              type="text"
              disabled
            />
          </FormField>

          <Button
            style="margin-top: 1rem"
            variant="raised"
            color="secondary"
            on:click={showAddModal}
          >
            {$_("general.edit")}
          </Button>
        </div>
      {/if}
    </div>

    <Dialog
      bind:open={showDataForm}
      aria-labelledby="event-title"
      aria-describedby="event-content"
      on:SMUIDialog:closed={onShowDataFormClosed}
    >
      <Title id="event-title">{$_("skyWeather.title")}</Title>
      <Content id="event-content">
        <FormField
          style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
        >
          <Textfield
            style="width: 100%;"
            value={formattedDate}
            label={$_("skyWeather.date")}
            type="datetime"
            disabled
          />
        </FormField>
        <FormField
          style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
        >
          <Slider
            style="width: 100%;"
            bind:value={formObject.fogLevel}
            min={0}
            max={10}
            step={1}
            discrete
          />
          <span
            slot="label"
            style="padding-right: 12px; width: max-content; display: block;"
          >
            {$_("skyWeather.fogLevel")}
          </span>
        </FormField>
        <FormField
          style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
        >
          <Slider
            style="width: 100%;"
            bind:value={formObject.cloudsLevel}
            min={0}
            max={10}
            step={1}
            discrete
          />
          <span
            slot="label"
            style="padding-right: 12px; width: max-content; display: block;"
          >
            {$_("skyWeather.cloudsLevel")}
          </span>
        </FormField>

        <FormField
          style="display: flex; flex-direction: column-reverse; margin-top: 1rem"
        >
          <SegmentedButton
            segments={precipitationTypes}
            let:segment
            singleSelect
            bind:selected={formObject.precipitationType}
            style="margin-top:1rem"
          >
            <!-- Note: the `segment` property is required! -->
            <Segment
              disabled={formObject.cloudsLevel == 0 && segment != "none"}
              {segment}
            >
              <Wrapper>
                <Label>{$_(`skyWeather.precipitationTypes.${segment}`)}</Label>
                {#if formObject.cloudsLevel == 0 && segment != "none"}
                  <Tooltip>
                    {$_("skyWeather.noClouds")}
                  </Tooltip>
                {/if}
              </Wrapper>
            </Segment>
          </SegmentedButton>
          <span
            slot="label"
            style="padding-right: 12px; width: max-content; display: block;"
          >
            {$_("skyWeather.precipitationType")}
          </span>
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
  {/await}
</div>

<style>
  .sky-weather {
    width: 100%;
    height: 100%;
  }
  .date-cell {
    text-align: right;
  }
  .data-container {
    margin: 0 auto;
    /* margin-top: 1rem; */
    width: 40%;
    text-align: center;
  }
</style>

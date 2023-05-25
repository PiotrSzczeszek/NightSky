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

  import { api } from "../api/ApiCalls";

  function addDays(days: number) {
    const tempDate = new Date(showedDate.valueOf());

    tempDate.setDate(tempDate.getDate() + days);

    showedDate = tempDate;
  }
  function showAddModal() {
    showDataForm = true;
  }
  async function saveData() {}

  async function getData() {
    const tempDate = new Date(showedDate.valueOf());
    tempDate.setDate(tempDate.getDate() - 3);

    const startDate = tempDate;
    tempDate.setDate(tempDate.getDate() + 6);
    const endDate = tempDate;

    const response = await api.get(
      `https://localhost:6002/api/sky-data?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`
    );

    console.log(response);
  }

  async function onShowDataFormClosed({ detail: { action } }) {
    if (action != "save") {
      formObject.fogLevel = 0;
      formObject.cloudsLevel = 0;
      formObject.precipitationType = "none";
    }
    await saveData();
  }

  let showedDate = new Date();
  let showDataForm = false;

  const formObject: SkyDataModel = {
    fogLevel: 0,
    cloudsLevel: 0,
    precipitationType: "none",
    dataDate: showedDate,
  };
  $: formattedDate = $date(showedDate, { format: "medium" });
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
  <div class="data-container">
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
        {$_("skyWeather.add")}
      </Button>

      <Dialog
        bind:open={showDataForm}
        aria-labelledby="event-title"
        aria-describedby="event-content"
        on:SMUIDialog:closed={onShowDataFormClosed}
      >
        <Title id="event-title">The Best Dog</Title>
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
                  <Label>{$_(`skyWeather.precipitationTypes.${segment}`)}</Label
                  >
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
            <Label>{$_("skyWeather.modal.cancel")}</Label>
          </Button>
          <Button action="save" default>
            <Label>{$_("skyWeather.modal.save")}</Label>
          </Button>
        </Actions>
      </Dialog>
    </div>
  </div>
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

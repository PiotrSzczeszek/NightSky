<script lang="ts">
  import { api } from "../api/ApiCalls";
  import { onMount } from "svelte";
  import type { ConstallationDataModel } from "../classes/ConstallationData";
  import CircularProgress from "@smui/circular-progress";
  import List, { Item, Graphic, Separator, Text } from "@smui/list";
  import { _ } from "svelte-i18n";

  let constellations: Array<{
    data: ConstallationDataModel;
    element: InstanceType<typeof List>;
  }> = [];
  let promise: Promise<any> | undefined = undefined;
  let hoveredStarId: Number | undefined = undefined;

  async function getData() {
    const { data } = await api.getRequest("constallations");
    constellations = data.map((e: ConstallationDataModel) => {
      return { data: e, element: null };
    });
  }

  onMount(async () => {
    promise = getData();
  });
</script>

<div style="display: flex; flex-direction: row; flex-wrap: wrap">
  {#await promise}
    <div style="display: flex; justify-content: center">
      <CircularProgress style="height: 96px; width: 96px;" indeterminate />
    </div>
  {:then}
    {#each constellations as constelation}
      <fieldset
        style="min-width: 200px; max-width: 250px; padding: 1rem; margin: 1rem"
      >
        <legend style="padding: 0 1rem">{constelation.data.name}</legend>
        <List bind:this={constelation.element} class="demo-list" dense>
          {#each constelation.data.stars as star}
            <Item>
              <Text>{star.starName}</Text>
            </Item>
          {/each}
          {#if !constelation.data.stars.length}
            <Item>
              <Text>{$_("skyWeather.noData")}</Text>
            </Item>
          {/if}
        </List>
      </fieldset>
    {/each}
  {/await}
</div>

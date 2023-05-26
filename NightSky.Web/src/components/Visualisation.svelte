<script lang="ts">
  import { api } from "../api/ApiCalls";
  import { onMount } from "svelte";
  import type { ConstallationDataModel } from "../classes/ConstallationData";
  import CircularProgress from "@smui/circular-progress";
  import List, { Item, Graphic, Separator, Text } from "@smui/list";
  import type { StarDataModel } from "../classes/StarData";

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

  function handleMouseOver(star: StarDataModel) {
    constellations.forEach((constallation) => {
      if (constallation.data.stars.some((x) => x.starId == star.starId)) {
        const indexToFocus = constallation.data.stars.findIndex(
          (e) => e.starId == star.starId
        );
        constallation.element.focusItemAtIndex(indexToFocus);
      }
    });
  }

  function handleMouseOut(star: StarDataModel) {}

  onMount(async () => {
    promise = getData();
  });
</script>

{#await promise}
  <div style="display: flex; justify-content: center">
    <CircularProgress style="height: 96px; width: 96px;" indeterminate />
  </div>
{:then}
  {#each constellations as constelation}
    <fieldset style="max-width: 250px; padding: 1rem; margin: 1rem">
      <legend style="padding: 0 1rem">{constelation.data.name}</legend>
      <List bind:this={constelation.element} class="demo-list" dense>
        {#each constelation.data.stars as star}
          <Item
            on:mouseover={() => handleMouseOver(star)}
            on:mouseout={() => handleMouseOut(star)}
          >
            <Text>{star.starName}</Text>
          </Item>
        {/each}
      </List>
    </fieldset>
  {/each}
{/await}

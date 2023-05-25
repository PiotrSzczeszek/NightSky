<script lang="ts">
  import image from "../assets/night_sky.jpg";
  import { _ } from "svelte-i18n";
</script>

<div class="landing-page">
  <div class="landing-page__text">
    <h1>
      {$_("landingPage.title")}
    </h1>
    <span>
      {$_("landingPage.subtitle")}
    </span>
  </div>
</div>

<style>
  .landing-page {
    background-image: url("../assets/night_sky.jpg");
    background-position: center; /* Center the image */
    background-repeat: no-repeat; /* Do not repeat the image */
    background-size: cover; /* Resize the background image to cover the entire container */

    width: 100%;
    height: 100%;

    display: grid;
    place-items: end center;
  }
  .landing-page__text {
    margin-top: 4rem;
    margin-bottom: 10rem;
    padding: 1rem 2rem;

    /* background-color: white; */
    text-shadow: -1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000,
      1px 1px 0 #000;
  }
</style>

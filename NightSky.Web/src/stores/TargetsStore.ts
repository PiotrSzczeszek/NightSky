import { writable, get } from "svelte/store";

import { LandingPageView, SkyWeatherView, Stars, Constellations } from "../components";
import { isMobile, isLeftMenuOpen } from "./AppStateStore";
import Visualisation from "../components/Visualisation.svelte";

export type TargetTypes = {
  landingPage: string;
  skyWeather: string;
  stars: string;
  constallations: string,
  visualisation: string
};

type TargetComponentsAssociacions = {
  type: keyof TargetTypes;
  component: any;
}

export const targetAssociations: Array<TargetComponentsAssociacions> = [
  { type: "landingPage", component: LandingPageView },
  { type: "skyWeather", component: SkyWeatherView },
  { type: "stars", component: Stars },
  { type: "constallations", component: Constellations },
  { type: "visualisation", component: Visualisation }
]

export const currentTargetType = writable<keyof TargetTypes>("landingPage");
export const currentTarget = writable<ConstructorOfATypedSvelteComponent>();

currentTargetType.subscribe(value => {
  const target = targetAssociations.find(e => e.type == value);

  currentTarget.set(target.component);

  const mobile = get(isMobile);

  if (!mobile) return;

  isLeftMenuOpen.set(false);

})




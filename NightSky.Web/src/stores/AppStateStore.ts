import { readable, writable } from "svelte/store";

const mobileDeviceWidth: number = 576;

const width = window.visualViewport.width;

export const documentWidth = writable(0);
export const isMobile = writable(width <= mobileDeviceWidth);
export const isLeftMenuOpen = writable(false);

documentWidth.subscribe(val => {
  const isMobileWidth = val <= mobileDeviceWidth;
  isMobile.set(isMobileWidth);

  isLeftMenuOpen.set(!isMobileWidth);
});

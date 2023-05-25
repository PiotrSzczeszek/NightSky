import { writable } from "svelte/store";
import type { SnackbarInfo } from "../classes/Snackbar";
import Kitchen from "@smui/snackbar/kitchen";

const mobileDeviceWidth: number = 576;

const width = window.visualViewport.width;

export const documentWidth = writable(0);
export const isMobile = writable(width <= mobileDeviceWidth);
export const isLeftMenuOpen = writable(false);

export const snackbars = writable<Kitchen>();

documentWidth.subscribe(val => {
  const isMobileWidth = val <= mobileDeviceWidth;
  isMobile.set(isMobileWidth);

  isLeftMenuOpen.set(!isMobileWidth);
});

const addSnackbar = (type: "success" | "error", message: string, showDismiss: boolean, showYes: boolean, dismissCallback: Function, yesCallback: Function) => {

  snackbars.update(x => {
    x.push({
      props: { leading: true, class: type },
      label: message,
      dismissButton: showDismiss,
      onDismiss: () => { dismissCallback() }
    });
    return x;
  })
}

export const addSuccessSnackbar = (text: string): void => {
  addSnackbar("success", text, false, false, () => { }, () => { });
}

export const addErrorSnackbar = (text: string, showDismiss: boolean, dismissCallback: Function) => {
  addSnackbar("error", text, showDismiss, false, dismissCallback, () => { });
}
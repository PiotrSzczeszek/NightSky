export type SnackbarInfo = {
  type: "success" | "error",
  message: string,
  showYes: boolean,
  showDismiss: boolean,
  yesCallback: Function,
  dismissCallback: Function
}
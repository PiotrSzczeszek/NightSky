export type SkyDataModel = {
  skyId?: number,
  fogLevel: number,
  cloudsLevel: number,
  precipitationType: "none" | "rain" | "snow" | "sleet" | "hail",
  dataDate: Date
}
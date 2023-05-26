import type { StarDataModel } from "./StarData"

export type ConstallationDataModel = {
  constallationId?: number,
  name: string,
  imageUrl: string,
  stars: Array<StarDataModel>
}
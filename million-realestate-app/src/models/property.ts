import { Owner } from "./owner";

export type Property = {
  id: string;
  idOwner: string;
  name: string;
  address: string;
  price: number;
  imageUrl: string;
  owner?: Owner;
};
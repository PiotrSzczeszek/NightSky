import axios, { type AxiosRequestConfig, type AxiosResponse } from "axios";
import { addErrorSnackbar, addSuccessSnackbar } from "../stores/AppStateStore";
import { _ } from "svelte-i18n";
import { get } from "svelte/store";

const a = axios.create({
  baseURL: "https://localhost:6002/api"
});

function handleError(e) {
  console.error(e);
  const translations = get(_);
  let text = translations("general.getDataError");
  if (e && e.response && e.response.status && e.response.status == 404) {
    text = translations("general.dataNotFound");
  }
  addErrorSnackbar(text, true, () => { });
}

function handleSaveData() {
  const translations = get(_);
  const text = translations("general.dataSaved");
  addSuccessSnackbar(text);
}

const getRequest = async (url: string, config?: AxiosRequestConfig<any>): Promise<AxiosResponse<any, any>> => {
  try {
    return await axios.get(url, config);
  }
  catch (e) {
    handleError(e);
  }
}


const postRequest = async (url: string, data?: AxiosRequestConfig<any>, config?: AxiosRequestConfig<AxiosRequestConfig<any>>): Promise<AxiosResponse<any, any>> => {
  try {
    const response = await axios.post(url, config);
    handleSaveData();
    return response;
  }
  catch (e) {
    handleError(e);
  }
}


const putRequest = async (url: string, config?: AxiosRequestConfig<any>): Promise<AxiosResponse<any, any>> => {
  try {
    const response = await axios.put(url, config);
    handleSaveData();
    return response;
  }
  catch (e) {
    handleError(e);
  }
}

export const api = {
  getRequest,
  postRequest,
  putRequest
}

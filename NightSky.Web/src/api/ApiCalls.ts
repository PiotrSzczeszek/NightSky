import axios, { type AxiosRequestConfig, type AxiosResponse } from "axios";
import { addErrorSnackbar, addSuccessSnackbar } from "../stores/AppStateStore";
import { _, locale } from "svelte-i18n";
import { get } from "svelte/store";

const a = axios.create({
  baseURL: "https://localhost:6002/api"
});

function handleGetError(e) {
  handleError(e, "general.getDataError");
}

function handleSendError(e) {
  handleError(e, "general.saveDataError");

}

function handleError(e, defaultTranslationKey: string) {
  console.error(e);
  const translations = get(_);
  const currentLocale = get(locale);


  let text = translations(defaultTranslationKey);
  if (e && e.response && e.response.data && e.response.data.length) {
    const validResponse = e.response.data.find(e => e.lang == currentLocale);
    text = validResponse.message;
  } else if (e && e.response && e.response.status && e.response.status == 404) {
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
    return await a.get(url, config);
  }
  catch (e) {
    handleGetError(e);
  }
}


const postRequest = async (url: string, data?: any, config?: AxiosRequestConfig<AxiosRequestConfig<any>>): Promise<AxiosResponse<any, any>> => {
  try {
    const response = await a.post(url, data, config);
    handleSaveData();
    return response;
  }
  catch (e) {
    handleSendError(e);
  }
}


const putRequest = async (url: string, data?: any, config?: AxiosRequestConfig<any>): Promise<AxiosResponse<any, any>> => {
  try {
    const response = await a.put(url, data, config);
    handleSaveData();
    return response;
  }
  catch (e) {
    handleSendError(e);
  }
}

const deleteRequest = async (url: string): Promise<AxiosResponse<any, any>> => {
  try {
    const response = await a.delete(url);
    // handleSaveData();
    return response;
  }
  catch (e) {
    handleSendError(e);
  }
}

export const api = {
  getRequest,
  postRequest,
  putRequest,
  deleteRequest
}

import axios from "axios";

const a = axios.create({
  baseURL: "https://localhost:6002/api"
});


export { a as api };
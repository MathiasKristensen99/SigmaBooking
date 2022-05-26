import axios from "axios";

export default axios.create({
  baseURL: "https://sigmabooking.azurewebsites.net/",
  headers: {
    "Content-type": "application/json",
  },
});

import axios from "axios";

const endpoint = "https://api.remotebootcamp.dev/api/entities/products";

let addProduct = (payload) => {
  const config = {
    method: "POST",
    url: `${endpoint}`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };

  return axios(config);
};

const entityService = { addProduct };
export default entityService; // export all your calls here

// if you had three functions to export
// const usersService = { logIn, register, thirdFunction }
// export default usersService;

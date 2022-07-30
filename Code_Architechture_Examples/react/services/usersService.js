import axios from "axios";

const endpoint = "https://api.remotebootcamp.dev/api/users/";

let logIn = (payload) => {
  const config = {
    method: "POST",
    url: `${endpoint}/login`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };

  return axios(config);
};

let register = (payload) => {
  const config = {
    method: "POST",
    url: `${endpoint}/register`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };

  return axios(config);
};

const usersService = { logIn, register };
export default usersService; // export all your calls here

// if you had three functions to export
// const usersService = { logIn, register, thirdFunction }
// export default usersService;

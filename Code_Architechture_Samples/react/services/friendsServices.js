import axios from "axios";

const endpoint = "https://api.remotebootcamp.dev/api/friends";

let getFriends = (pageIndex, pageSize) => {
   const config = {
      method: "GET",
      url: `${endpoint}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json" },
   };

   return axios(config);
};

let searchFriends = (pageIndex, pageSize, query) => {
   const config = {
      method: "GET",
      url: `${endpoint}/search?pageIndex=${pageIndex}&pageSize=${pageSize}&q=${query}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json" },
   };

   return axios(config).then((response) => {
      return { foundFriends: response.data.item, fullResponse: response };
   });
};

let addFriends = (payload) => {
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

let updateFriend = (id, payload) => {
   const config = {
      method: "PUT",
      url: `${endpoint}/${id}`,
      data: payload,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json" },
   };

   return axios(config).then(() => {
      return { id, payload };
   });
};

let getFriendById = (id) => {
   const config = {
      method: "GET",
      url: `${endpoint}/${id}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json" },
   };

   return axios(config).then((response) => {
      return { friendObj: response.data.item, fullResponse: response };
   });
};

let removeFriends = (id) => {
   const config = {
      method: "DELETE",
      url: `${endpoint}/${id}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json" },
   };

   return axios(config).then(() => {
      return id;
   });
};

const friendsServices = {
   getFriends,
   addFriends,
   removeFriends,
   getFriendById,
   updateFriend,
   searchFriends,
};
export default friendsServices; // export all your calls here

// if you had three functions to export
// const usersService = { logIn, register, thirdFunction }
// export default usersService;

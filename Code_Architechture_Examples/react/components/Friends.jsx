import React, { useState, useEffect, useCallback } from "react";
import friendsServices from "../services/friendsServices";
import { Link } from "react-router-dom";
import Friend from "./Friend";
import toastr from "toastr";
import Search from "./search";
import Pagination from "rc-pagination";
import "rc-pagination/assets/index.css";
import locale from "rc-pagination/lib/locale/en_US";

function Friends() {
   const [friendData, setFriendData] = useState({
      arrayOfPeople: [],
      peopleComponents: [],
   });

   const [state, setState] = useState({
      pageIndex: 0,
      pageSize: 0,
      numberOfPages: 0,
   });

   const onChange = (page) => {
      console.log(page);

      setState((prevState) => {
         const pi = { ...prevState };
         console.log(pi);
         pi.pageIndex = page;
         return pi;
      });

      // friendsServices
      //    .getFriends(0, 10)
      //    .then(onGetFriendsSuccess)
      //    .catch(onGetFriendsError);
   };

   const mapFriendData = (aFriend) => {
      // console.log(aFriend);
      return (
         <Friend
            friend={aFriend}
            key={"ListA-" + aFriend.id}
            onDeleteClicked={onDeleteRequested}
            onEditClicked={onEditRequested}
         />
      );
   };

   const onEditRequested = useCallback((myFriend, eObj) => {
      console.log("onEditRequested firing", eObj, myFriend.id);
   }, []);

   const onDeleteRequested = useCallback((myFriend, eObj) => {
      console.log("onDeleteRequested firing", eObj);
      // console.log(`sending ${myFriend.id} to deleteFriend`);
      deleteFriend(myFriend.id);
   }, []);

   const deleteFriend = (idToBeDeleted) => {
      console.log("deleteFriend firing", idToBeDeleted);

      setFriendData((prevState) => {
         const fd = { ...prevState };
         fd.arrayOfPeople = [...fd.arrayOfPeople];

         const idxOf = fd.arrayOfPeople.findIndex((friend) => {
            let result = false;

            if (friend.id === idToBeDeleted) {
               friendsServices
                  .removeFriends(friend.id)
                  .then(onRemoveFriendsSuccess)
                  .catch(onRemoveFriendsError);
               result = true;
            }
            return result;
         });

         if (idxOf >= 0) {
            fd.arrayOfPeople.splice(idxOf, 1);
            // console.log(`friend ${idToBeDeleted} spliced`);
            fd.peopleComponents = fd.arrayOfPeople.map(mapFriendData);
         }

         // console.log(`friend ${idToBeDeleted} deleted!!`);
         return fd;
      });
   };

   const onGetFriendsSuccess = (response) => {
      let friendsArray = response.data.item.pagedItems;
      const { pageIndex, totalPages, pageSize } = response.data.item;
      console.log(pageIndex, totalPages, pageSize);

      console.log("onGetFriendsSuccess", friendsArray);

      setFriendData((prevState) => {
         const fd = { ...prevState };
         fd.arrayOfPeople = friendsArray;
         fd.peopleComponents = friendsArray.map(mapFriendData);
         return fd;
      });

      setState((prevState) => {
         const pi = { ...prevState };
         pi.pageIndex = pageIndex;
         pi.pageSize = pageSize;
         pi.numberOfPages = totalPages;
         return pi;
      });
   };
   const onGetFriendsError = (error) => {
      console.warn("onGetFriendsSuccess", error);
   };

   const onRemoveFriendsSuccess = (response) => {
      toastr.success("Friend Removed");
      console.log("onRemoveFriendsSuccess", response);
   };
   const onRemoveFriendsError = (error) => {
      toastr.error("Error: Friend Not Removed");
      console.warn("onRemoveFriendsError", error);
   };

   useEffect(() => {
      friendsServices
         .getFriends(0, 10)
         .then(onGetFriendsSuccess)
         .catch(onGetFriendsError);
   }, []);

   console.log(state);

   const onSearchChildClicked = (queryObj) => {
      //console.log("Parent Functions from Friends Firing");
      console.log(queryObj.query);

      if (queryObj.query.length > 0) {
         const q = queryObj.query;
         friendsServices
            .searchFriends(0, 10, q)
            .then(onSearchFriendsSuccess)
            .catch(onSearchFriendsError);
      } else {
         console.log("no query");
         toastr.error("No Friend Found");
      }
   };

   const onSearchFriendsSuccess = (response) => {
      toastr.success("Friend Found");
      console.log("onSearchFriendsSuccess", response.foundFriends.pagedItems);
      console.log({ totalPages: response.foundFriends.totalPages });
      console.log({ pageSize: response.foundFriends.pageSize });

      const queryArray = response.foundFriends.pagedItems;

      setFriendData((prevState) => {
         const fsd = { ...prevState };
         fsd.arrayOfPeople = queryArray;
         fsd.peopleComponents = queryArray.map(mapFriendData);
         return fsd;
      });
   };
   const onSearchFriendsError = (error) => {
      toastr.error("Error: Friend Not Found");
      console.warn("onSearchFriendsError", error);
   };

   return (
      <>
         <div className="container">
            <div className="row">
               <div className="col-3" style={{ display: "contents" }}>
                  <h1 className="mb-4 mt-4 col-3" style={{ textAlign: "center" }}>
                     Friends
                  </h1>
                  <Link
                     to="/friends/new"
                     data-page="new"
                     className="btn btn-primary mb-3"
                     style={{
                        height: "35px",
                        width: "100px",
                        backgroundColor: "blue",
                        fontWeight: "bold",
                        marginLeft: "10px",
                        marginTop: "33px",
                        paddingBottom: "30px",
                     }}
                  >
                     + Friends
                  </Link>
                  <div className="col" style={{ display: "flex" }}></div>
                  <Search onChildClicked={onSearchChildClicked} />
                  <div />
               </div>
            </div>
         </div>
         <div className="container">
            <div className="row" style={{ textAlign: "center" }}>
               {friendData.peopleComponents}
            </div>
         </div>
         <div style={{ textAlign: "center" }}>
            <Pagination
               onChange={onChange}
               current={state.pageIndex}
               total={25}
               locale={locale}
            />
         </div>
      </>
   );
}

export default React.memo(Friends);

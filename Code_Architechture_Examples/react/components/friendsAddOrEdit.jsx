import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import friendsServices from "../services/friendsServices";
import toastr from "toastr";

function AddOrEditForm() {
   const navigate = useNavigate();
   const { state } = useLocation();

   const [friendFormData, setFriendFormData] = useState(
      {
         id: 0,
         title: "",
         bio: "",
         summary: "",
         headline: "",
         slug: "",
         statusId: "Active",
         primaryImage: "",
      },
      []
   );

   const onFormInputsChanged = (event) => {
      event.preventDefault();
      console.log("onFormInputChange");

      const { name, value } = event.target;

      setFriendFormData((prevState) => {
         const newFriendData = { ...prevState };
         newFriendData[name] = value;
         // console.log(newFriendData);
         return newFriendData;
      });
   };

   const onSubmitClicked = (e) => {
      e.preventDefault();
      console.log("onSubmitClicked");
      //console.log(friendFormData.id);

      if (friendFormData.id) {
         friendsServices
            .updateFriend(friendFormData.id, friendFormData)
            .then(onUpdateFriendSuccess)
            .catch(onUpdateFriendError);
      } else {
         friendsServices
            .addFriends(friendFormData)
            .then(onAddFriendSuccess)
            .catch(onAddFriendError);
      }
   };

   const onUpdateFriendSuccess = (response) => {
      toastr.success("Friend Successfully Updated");
      console.log("onUpdateFriendSuccess", response);

      setFriendFormData(() => {
         const newFriendData = { ...response.payload };
         newFriendData.primaryImage = response.payload.primaryImage.imageUrl;
         console.log(newFriendData);
         return newFriendData;
      });
      setTimeout(() => {
         navigate("/friends");
      }, 2000);
   };
   const onUpdateFriendError = (error) => {
      toastr.error("Friend Not Updated");
      console.log("onUpdateFriendError", error);
   };

   const onAddFriendSuccess = (response) => {
      toastr.success("Friend Added");
      console.log("onAddFriendSuccess", response);

      setFriendFormData((prevState) => {
         const newFriendData = { ...prevState };
         newFriendData.id = response.data.item;
         console.log(newFriendData.id);
         return newFriendData;
      });
      //setTimeout(()=> {navigate("/friends")}, 2000)
   };
   const onAddFriendError = (error) => {
      toastr.error("Friend Not Added");
      console.log("onAddFriendError", error);
   };

   useEffect(() => {
      // const id = state.payload.id
      // console.log(id)
      if (state?.type === "FRIEND_VIEW" && state.payload) {
         setFriendFormData(() => {
            const newFriendData = { ...state.payload };
            newFriendData.primaryImage = newFriendData.primaryImage.imageUrl;
            console.log(newFriendData);
            return newFriendData;
         });
      } else {
         console.log("state is null");
      }
   }, []);

   console.log(friendFormData);

   return (
      <>
         <div className="container">
            <div className="row">
               <h1 style={{ textAlign: "center" }}>Add or Edit Friend</h1>
            </div>
         </div>
         <div className="container">
            <div className="row" style={{ textAlign: "center" }}>
               <div className="form-group">
                  <form className="form">
                     <div className="row mb-3 p-2 ">
                        <label htmlFor="inputTitle" className="col-form-label">
                           Title
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="title"
                              value={friendFormData.title}
                              name="title"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputBio" className="col-form-label">
                           Bio
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              id="bio"
                              className="form-control"
                              value={friendFormData.bio}
                              name="bio"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputSummary" className="col-form-label">
                           Summary
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="summary"
                              value={friendFormData.summary}
                              name="summary"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputHeadline" className="col-form-label">
                           Headline
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="headline"
                              value={friendFormData.headline}
                              name="headline"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputSlug" className="col-form-label">
                           Slug
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="slug"
                              value={friendFormData.slug}
                              name="slug"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputStatusId" className="col-form-label">
                           StatusId
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="statusId"
                              value={friendFormData.statusId}
                              name="statusId"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputPrimaryImage" className="col-form-label">
                           Primary Image URL
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="primaryImage"
                              value={friendFormData.primaryImage}
                              name="primaryImage"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <button
                        type="submit"
                        className="btn btn-primary mt-4"
                        onClick={onSubmitClicked}
                     >
                        Submit
                     </button>
                  </form>
               </div>
            </div>
         </div>
      </>
   );
}

export default AddOrEditForm;

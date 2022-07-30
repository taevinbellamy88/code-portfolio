import React, { useState } from "react";
import "./formStyle.css";
import usersService from "../services/usersService.js";
import toastr from "toastr";

function Register() {
   const [userFormData, setUserFormData] = useState({
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      passwordConfirm: "",
      avatarUrl: "",
      tenantId: "U03BSTERARW",
   });
   console.log({ userFormData, setUserFormData });

   const onFormInputChange = (event) => {
      event.preventDefault();
      console.log("onFormInputChange");

      const { name, value } = event.target;

      setUserFormData((prevState) => {
         const newUserData = { ...prevState };
         newUserData[name] = value;
         console.log(newUserData);
         return newUserData;
      });
   };

   const onRegisterClicked = () => {
      usersService
         .register(userFormData)
         .then(onRegisterUserSuccess)
         .catch(onRegisterUserError);
   };

   const onRegisterUserSuccess = (response) => {
      toastr.success("Successfully Resgistered User", { id: response });
      console.log(response, "onRegisterUserSuccess");
   };
   const onRegisterUserError = (error) => {
      toastr.error("Error: Unable to Register User");
      console.warn(error, "onRegisterUserError");
   };
   return (
      <>
         <div className="container">
            <div className="rowform">
               <div className="col">
                  <h1>Register</h1>
               </div>
            </div>
         </div>
         <div className="form-container">
            <div className="row2">
               <div className="col col-12">
                  <form className="form">
                     <div className="row mb-2 p-2">
                        <label htmlFor="inputEmail3" className="col-form-label">
                           Email Address
                        </label>
                        <div className="col-12">
                           <input
                              type="email"
                              className="form-control"
                              id="inputEmail"
                              placeholder="Enter Your Email Address"
                              value={userFormData.email}
                              name="email"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <div className="row mb-2 p-2">
                        <label htmlFor="firstName" className="col-form-label">
                           First Name
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="inputfName"
                              placeholder="Enter Your First Name"
                              value={userFormData.firstName}
                              name="firstName"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <div className="row mb-2 p-2">
                        <label htmlFor="lastName" className="col-form-label">
                           Last Name
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="inputlName"
                              placeholder="Enter Your Last Name"
                              value={userFormData.lastName}
                              name="lastName"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <div className="row mb-2">
                        <label htmlFor="inputPassword" className="col-form-label">
                           Password
                        </label>
                        <div className="col-12">
                           <input
                              type="password"
                              className="form-control"
                              id="inputPassword"
                              placeholder="Enter Your Password"
                              value={userFormData.password}
                              name="password"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <div className="row mb-2">
                        <label htmlFor="confirmPassword" className="col-form-label">
                           Confirm Password
                        </label>
                        <div className="col-12">
                           <input
                              type="password"
                              className="form-control"
                              id="confirmPassword"
                              placeholder="Re-Enter Your Password"
                              value={userFormData.passwordConfirm}
                              name="passwordConfirm"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <div className="row mb-2 p-2">
                        <label htmlFor="profileUrl" className="col-form-label">
                           Profile Url
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="profileUrl"
                              placeholder="Provide a Url to an Image"
                              value={userFormData.avatarUrl}
                              name="avatarUrl"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <div className="row mb-2 p-2">
                        <label htmlFor="tenantId" className="col-form-label d-none">
                           tenantId
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control d-none"
                              id="tenantId"
                              value={userFormData.tenantId}
                              name="tenantId"
                              onChange={onFormInputChange}
                           />
                        </div>
                     </div>
                     <button
                        type="button"
                        className="btn btn-primary"
                        onClick={onRegisterClicked}
                     >
                        Register
                     </button>
                  </form>
               </div>
            </div>
         </div>
      </>
   );
}

export default Register;

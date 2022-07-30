import React, { useState } from "react";
import "./formStyle.css";
import usersService from "../services/usersService.js";
import toastr from "toastr";

function LogIn(props) {
   console.log(props);
   const [userFormData, setUserFormData] = useState({
      email: "",
      password: "",
      tenantId: "U03BSTERARW",
   });
   console.log({ userFormData, setUserFormData });

   const onFormInputsChanged = (event) => {
      console.log("onFormInputsChanged");

      event.preventDefault();
      console.log("onFormInputChange");

      const { name, value } = event.target;

      setUserFormData((prevState) => {
         const newUserData = { ...prevState };
         newUserData[name] = value;
         props.user.isLoggedIn = true;
         console.log(newUserData);
         return newUserData;
      });
   };
   const onSignInClicked = () => {
      console.log("onSignInClicked");
      usersService.logIn(userFormData).then(onLoginSuccess).catch(onLoginError);
   };
   const onLoginSuccess = (response) => {
      toastr.success("Login Successfull");
      console.log("onLoginSuccess", response);
   };
   const onLoginError = (error) => {
      toastr.error("Error: Email and/or Password Incorrect");
      console.warn(error, "onLoginError");
   };
   return (
      <>
         <div className="container">
            <div className="rowform">
               <div className="col">
                  <h1>LogIn</h1>
               </div>
            </div>
         </div>
         <div className="form-container">
            <div className="row2">
               <div className="col col-12">
                  <form className="form">
                     <div className="row mb-4 p-2">
                        <label htmlFor="inputEmail3" className="col-form-label">
                           Email Address
                        </label>
                        <div className="col-12">
                           <input
                              type="email"
                              className="form-control"
                              id="inputEmail"
                              value={userFormData.email}
                              name="email"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputPassword3" className="col-form-label">
                           Password
                        </label>
                        <div className="col-12">
                           <input
                              type="password"
                              className="form-control"
                              id="inputPassword"
                              value={userFormData.password}
                              name="password"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <button
                        type="button"
                        className="btn btn-primary mt-4"
                        onClick={onSignInClicked}
                     >
                        Sign in
                     </button>
                  </form>
               </div>
            </div>
         </div>
      </>
   );
}

export default LogIn;

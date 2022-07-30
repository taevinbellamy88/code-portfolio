import React, { useState } from "react";
import entityService from "./services/productService.js";
import toastr from "toastr";

function Product() {
   const [productFormData, setProductFormData] = useState({
      name: "",
      manufacturer: "",
      description: "",
      cost: Number(),
   });

   console.log({ productFormData, setProductFormData });

   const onFormInputsChanged = (event) => {
      console.log("onFormInputsChanged");

      event.preventDefault();
      console.log("onFormInputChange");

      const { name, value } = event.target;

      setProductFormData((prevState) => {
         const newProductData = { ...prevState };
         newProductData[name] = value;
         console.log(newProductData);
         return newProductData;
      });
   };

   const onProductClicked = (e) => {
      e.preventDefault();
      console.log("onProductClicked");
      entityService
         .addProduct(productFormData)
         .then(onAddProductSuccess)
         .catch(onAddProductError);
   };

   const onAddProductSuccess = (response) => {
      toastr.success("Product Added");
      console.log("onAddProductSuccess", { productId: response.data.item });
   };
   const onAddProductError = (error) => {
      toastr.error("Product Not Added");
      console.log("onAddProductError", error);
   };

   return (
      <>
         <div className="container">
            <div className="row">
               <h1>Product</h1>
            </div>
         </div>
         <div className="container">
            <div className="row" style={{ textAlign: "center" }}>
               <div className="form-group">
                  <form className="form">
                     <div className="row mb-3 p-2">
                        <label htmlFor="inputName" className="col-form-label">
                           Name
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="name"
                              value={productFormData.name}
                              name="name"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputManufacturer" className="col-form-label">
                           Manufacturer
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              id="manufacturer"
                              className="form-control"
                              value={productFormData.manufacturer}
                              name="manufacturer"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputDescription" className="col-form-label">
                           Description
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="description"
                              value={productFormData.description}
                              name="description"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <div className="row mb-3">
                        <label htmlFor="inputCost" className="col-form-label">
                           Cost
                        </label>
                        <div className="col-12">
                           <input
                              type="text"
                              className="form-control"
                              id="cost"
                              value={productFormData.cost}
                              name="cost"
                              onChange={onFormInputsChanged}
                           />
                        </div>
                     </div>
                     <button
                        type="submit"
                        className="btn btn-primary mt-4"
                        onClick={onProductClicked}
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

export default Product;

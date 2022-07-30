import React, { useState } from "react";

const Search = (props) => {
   const [searchData, setSearchData] = useState({ query: "" });

   const onSearchBtnClicked = (e) => {
      e.preventDefault();
      console.log(e);
      console.log("onSearchBtnClicked");

      //   setSearchData(() => {
      //      const newSearchData = { ...searchData };
      //      return newSearchData;
      //   });

      props.onChildClicked(searchData);
   };
   const onSearchInputsChanged = (event) => {
      event.preventDefault();
      console.log("onFormInputChange");

      const { name, value } = event.target;

      setSearchData((prevState) => {
         const newSearchData = { ...prevState };
         newSearchData[name] = value;
         // console.log(newFriendData);
         return newSearchData;
      });
   };

   return (
      <>
         <form>
            <input
               value={searchData.query}
               name="query"
               type="text"
               style={{
                  width: "auto",
                  height: "40px",
                  marginTop: "33px",
                  textAlign: "right",
                  marginRight: "12px",
               }}
               onChange={onSearchInputsChanged}
            ></input>
         </form>
         <button
            className="btn btn-primary mb-3"
            style={{
               width: "auto",
               height: "40px",
               marginTop: "33px",
               textAlign: "right",
               marginRight: "12px",
            }}
            onClick={onSearchBtnClicked}
         >
            Search
         </button>
      </>
   );
};
export default Search;

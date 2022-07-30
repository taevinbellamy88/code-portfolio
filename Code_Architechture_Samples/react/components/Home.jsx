import React from "react";

function Home(props) {
   console.log(props);
   return (
      <div className="container">
         <div className="row">
            <div className="col">
               <h1>
                  Hello {props.user?.firstName} {props.user?.lastName}
               </h1>
            </div>
         </div>
      </div>
   );
}

export default Home;

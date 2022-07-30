import React, { useState } from "react";

function SingleCar(props) {
   console.log(props);

   const aCar = props.car;

   const [singleCar] = useState({});

   console.log(singleCar);

   return (
      <div className="row" style={{ justifyContent: "center" }}>
         <div className="card col-md-3 m-1">
            <div className="card-body">
               <h5 className="card-title">{aCar.make}</h5>
               <h5 className="card-text">{aCar.model}</h5>
               <h5 className="card-text">{aCar.year}</h5>
            </div>
         </div>
      </div>
   );
}

export default SingleCar;

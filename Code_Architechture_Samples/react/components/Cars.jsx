import React, { useState } from "react";
import SingleCar from "./SingleCar";

const Cars = () => {
   const [state, setState] = useState(false);

   const [carState, setCarState] = useState({
      arrayOfCars: [
         {
            make: "Kia",
            model: "Sorento",
            year: 2020,
         },
         {
            make: "Kia",
            model: "Optima",
            year: 2019,
         },
         {
            make: "Tesla",
            model: "Model 3",
            year: 2021,
         },
         {
            make: "Honda",
            model: "Civic",
            year: 2019,
         },
         {
            make: "Honda",
            model: "Accord",
            year: 2018,
         },
         {
            make: "Volkswagen",
            model: "Jetta",
            year: 2021,
         },
         {
            make: "Toyota",
            model: "Camry",
            year: 2021,
         },
         {
            make: "Ford",
            model: "Mustang",
            year: 2019,
         },
         {
            make: "Ford",
            model: "F-150",
            year: 2019,
         },
         {
            make: "Toyota",
            model: "Camry",
            year: 2020,
         },
         {
            make: "Ford",
            model: "F-150",
            year: 2021,
         },
      ],
      carComponents: [],
   });

   const onClickHandler = () => {
      setCarState((prevState) => {
         const cd = { ...prevState };
         cd.arrayOfCars = [...cd.arrayOfCars];
         cd.carComponents = cd.arrayOfCars.map(mapCarData);
         return cd;
      });

      setState(!state);
      console.log(carState);
   };

   const onFilterClicked = (e) => {
      e.preventDefault();

      const carId = e.target.id;

      setState(() => {
         if (state) {
            return;
         } else {
            return !state;
         }
      });

      setCarState((prevState) => {
         const cd = { ...prevState };
         cd.arrayOfCars = [...cd.arrayOfCars];

         const filteredCars = cd.arrayOfCars.filter((car) => {
            let result = false;

            if (`show-${car.year}-cars` === carId) {
               result = true;
            }

            return result;
         });

         console.log(filteredCars);

         if (filteredCars.length >= 0) {
            cd.carComponents = filteredCars.map(mapCarData);
         }

         return cd;
      });
   };

   const mapCarData = (aCar) => {
      console.log(aCar);

      return <SingleCar car={aCar} key={`show-${aCar.year}-cars` + aCar.model} />;
   };

   return (
      <>
         <div style={{ textAlign: "center" }}>
            <button
               type="button"
               className="btn button1 btn-prime"
               onClick={onClickHandler}
            >
               Show Cars
            </button>
            <button
               type="button"
               id="show-2018-cars"
               className="btn button1 btn-prime"
               onClick={onFilterClicked}
            >
               2018 Cars
            </button>
            <button
               type="button"
               id="show-2019-cars"
               className="btn button1 btn-prime"
               onClick={onFilterClicked}
            >
               2019 Cars
            </button>
            <button
               type="button"
               id="show-2020-cars"
               className="btn button1 btn-prime"
               onClick={onFilterClicked}
            >
               2020 Cars
            </button>
            <button
               type="button"
               id="show-2021-cars"
               className="btn button1 btn-prime"
               onClick={onFilterClicked}
            >
               2021 Cars
            </button>
            {state && <h1>{carState.carComponents}</h1>}
         </div>
      </>
   );
};

export default Cars;

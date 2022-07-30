import React from "react";
import { useNavigate } from "react-router-dom";

function Friend(props) {
   const navigate = useNavigate();

   const aFriend = props.friend;

   const onLocalDeleteClicked = (e) => {
      e.preventDefault();
      console.log(e.target);
      props.onDeleteClicked(aFriend, e);
   };

   const onLocalEditClicked = (e) => {
      e.preventDefault();
      console.log(e.target);
      props.onEditClicked(aFriend, e);

      const state = { type: "FRIEND_VIEW", payload: aFriend };

      navigate(`new/${aFriend.id}`, { state });
   };

   return (
      <div className="col-3">
         <div className="card mb-4">
            <img
               src={aFriend.primaryImage.imageUrl}
               className="card-img-top"
               alt="..."
               style={{
                  width: "100%",
                  height: "15vw",
                  objectFit: "cover",
               }}
            />
            <div className="card-body">
               <h5 className="card-title">{aFriend.title}</h5>
               <p className="card-text">{aFriend.summary}</p>
               <button
                  className="btn button btn-prime1"
                  onClick={onLocalDeleteClicked}
               >
                  Delete
               </button>

               <button
                  aria-current="page"
                  className="btn button1 btn-prime"
                  onClick={onLocalEditClicked}
               >
                  Edit
               </button>
            </div>
         </div>
      </div>
   );
}

export default React.memo(Friend);

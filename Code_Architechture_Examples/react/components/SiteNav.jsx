import React from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

function SiteNav() {
   const navigate = useNavigate();
   const goToPage = (e) => {
      console.log(e.currentTarget.dataset.page);
      navigate(e.currentTarget.dataset.page);
   };
   return (
      <React.Fragment>
         <nav
            className="navbar navbar-expand-md navbar-dark bg-dark"
            aria-label="Fourth navbar example"
         >
            <div className="container">
               <a className="navbar-brand" href="/">
                  <img
                     src="https://pw.sabio.la/images/Sabio.png"
                     width="30"
                     height="30"
                     className="d-inline-block align-top"
                     alt="Sabio"
                  />
               </a>
               <button
                  className="navbar-toggler"
                  type="button"
                  data-bs-toggle="collapse"
                  data-bs-target="#navbarsExample04"
                  aria-controls="navbarsExample04"
                  aria-expanded="false"
                  aria-label="Toggle navigation"
               >
                  <span className="navbar-toggler-icon"></span>
               </button>

               <div className="collapse navbar-collapse" id="navbarsExample04">
                  <ul className="navbar-nav me-auto mb-2 mb-md-0">
                     <li className="nav-item">
                        <Link
                           to="/"
                           className="nav-link px-2 text-white link-button"
                           data-page="/"
                           onClick={goToPage}
                        >
                           Home
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/friends"
                           className="nav-link px-2 text-white link-button"
                           data-page="/friends"
                           onClick={goToPage}
                        >
                           Friends
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/jobs"
                           href="#"
                           className="nav-link px-2 text-white link-button"
                           data-page="/jobs"
                           onClick={goToPage}
                        >
                           Jobs
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/companies"
                           href="#"
                           className="nav-link px-2 text-white link-button"
                           data-page="/companies"
                           onClick={goToPage}
                        >
                           Tech Companies
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/events"
                           href="#"
                           className="nav-link px-2 text-white link-button"
                           data-page="/events"
                           onClick={goToPage}
                        >
                           Events
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/test"
                           href="#"
                           className="nav-link px-2 text-white link-button"
                           data-page="/test"
                           onClick={goToPage}
                        >
                           Test and Ajax Call
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/product"
                           href="#"
                           className="nav-link px-2 text-white link-button"
                           data-page="/product"
                           onClick={goToPage}
                        >
                           Product
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/cars"
                           href="#"
                           className="nav-link px-2 text-white link-button"
                           data-page="/cars"
                           onClick={goToPage}
                        >
                           Cars
                        </Link>
                     </li>
                     <li className="nav-item">
                        <Link
                           to="/friends/new"
                           className="d-none"
                           data-page="new"
                           onClick={goToPage}
                        >
                           New AddOrEdit Form
                        </Link>
                     </li>
                  </ul>
                  <div className="text-end">
                     <a
                        href="/"
                        className="align-items-center mb-2 me-2 mb-lg-0 text-white text-decoration-none"
                     >
                        Unknown User
                     </a>
                     <Link
                        to="/login"
                        type="button"
                        className="btn btn-outline-light me-2"
                        data-page="/login"
                        onClick={goToPage}
                     >
                        Login
                     </Link>
                     <Link
                        to="/register"
                        type="button"
                        className="btn btn-warning"
                        data-page="/register"
                        onClick={goToPage}
                     >
                        Register
                     </Link>
                  </div>
               </div>
            </div>
         </nav>
      </React.Fragment>
   );
}

export default SiteNav;

import React, { Component, useEffect } from "react";
import { Link } from "react-router-dom";
import { GlobalValues } from "../../../infrastructure/utilities";
import { MenuLinkPositionEnum } from "../../../infrastructure/models";
import { NavbarMenu } from "./NavbarMenu";
import { getMenuLinks } from "../../../services/commonService";

import { BrowserRouter as Router, Switch, useLocation } from "react-router-dom";
import { useState } from "react";

export function Header({ userLogedIn }) {
  const [menus, setMenus] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);
  const [error, setError] = useState(null);
  const [returlUrl, setReturlUrl] = useState(window.location.href);

  useEffect(() => {
    let menus = JSON.parse(
      localStorage.getItem(GlobalValues.MenuLocalStorageKey)
    );
    if (
      menus == undefined ||
      menus == null ||
      menus.Expire < new Date().getTime()
    ) {
      (async () => {
        try {
          debugger
          const { data: result } = await getMenuLinks(MenuLinkPositionEnum.Navbar);
          setMenus(result.data);
          setIsLoaded(true);
          if (result.data)
            localStorage.setItem(
              GlobalValues.MenuLocalStorageKey,
              JSON.stringify({
                data: result.data,
                Expire: new Date(new Date().getTime() + 1 * 24 * 60 * 60 * 1000)
              })
            );
        } catch (error) {
          setIsLoaded(true);
          setError(error);
        }
      })()

    } else {
      setMenus(menus.data);
      setIsLoaded(true);
    }
    //eslint-disable-next-line
  }, []);

  let location = useLocation();
  React.useEffect(() => {
    setReturlUrl(GlobalValues.AppUrl + location.pathname);
  }, [location]);

  return (
    <header className="header">
      <nav className="navbar navbar-expand-lg fixed-top shadow navbar-light bg-white">
        <div className="container">
          <div className="d-flex align-items-center">
            <Link to="/" className="navbar-brand " tabIndex="-1">
              <img src="/assets/img/logo.png" alt="پرهان ترانسفر" width="180" />
            </Link>
          </div>
          <button
            type="button"
            data-toggle="collapse"
            data-target="#navbarCollapse"
            aria-controls="navbarCollapse"
            aria-expanded="false"
            aria-label="Toggle navigation"
            className="navbar-toggler navbar-toggler-right"
          >
            <i className="fa fa-bars"></i>
          </button>
          <div id="navbarCollapse" className="collapse navbar-collapse">
            <ul className="navbar-nav ml-auto w-100">
              {isLoaded && !error ? <NavbarMenu menus={menus} /> : "..."}
              <li className="nav-item mr-auto">
                {userLogedIn ? (
                  <a
                    className="nav-link"
                    href={`${GlobalValues.DashboardUrl}/member`}
                  >
                    داشبورد
                  </a>
                ) : (
                    <a
                      className="nav-link"
                      href={`${GlobalValues.DashboardUrl}?ReturnUrl=${returlUrl}`}
                    >
                      ورود به حساب
                  </a>
                  )}
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
}

export default Header;

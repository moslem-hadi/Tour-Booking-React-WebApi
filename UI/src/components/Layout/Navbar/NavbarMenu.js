import React from "react";
import { Link } from "react-router-dom";

export const NavbarMenu = ({ menus }) => {
  var mainMenus = menus.filter(item => {
    return item.parentId === 0;
  });

  mainMenus.map(
    a =>
      (a.subMenu = menus.filter(item => {
        return item.parentId === a.id;
      }))
  );
  return mainMenus.length === 0
    ? ""
    : mainMenus.map(menu =>
        menu.subMenu.length == 0 ? (
          <li key={menu.id} className="nav-item ml-2">
            <Link to={menu.link} className="nav-link">
              {menu.title}
            </Link>
          </li>
        ) : (
          <li key={menu.id} className="nav-item ml-2 dropdown">
            <Link
              to="javascript:void(0)"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
              className="nav-link dropdown-toggle "
            >
              {menu.title}
            </Link>
            <div
              aria-labelledby="docsDropdownMenuLink"
              className="dropdown-menu dropdown-menu-right"
            >
              {menu.subMenu.map(sub => (
                <Link key={sub.id} to={sub.link} className="dropdown-item">
                  {sub.title}
                </Link>
              ))}
            </div>
          </li>
        )
      );
};

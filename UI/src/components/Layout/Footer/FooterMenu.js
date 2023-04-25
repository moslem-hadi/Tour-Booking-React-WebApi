import React, { Component } from "react";
import { Link } from "react-router-dom";
import { GlobalValues } from "../../../infrastructure/utilities";
import { MenuLinkPositionEnum } from "../../../infrastructure/models";
import { getMenuLinks } from "../../../services/commonService";

export class FooterMenu extends Component {
  state = { menus: null, isLoaded: false, error: null };
  componentDidMount() {
    let menus = JSON.parse(
      localStorage.getItem(GlobalValues.FooterMenuLocalStorageKey)
    );
    if (
      menus == undefined ||
      menus == null ||
      menus.Expire < new Date().getTime()
    ) {
      try {
        const { data: result } = getMenuLinks(MenuLinkPositionEnum.Footer);
        this.setState({
          isLoaded: true,
          menus: result.data
        });
        if (result.data)
          localStorage.setItem(
            GlobalValues.FooterMenuLocalStorageKey,
            JSON.stringify({
              data: result.data,
              Expire: new Date(new Date().getTime() + 1 * 24 * 60 * 60 * 1000)
            })
          );
      } catch (error) {
        this.setState({
          isLoaded: true,
          error
        });
      }
    } else {
      this.setState({
        isLoaded: true,
        menus: menus.data
      });
    }
  }
  render() {
    let { isLoaded, menus } = this.state;
    return isLoaded && menus != null && menus.length > 0 ? (
      <ul className="list-unstyled">
        {menus.map(menu => (
          <li key={menu.id}>
            <Link to={menu.link} className="text-muted">
              {menu.title}
            </Link>
          </li>
        ))}
      </ul>
    ) : (
      "..."
    );
  }
}

export default FooterMenu;

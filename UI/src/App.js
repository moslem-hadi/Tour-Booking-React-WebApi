import React, { useEffect, useState } from "react";
import Header from "./components/Layout/Navbar/Header";
import Home from "./components/Pages/HomePage";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Footer from "./components/Layout/Footer/Footer";
import store from "./store";
import { Provider } from "react-redux";
import { ProductTypesEnum } from "./infrastructure/models";
import SearchPage from "./components/Pages/SearchPage";
import OrderPage from "./components/Pages/OrderPage";
import ReactNotification from "react-notifications-component";
import "react-notifications-component/dist/theme.css";
import OrderDetail from "./components/Pages/OrderDetail";
import { NotFoundPage } from "./components/Pages/NotFoundPage";
import PagePage from "./components/Pages/PagePage";
import BlogPage from "./components/Pages/BlogPage";
import { BlogSinglePage } from "./components/Pages/BlogSinglePage";
import ViewPage from "./components/Pages/ViewPage";
import ContactUsPage from "./components/Pages/ContactUsPage";
import Cookies from "universal-cookie";

import { GlobalValues } from "./infrastructure/utilities";

const App = () => {
  const [userLogedIn, setUuserLogedIn] = useState(false);

  useEffect(() => {
    let token = new URLSearchParams(window.location.search).get("token");
    let d = new Date();
    d.setTime(d.getTime() + 2 * 24 * 60 * 60 * 1000); //2 Days
    const cookies = new Cookies();
    if (token != null && token != undefined) {
      cookies.set(GlobalValues.LoginTokenCookieName, token, {
        path: "/",
        expires: d
      });

      setUuserLogedIn(true);
    } else {
      var authToken = cookies.get(GlobalValues.LoginTokenCookieName);
      if (authToken != null && authToken != undefined) {
        setUuserLogedIn(true);
      }
    }

    //eslint-disable-next-line
  }, []);

  return (
    <Provider store={store}>
      <Router>
        <ReactNotification />
        <Header userLogedIn={userLogedIn} />
        <Switch>
          <Route exact path="/" component={Home}></Route>
          <Route exact path="/order" component={OrderPage}></Route>
          <Route exact path="/contactus" component={ContactUsPage}></Route>
          <Route
            exact
            path="/orderdetail/:code"
            component={OrderDetail}
          ></Route>
          <Route
            path="/Gasht"
            render={props => (
              <SearchPage {...props} type={ProductTypesEnum.Gasht} />
            )}
          />
          <Route
            path="/Transfer"
            render={props => (
              <SearchPage {...props} type={ProductTypesEnum.Transfer} />
            )}
          />
          <Route
            path="/Tour"
            render={props => (
              <SearchPage {...props} type={ProductTypesEnum.Tour} />
            )}
          />
          <Route exact path="/page/:slug" component={PagePage}></Route>
          <Route exact path="/blog" component={BlogPage}></Route>
          <Route exact path="/blog/:slug" component={BlogSinglePage}></Route>
          <Route exact path="/view/:slug" component={ViewPage}></Route>
          <Route exact path="/404" component={NotFoundPage}></Route>
          <Route component={NotFoundPage}></Route>
        </Switch>
        <Footer />
      </Router>
    </Provider>
  );
};

export default App;

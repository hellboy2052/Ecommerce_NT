
import React from "react";
import "bootstrap/dist/css/bootstrap.css";
import {
  BrowserRouter as Router,
  Route,
  Switch,

} from "react-router-dom";
import TopMenu from "./components/TopMenu.js";
import Oidc, { UserManager } from 'oidc-client'
import Category from './containers/Category';
import Product from "./containers/Product.js";
import Login from './components/Login/Login';
import LoginCallback from './components/Login/LoginCallBack';
import User from "./containers/User.js";

export default function App() {
  const config = {
    userStore:new Oidc.WebStorageStateStore({store:window.localStorage}),
    authority: "https://localhost:44309/",
    client_id: "react-admin",  
    redirect_uri: "http://localhost:3000/signin-oidc",
    // post_logout_redirect_uri: `${process.env.REACT_APP_ADMIN}/signout-oidc`,
    response_type: "id_token token",
    scope: "openid profile rookieshop.api",
  }
  var userManager = new Oidc.UserManager(config)
  return (
    <Router>
      <TopMenu />
      <br/><br/><br/>
      <Switch>
        <Route exact path="/">
        <Login userManager={userManager}/>
        </Route>
        <Route exact path="/category">
          <Category />
        </Route>
        <Route exact path="/user">
          <User />
        </Route>
        <Route exact path="/product"><Product/></Route>
          <Route exact path="/signin-oidc"><LoginCallback/></Route>
      </Switch>
    </Router>
  );
}

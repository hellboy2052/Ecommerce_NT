
import React from "react";
import "bootstrap/dist/css/bootstrap.css";
import {
  BrowserRouter as Router,
  Route,
  Switch,

} from "react-router-dom";
import TopMenu from "./components/TopMenu.js";
import Home from "./containers/Home";
import Banner from "./containers/Banner.js";
import Product from "./containers/Product.js";

export default function App() {
  return (
    <Router>
      <TopMenu />
      <br/><br/><br/>
      <Switch>
        <Route exact path="/">
          <Product />
        </Route>
        <Route exact path="/banner">
          <Banner />
        </Route>
        
      </Switch>
    </Router>
  );
}

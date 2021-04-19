
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

export default function App() {
  return (
    <Router>
      <TopMenu />
      <Switch>
        <Route exact path="/">
          <Home />
        </Route>
        <Route exact path="/banner">
          <Banner />
        </Route>
      </Switch>
    </Router>
  );
}

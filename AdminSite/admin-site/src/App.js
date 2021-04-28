import React from "react";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import TopMenu from "./components/TopMenu.js";
import Oidc from "oidc-client";
import Category from "./containers/Category";
import Product from "./containers/Product.js";
import Login from "./components/Login/Login";
import LoginCallback from "./components/Login/LoginCallBack";
import User from "./containers/User.js";
import CreateProduct from "./components/Product/create.js";
import UpdateProduct from "./components/Product/update";
import { Container, Row } from "reactstrap";
import CreateCategory from "./components/Category/create.js";
import UpdateCategory from "./components/Category/update.js";
export default function App() {
  const config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
    authority: "https://localhost:44309/",
    client_id: "react-admin",
    redirect_uri: "http://localhost:3000/signin-oidc",
    response_type: "id_token token",
    scope: "openid profile rookieshop.api",
  };
  var userManager = new Oidc.UserManager(config);
  return (
    <Router>
      <Container fluid="md">
        <TopMenu />
        <br />
        <br />
        <br />
        <Row>
          <Switch>
            <Route exact path="/">
              <Login userManager={userManager} />
            </Route>
            <Route exact path="/category">
              <Category />
            </Route>
            <Route exact path="/user">
              <User />
            </Route>
            <Route exact path="/createProduct">
              <CreateProduct />
            </Route>
            <Route exact path="/product">
              <Product />
            </Route>
            <Route exact path="/createCategory">
              <CreateCategory />
            </Route>
            <Route
              exact
              path={["/updateCategory", "/updateCategory/:id"]}
              render={({ match }) => <UpdateCategory match={match} />}
            ></Route>
            <Route exact path="/signin-oidc">
              <LoginCallback />
            </Route>
            <Route
              exact
              path={["/updateProduct", "/updateProduct/:id"]}
              render={({ match }) => <UpdateProduct match={match} />}
            />
          </Switch>
        </Row>
      </Container>
    </Router>
  );
}

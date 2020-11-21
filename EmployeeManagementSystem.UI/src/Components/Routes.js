import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Employees from "./Features/Employees/Employees";
import EmployeeDetails from "./Features/Employees/EmployeeDetails";
import Login from "./User/Login";
import Register from "./User/Register";
import NewEmployee from "./Features/Employees/NewEmployee";
import Home from "./Features/Layout/Home";
import NavBar from "./Features/Layout/NavBar";
import Departments from "./Features/Departments/Departments";
import NewDepartment from "./Features/Departments/NewDepartment";
import NotFound from "./Common/NotFound";

function Routes() {
  return (  
    <Router>
      <NavBar></NavBar>
      <Switch>
        <Route path="/login" component={Login}></Route>
        <Route path="/register" component={Register}></Route>

        <Route path="/employees/new" component={NewEmployee}></Route>
        <Route path="/employees/:id" component={EmployeeDetails}></Route>
        <Route path="/employees" exact component={Employees}></Route>

        <Route path="/departments/new" exact component={NewDepartment}></Route>
        <Route path="/departments" exact component={Departments}></Route>

        {/* <Route path="/departments/:id" component={EmployeeDetails}></Route> */}

        <Route path={["/", "/home"]} component={Home}></Route>

        <Route path="/notfound" component={NotFound}></Route>
      </Switch>
    </Router>
  );
}

export default Routes;

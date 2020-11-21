import React, { useEffect, useState } from "react";
import { Navbar, Nav } from "react-bootstrap";
import AuthService from "../Services/AuthService";

function AppNavbar(props) {
  const [currentUser, setCurrentUser] = useState({});

  useEffect(() => {
    setCurrentUser(AuthService.getCurrentUser());
  });

  function logout() {
    AuthService.logout();
    window.location.reload();
  }

  return (
    <ul
      class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"
      id="accordionSidebar"
    >
      <a
        class="sidebar-brand d-flex align-items-center justify-content-center"
        href="/"
      >
        <div class="sidebar-brand-icon rotate-n-15">
          <i class="fas fa-laugh-wink"></i>
        </div>
        <div class="sidebar-brand-text mx-3">EMS</div>
      </a>

      <hr class="sidebar-divider my-0"></hr>

      <li class="nav-item">
        <a class="nav-link" href="/">
          <i class="fas fa-fw fa-tachometer-alt"></i>
          <span>Employees</span>
        </a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="/Employees/add">
          <i class="fas fa-fw fa-tachometer-alt"></i>
          <span>Add New Employee</span>
        </a>
      </li>
      {currentUser ? (
        <li class="nav-item">
          <a class="nav-link" onClick={() => logout()} href="void()">
            <i class="fas fa-fw fa-tachometer-alt"></i>
            <span>Logout</span>
          </a>
        </li>
      ) : (
        <li class="nav-item">
          <a class="nav-link" href="/login">
            <i class="fas fa-fw fa-tachometer-alt"></i>
            <span>Login</span>
          </a>
        </li>
      )}
      <li class="nav-item">
        <a class="nav-link" href="/register">
          <i class="fas fa-fw fa-tachometer-alt"></i>
          <span>Register</span>
        </a>
      </li>
    </ul>
  );
}

export default AppNavbar;

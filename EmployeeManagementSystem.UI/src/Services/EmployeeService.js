import Agent from "./Agent";

class EmployeeService {
  getAll() {
    return Agent.requests.get("/employee");
  }

  create(newEmployee) {
    return Agent.requests.post("/employee", newEmployee);
  }

  get(id) {
    return Agent.requests.get("/employee/" + id);
  }

  searchByName(name) {
    return Agent.requests.get("/employee/search/" + name);
  }

  delete(id) {
    return Agent.requests.del("/employee/" + id);
  }

  // getById(id) {
  //   const options = {
  //     method: "get",
  //     url: API_URL + id,
  //   };
  //   return axios(options);
  // }
  // create(employeeDto) {
  //   const options = {
  //     method: "post",
  //     url: API_URL,
  //     data: employeeDto,
  //   };
  //   return axios(options);
  // }
}

export default new EmployeeService();

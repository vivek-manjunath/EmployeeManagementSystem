import Agent from "./Agent";

class DepartmentService {
  get() {
    return Agent.requests.get("/department");
  }

  create(newDepartment) {
    return Agent.requests.post("/department", newDepartment);
  }

  delete(id) {
    return Agent.requests.del("/department/" + id);
  }
}

export default new DepartmentService();

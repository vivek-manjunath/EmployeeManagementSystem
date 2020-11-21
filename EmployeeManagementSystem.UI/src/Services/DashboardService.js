import Agent from "./Agent";

class DashboardService {
  get() {
    return Agent.requests.get("/dashboard");
  }
}

export default new DashboardService();

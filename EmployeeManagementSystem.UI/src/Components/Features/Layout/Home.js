import React, { useEffect, useState } from "react";
import { Segment, Container, Header } from "semantic-ui-react";
import { observer } from "mobx-react";
import Statistics from "../Dashboard/Statistics";
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";
import { toast } from "react-toastify";
import DashboardService from "../../../Services/DashboardService";

const Home = () => {
  var [dashboardInfo, setDashboardInfo] = useState({});

  useEffect(() => {
    getDashboardInfo();
    configSignalR();
  }, [getDashboardInfo, configSignalR]);

  var getDashboardInfo = () => {
    DashboardService.get()
      .then(res => setDashboardInfo(res))
  }

  var configSignalR = () => {
    var hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/dashboardhub", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();

    hubConnection
      .start()
      .then(() => console.log("Connected to dashboard hub!"))
      .catch((err) => {
        console.log("Connection error");
      });

    hubConnection.on("RefreshData", (data) => {
      setDashboardInfo(data);
    });

    hubConnection.on("DepartmentUpdated", (data) => {
      toast.info(data);
    });

    hubConnection.on("Message", (msg) => {
      console.log(msg);
    });
  };

  return (
    <Segment textAlign="center" vertical>
      <Container text>
        <Statistics data={dashboardInfo}></Statistics>
      </Container>
    </Segment>
  );
};

export default observer(Home);

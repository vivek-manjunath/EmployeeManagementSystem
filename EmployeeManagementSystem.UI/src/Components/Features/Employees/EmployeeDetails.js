import React, { useState, useEffect } from "react";
import axios from "axios";

import EmployeeService from "../../../Services/EmployeeService";
import { Container, Grid, Segment, Form } from "semantic-ui-react";
import AppLoader from "../../Common/AppLoader";
import DetailField from "../../Common/DetailField";

function EmployeeDetails(props) {
  const [fetched, setFetched] = useState(false);
  const [employeeData, setEmployeeData] = useState({});

  useEffect(() => {
    if (fetched === false) {
      EmployeeService.get(props.match.params.id).then((res) => {
        setEmployeeData(res);
        setFetched(true);
      });
    }
  }, [fetched, props.match.params.id]);

  if (fetched === false) {
    return <AppLoader></AppLoader>;
  }

  return (
    <Segment style={{width: "50%"}}>
      {/* <Grid>
        <Grid.Row>
          <Grid.Column width={2}>
            <h5>Id</h5>
          </Grid.Column>
          <Grid.Column width={2}>{employeeData.id}</Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={2}>
            <h5>First Name</h5>
          </Grid.Column>
          <Grid.Column width={2}>{employeeData.firstName}</Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={2}>
            <h5>Last Name</h5>
          </Grid.Column>
          <Grid.Column width={2}>{employeeData.lastName}</Grid.Column>
        </Grid.Row>
      </Grid> */}

      <Grid className="ui grid">
        <DetailField name="First Name" value={employeeData.firstName}></DetailField>
        <DetailField name="Last Name" value={employeeData.lastName}></DetailField>
        <DetailField name="Date of Birth" value={employeeData.dob}></DetailField>
        <DetailField name="Department" value={employeeData.department}></DetailField>
      </Grid>
    </Segment>
  );
}

export default EmployeeDetails;

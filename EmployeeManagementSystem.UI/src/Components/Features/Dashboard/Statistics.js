import React from "react";
import { Statistic, Icon, Image, Segment } from "semantic-ui-react";

export default function Statistics(props) {
  return (
    <Segment>
      <Statistic.Group widths="three">
        <Statistic>
          <Statistic.Value>{props.data.employeeCount}</Statistic.Value>
          <Statistic.Label>Employees</Statistic.Label>
        </Statistic>

        <Statistic>
          <Statistic.Value>{props.data.departmentCount}</Statistic.Value>
          <Statistic.Label>Departments</Statistic.Label>
        </Statistic>

        <Statistic>
          <Statistic.Value>42</Statistic.Value>
          <Statistic.Label>Users</Statistic.Label>
        </Statistic>
      </Statistic.Group>
    </Segment>
  );
}

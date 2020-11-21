import React from "react";
import { Grid } from "semantic-ui-react";

const DetailField = ({ name, value }) => {
  return (
    <Grid.Row>
      <Grid.Column width={4}>
        <h4 textAlign="right">{name}</h4>
      </Grid.Column>
      <Grid.Column width={6}>
        <span className="grey">{value}</span>
      </Grid.Column>
    </Grid.Row>
  );
};

export default DetailField;
import React, { useState } from "react";
import { Form, Button, Segment } from "semantic-ui-react";
import DepartmentService from "../../../Services/DepartmentService";
import { useHistory } from "react-router-dom";

const NewDepartment = () => {
  const history = useHistory();
  const [departmentName, setDepartmentName] = useState("");
  const submitHandler = () => {
    DepartmentService.create({ name: departmentName }).then(() => {
      history.push("/departments");
    });
  };

  return (
    <Segment>
      <Form onSubmit={submitHandler}>
        <Form.Field>
          <Form.Input
            fluid
            width="6"
            label="Department name"
            placeholder="Department name"
            onChange={(e) => {
              setDepartmentName(e.target.value);
            }}
          />
        </Form.Field>
        <Button type="submit" primary>
          Save
        </Button>
      </Form>
    </Segment>
  );
};

export default NewDepartment;

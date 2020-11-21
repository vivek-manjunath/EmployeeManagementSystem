import React, { useState } from "react";
import { Card, Confirm, Icon } from "semantic-ui-react";
import EmployeeService from "../../../Services/EmployeeService";
import { useHistory } from "react-router-dom";

const EmployeeCard = (props) => {
  const history = useHistory();
  const [deleteEmployeeId, setDeleteEmployeeId] = useState();
  const [openConfirmation, setOpenConfirmation] = useState();

  var deleteEmployee = () => {
    EmployeeService.delete(deleteEmployeeId).then((res) => {
      setOpenConfirmation(false);
      props.refresh();
    });
  };
  var editEmployee = (id) => {};

  var confirmAndDelete = (id) => {
    setDeleteEmployeeId(id);
    setOpenConfirmation(true);
  };

  return (
    <div>
      <Card style={{margin: ".5rem"}}>
        <Card.Content>
          <Card.Header
            className="ui header blue"
            href={"/employees/" + props.id}
          >
            {props.fullName}
          </Card.Header>
          <Card.Meta>
            <span className="">{props.department}</span>
          </Card.Meta>
        </Card.Content>
        <Card.Content extra textAlign="right">
          <a onClick={() => editEmployee(props.id)}>
            <Icon name="edit" className="" />
          </a>
          <a onClick={() => confirmAndDelete(props.id)}>
            <Icon name="trash alternate" className="" />
          </a>
        </Card.Content>
      </Card>

      <Confirm
        open={openConfirmation}
        content="Are your sure want to delete?"
        onCancel={() => setOpenConfirmation(false)}
        onConfirm={() => deleteEmployee()}
        size="mini"
      />
    </div>
  );
};

export default EmployeeCard;

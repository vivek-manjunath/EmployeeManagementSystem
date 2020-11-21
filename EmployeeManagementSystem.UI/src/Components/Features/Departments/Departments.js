import React, { useEffect, useState, useContext } from "react";
import DepartmentService from "../../../Services/DepartmentService";
import {
  Container,
  Table,
  Button,
  Header,
  Icon,
  Confirm,
} from "semantic-ui-react";

import AppLoader from "../../Common/AppLoader";
import { modalStoreContext } from "../../../Stores/ModalStore";
import { UserStoreContext } from "../../../Stores/UserStore";
import { useHistory, Link } from "react-router-dom";
import { toast } from "react-toastify";
import NoData from "../../Common/NoData";
import AddEntityButton from "../../Common/AddEntityButton";

const Departments = () => {
  const [departments, setDepartments] = useState([]);
  const [fetched, setFetched] = useState(false);
  const history = useHistory();
  const [openConfirmation, setOpenConfirmation] = useState(false);
  const [deleteDepartmentId, setDeleteDepartmentId] = useState();

  const modalStore = useContext(modalStoreContext);
  const userStore = useContext(UserStoreContext);

  const { showModal } = modalStore;
  const { isUserLoggedin, logout } = userStore;

  useEffect(() => {
    if (fetched === false) {
      getDepartments();
    }
  }, [getDepartments, fetched]);

  var getDepartments = () => {
    DepartmentService.get().then((res) => {
      setDepartments(res);
      setFetched(true);
    });
  };

  var deleteDepartment = () => {
    DepartmentService.delete(deleteDepartmentId).then(() => {
      setOpenConfirmation(false);
      toast.info("Department deleted");
      getDepartments();
    });
  };

  var confirmAndDelete = (id) => {
    setDeleteDepartmentId(id);
    setOpenConfirmation(true);
  };

  return (
    <Container>
      {fetched === false ? (
        <AppLoader />
      ) : (
        <div>
          {departments && departments.length > 0 ? (
            <Table compact celled>
              <Table.Header>
                <Table.Row>
                  <Table.HeaderCell>Name</Table.HeaderCell>
                  <Table.HeaderCell>Registration Date</Table.HeaderCell>
                  <Table.HeaderCell>Total Employees</Table.HeaderCell>
                  <Table.HeaderCell></Table.HeaderCell>
                </Table.Row>
              </Table.Header>

              <Table.Body>
                {departments.map((item, key) => {
                  return (
                    <Table.Row key={item.id}>
                      <Table.Cell>{item.name}</Table.Cell>
                      <Table.Cell>{item.createdOn}</Table.Cell>
                      <Table.Cell>{item.totalEmployees}</Table.Cell>
                      <Table.Cell className="center aligned">
                        <Link onClick={() => confirmAndDelete(item.id)}>
                          <Icon name="delete" className="red"></Icon>
                        </Link>
                      </Table.Cell>
                    </Table.Row>
                  );
                })}
              </Table.Body>
            </Table>
          ) : (
            <NoData></NoData>
          )}

          <Confirm
            open={openConfirmation}
            content="Are your sure want to delete?"
            onCancel={() => setOpenConfirmation(false)}
            onConfirm={() => deleteDepartment()}
            size="mini"
          />
          <AddEntityButton linkTo="/departments/new" buttonText="Add Department"></AddEntityButton>
        </div>
      )}
    </Container>
  );
};

export default Departments;

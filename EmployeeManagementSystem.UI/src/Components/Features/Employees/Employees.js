import React, { useState, useEffect, useContext } from "react";
import { useHistory } from "react-router-dom";
import EmployeeService from "../../../Services/EmployeeService";
import EmployeeCard from "./EmployeeCard";
import {
  Container,
  Table,
  Image,
  Button,
  Header,
  Confirm,
  Icon,
  Segment,
  Grid,
  Input,
} from "semantic-ui-react";
import AppLoader from "../../Common/AppLoader";
import { modalStoreContext } from "../../../Stores/ModalStore";
import { UserStoreContext } from "../../../Stores/UserStore";
import NewEmployee from "./NewEmployee";
import AddEntityButton from "../../Common/AddEntityButton";
import NoData from "../../Common/NoData";

function Employee(props) {
  const modalStore = useContext(modalStoreContext);
  const userStore = useContext(UserStoreContext);
  const history = useHistory();

  const [employees, setEmployees] = useState([]);
  const [fetched, setFetched] = useState(false);
  const [openConfirmation, setOpenConfirmation] = useState(false);
  const { showModal } = modalStore;
  const { isUserLoggedin, logout } = userStore;
  const [ searchValue, setSearchValue ] = useState("");

  useEffect(() => {
    if (fetched === false) {
      getAllEmployees();
    }
  }, [fetched, getAllEmployees]);

  var getAllEmployees = () => {
    EmployeeService.getAll().then((res) => {
      setEmployees(res);
      setFetched(true);
    });
  };

  const deleteEmployee = () => {
    // history.push("/newEmployee");
  };

  const handleSearch = (e) => {
    setSearchValue(e.target.value);

    EmployeeService.searchByName(e.target.value)
      .then(res => {
        setEmployees(res);
      })
  };

  return (
    <div>
      <Segment>
        <Input
          icon="search"
          placeholder="Search..."
          onChange={handleSearch}
        ></Input>
        <AddEntityButton
          linkTo="/employees/new"
          buttonText="Add Employee"
        ></AddEntityButton>
      </Segment>
      {fetched === false ? (
        <AppLoader />
      ) : (
        <div>
          {employees && employees.length > 0 ? (
            <Grid columns={4}>
              <Grid.Row>
                {employees.map((item, key) => {
                  return (
                    <Grid.Column>
                      <EmployeeCard
                        {...item}
                        refresh={getAllEmployees}
                      ></EmployeeCard>
                    </Grid.Column>
                  );
                })}
              </Grid.Row>
            </Grid>
          ) : (
            <NoData></NoData>
          )}

          <Confirm
            open={openConfirmation}
            content="Are your sure want to delete?"
            onCancel={() => setOpenConfirmation(false)}
            onConfirm={() => deleteEmployee()}
            size="mini"
          />
        </div>
      )}
    </div>
  );
}

export default Employee;

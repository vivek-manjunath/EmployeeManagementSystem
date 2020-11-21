import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import EmployeeService from "../../../Services/EmployeeService";
import {
  Form,
  Radio,
  Button,
  Container,
  Select,
  Segment,
  Label,
  Grid,
} from "semantic-ui-react";
import DepartmentService from "../../../Services/DepartmentService";
import { DateInput } from "semantic-ui-calendar-react";
import DatePicker from "react-datepicker";

var NewEmployee = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [dob, setDob] = useState(new Date());
  const [departments, setDepartments] = useState([]);
  const [department, setDepartment] = useState("");
  const [departmentFetched, setDepartmentsFetched] = useState(false);
  const [gender, setGender] = useState();
  const history = useHistory();

  useEffect(() => {
    if (departmentFetched === false) {
      DepartmentService.get().then((res) => {
        let dptOptions = [];
        res.map((item, index) => {
          dptOptions.push({ text: item.name, value: item.id, key: item.id });
        });

        setDepartments(dptOptions);
        setDepartmentsFetched(true);
      });
    }
  }, [departmentFetched]);

  const submitForm = (e) => {
    e.preventDefault();

    let employeeDto = {};
    employeeDto.firstName = firstName;
    employeeDto.lastName = lastName;
    employeeDto.departmentId = department;
    employeeDto.dob = dob;

    EmployeeService.create(employeeDto).then(() => {
      history.push("/employees");
    });
  };

  const handleDateChange = (date) => {
    setDob(date);
  };

  return (
    <Segment style={{ width: "100%" }}>
      <Form onSubmit={submitForm}>
        <Form.Field inline>
          <label>First Name</label>
          <input
            placeholder="First Name"
            onChange={(e) => setFirstName(e.target.value)}
          />
        </Form.Field>
        <Form.Field inline>
          <label>Last Name</label>
          <input
            placeholder="Last Name"
            onChange={(e) => setLastName(e.target.value)}
          />
        </Form.Field>

        <Form.Field inline>
          <label>DOB</label>
          <DatePicker
            selected={dob}
            onChange={handleDateChange}
            dateFormat="MM-dd-yyyy"
          ></DatePicker>
        </Form.Field>

        <Form.Field inline>
          <label>Gender</label>
          <Radio
            label="Male"
            name="radioGroup"
            value="male"
            checked={gender === "male"}
            onChange={() => setGender("male")}
          />
          <Radio
            label="Female"
            name="radioGroup"
            value="female"
            checked={gender === "female"}
            onChange={() => setGender("female")}
          />
        </Form.Field>

        <Form.Field inline>
          <label>Department</label>
          <Select
            placeholder="Select Department"
            options={departments}
            onChange={(e, data) => {
              console.log("department --> " + data.value);
              setDepartment(data.value);
            }}
            value={department}
          ></Select>
        </Form.Field>

        <Button type="submit" primary>
          Save
        </Button>
        <Button>Cancel</Button>
      </Form>
    </Segment>
  );
};

export default NewEmployee;

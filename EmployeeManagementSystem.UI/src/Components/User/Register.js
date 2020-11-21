import React, { useState, useContext } from "react";
import AuthService from "../../Services/AuthService";
import { Form, Container, Button } from "semantic-ui-react";
import { modalStoreContext } from "../../Stores/ModalStore";
import { UserStoreContext } from "../../Stores/UserStore";

export default function Register() {
  const modalStore = useContext(modalStoreContext);
  const userStore = useContext(UserStoreContext);
  const { closeModal } = modalStore;

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const submitForm = (e) => {
    e.preventDefault();

    let registrationDto = {};
    registrationDto.firstName = firstName;
    registrationDto.lastName = lastName;
    registrationDto.email = email;
    registrationDto.password = password;
    registrationDto.confirmPassword = confirmPassword;

    AuthService.register(registrationDto).then(() => {
      closeModal();
    });
  };

  return (
    <Container>
      <Form onSubmit={submitForm}>
        <Form.Field>
          <input
            placeholder="First Name"
            onChange={(e) => setFirstName(e.target.value)}
          />
        </Form.Field>
        <Form.Field>
          <input
            placeholder="Last Name"
            onChange={(e) => setLastName(e.target.value)}
          />
        </Form.Field>
        <Form.Field>
          <input
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Field>
        <Form.Field>
          <input
            placeholder="Password"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Field>
        <Form.Field>
          <input
            type="password"
            placeholder="Confirm Password"
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </Form.Field>
        <Button content="Register" type="submit" primary fluid></Button>
      </Form>
    </Container>
  );
}
